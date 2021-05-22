using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Repository;
using Advertisement.Application.Services.Ad.Contracts.GetById;
using Advertisement.Application.Services.Ad.Interfaces;
using Advertisement.Application.Services.Comment.Contracts;
using Advertisement.Application.Services.Comment.Contracts.CreateComment;
using Advertisement.Application.Services.Comment.Contracts.DeleteComment;
using Advertisement.Application.Services.Comment.Interfaces;
using Advertisement.Application.Services.Comment.Contracts.GetPagedComment;
using Advertisement.Application.Services.Comment.Contracts.UpdateComment;
using Advertisement.Application.Services.Comment.Exceptions;
using Advertisement.Application.Services.Identity.Interfaces;
using Advertisement.Application.Services.User.Contracts.GetByIdDomainUser;
using Advertisement.Application.Services.User.Interfaces;
using AutoMapper;

namespace Advertisement.Application.Services.Comment.Implementations
{
    public sealed class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IUserService _userService;
        private readonly IIdentityService _identityService;
        private readonly IAdService _adService;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository repository, IUserService userService, IIdentityService identityService, IAdService adService, IMapper mapper)
        {
            _repository = repository;
            _userService = userService;
            _identityService = identityService;
            _adService = adService;
            _mapper = mapper;
        }

        public async Task<CreateCommentResponse> CreateComment(CreateCommentRequest request,CancellationToken cancellationToken)
        {
            var identityUser = await _identityService.GetCurrentIdentityUserId(cancellationToken);
            if (identityUser == null)
                throw new NoUserForCommentCreationException($"Попытка создания коментария [{request.Text}] без пользователя.");

            var ad = await _adService.GetById(new GetByIdAdRequest{Id = request.AdId}, cancellationToken);
            if (ad == null)
                throw new NoAdForCommentCreationException($"Попытка создания коментария [{request.Text}] без обьявелния.");

            var comment = _mapper.Map<Domain.Comment>(request);
            comment.AuthorId = new Guid(identityUser.Id);
            
            await _repository.Save(comment, cancellationToken);
            return _mapper.Map<CreateCommentResponse>(comment);
        }

        public async Task<GetPagedCommentResponse> GetPagedComment(GetPagedCommentRequest request, CancellationToken cancellationToken)
        {
            var total = await _repository.Count(cancellationToken);

            if (total == 0)
                return _mapper.Map<GetPagedCommentResponse>(request);
            

            var foundComment  = await _repository.FindWhere(
                find => find.AdId == request.AdId || request.AdId == Guid.Empty , cancellationToken);

            return new GetPagedCommentResponse
            {
                Items = 
                    foundComment.OrderByDescending(s=>s.UpdatedAt) 
                    .Skip(request.Offset)
                    .Take(request.Limit)
                    .Select(com => _mapper.Map<GetPagedCommentResponseItem>(com)),
                Total = foundComment.Count(),
                AdId=request.AdId,
                Limit = request.Limit,
                Offset = request.Offset
            };
        }
        public async Task DeleteComment(DeleteCommentRequest request, CancellationToken cancellationToken)
        {
            var comment = await _repository.FindById(request.Id, cancellationToken);
            if (comment == null)
                throw new NotFoundCommentException(request.Id);

            await _repository.DeleteById(comment.Id, cancellationToken);
        }

        public async Task UpdateComment(UpdateCommentRequest request, CancellationToken cancellationToken)
        {
            var comment = await _repository.FindById(request.Id, cancellationToken);
            if (comment == null)
                throw new NotFoundCommentException(request.Id);

            var user = await _identityService.GetCurrentIdentityUserId(cancellationToken);
            if (user.Id != comment.AuthorId.ToString())
                throw new NoUserForCommentCreationException("Нет прав для редактирования комментария.");
            

            await _repository.Save(_mapper.Map(request,comment), cancellationToken);
        }
    }
}
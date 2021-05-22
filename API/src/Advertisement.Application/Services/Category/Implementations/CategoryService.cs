using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Common;
using Advertisement.Application.Repository;
using Advertisement.Application.Services.Category.Contracts.CreateCategory;
using Advertisement.Application.Services.Category.Contracts.DeleteCategory;
using Advertisement.Application.Services.Category.Contracts.GetAllCategory;
using Advertisement.Application.Services.Category.Contracts.GetByIdCategory;
using Advertisement.Application.Services.Category.Contracts.UpdateCategory;
using Advertisement.Application.Services.Category.Exceptions;
using Advertisement.Application.Services.Category.Interfaces;
using Advertisement.Application.Services.Identity.Contracts.IsInIdentityUserRole;
using Advertisement.Application.Services.Identity.Interfaces;
using AutoMapper;

namespace Advertisement.Application.Services.Category.Implementations
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _repository;
        private IIdentityService _identityService;
        private IMapper _mapper;
        
        public CategoryService(ICategoryRepository repository, IIdentityService identityService, IMapper mapper)
        {
            _repository = repository;
            _identityService = identityService;
            _mapper = mapper;
        }
        public async Task<CreateCategoryResponse> CreateCategory(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var userId = await _identityService.GetCurrentIdentityUserId(cancellationToken);
            var checkRole = await _identityService.IsInIdentityUserRole(new IsInIdentityUserRoleRequest() {Id = userId.Id,Role = RoleConstants.AdminRole});
            if (!checkRole.IsInRole)
                throw new NoRightsToAddCategoryException("У пользователя нет прав на добавление категории");

            if (request.ParentCategoryId != null)
            {
                var parentCategory = await _repository.FindById(request.ParentCategoryId, cancellationToken);
                if (parentCategory == null)
                {
                    throw new NotFoundCategoryException(request.ParentCategoryId ?? Guid.Empty);
                }
            }
            var category = _mapper.Map<Domain.Category>(request);
            await _repository.Save(category, cancellationToken);
            
            return _mapper.Map<CreateCategoryResponse>(category);
        }

        public async Task UpdateCategory(UpdateCategoryRequst request, CancellationToken cancellationToken)
        {
            var category = await _repository.FindById(request.Id, cancellationToken);
            if (category == null)
                throw new NotFoundCategoryException(request.Id);
            
            await _repository.Save(_mapper.Map(request, category), cancellationToken);

        }

        public async Task DeleteCategory(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await _repository.FindById(request.Id, cancellationToken);
            if (category == null)
                throw new NotFoundCategoryException(request.Id);
            
            await _repository.DeleteById(request.Id, cancellationToken);
        }

        public async Task<GetByIdCategoryResponse> GetByIdCategory(GetByIdCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await _repository.FindById(request.Id, cancellationToken);
            if (category == null)
                throw new NotFoundCategoryException(request.Id);

            return _mapper.Map<GetByIdCategoryResponse>(category);
        }

        public async Task<GetAllCategoryResponse> GetAllCategory(CancellationToken cancellationToken)
        {
            var categories = await _repository.Get(cancellationToken);

            return new GetAllCategoryResponse()
            {
                Items = categories.Where(item=> item.ParentCategoryId == null).Select(item => _mapper.Map<GetAllCategoryResponseItem>(item))
            };
        }
    }
}
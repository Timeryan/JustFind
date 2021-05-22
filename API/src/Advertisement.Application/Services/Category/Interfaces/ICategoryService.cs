using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Services.Category.Contracts.CreateCategory;
using Advertisement.Application.Services.Category.Contracts.DeleteCategory;
using Advertisement.Application.Services.Category.Contracts.GetAllCategory;
using Advertisement.Application.Services.Category.Contracts.GetByIdCategory;
using Advertisement.Application.Services.Category.Contracts.UpdateCategory;

namespace Advertisement.Application.Services.Category.Interfaces
{
    public interface ICategoryService
    {
        public Task<CreateCategoryResponse> CreateCategory(CreateCategoryRequest request, CancellationToken cancellationToken);
        public Task UpdateCategory(UpdateCategoryRequst request, CancellationToken cancellationToken);
        public Task DeleteCategory(DeleteCategoryRequest request, CancellationToken cancellationToken);
        public Task<GetByIdCategoryResponse> GetByIdCategory(GetByIdCategoryRequest request, CancellationToken cancellationToken);
        public Task<GetAllCategoryResponse> GetAllCategory(CancellationToken cancellationToken);
    }
}
using Advertisement.Application.Services.Category.Contracts.CreateCategory;
using Advertisement.Application.Services.Category.Contracts.DeleteCategory;
using Advertisement.Application.Services.Category.Contracts.GetByIdCategory;
using Advertisement.Application.Services.Category.Contracts.UpdateCategory;
using Advertisement.PublicApi.Models.Category;
using AutoMapper;

namespace Advertisement.PublicApi.MapProfiles
{
    public class CategoryMapControllerProfile : Profile
    {
        public CategoryMapControllerProfile()
        {
            CreateMap<CategoryCreateDto, CreateCategoryRequest>();
            CreateMap<CategoryUpdateDto, UpdateCategoryRequst>();
            CreateMap<CategoryGetByIdDto, GetByIdCategoryRequest>();
            CreateMap<CategoryDeleteDto, DeleteCategoryRequest>();
        }
    }
}
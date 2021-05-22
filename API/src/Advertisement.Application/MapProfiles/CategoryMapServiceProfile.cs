using Advertisement.Application.Services.Category.Contracts.CreateCategory;
using Advertisement.Application.Services.Category.Contracts.GetAllCategory;
using Advertisement.Application.Services.Category.Contracts.GetByIdCategory;
using Advertisement.Application.Services.Category.Contracts.UpdateCategory;
using Advertisement.Domain;
using AutoMapper;

namespace Advertisement.Application.MapProfiles
{
    public class CategoryMapServiceProfile : Profile
    {
        public CategoryMapServiceProfile()
        {
            CreateMap<CreateCategoryRequest, Category>().ForMember(dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(dest => dest.ParentCategory,
                    opt => opt.Ignore())
                .ForMember(dest => dest.ChildCategories,
                    opt => opt.Ignore());
            CreateMap<Category, CreateCategoryResponse>();
            CreateMap<UpdateCategoryRequst, Category>().ForMember(dest => dest.ParentCategory,
                opt => opt.Ignore())
                .ForMember(dest => dest.ChildCategories,
                opt => opt.Ignore());
            CreateMap<Category, GetByIdCategoryResponse>();
            CreateMap<Category, GetAllCategoryResponseItem>();
        }
    }
}
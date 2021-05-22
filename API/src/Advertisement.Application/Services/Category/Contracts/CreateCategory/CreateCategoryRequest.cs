using System;

namespace Advertisement.Application.Services.Category.Contracts.CreateCategory
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}
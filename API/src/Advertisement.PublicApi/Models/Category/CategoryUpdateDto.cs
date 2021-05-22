using System;

namespace Advertisement.PublicApi.Models.Category
{
    public class CategoryUpdateDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}
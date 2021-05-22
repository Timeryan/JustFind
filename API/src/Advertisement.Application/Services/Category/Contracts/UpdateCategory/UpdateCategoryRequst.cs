using System;

namespace Advertisement.Application.Services.Category.Contracts.UpdateCategory
{
    public class UpdateCategoryRequst
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ParentCategoryId { get; set; }
    }
}
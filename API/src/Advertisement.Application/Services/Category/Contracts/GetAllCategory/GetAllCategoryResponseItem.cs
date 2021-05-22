using System;
using System.Collections.Generic;
namespace Advertisement.Application.Services.Category.Contracts.GetAllCategory
{
    public class GetAllCategoryResponseItem
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public ICollection<GetAllCategoryResponseItem> ChildCategories { get; set; }

    }
}
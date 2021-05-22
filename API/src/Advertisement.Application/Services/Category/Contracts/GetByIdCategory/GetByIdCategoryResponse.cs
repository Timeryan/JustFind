using System;
using System.Collections.Generic;

namespace Advertisement.Application.Services.Category.Contracts.GetByIdCategory
{
    public class GetByIdCategoryResponse
    {
        public string Name { get; set; }
        public Guid ParentCategoryId { get; set; }
        
        public virtual ICollection<Domain.Category> ChildCategories { get; set; }

    }
}
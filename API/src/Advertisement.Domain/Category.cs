using System;
using System.Collections.Generic;
using Advertisement.Domain.Shared;

namespace Advertisement.Domain
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public  Guid? ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> ChildCategories { get; set; }
    }
}
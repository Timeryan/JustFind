using System;
using System.ComponentModel.DataAnnotations;

namespace Advertisement.PublicApi.Models.Category
{
    public class CategoryCreateDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Имя должно содержать менее 50 символов")]
        public string Name { get; set; }
        public  Guid? ParentCategoryId { get; set; }
    }
}
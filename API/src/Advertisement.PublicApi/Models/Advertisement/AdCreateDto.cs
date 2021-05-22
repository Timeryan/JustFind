using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Advertisement.Domain;


namespace Advertisement.PublicApi.Models.Advertisement
{
    public sealed class AdCreateDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Максимум 100 символов!")]
        public string Name { get; set; }
        [MaxLength(2000, ErrorMessage = "Максимум 2000 символов!")]
        public string Text { get; set; }
        public ICollection<Guid> Photos { get; set; }
        public Guid? CategoryId { get; set; }
        [Required]
        [Range(1, 9999999, ErrorMessage = "Укажите стоимость от {1} руб. до {2} руб.")]
        public decimal Price { get; set; }
        
        public string LocationKladrId { get; set; }
        public string LocationText { get; set; }
        public string LocationX { get; set; }
        public string LocationY { get; set; }
    }
}


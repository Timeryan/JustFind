using System;
using System.ComponentModel.DataAnnotations;


namespace Advertisement.PublicApi.Models.Comment
{
    public sealed class CommentCreateDto
    {
        [Required]
        public Guid AdId { get; set; }

        [Required, MaxLength(2000)]
        public string Text { get; set; }
    }
}


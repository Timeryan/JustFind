using System;
using System.ComponentModel.DataAnnotations;



namespace Advertisement.PublicApi.Models.Comment
{
    public sealed class CommentUpdateDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required, MaxLength(2000)]
        public string Text { get; set; }

    }
}


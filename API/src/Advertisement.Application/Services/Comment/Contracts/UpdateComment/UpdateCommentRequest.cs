using System;

namespace Advertisement.Application.Services.Comment.Contracts.UpdateComment
{
    public class UpdateCommentRequest
    {
        public Guid Id { get; set; }
        public string? Text { get; set; }

    }
}
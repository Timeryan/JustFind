using System;

namespace Advertisement.Application.Services.Comment.Contracts.CreateComment
   
{
    public sealed class CreateCommentRequest
    {
        public string Text { get; set; }
        public Guid AdId { get; set; }

    }

    
}

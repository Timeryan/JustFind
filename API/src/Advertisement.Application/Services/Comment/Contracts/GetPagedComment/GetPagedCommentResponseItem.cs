using System;

namespace Advertisement.Application.Services.Comment.Contracts.GetPagedComment
{
    public class GetPagedCommentResponseItem
    {
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Photo { get; set; }
        public string Text { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
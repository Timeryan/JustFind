using System;

namespace Advertisement.Application.Services.Comment.Contracts.GetPagedComment
{
    public sealed class GetPagedCommentRequest
    {

        public Guid AdId { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}
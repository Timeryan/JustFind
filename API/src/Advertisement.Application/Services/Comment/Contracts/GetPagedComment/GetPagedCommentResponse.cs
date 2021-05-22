using System;
using System.Collections.Generic;
using System.Linq;

namespace Advertisement.Application.Services.Comment.Contracts.GetPagedComment
{
    public sealed class GetPagedCommentResponse
    {
        
        public int Total { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public Guid AdId { get; set; }

        public IEnumerable<GetPagedCommentResponseItem> Items { get; set; } = Enumerable.Empty<GetPagedCommentResponseItem>();
    }
}
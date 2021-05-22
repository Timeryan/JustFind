using System;

namespace Advertisement.PublicApi.Models.Comment
{
    public class CommentGetPagedDto
    {
        public Guid AdId { get; set; }
        public int offset { get; set; } = 0;
        public int limit { get; set; } = 10;
    }
}
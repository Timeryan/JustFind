using Advertisement.Domain.Shared;
using System;

namespace Advertisement.Domain
{
    public  class Comment : Entity
    {
        public Guid AuthorId { get; set; }
        public virtual User Author { get; set; }
        public Guid AdId { get; set; }
        public virtual Ad AdOwner { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

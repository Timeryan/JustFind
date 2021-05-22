using System;
using System.Collections.Generic;
using Advertisement.Domain.Shared;

namespace Advertisement.Domain
{
    public class Ad : Entity
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public decimal Price { get; set; }
        public Statuses Status { get; set; }
        public Guid OwnerId { get; set; }
        public virtual User Owner { get; set; }
        public long LocationKladrId { get; set; }
        public string LocationText { get; set; }
        public string LocationX { get; set; }
        public string LocationY { get; set; }
        public Guid? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public long Views { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModerationComment { get; set; }

    }
}

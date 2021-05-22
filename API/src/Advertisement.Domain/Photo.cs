using System;
using Advertisement.Domain.Shared;

namespace Advertisement.Domain
{
    public class Photo : Entity
    {
        public string KodBase64 { get; set; }
        public Guid? AdOwnerId { get; set; }
        public virtual Ad AdOwner { get; set; }
    } 
}
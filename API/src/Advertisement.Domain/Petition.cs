using System;
using Advertisement.Domain.Shared;

namespace Advertisement.Domain
{
    public class Petition : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public bool Reviewed { get; set; }
        public string ReviewResult { get; set; }
        public DecisionEnum DecisionEnum { get; set; }
        public virtual Ad Ad { get; set; }
        public Guid AdId { get; set; }
    } 
}
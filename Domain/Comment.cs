using System;

namespace Domain
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public virtual AppUser Author { get; set; }//virtual for lazy loading
        public virtual Activity Activity { get; set; }//virtual for lazy loading
        public DateTime CreatedAt { get; set; }

    }
}
using System;

namespace Domain
{
    public class UserActivity
    {
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; } //the virtual keyword enables the lazy loading
        public Guid ActivityId { get; set; }
        public virtual Activity Activity { get; set; } //the virtual keyword enables the lazy loading
        public DateTime DateJoined { get; set; }
        public bool IsHost { get; set; }
    }
}
using System;

namespace Application.Profiles
{
    public class UserActivityDto
    {
        //this specific dto is only to display activities the user has attended in his profile - so no need for chat, attendees etc.
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
    }
}
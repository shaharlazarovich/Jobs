using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.Comments;

namespace Application.Activities
{
    public class ActivityDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }
        public DateTime Date { get; set; }
        
        [JsonPropertyName("attendees")] 
        public ICollection<AttendeeDto> UserActivities { get; set; } //although its a collection of attendees we call it UserActivities so auto mapper will pick it up
                                                                     //but we're adding the JsonPropertyName so to the client we will return Attendees
        public ICollection<CommentDto> Comments { get; set; }
    }
}
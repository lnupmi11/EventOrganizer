namespace EventOrganizer.DAL.Models
{
    public class EventCartItem
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string UserId { get; set; }
    }
}

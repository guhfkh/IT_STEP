namespace IT_STEP
{
    public class UserMovieView
    {
        public int UserId { get; set; }
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public int? MovieId { get; set; }
        public string? Title { get; set; }
        public int? Year { get; set; }
        public DateTime? DateAdded { get; set; }
    }
}

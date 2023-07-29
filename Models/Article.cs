namespace RESTful_APIs.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public List<Comment> comments { get; set; }
        public string? content { get; set; }
        public int blogId { get; set; }
    }
}

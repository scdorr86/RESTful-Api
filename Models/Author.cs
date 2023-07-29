namespace RESTful_APIs.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Blog> blogs { get; set; }
        public List<Comment> comments { get; set; }
    }
}

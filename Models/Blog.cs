namespace RESTful_APIs.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string? blogTitle { get; set; }
        public List<Author> authors { get; set; }
        public List<Article> articles { get; set; }
        public string content { get; set; }

    }
}

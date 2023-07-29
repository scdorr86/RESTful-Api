namespace RESTful_APIs.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? comment { get; set; }
        public int authorId { get; set; }
        public int articleId { get; set; }
    }
}

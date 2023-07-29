using RESTful_APIs.Models;
var builder = WebApplication.CreateBuilder(args);

List<Article> articles = new List<Article>
{
    new Article()
    {
        Id = 1,
        Title = "Article 1",
        content = "article 1 content",
        blogId = 1,
    },
    new Article()
    {
        Id = 2,
        Title = "Article 2",
        content = "article 2 content",
        blogId = 2,
    },
    new Article()
    {
        Id = 3,
        Title = "Article 3",
        content = "article 3 content",
        blogId = 3
    },

};

List<Author> authors = new List<Author>
{
    new Author()
    {
        Id=1,
        Name="Author 1",
    },
    new Author()
    {
        Id=2,
        Name="Author 2",
    },
    new Author()
    {
        Id=3,
        Name="Author 3",
    }
};

List<Blog> blogs = new List<Blog>
{
    new Blog()
    {
        Id=1,
        blogTitle = "blog 1",
        content = "blog 1 content"

    },
    new Blog()
    {
        Id=2,
        blogTitle = "blog 2",
        content = "blog 2 content"

    },
    new Blog()
    {
        Id=3,
        blogTitle = "blog 3",
        content = "blog 3 content"

    },
};

List<Comment> comments = new List<Comment>
{
    new Comment()
    {
        Id=1,
        comment="comment 1",
        articleId = 1,
        authorId = 1,
    },
    new Comment()
    {
        Id=2,
        comment="comment 2",
        articleId = 3,
        authorId = 3,
    },
    new Comment()
    {
        Id=3,
        comment="comment 3",
        articleId = 2,
        authorId = 2,
    },
};

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/blogs", () =>
{
    return blogs;
});

app.MapGet("/articles", () =>
{
    return articles;
});

app.MapGet("/comments", () =>
{
    return comments;
});

app.MapPost("/blogs", (Blog blog) =>
{
    blog.Id = blogs.Max(x => x.Id) + 1;
    blogs.Add(blog);
    return blog;
});

app.MapGet("/blogs/{id}", (int id) =>
{
    Blog blog = blogs.FirstOrDefault(b => b.Id == id);
    if (blog == null)
    {
        return Results.NotFound();
    }
    blog.articles = articles.Where(a => a.blogId == id).ToList();
    return Results.Ok(blog);
});

app.MapGet("/articles/{id}", (int id) =>
{
    Article article = articles.FirstOrDefault(b => b.Id == id); //find article by id
    if (article == null)
    {
        return Results.NotFound();
    }
    article.comments = comments.Where(c => c.articleId == id).ToList(); //put all comments with article id into a list
    return Results.Ok(article);
});

app.MapPut("/articles/{id}", (int id, Article article) =>
{
    Article articleToUpdate = articles.FirstOrDefault(a => a.Id == id); //find article by id
    int articleIndex = articles.IndexOf(articleToUpdate);
    if (articleToUpdate == null)
    {
        return Results.NotFound();
    }
    if (id != article.Id)
    {
        return Results.BadRequest();
    }
    articles[articleIndex] = article; //replace the found article with the updated article payload
    return Results.Ok();
});

app.MapDelete("/comments/{id}", (int id) =>
{
    Comment comment = comments.FirstOrDefault(c => c.Id == id);
    comments.Remove(comment);
    return Results.Ok(comments);
});

app.MapPost("/comments", (Comment comment) =>
{
    comment.Id = comments.Max(c => c.Id) + 1;
    comments.Add(comment);
});

app.Run();

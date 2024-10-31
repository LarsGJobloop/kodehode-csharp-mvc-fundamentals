var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var blogController = new Controllers.BlogController();
blogController.AddBlog("LarsG", "High Tides", "A stormy Saturday afternoon...");
blogController.AddBlog("LarsG", "Full Moon", "Nothing beats watching the sun get overtaken by the moon...");

app.MapGet("/blog", () =>
{

  var blogs = blogController.GetAllBlogs();
  var str = "";
  foreach (var blog in blogs)
  {
    str += $"Title: {blog.Title}\n{blog.Body}\n\n";
  }
  return str;
});

app.Run();

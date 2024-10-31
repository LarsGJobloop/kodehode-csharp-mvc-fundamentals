WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

// Initialize a BlogController instance to manage blog data
var blogController = new Controllers.BlogController();

// Pre-populate the controller with some sample blog entries for demonstration purposes
blogController.AddBlog("LarsG", "High Tides", "A stormy Saturday afternoon...");
blogController.AddBlog("LarsG", "Full Moon", "Nothing beats watching the sun get overtaken by the moon...");

/// <summary>
/// Retrieves the full list of blogs.
/// Accessible via a GET request to /blog
/// </summary>
app.MapGet("/blog", () =>
{
  // Retrieve all blogs from the controller
  Models.Blog[] blogs = blogController.GetAllBlogs();

  // Returns blogs in JSON format for compatibility with web clients
  return Results.Ok(blogs);
});

/// <summary>
/// Retrieves a specific blog by its unique ID.
/// Accessible via a GET request to /blog/{id}
/// </summary>
app.MapGet("/blog/{id}", (string id) =>
{
  // Use the controller to find a blog by ID
  Models.Blog? blog = blogController.GetBlogById(id);

  // Check if the blog exists and return accordingly
  if (blog != null)
  {
    return Results.Ok(blog);
  }
  else
  {
    return Results.NotFound("Blog not found.");
  }
});

/// <summary>
/// Adds a new blog if the user is authorized.
/// Accessible via a POST request to /blog
/// Expects JSON input with userName, title, and body properties.
/// </summary>
app.MapPost("/blog", (Models.CreateBlogRequest request) =>
{
  try
  {
    // Attempt to add a new blog using the controller
    Models.Blog newBlog = blogController.AddBlog(request.UserName, request.Title, request.Body);

    // Provide the location of the new resource as well as a success message
    return Results.Created($"/blog/{newBlog.Id}", newBlog);
  }
  catch (UnauthorizedAccessException)
  {
    // Return a 403 error if authorization fails
    return Results.Forbid();
  }
  catch (ArgumentException ex)
  {
    // Return a 400 error if validation fails
    return Results.BadRequest(new { message = "Failed to add blog: " + ex.Message });
  }
  catch (Exception)
  {
    // Catch-all for unexpected errors, returns a 500 error without leaking implementation details
    return Results.Problem();
  }
});

/// <summary>
/// Runs the application and listens for incoming requests.
/// </summary>
app.Run();

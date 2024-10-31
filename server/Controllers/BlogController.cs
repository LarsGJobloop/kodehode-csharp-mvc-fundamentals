namespace Controllers;

/// <summary>
/// Manages blog-related actions and controls access to blog data,
/// serving as an intermediary between the model and view layers.
/// </summary>
class BlogController
{
  // Holds a reference to the model layer that manages blog data
  private readonly Models.BlogList blogs;

  /// <summary>
  /// Initializes a new instance of the BlogController and sets up
  /// the BlogList model, establishing the controller's link to blog data.
  /// </summary>
  public BlogController()
  {
    blogs = new Models.BlogList();
  }

  /// <summary>
  /// Retrieves all blogs from the model layer.
  /// No authorization or validation is required here.
  /// </summary>
  /// <returns>An array of all blogs.</returns>
  public Models.Blog[] GetAllBlogs()
  {
    return blogs.GetAllBlogs();
  }

  /// <summary>
  /// Retrieves a specific blog by its unique ID.
  /// </summary>
  /// <param name="id">The unique identifier of the blog.</param>
  /// <returns>The blog with the specified ID, or null if not found.</returns>
  public Models.Blog? GetBlogById(string id)
  {
    return blogs.GetBlogById(id);
  }

  /// <summary>
  /// Adds a new blog entry, with basic authorization and validation handling.
  /// </summary>
  /// <param name="userName">The username of the person adding the blog.</param>
  /// <param name="title">The title of the new blog post.</param>
  /// <param name="body">The content of the new blog post.</param>
  /// <exception cref="UnauthorizedAccessException">Thrown if the user lacks permissions or if data is invalid.</exception>
  public Models.Blog AddBlog(string userName, string title, string body)
  {
    // Basic authorization: only the user "LarsG" can add new blogs.
    if (userName != "LarsG")
    {
      throw new UnauthorizedAccessException("User has no rights to add blogs");
    }

    // Tries to create a new blog and add it to the model layer, catching errors related to invalid data.
    try
    {
      Models.Blog newBlog = new Models.Blog(title, body);
      blogs.AddNewBlog(newBlog);

      return newBlog;
    }
    catch (Exception)
    {
      // Provides a general error message, hiding detailed errors for security and simplicity.
      throw new ArgumentException("Incorrect title or body, please correct");
    }
  }
}

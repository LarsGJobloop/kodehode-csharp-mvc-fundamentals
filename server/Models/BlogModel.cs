namespace Models;

/// <summary>
/// Represents a blog post with basic properties such as title, body, creation date, and last updated date.
/// </summary>
class Blog
{
  public string Id { get; init; }  // Unique identifier for each blog
  public string CreatedAt { get; init; }  // Date the blog was created
  public string LastUpdated { get; set; } // Date the blog was last updated

  public string Title { get; set; } // Title of the blog post
  public string Body { get; set; }  // Content of the blog post

  /// <summary>
  /// Constructor initializes a new Blog instance with a title and body.
  /// Assigns unique ID and timestamps for creation and last update.
  /// </summary>
  /// <param name="title">The title of the blog post.</param>
  /// <param name="body">The main content of the blog post.</param>
  /// <exception cref="ArgumentException">Thrown if title or body is empty.</exception>
  public Blog(string title, string body)
  {
    // Validating title and body to ensure they contain actual content.
    if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(body))
    {
      throw new ArgumentException("Title and Body cannot be empty.");
    }

    Title = title;
    Body = body;

    // Additional information for bookeeping
    Id = Guid.NewGuid().ToString();
    CreatedAt = Utilities.Time.ToISOString(DateTime.Now);
    LastUpdated = Utilities.Time.ToISOString(DateTime.Now);
  }

  /// <summary>
  /// Updates the title and body of the blog and refreshes the LastUpdated timestamp.
  /// </summary>
  /// <param name="title">New title for the blog.</param>
  /// <param name="body">New content for the blog.</param>
  /// <exception cref="ArgumentException">Thrown if title or body is empty.</exception>
  public void UpdateBlog(string title, string body)
  {
    if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(body))
    {
      throw new ArgumentException("Title and Body cannot be empty.");
    }

    Title = title;
    Body = body;
    LastUpdated = Utilities.Time.ToISOString(DateTime.Now);
  }

  /// <summary>
  /// Returns a string representation of the blog for easy viewing.
  /// </summary>
  // public override string ToString()
  // {
  //   return $"Title: {Title}\tID: {Id}\nCreated: {CreatedAt}\nLast Updated: {LastUpdated}\n\n{Body}";
  // }
}

/// <summary>
/// Manages a list of blogs, providing methods to add, retrieve, update, and delete blogs.
/// </summary>
class BlogList
{
  private readonly List<Blog> blogs;

  /// <summary>
  /// Initializes a new, empty blog list.
  /// </summary>
  public BlogList()
  {
    blogs = new List<Blog>();
  }

  /// <summary>
  /// Retrieves all blogs currently stored in the list.
  /// </summary>
  /// <returns>An array of all blogs.</returns>
  public Blog[] GetAllBlogs()
  {
    return blogs.ToArray();
  }

  /// <summary>
  /// Adds a new blog to the list, ensuring no duplicate IDs.
  /// </summary>
  /// <param name="blog">The blog to add.</param>
  /// <exception cref="InvalidOperationException">Thrown if a blog with the same ID already exists.</exception>
  public void AddNewBlog(Blog blog)
  {
    if (blogs.Any(b => b.Id == blog.Id))
    {
      throw new InvalidOperationException("Blog with the same ID already exists.");
    }

    blogs.Add(blog);
  }

  /// <summary>
  /// Finds a blog by its unique ID.
  /// </summary>
  /// <param name="id">The ID of the blog to find.</param>
  /// <returns>The matching blog, or null if not found.</returns>
  public Blog? GetBlogById(string id)
  {
    return blogs.Find(blog => blog.Id == id);
  }

  /// <summary>
  /// Deletes a blog by its ID.
  /// </summary>
  /// <param name="id">The ID of the blog to delete.</param>
  /// <returns>True if the blog was found and deleted; false otherwise.</returns>
  public bool DeleteBlog(string id)
  {
    Blog? blog = GetBlogById(id);
    if (blog != null)
    {
      blogs.Remove(blog);
      return true;
    }
    return false;
  }

  /// <summary>
  /// Updates an existing blog by its ID, changing its title and body.
  /// </summary>
  /// <param name="id">The ID of the blog to update.</param>
  /// <param name="title">The new title of the blog.</param>
  /// <param name="body">The new body of the blog.</param>
  /// <exception cref="KeyNotFoundException">Thrown if no blog with the given ID is found.</exception>
  public void UpdateBlog(string id, string title, string body)
  {
    Blog? blog = GetBlogById(id);
    if (blog == null)
    {
      throw new KeyNotFoundException("Blog not found.");
    }

    blog.UpdateBlog(title, body);
  }
}

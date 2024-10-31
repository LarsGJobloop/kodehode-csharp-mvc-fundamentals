namespace Models
{
  class Blog
  {
    public string Id { get; init; }
    public string CreatedAt { get; init; }
    public string LastUpdated { get; set; }

    public string Title { get; set; }
    public string Body { get; set; }

    public Blog(string title, string body)
    {
      // Bookkeeping information
      Id = new Guid().ToString(); // GUID => Globally Unique Identifier
      CreatedAt = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture); // ISO8601 compliant time string
      LastUpdated = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture); // ISO8601 compliant time string

      // User supplied information
      Title = title;
      Body = body;
    }
  }

  class BlogList
  {
    readonly List<Blog> blogs;

    public BlogList()
    {
      blogs = new List<Blog>();
    }

    public void AddNewBlog(Blog blog)
    {
      blogs.Add(blog);
    }

    public void UpdateBlog(string id, string? title, string? body)
    {
      var blog = blogs.Find(blog => blog.Id == id);

      if (blog == null)
      {
        return;
      }

      blog.Title = title ?? blog.Title;
      blog.Body = body ?? blog.Body;
    }

    public Blog[] GetAllBlogs()
    {
      return blogs.ToArray();
    }
  }
}
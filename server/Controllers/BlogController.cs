using Models;

namespace Controllers;

class BlogController
{
  BlogList blogs;

  public BlogController()
  {
    blogs = new BlogList();
  }

  public Blog[] GetAllBlogs()
  {
    // Nothing to authorize or validate here
    return blogs.GetAllBlogs();
  }

  public void AddBlog(string userName, string title, string body)
  {
    // Very basic authorization check
    if (userName != "LarsG")
    {
      throw new Exception("User has no rights to add blogs");
    }

    // Parse errors from the Model layer and
    // decide on how to communicate them to the
    // view layer
    try
    {
      Blog newBlog = new Blog(title, body);
      blogs.AddNewBlog(newBlog);
    }
    catch (Exception)
    {
      throw new Exception("Incorrect title or body, please correct");
    }
  }
}

namespace Models;

class CreateBlogRequest
{
  public required string UserName { get; set; }
  public required string Title { get; set; }
  public required string Body { get; set; }
}

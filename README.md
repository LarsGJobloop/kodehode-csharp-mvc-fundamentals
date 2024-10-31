# MVC Pattern in C#

## Local Development

### Setup

1. Start development server

   ```shell
   dotnet run --project server
   ```

## Supported commands

- List all blogs
  ```sh
  curl \
   -X GET "http://localhost:5211/blog"
  ```
- Add a new blog
  ```sh
  curl \
   -X POST "http://localhost:5211/blog" \
   -H "Content-Type: application/json" \
   -d '{
         "userName": "LarsG",
         "title": "Surprise Eclipse",
         "body": "Venus have never looked so cool as the day..."
       }'
  ```

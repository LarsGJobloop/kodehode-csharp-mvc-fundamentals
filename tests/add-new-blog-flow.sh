#!/bin/bash

server_address="http://localhost:5211"
content_type="Content-Type: application/json"

# Check if jq is installed
if ! command -v jq &> /dev/null; then
  echo "Error: jq is not installed. Please install jq to run this script."
  exit 1
fi

# Utility function: Makes a POST request to add a new blog
add_blog() {
  local user_name="$1"
  local title="$2"
  local body="$3"

  # Perform POST request and capture both response body and status code
  local response
  response=$(curl -s -X POST "${server_address}/blog" \
    -H "$content_type" \
    -d "{\"userName\": \"$user_name\", \"title\": \"$title\", \"body\": \"$body\"}" \
    -w "\n%{http_code}")

  # Separate the response body from the status code
  local http_code
  http_code=$(echo "$response" | tail -n1)
  local response_body
  response_body=$(echo "$response" | sed '$d')

  # Check if the POST was successful and return the blog ID if so
  if [[ "$http_code" == "201" ]]; then
    echo "$response_body" | jq -r '.id'
  else
    echo "Error: Failed to add blog (HTTP status $http_code). Response:"
    echo "$response_body"
    return 1
  fi
}

# Utility function: Makes a GET request to retrieve a blog by ID
get_blog_by_id() {
  local blog_id="$1"

  # Perform GET request and capture both response body and status code
  local response
  response=$(curl -s -X GET "${server_address}/blog/${blog_id}" -w "\n%{http_code}")

  # Separate the response body from the status code
  local http_code
  http_code=$(echo "$response" | tail -n1)
  local response_body
  response_body=$(echo "$response" | sed '$d')

  # Check if the GET was successful and return the blog details if so
  if [[ "$http_code" == "200" ]]; then
    echo "$response_body"
  else
    echo "Error: Failed to retrieve blog (HTTP status $http_code). Response:"
    echo "$response_body"
    return 1
  fi
}

# Main script flow

# Step 1: Add a new blog and retrieve its ID
echo "Adding a new blog..."
new_blog_id=$(add_blog "LarsG" "Distant Family" "Now you listen here! In my days, your cousin Pluto still...")

# Verify if the blog was added successfully
if [[ $? -ne 0 || -z "$new_blog_id" ]]; then
  echo "Failed to add a new blog. Exiting..."
  exit 1
fi

echo "Blog added successfully with ID: $new_blog_id"

# Step 2: Retrieve the blog by ID
echo "Retrieving the blog..."
blog_details=$(get_blog_by_id "$new_blog_id")

# Verify if the blog was retrieved successfully
if [[ $? -ne 0 || -z "$blog_details" ]]; then
  echo "Failed to retrieve blog. Exiting..."
  exit 1
fi

# Display the blog details
echo "Retrieved blog details:"
echo "$blog_details" | jq

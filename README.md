# Social Media Platform API

This is a RESTful API for a social media platform built with ASP.NET Core. The API allows users to register, authenticate, create posts, comments, likes, friendships, and messages. This document provides detailed instructions on how to set up and use the API.

## Table of Contents

- [Introduction](#introduction)
- [Components and Functionality](#components-and-functionality)
- [Setup](#setup)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Database Migration](#database-migration)
- [Usage](#usage)
  - [Authentication](#authentication)
  - [User Endpoints](#user-endpoints)
  - [Post Endpoints](#post-endpoints)
  - [Comment Endpoints](#comment-endpoints)
  - [Like Endpoints](#like-endpoints)
  - [Friendship Endpoints](#friendship-endpoints)
  - [Message Endpoints](#message-endpoints)
- [Database Schema](#database-schema)
- [Contributing](#contributing)

## Introduction

This project is a social media platform API that provides the following functionalities:

- User registration and authentication
- CRUD operations for users, posts, comments, likes, friendships, and messages
- JWT-based authentication for secure access to the API

The API is built using ASP.NET Core and Entity Framework Core for database operations.

## Components and Functionality

### 1. User Management
- **Registration**: Allows new users to sign up by providing a username, email, password, name, bio, and profile picture.
- **Authentication**: Supports user login and JWT token generation for secure access to the platform.
- **Profile Management**: Users can update their profile information and delete their account.

### 2. Content Sharing
- **Posts**: Users can create posts with text and media (images or videos), view all posts, and interact with posts (commenting and liking).
- **Comments**: Users can comment on posts, view comments, and manage their comments (create, update, delete).
- **Likes**: Users can like posts and comments to show their appreciation.

### 3. Social Interactions
- **Friendships**: Users can send and accept friend requests, view their friends, and remove friendships.
- **Messages**: Users can send private messages to their friends, view received and sent messages, and manage their messages.
## Setup

### Prerequisites

- .NET 6 SDK or later
- SQL Server or SQL Server Express
- `curl` for testing endpoints
- `jq` for parsing JSON in bash (optional, but recommended)

### Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/vikramsinghbrahm/social-media-app.git
    cd social-media-app
    ```

2. Restore the dependencies:
    ```bash
    dotnet restore
    ```

3. Update the \`appsettings.json\` file with your database connection string:
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=SocialMediaDB;Trusted_Connection=True;MultipleActiveResultSets=true"
      },
      "Jwt": {
        "Key": "YourSecretKeyGoesHere", // Replace with your actual secret key
        "Issuer": "YourIssuer",
        "Audience": "YourAudience",
        "ExpiryMinutes": 60
      },
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      },
      "AllowedHosts": "*"
    }
    ```

### Database Migration

1. Create the initial migration and update the database:
    ```bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

## Usage

### Authentication

Authenticate a user to get the JWT token:
```bash
token=$(curl -X POST http://localhost:5282/api/Users/authenticate -H "Content-Type: application/json" -d "{\"email\": \"johndoe@example.com\", \"password\": \"password123\"}" | jq -r .token)
```

### User Endpoints

**Register a new user:**
```bash
curl -X POST http://localhost:5282/api/Users/register -H "Content-Type: application/json" -d "{\"username\": \"johndoe\", \"email\": \"johndoe@example.com\", \"password\": \"password123\", \"name\": \"John Doe\", \"bio\": \"Just a regular John.\", \"profilePicture\": \"http://example.com/johndoe.jpg\"}"
```

**Get all users:**
```bash
curl http://localhost:5282/api/Users -H "Authorization: Bearer $token"
```

**Get a user by ID:**
```bash
curl http://localhost:5282/api/Users/1 -H "Authorization: Bearer $token"
```

**Update a user:**
```bash
curl -X PUT http://localhost:5282/api/Users/1 -H "Authorization: Bearer $token" -H "Content-Type: application/json" -d "{\"username\": \"johndoe_updated\", \"email\": \"johndoe_updated@example.com\", \"password\": \"newpassword123\", \"name\": \"John Doe Updated\", \"bio\": \"Updated bio.\", \"profilePicture\": \"http://example.com/johndoe_updated.jpg\"}"
```

**Delete a user:**
```bash
curl -X DELETE http://localhost:5282/api/Users/1 -H "Authorization: Bearer $token"
```

### Post Endpoints

**Create a new post:**
```bash
curl -X POST http://localhost:5282/api/Posts -H "Authorization: Bearer $token" -H "Content-Type: application/json" -d "{\"userID\": 1, \"content\": \"This is a new post.\", \"mediaType\": \"image\", \"mediaURL\": \"http://example.com/image.jpg\", \"timestamp\": \"2024-06-14T00:00:00Z\"}"
```

**Get all posts:**
```bash
curl http://localhost:5282/api/Posts -H "Authorization: Bearer $token"
```

**Get a post by ID:**
```bash
curl http://localhost:5282/api/Posts/1 -H "Authorization: Bearer $token"
```

**Update a post:**
```bash
curl -X PUT http://localhost:5282/api/Posts/1 -H "Authorization: Bearer $token" -H "Content-Type: application/json" -d "{\"userID\": 1, \"content\": \"This is an updated post.\", \"mediaType\": \"video\", \"mediaURL\": \"http://example.com/video.mp4\", \"timestamp\": \"2024-06-14T00:00:00Z\"}"
```

**Delete a post:**
```bash
curl -X DELETE http://localhost:5282/api/Posts/1 -H "Authorization: Bearer $token"
```

### Comment Endpoints

**Create a new comment:**
```bash
curl -X POST http://localhost:5282/api/Comments -H "Authorization: Bearer $token" -H "Content-Type: application/json" -d "{\"postID\": 1, \"userID\": 1, \"content\": \"This is a new comment.\", \"timestamp\": \"2024-06-14T00:00:00Z\"}"
```

**Get all comments:**
```bash
curl http://localhost:5282/api/Comments -H "Authorization: Bearer $token"
```

**Get a comment by ID:**
```bash
curl http://localhost:5282/api/Comments/1 -H "Authorization: Bearer $token"
```

**Update a comment:**
```bash
curl -X PUT http://localhost:5282/api/Comments/1 -H "Authorization: Bearer $token" -H "Content-Type: application/json" -d "{\"postID\": 1, \"userID\": 1, \"content\": \"This is an updated comment.\", \"timestamp\": \"2024-06-14T00:00:00Z\"}"
```

**Delete a comment:**
```bash
curl -X DELETE http://localhost:5282/api/Comments/1 -H "Authorization: Bearer $token"
```

### Like Endpoints

**Create a new like for a post:**
```bash
curl -X POST http://localhost:5282/api/Likes -H "Authorization: Bearer $token" -H "Content-Type: application/json" -d "{\"postID\": 1, \"userID\": 1, \"timestamp\": \"2024-06-14T00:00:00Z\"}"
```

**Create a new like for a comment:**
```bash
curl -X POST http://localhost:5282/api/Likes -H "Authorization: Bearer $token" -H "Content-Type: application/json" -d "{\"commentID\": 1, \"userID\": 1, \"timestamp\": \"2024-06-14T00:00:00Z\"}"
```

**Get all likes:**
```bash
curl http://localhost:5282/api/Likes -H "Authorization: Bearer $token"
```

**Get a like by ID:**
```bash
curl http://localhost:5282/api/Likes/1 -H "Authorization: Bearer $token"
```

**Delete a like:**
```bash
curl -X DELETE http://localhost:5282/api/Likes/1 -H "Authorization: Bearer $token"
```

### Friendship Endpoints

**Create a new friendship:**
```bash
curl -X POST http://localhost:5282/api/Friendships -H "Authorization: Bearer $token" -H "Content-Type: application/json" -d "{\"userID1\": 1, \"userID2\": 2, \"timestamp\": \"2024-06-14T00:00:00Z\"}"
```

**Get all friendships:**
```bash
curl http://localhost:5282/api/Friendships -H "Authorization: Bearer $token"
```

**Get a friendship by ID:**
```bash
curl http://localhost:5282/api/Friendships/1 -H "Authorization: Bearer $token"
```

**Delete a friendship:**
```bash
curl -X DELETE http://localhost:5282/api/Friendships/1 -H "Authorization: Bearer $token"
```

### Message Endpoints

**Create a new message:**
```bash
curl -X POST http://localhost:5282/api/Messages -H "Authorization: Bearer $token" -H "Content-Type: application/json" -d "{\"senderID\": 1, \"receiverID\": 2, \"content\": \"Hello, how are you?\", \"timestamp\": \"2024-06-14T00:00:00Z\"}"
```

**Get all messages:**
```bash
curl http://localhost:5282/api/Messages -H "Authorization: Bearer $token"
```

**Get a message by ID:**
```bash
curl http://localhost:5282/api/Messages/1 -H "Authorization: Bearer $token"
```

**Update a message:**
```bash
curl -X PUT http://localhost:5282/api/Messages/1 -H "Authorization: Bearer $token" -H "Content-Type: application/json" -d "{\"senderID\": 1, \"receiverID\": 2, \"content\": \"Hello, how have you been?\", \"timestamp\": \"2024-06-14T00:00:00Z\"}"
```

**Delete a message:**
```bash
curl -X DELETE http://localhost:5282/api/Messages/1 -H "Authorization: Bearer $token"
```
## Database Schema

The database schema is defined using the following entities and relationships:
![image](https://github.com/Vikramsinghbrahm/social-media-app/assets/80578080/12d1e284-940a-495e-8a18-8d1d405d113f)

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.

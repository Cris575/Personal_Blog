# Personal Blog

A simple personal blog application built with ASP.NET Core MVC and .NET 9.

**Project roadmap:** [Personal Blog - roadmap.sh](https://roadmap.sh/projects/personal-blog)

## Features
- Home page with article listing
- Admin-only article creation, editing, and deletion
- Cookie-based authentication
- JSON data storage for users and articles
- Responsive layout for desktop and mobile

## Requirements
- .NET 9 SDK
- Visual Studio 2026 or later, or any compatible .NET editor
- Windows, macOS, or Linux

## Project Structure
- `Personal_Blog/Controllers` - MVC controllers
- `Personal_Blog/Models` - Application models
- `Personal_Blog/Views` - Razor views
- `Personal_Blog/Repository` - Repository logic
- `Personal_Blog/Data` - JSON files for data storage
- `Personal_Blog/wwwroot/css` - Stylesheets

## Run the Project
1. Open the solution file: `Personal_Blog.sln`.
2. Restore NuGet packages.
3. Build the solution.
4. Run the project using Visual Studio or the .NET CLI:

   ```bash
   dotnet run --project Personal_Blog
   ```

5. Open the local URL displayed in the console.

## Default Users

The application uses JSON files in `Personal_Blog/Data` to store user accounts.

Default accounts:

- **Admin**
  - Email: `admin@temp.com`
  - Password: `admin`
- **User**
  - Email: `user@temp.com`
  - Password: `users`

## How to Use

### Login
1. Open the application in your browser.
2. Navigate to the login page.
3. Enter one of the valid email addresses and passwords listed above.
4. After logging in, you will be redirected to the home page.

### Admin Features

Users with the `admin` role can:
- Create new articles.
- Edit existing articles.
- Delete articles.

### Articles
- Articles are loaded from `Personal_Blog/Data/Articles.json`.
- New and edited articles are saved to the JSON file.

## Important Notes
- The application uses local JSON files instead of a database.
- Data is stored in the project folder, making it easy to modify for testing.
- For production environments, replacing JSON storage with a database is recommended.
- The

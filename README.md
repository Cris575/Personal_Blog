# Personal Blog

A simple personal blog application built with ASP.NET Core MVC and .NET 9.

## Features
- Home page with article listing
- Admin-only article creation, editing and deletion
- Cookie-based authentication
- JSON data storage for users and articles
- Responsive layout for desktop and mobile

## Requirements
- .NET 9 SDK
- Visual Studio 2026 or later, or any compatible .NET editor
- Windows, macOS, or Linux

## Project Structure
- `Personal_Blog/Controllers` - MVC controllers
- `Personal_Blog/Models` - application models
- `Personal_Blog/Views` - Razor views
- `Personal_Blog/Repository` - repository logic
- `Personal_Blog/Data` - JSON files for data storage
- `Personal_Blog/wwwroot/css` - styles

## Run the project
1. Open the solution file: `Personal_Blog.sln`
2. Restore NuGet packages
3. Build the solution
4. Run the project using Visual Studio or:
5. Open the browser at:


## Default users
The app uses JSON files in `Personal_Blog/Data` for user accounts.

Default accounts:
- Admin:
  - Email: `admin@temp.com`
  - Password: `admin`
- User:
  - Email: `user@temp.com`
  - Password: `users`

## How to use
### Login
1. Open the app in the browser
2. Navigate to the login page
3. Enter one of the valid emails and passwords above
4. After login, you will be redirected to the home page

### Admin features
If the logged user has the `admin` role:
- You can create new articles
- You can edit existing articles
- You can delete articles

### Articles
- Articles are loaded from `Personal_Blog/Data/Articles.json`
- If you add or edit entries, they are saved in the JSON file

## Important notes
- The app currently uses local JSON files instead of a database
- The data is stored in the project folder, so it is easy to modify for testing
- For production, it is recommended to replace JSON storage with a real database

## Troubleshooting
If the app does not start:
- Make sure the .NET 9 SDK is installed
- Restore NuGet packages
- Check if `appsettings.Development.json` has the correct paths
- Ensure the JSON files under `Data` exist and are readable

## License
This project is for educational and personal use.

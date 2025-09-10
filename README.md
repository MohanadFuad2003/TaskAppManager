# TaskManagerApp

**TaskManagerApp** is a web-based task management application built using **ASP.NET Core MVC (.NET 9)**. It helps users manage their tasks efficiently with a clean, dynamic user interface, authentication, and theming features.

## Features
- **User Authentication**
  - Login and logout via ASP.NET Core Identity.
  - Configurable password validation rules.
  - Access restricted to authenticated users.
- **Task Management**
  - Create, edit, and delete tasks.
  - Mark tasks as completed.
  - Filter tasks by status (Completed or Pending).
  - Assign tasks to the logged-in user.
- **Task Statistics**
  - Real-time statistics for completed and pending tasks.
- **Dynamic UI**
  - Multiple themes: Light, Dark, Blue, etc.
  - Countdown timers for task deadlines.
  - Responsive design for desktop and mobile.
- **Security**
  - Anti-forgery token protection on all forms.
  - Cookie-based authentication with sliding expiration.
  - Lockout rules after multiple failed login attempts.
- **Database**
  - Uses **SQLite**, stored in the `App_Data` folder.
  - Entity Framework Core handles migrations automatically.
  - Database is auto-created if missing.
- **Routing**
  - Default route redirects authenticated users to the tasks index.
  - Unauthenticated users are redirected to the login page.
  - Controllers are separated for authentication, task operations, editing, and deletion for better maintainability.

## Technologies Used
- **ASP.NET Core MVC (.NET 9)**
- **Entity Framework Core**
- **ASP.NET Core Identity**
- **SQLite**
- **Bootstrap 5**
- **C# 11**
- **JavaScript** for dynamic UI features

## How to Run
1. Clone the repository to your local machine.  
2. Open the project in **Visual Studio 2022** or **VS Code**.  
3. Restore NuGet packages.  
4. Run the project (press **F5**). The SQLite database will be generated automatically in the `App_Data` folder.  
5. Open the app in your browser at `https://localhost:5001` (or the assigned port).  
6. If not authenticated, you will be redirected to the **Login** page.

## Usage Scenario
1. Open the project.  
2. Press **F5** to run.  
3. Click **Register** and create an account.  
4. Log in with your credentials.  
5. Start testing by adding, editing, or completing tasks.

## Notes
- Login is the entry point for unauthenticated users.  
- All task operations are tied to the logged-in user.  
- Themes and countdown timers are dynamic and persist across sessions.  
- Once a task is marked **completed**, it cannot be deleted.  
- Additional features implemented:  
  - Header clock/timer.  
  - Multiple theme colors.  
  - Task difficulty levels (Easy, Medium, Hard).  
  - Edit profile and change password.  

## Future Improvements
- Email confirmation and password recovery.  
- Drag-and-drop task reordering.  
- Notifications for upcoming deadlines.  
- Support for task categories and priorities.  
- Improved UI/UX with animations and new themes.  

## Project Details
- **Development time:** ~6 days  
- **Known issues:**  
  - Code needs refactoring for cleaner structure.  
  - Frontend pages need design and UI/UX improvements (frontend is my weak spot).  
  - AI support is being used to improve frontend pages.  

**Author:** [Muhannad Oshaibe]  
**Date:** September 2025

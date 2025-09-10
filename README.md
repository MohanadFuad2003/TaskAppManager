
**TaskManagerApp** is a web-based task management application built using **ASP.NET Core MVC (.NET 9)**. It allows users to manage their tasks efficiently while providing a clean and dynamic user interface with authentication and theming features.

---

## Features

- **User Authentication**
  - Login and Logout using ASP.NET Core Identity.
  - Password validation rules are configurable.
  - Access control for authenticated users.

- **Task Management**
  - Create, edit, and delete tasks.
  - Mark tasks as completed.
  - Filter tasks based on status: Completed or Pending.
  - Assign tasks to the currently logged-in user.

- **Task Statistics**
  - Real-time statistics showing the number of completed and pending tasks.

- **Dynamic UI**
  - Multiple themes: Light, Dark, Blue, etc.
  - Dynamic countdown for task deadlines.
  - Responsive design for desktop and mobile devices.

- **Security**
  - Anti-forgery token protection for all forms.
  - Cookie-based authentication with sliding expiration.
  - Lockout rules for multiple failed login attempts.

- **Database**
  - Uses **SQLite** database stored in the `App_Data` folder.
  - Entity Framework Core handles migrations automatically.
  - Database is automatically created if missing.

- **Routing**
  - Default route redirects authenticated users to the tasks index page.
  - Unauthenticated users are redirected to the login page.
  - Controllers handle task operations and user authentication separately for better organization.

---

## Technologies Used

- **ASP.NET Core MVC (.NET 9)**
- **Entity Framework Core**
- **ASP.NET Core Identity**
- **SQLite**
- **Bootstrap 5**
- **C# 11**
- **JavaScript** for dynamic UI interactions

---

## How to Run

1. Clone the repository to your local machine.
2. Open the project in **Visual Studio 2022** or **VS Code**.
3. Restore NuGet packages.
4. Run the project. The SQLite database will be created automatically in the `App_Data` folder.
5. Access the application via `https://localhost:5001` (or the port assigned by your IDE).
6. You will be redirected to the **Login page** if not authenticated.

--> you can run the project whenn you open it in visual studio , just enter F5 to run it

--- 

## Scenario : Open project --> run project click F5  --> go to register button and enter your informations --> login --> try the project by add task ....etc

---

## Notes

- The login page is the entry point for unauthenticated users.
- All task operations are linked to the currently logged-in user.
- Theme selection and countdown timers are dynamic and persist across sessions.
- The project is designed to be modular, with separate controllers for login, task management, editing, and deletion to simplify maintenance and scalability.
- when you add task , you can not delete it after completed this task
- add more features like : timer on header , more theme color , timer for tasks , levels for tasks (mid , easy , hard) , edit profile , change password ..  etc 
---

## Future Improvements

- Add email confirmation and password recovery functionality.
- Add drag-and-drop task reordering.
- Implement notifications for upcoming deadlines.
- Support for task categories and priorities.
- Enhance the UI with animations and additional themes.

---

**Author:** [Muhannad Oshaibe]  
**Date:** September 2025

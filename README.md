# CompanySystemManager

**CompanySystemManager** is a lightweight ASP.NET MVC web application designed to manage company departments and employees. It supports full Create, Read, Update, Delete (CRUD) operations, advanced role-based access control (RBAC), and AJAX-powered search for efficient data handling.
**🚀 AI Integration**
The system is enhanced with an AI-powered chatbot that leverages LangChain SQL to translate natural language queries into SQL commands. It includes validation and cleanup steps for accurate results, applies quantization for optimized performance, and was tested externally using ngrok for secure remote access.

## 🛠️ Technologies Used

- **ASP.NET MVC**
- **Entity Framework**
- **SQL Server**
- **C#**
- **Razor Views**
- **Bootstrap** (for styling)
- **AJAX** (for enhanced search and dynamic updates)
- **AI Chatbot** (SQL query generator)

## ✨ Features

### 📁 Department Management
- Create, view, update, and delete departments
- Comprehensive department listing

### 👥 Employee Management
- Assign employees to departments
- Edit and delete employee records
- List employees per department
- Complete employee profile management

### 🔑 Authentication & User Management
- User registration, login, and logout
- Forgot password functionality
- Role-based access control (RBAC)
- User Manager & Role Manager for admin operations

### 🧩 Git Workflow
- Branch-per-feature development
- Commit-per-feature/fix workflow
- Merges handled via dev branch before release to master

### 🔍 Search Functionality
- AJAX-powered search 
- Fast and efficient filtering without full page reloads
  
### 🤖 AI Chatbot
- SQL query generator chatbot integrated into the system
- **Generates only `SELECT` queries** based on user input
- Helps admins and developers quickly retrieve data without writing SQL manually

## 🚀 Getting Started

### Prerequisites
- Visual Studio 2019 or later
- SQL Server (LocalDB or Express)
- .NET Framework 4.7.2 or higher

### Setup Instructions

1. **Clone the repository:**
   ```bash
   git clone https://github.com/Omar-Badwilan/CompanySystemManager.git
   ```

2. **Open the solution** in Visual Studio.

3. **Configure the database connection string** in `Web.config`:


4. **Apply EF Migrations**: 
   Open the Package Manager Console and run:
   ```bash
   Update-Database
   ```

5. **Run the application** using Visual Studio (F5 or Ctrl+F5).

## 📁 Project Structure

```
CompanySystemManager/
├── Controllers/          # Department, Employee, User, Role controllers
├── Models/               # Entity classes and validation
├── Views/                # Razor views for all operations
├── Data/                 # Entity Framework context and configurations
├── Content/              # CSS and styling files
├── Scripts/              # JavaScript files
└── App_Data/             # Database files
```


## 🗃️ Database Schema

The application uses the following main entities:

- **Department**: Manages company departments
- **Employee**: Manages employee information with department relationships
- **User**: Application users
- **Role**: User roles for access control

## 🎯 Usage

1. Navigate to the **Departments** section to create and manage departments.
2. Use the **Employees** to add, edit, or delete employees and assign them to departments.
3. Use Users & Roles (admin only) to manage access permissions.
4. Utilize AJAX search for fast, dynamic filtering.
5. Edit or delete records via an intuitive interface.
6. Use the **AI Chatbot** to generate `SELECT` SQL queries quickly for retrieving data.


## 📌 To-Do / Future Enhancements

- [ ] Complete Reset Password functionality.
- [ ] Enhance AI Chatbot with advanced query parsing and error handling.


## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request


## 👤 Author

**Omar Ahmed Badwilan**

- 🌐 [GitHub Profile](https://github.com/Omar-Badwilan)
- 📧 Email: badwilanomar@gmail.com

## 🙏 Acknowledgments

- Thanks to the ASP.NET MVC community for excellent documentation
- Bootstrap for responsive design components
- Entity Framework for simplified data access
---

⭐ **If you found this project helpful, please consider giving it a star!** ⭐

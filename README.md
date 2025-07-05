# CompanySystemManager

**CompanySystemManager** is a lightweight ASP.NET MVC web application designed to manage company departments and employees. It supports full **Create, Read, Update, Delete (CRUD)** operations and offers search functionality for efficient data handling.

## 🛠️ Technologies Used

- **ASP.NET MVC**
- **Entity Framework**
- **SQL Server**
- **C#**
- **Razor Views**
- **Bootstrap** (for styling)

## ✨ Features

### 📁 Department Management
- Create, view, update, and delete departments
- Comprehensive department listing

### 👥 Employee Management
- Assign employees to departments
- Edit and delete employee records
- List employees per department
- Complete employee profile management

### 🔍 Search Functionality
- Search departments and employees by name or keyword
- Fast and efficient filtering

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
├── Controllers/           # Department and Employee controllers
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

## 🎯 Usage

1. Navigate to the **Departments** section to create and manage departments
2. Use the **Employees** section to add employees and assign them to departments
3. Utilize the search functionality to quickly find specific departments or employees
4. Edit or delete records as needed through the intuitive interface

## 📌 To-Do / Future Enhancements

- [ ] User authentication (login & role-based access)
- [ ] Pagination for large datasets
- [ ] Client-side validation improvements
- [ ] Responsive design enhancements
- [ ] Export functionality (Excel/PDF)
- [ ] Advanced filtering options
- [ ] Employee photo upload
- [ ] Department hierarchy support

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

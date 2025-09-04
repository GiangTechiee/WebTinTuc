# News Website System for Professional Association

A comprehensive news management platform built with .NET MVC, designed for professional associations to publish and manage news content efficiently.

## 📰 Overview

This news website system provides a complete content management solution for professional associations, featuring article publishing, categorization, search functionality, and user management. Built with .NET MVC architecture and SQL Server database.

## 📋 Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Installation](#installation)
- [Usage](#usage)
- [Database Schema](#database-schema)
- [API Documentation](#api-documentation)
- [Project Structure](#project-structure)
- [Team](#team)
- [Contributing](#contributing)
- [License](#license)

## ✨ Features

- 📝 **Article Management** - Create, edit, delete, and publish news articles
- 🗂️ **Category System** - Organize articles by categories and tags
- 🔍 **Advanced Search** - Full-text search with filters and sorting
- 👥 **User Management** - Role-based access control (Admin, Editor, Viewer)
- 🔐 **Authentication System** - Secure login and session management
- 📱 **Responsive Design** - Mobile-friendly interface with Bootstrap
- 📊 **Content Analytics** - View article statistics and engagement
- 💬 **Comment System** - User comments and moderation
- 📰 **RSS Feeds** - Syndication support for news updates
- 📧 **Newsletter** - Email subscription and notifications

## 🛠️ Tech Stack

### Backend
- **.NET Framework/Core** - Web application framework
- **ASP.NET MVC** - Model-View-Controller architecture
- **Entity Framework** - Object-relational mapping
- **SQL Server** - Relational database management system
- **Identity Framework** - Authentication and authorization

### Frontend
- **Razor Pages** - Server-side rendering
- **HTML5/CSS3** - Modern web standards
- **Bootstrap** - Responsive CSS framework
- **JavaScript/jQuery** - Client-side interactivity

## 🔧 Installation

### Prerequisites
- Visual Studio 2019 or later
- .NET Framework 4.8 or .NET 6.0+
- SQL Server 2016 or later
- IIS (for deployment)

### Setup Instructions

1. **Clone the repository**
```bash
git clone https://github.com/GiangTechiee/WebTinTuc.git
cd WebTinTuc
```

2. **Database Setup**
```sql
-- Create database
CREATE DATABASE NewsWebsite;

-- Update connection string in appsettings.json or web.config
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=NewsWebsite;Trusted_Connection=true;MultipleActiveResultSets=true"
}
```

3. **Build and Run**
```bash
# Restore NuGet packages
dotnet restore

# Build the project
dotnet build

# Run database migrations
dotnet ef database update

# Start the application
dotnet run
```

4. **Access the Application**
- Open browser and navigate to `https://localhost:5001`
- Default admin credentials: admin@example.com / Admin123!

## 🎯 Usage

### For Administrators
1. **Content Management**: Create and manage news articles
2. **User Management**: Add/remove users and assign roles  
3. **Category Management**: Organize content categories
4. **Site Settings**: Configure website settings and preferences

### For Editors
1. **Article Creation**: Write and publish news articles
2. **Content Review**: Edit and approve submitted articles
3. **Media Management**: Upload and manage images/files

### For Viewers
1. **Browse News**: Read latest articles and archives
2. **Search Content**: Find specific articles using search
3. **Subscribe**: Sign up for newsletter notifications

## 🗄️ Database Schema

The database follows 3NF normalization principles:

### Core Tables
- **Users** - User account information
- **News** - News article content and metadata
- **Categories** - Article categorization
- **Comments** - User comments on articles
- **Roles** - User role definitions

### Relationship Tables
- **NewCategories** - Many-to-many: Articles ↔ Categories
- **NewTags** - Many-to-many: Articles ↔ Tags
- **UserRoles** - Many-to-many: Users ↔ Roles

```sql
-- Example table structure
CREATE TABLE News (
    Id int IDENTITY(1,1) PRIMARY KEY,
    Title nvarchar(200) NOT NULL,
    Content ntext NOT NULL,
    Summary nvarchar(500),
    AuthorId int FOREIGN KEY REFERENCES Users(Id),
    PublishedDate datetime2,
    CreatedDate datetime2 DEFAULT GETDATE(),
    Status nvarchar(20) DEFAULT 'Draft',
    ViewCount int DEFAULT 0
);
```

## 📚 API Documentation

### Article Endpoints
```
GET    /News              - List all news
GET    /News/{id}         - Get new details  
POST   /News/Create       - Create new new
PUT    /News/{id}         - Update new
DELETE /News/{id}         - Delete new
GET    /News/Search       - Search new
```

### Category Endpoints
```
GET    /Categories            - List all categories
POST   /Categories/Create     - Create category
PUT    /Categories/{id}       - Update category  
DELETE /Categories/{id}       - Delete category
```

### User Management
```
GET    /Account/Login         - Login page
POST   /Account/Login         - Process login
GET    /Account/Register      - Registration page
POST   /Account/Register      - Process registration
POST   /Account/Logout        - User logout
```

## 📁 Project Structure

```
WebTinTuc/
├── Controllers/          # MVC Controllers
│   ├── NewsController.cs
│   ├── CategoriesController.cs
│   ├── AccountController.cs
│   └── HomeController.cs
├── Models/              # Data models
│   ├── New.cs
│   ├── Category.cs
│   ├── User.cs
│   └── ViewModels/
├── Views/               # Razor views
│   ├── News/
│   ├── Categories/
│   ├── Account/
│   └── Shared/
├── Data/               # Entity Framework context
│   ├── ApplicationDbContext.cs
│   └── Migrations/
├── wwwroot/            # Static files
│   ├── css/
│   ├── js/
│   └── images/
├── Services/           # Business logic services
├── Helpers/            # Utility classes
└── appsettings.json    # Configuration
```

## 👥 Team

This project was developed by a team of 3 members:

**Team Leader: Trần Trường Giang**
- Role: Project Management, Backend Development, Database Design
- Responsibilities: Architecture planning, API development, team coordination

**Team Members:**
- Nguyễn Thị Huyền Phương: Frontend Development, UI/UX Design
- Lâm Minh Quân: Testing, Documentation, Deployment

## 🏆 Achievements

- **Faculty-level Recognition**: Successfully presented at university level
- **Code Quality**: Implemented industry best practices and design patterns
- **Performance Optimization**: Achieved fast search performance with proper indexing
- **Security**: Implemented robust authentication and data validation



## 🚀 Deployment

### Docker Deployment
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY . .
EXPOSE 80
ENTRYPOINT ["dotnet", "WebTinTuc.dll"]
```

## 🤝 Contributing

We welcome contributions from the community:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/new-feature`)
3. Make your changes following coding standards
4. Write tests for new functionality
5. Submit a pull request with detailed description

### Coding Standards
- Follow C# naming conventions
- Use async/await for database operations
- Add XML documentation for public methods
- Maintain consistent code formatting

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👤 Contact

**Trần Trường Giang** - Team Leader
- Email: giangtt8726@gmail.com
- GitHub: [@GiangTechiee](https://github.com/GiangTechiee)
- University: Ha Noi Open University - Software Engineering

## 🙏 Acknowledgments

- Ha Noi Open University for project guidance
- Faculty of Information Technology for technical support
- Team members for collaborative development
- Microsoft for .NET framework and documentation

---

📚 This project demonstrates enterprise-level web development skills with professional software engineering practices.

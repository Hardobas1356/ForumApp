# 🧭 ForumApp

**ForumApp** is a web-based forum platform built with **ASP.NET Core** and **Entity Framework Core**.  
It allows users to create and engage in topic-based discussions, managed by board moderators and administrators.

---

## 💡 Key Features

- User registration, login, and profile management  
- Board and post creation with tags and categories  
- Reply creation and soft deletion of content  
- Admin panel for managing users and boards  
- Role-based access (users, moderators, admins)

---

## ⚙️ Tech Stack

- ASP.NET Core 8.0  
- Entity Framework Core  
- Identity (User and Role Management)  
- **NUnit** + **Moq** (Unit Testing)  
- SQL Server  
- Coverlet + ReportGenerator (Code Coverage)

---

## 🚀 Setup Instructions

1. **Clone the repository**
2. **Update** `appsettings.json` with your DB connection string
3. **Run database migrations**
4. **Run the application**
5. (Optional) **Run tests**

---

## 👥 User Roles

- User – Create posts and replies
- Moderator – Manage posts and replies in assigned boards
- Admin – Full access to users, boards, and system-wide settings. May assign moderators and add admins

---

## 🧪 Testing & Coverage

- The application uses NUnit and Moq for unit testing
- Key services have >65% test coverage
- Coverage reports can be generated using Coverlet + ReportGenerator

---

## 📂 Project Structure 

- ForumApp.Data – EF Core models and configurations
- ForumApp.Services – Business logic and service layer
- ForumApp.Web – MVC Controllers and Views
- ForumApp.Services.Tests – Unit tests

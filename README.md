# ASP.NET Core REST API Starter Template (PostgreSQL)
  
A clean, modular, and production-ready REST API starter built on top of **ASP.NET Core Web API** with **PostgreSQL** as database.
Designed to save time by eliminating repetitive setup and allowing you to build new APIs quickly.

> **🚀 Same template with MongoDB as the database is available at:**  
> 👉 [ASP.NET Core REST API Starter Template (MongoDB)](https://github.com/jjammeso/aspnet-mongo-api)

✨ Features

    ✅ Clean Architecture (Controllers, Services, Repositories, DTOs)
    🔐 JWT Authentication + Refresh Token Flow
    👥 Role-Based Authorization
    🧾 Request Validation using FluentValidation
    🧼 Global Error Handling Middleware
    🐘 PostgreSQL Integration via Entity Framework Core
    📖 Swagger UI (OpenAPI) for API testing
    ⚙️ Dependency Injection for modularity and testability
    🌐 CORS enabled
    🔒 Secure headers + HTTPS enabled

📁 Folder Structure
```
RestApiTemplate/
├── Controllers/
├── Database/
├── DTOs/
├── Middlewares/
├── Migrations/
├── Models/
├── Repositories/
│ └── Interfaces/
├── Services/
│ └── Interfaces/
├── Validators/
├── appsettings.json
└── Program.cs
```
⚙️ Getting Started
1. Clone the Repository

git clone https://github.com/jjammeso/aspnet-postgres-api.git

2. Set Up PostgreSQL

Make sure PostgreSQL is running locally or remotely. and your credentials are correct in `appsettings.json`.

"MongoDbSettings": {
  "PostgresConnection": "Host=localhost;Port=5432;Database=your_db;Username=your_user;Password=your_password",
}

3. Restore Packages & Run

dotnet restore
dotnet run

    API will run at: http://localhost:5173
    Swagger UI available at: https://localhost:7095/swagger/index.html

🔐 Authentication Flow

Register → POST /auth/register    //Register a new user

Login → POST /auth/login         //Log in and receive access + refresh tokens

Use Bearer token in Authorization header      //Use the Bearer token in the Authorization header

Refresh token → POST /auth/refresh          //Refresh the access token using a valid refresh token

🧪 API Testing

Swagger UI is enabled at:

https://localhost:7095/swagger/index.html

Use it to test endpoints and view request/response models.
💡 Why I build this template?

I often found myself repeating the same setup for new projects: authentication, architecture, error handling, and more. This template helps spin up a new REST API in minutes with all essentials ready to go.
🤝 Contribute or Use It

Feel free to use it in your own projects or contribute to improve it!

Pull requests are welcome. 🌟
📄 License

MIT License
✍️ Author

Sonam Jamtsho

📧 Email: sjjamtsho@gmail.com

🔗 LinkedIn: https://www.linkedin.com/in/sonam-jamtsho-944288228/

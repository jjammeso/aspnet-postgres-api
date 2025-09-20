# ASP.NET Core REST API Starter Template (PostgreSQL)
  
A clean, modular, and production-ready REST API starter built on top of **ASP.NET Core Web API** with **PostgreSQL** as database.
Designed to save time by eliminating repetitive setup and allowing you to build new APIs quickly.

> **🚀 Same template with MongoDB as the database is available at:**  
> 👉 [ASP.NET Core REST API Starter Template (MongoDB)](https://github.com/jjammeso/aspnet-mongo-api)

## ✨ Features

    ✅ Clean Architecture (Controllers, Services, Repositories, DTOs)
    🔐 JWT Authentication & Authorisation with Refresh Token Flow
    🐘 PostgreSQL Integration via Entity Framework Core
    📖 Swagger UI (OpenAPI) for API testing
    ⚙️ Dependency Injection for modularity and testability
    🌐 CORS enabled *(add or customize via Program.cs)*
    🔒 Secure headers + HTTPS enabled *(basic HTTPS enabled, custom security headers can be extended)*
    🧾 User credential Validation using FluentValidation
    👥 Role-Based Authorization


## 🔧 Possible Additions
    🧼 Global Error Handling Middleware

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

## ⚙️ Getting Started

### 1. Clone the Repository
```
git clone https://github.com/jjammeso/aspnet-postgres-api.git
cd aspnet-postgres-api/RestApiTemplate
```

### 2. Rename Configuration File

Rename **appsettings.sample.json** to **appsettings.json**
```
mv appsettings.sample.json appsettings.json
```
### 3. Configure PostgreSQL Connection

Make sure PostgreSQL is running locally or remotely, then update the PostgresConnection string in the `appsettings.json`. 

```json
"ConnectionStrings": {
  "PostgresConnection": "Host=localhost;Port=5432;Database=your_db;Username=your_user;Password=your_password",
}
```
### 4. Configure JWT settings

Set the following fields in appsettings.json under JwtSettings:

```json
"JwtSettings": {
  "Issuer": "YourAppName",
  "Audience": "YourAppUser",
  "Secret": "CreateASecretKeyWith32characters",
  "ExpiryMinutes": 60
}
```

### 5. Restore Packages & Run the App
```bash
dotnet restore
dotnet run
```

### 6. Access the API at links below

- Swagger UI: https://localhost:7095/swagger/index.html
- API Base URL: http://localhost:5173

---

## 🔐 Authentication Flow

- Register a new user
    → POST /auth/register


- Log in and receive access + refresh tokens
     → POST /auth/login

      * Use Bearer token in Authorization header      Add Authorization: Bearer <token> header to protected requests

- Refresh token → POST /auth/refresh          //Refresh the access token using a valid refresh token

🧪 API Testing

Swagger UI is enabled at:

    https://localhost:7095/swagger/index.html

Use it to test endpoints and view request/response models.

---

## 💡 Why I build this template?

I often found myself repeating the same setup for new projects: authentication, architecture, error handling, and more. This template helps spin up a new REST API in minutes with all essentials ready to go.


## 🤝 Contribute or Use It

Feel free to use it in your own projects or contribute to improve it!

Pull requests are welcome. 🌟

---

### 📄 License

MIT License

---

✍️ Author

Sonam Jamtsho

📧 Email: sjjamtsho@gmail.com

🔗 LinkedIn: https://www.linkedin.com/in/sonam-jamtsho-944288228/

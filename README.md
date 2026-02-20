# Torus — 3D Model Sharing Platform

A full-stack web application for uploading, viewing, and sharing 3D models (`.glb` files) rendered in the browser. Users can sign up, manage a profile, upload models, and browse a community feed.

---

## Tech Stack

| Layer    | Technology                                      |
| -------- | ----------------------------------------------- |
| Frontend | React 19, Vite, React Three Fiber, React Router |
| Backend  | ASP.NET Core 8 Web API                          |
| ORM      | Entity Framework Core (Pomelo MySQL provider)   |
| Database | MySQL                                           |
| Auth     | JWT (JSON Web Tokens)                           |

---

## Prerequisites

Make sure you have the following installed before continuing:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (v18+ recommended)
- [MySQL](https://dev.mysql.com/downloads/) (8.0+)

---

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/macchi-ato/torus.git
cd torus
```

### 2. Set up the database

Open a MySQL shell and create a database:

```sql
CREATE DATABASE torus;
```

### 3. Configure the server

The API uses [.NET User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) to store the database connection string outside of source control.

```bash
cd server
dotnet restore
```

Set your MySQL connection string as a user secret:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=torus;User=root;Password=<your-mysql-password>;"
```

> Replace `<your-mysql-password>` with your actual MySQL root password (or whichever user you created).

### 4. Apply database migrations

Still inside the `server/` directory:

```bash
dotnet ef database update
```

> If you don't have the EF Core CLI tool installed, run `dotnet tool install --global dotnet-ef` first.

### 5. Run the server

```bash
dotnet run
```

### 6. Set up and run the client

Open a **new terminal**, then:

```bash
cd client
npm install
npm run dev
```

---

## Usage

1. Open **http://localhost:5173** in your browser.
2. **Sign up** for a new account.
3. **Upload** `.glb` 3D model files from the work upload page.
4. **Browse** the community feed to view models uploaded by all users.
5. **Manage** your profile title and description from the profile page.

---

## Project Structure

```
torus/
├── client/                 # React frontend (Vite)
│   └── src/
│       ├── components/     # Shared components (Navbar, WorkView)
│       ├── contexts/       # Auth context (JWT)
│       ├── pages/          # Page components (Home, Login, Signup, Profile, etc.)
│       └── routes/         # Client-side routing
│
└── server/                 # ASP.NET Core Web API
    ├── Controllers/        # API endpoints (Users, Models3D)
    ├── Database/           # EF Core DbContext
    ├── Migrations/         # EF Core migrations
    ├── Models/             # Entity models and DTOs
    ├── Services/           # JWT service
    └── Properties/         # Launch settings
```

---

## API Overview

| Method | Endpoint                        | Auth     | Description                   |
| ------ | ------------------------------- | -------- | ----------------------------- |
| POST   | `/api/users`                    | No       | Sign up a new user            |
| POST   | `/api/users/login`              | No       | Log in and receive a JWT      |
| GET    | `/api/users/user`               | Yes      | Get current user profile      |
| PUT    | `/api/users/update`             | Yes      | Update profile title/desc     |
| GET    | `/api/users/reports/myuploads`  | Yes      | Get current user's uploads    |
| POST   | `/api/models3d`                 | Yes      | Upload a `.glb` model         |
| GET    | `/api/models3d/all`             | No       | List all models               |
| GET    | `/api/models3d/user`            | Yes      | List current user's models    |
| GET    | `/api/models3d/{id}`            | No       | Download a model file         |
| DELETE | `/api/models3d/{id}`            | Yes      | Delete a model                |

---

## Troubleshooting

- **HTTPS certificate errors** — If the browser warns about the dev certificate, run `dotnet dev-certs https --trust` (may require admin/sudo).
- **CORS errors** — Make sure the server is running on port `7125` and the client on port `5173`. The server's CORS policy is configured for these ports.
- **Database connection issues** — Double-check your connection string in user secrets with `dotnet user-secrets list` inside the `server/` directory.
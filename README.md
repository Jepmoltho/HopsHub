# HopsHub

Add and rate beers to keep track of your favorite brews.

## Project Overview

HopsHub is a web application where users can:

- Add beers to a personal list.
- Rate beers and view public ratings.
- Explore beers by categories such as pilsners, IPAs, and more.
- This project uses Docker Compose for streamlined setup and deployment.

## Repositories

GitHub Repository: https://github.com/Jepmoltho/HopsHub
Docker Hub Repository: https://hub.docker.com/repository/docker/jepmoltho/hopshub/general

## Prerequisites

Before running the project, ensure you have the following installed:

Docker
Docker Compose

## Getting Started

1. Clone the Repository

- git clone https://github.com/Jepmoltho/HopsHub.git
- cd HopsHub

2. Set Up Environment Variables
   Environment variables are managed using Docker secrets. Create the following secret files in the project root directory:

- db_password.secret: Contains the SQL Server SA_PASSWORD.
- testuser_password.secret: Contains the test user password.
- jwt_login_token_key.secret: Contains the JWT login token key used to hash user password.

echo "YOUR_STRONG_PASSWORD" > db_password.secret
echo "TEST_USER_PASSWORD" > testuser_password.secret
echo "YOUR_JWT_KEY" > jwt_login_token_key.secret

3. Run the Project

Use Docker Compose to build and start all services:

- docker compose build
- docker compose up

This will start the following services:

- Database: SQL Server container running on localhost:1433.
- Backend: ASP.NET Core API running on http://localhost:8080.
- Frontend: Blazor WebAssembly served via Nginx on http://localhost:7148.

## Accessing the Application

- Frontend: Access the frontend interface at http://localhost:7148.
- Backend: View API documentation at http://localhost:8080/swagger/index.html.
- DB: Install a DBMS to view the database. The database will be created and seeded with testdata automatically when running the application the first time.

Project Structure: The solution is divided three services managed by the Docker Compose yml file:

- HopsHub.Client: Blazor WebAssembly frontend.
- HopsHub.Server: ASP.NET Core API backend.
- DB: An unmodified instance of the Microsoft SQL Server image.

## Stopping the project

- docker compose down

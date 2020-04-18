# Print Center
>Web application for placing orders in the print department

## Technologies

  * ASP.NET Core 3.1 (with .NET Core 3.1) 
  * ASP.NET WebApi Core with JWT Bearer Authentication
  * Entity Framework Core 3.1
  * PostgreSQL 12
  * .NET Core Native DI
  * AutoMapper
  * FluentValidator
  * MediatR (CQRS with MediatR and AutoMapper)
  * Swagger UI with JWT support
  * Testing with xUnit, NSubstitute

## Building, Docker
  * Nuke - `dotnet run -p build/_build.csproj`
  * Docker Build and Run - `docker-compose up --build`
  
## Swagger URL
  * `http://localhost:5000/swagger`
  
## GitHub Actions build

![build-and-test](https://github.com/t1mur619533/printcenter/workflows/build-and-test/badge.svg)

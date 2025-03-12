# E-Commerce

## Description
This is an E-Commerce application built using C#. The project aims to provide a platform for users to browse and purchase products online.

## Project Structure
This project is built following 4-Layered Onion Archeticture

  ### Domain Layer (Core)
  This layer contains the business models of the system along with the interfaces for repositories and unit of work.

  ### Application Layer
  This layer contains the workflow of the proejct such as interfaces for services, Data transfer objects, Mappers from models to DTOs and vice versa, generic responses for dealing with     errors and finally the services implementation.

  ### Infrastructure Layer
  This layer concerned with dealing with outside world, like dealing with database, containing configurations, DBContext and the implementation of the interfaces in Domain layer.

  ### Presentation Layer (API)
  This layer contains the application endpoints and the configuration for web app and service collections like dependency injection, authentication, authorization, cookie config and so on.

## Features
- User authentication and authorization
- Product listing and search
- Shopping cart functionality
- Order management
- Payment processing

## Technologies Used
- C#
- ASP.NET Core
- Entity Framework Core
- SQL Server

## Getting Started
### Prerequisites
- .NET 8.0 SDK
- SQL Server


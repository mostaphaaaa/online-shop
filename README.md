\# 🛒 ShopStock



ShopStock is an ASP.NET Core-based e-commerce web application built with a layered architecture, with a focus on maintainability, separation of concerns, and clean project structure.



The project includes a public-facing online store, an administration panel, and a user panel.



> ⚠️ \*\*Development Status\*\*

>

> ShopStock is currently under active development.

> The project is not yet complete, and new features, improvements, and refinements are still being added.



\---



\## ✨ Features



\### 🛍️ Store



\* Product listing and product details

\* Category-based product organization

\* Product features

\* Product tags

\* Product image gallery

\* Product thumbnails

\* Rich-text product descriptions



\### 👨‍💼 Admin Panel



\* Product management

\* Category management

\* Product gallery management

\* Product feature management

\* Product tag management

\* User and role management

\* Permission-based authorization



\### 👤 User Panel



\* User-specific functionality

\* User account management



\### 🔐 Security



\* Authentication and authorization

\* Role-based access control

\* Permission-based authorization

\* Custom authorization filters



\---



\## 🏗️ Architecture



ShopStock follows a \*\*layered architecture\*\* to separate responsibilities between different parts of the application.



```text

ShopStock

│

├── BaseEntity

│

├── ShopStock.Domain

│   ├── Models

│   └── ViewModels

│

├── ShopStock.Application

│   ├── Services

│   ├── Mapper

│   ├── Security

│   └── Utilities

│

├── ShopStock.Infra.Data

│   ├── Context

│   ├── Migrations

│   ├── Repositories

│   └── Configurations

│

├── ShopStock.Infra.IOC

│

└── ShopStock.Web

&#x20;   ├── Areas

&#x20;   │   ├── Admin

&#x20;   │   └── UserPanel

&#x20;   ├── Controllers

&#x20;   ├── Views

&#x20;   └── wwwroot

```



\### Layer Responsibilities



\*\*BaseEntity\*\*



Contains shared base functionality used by entities throughout the application.



\*\*ShopStock.Domain\*\*



Contains the core domain models, view models, enums, and contracts of the application.



\*\*ShopStock.Application\*\*



Contains application services and business logic. It also includes mapping, security-related functionality, and utility classes.



\*\*ShopStock.Infra.Data\*\*



Responsible for data access and persistence using Entity Framework Core. This layer contains the database context, repositories, entity configurations, and database-related infrastructure.



\*\*ShopStock.Infra.IOC\*\*



Responsible for dependency injection and registering application and infrastructure dependencies.



\*\*ShopStock.Web\*\*



The presentation layer of the application. It contains controllers, views, areas, and static files.



\---



\## 🛠️ Technologies



\* \*\*C#\*\*

\* \*\*ASP.NET Core\*\*

\* \*\*Entity Framework Core\*\*

\* \*\*SQL Server\*\*

\* \*\*HTML / CSS / JavaScript\*\*

\* \*\*Razor Views\*\*

\* \*\*CKEditor\*\*

\* \*\*Git / GitHub\*\*



\---



\## 📂 Project Structure



```text

ShopStock/

│

├── BaseEntity/

│   └── Shared base classes

│

├── ShopStock.Domain/

│   ├── Models/

│   │   └── Domain entities

│   │

│   └── ViewModels/

│       └── Application ViewModels

│

├── ShopStock.Application/

│   ├── Services/

│   ├── Mapper/

│   ├── Security/

│   └── Utilities/

│

├── ShopStock.Infra.Data/

│   ├── Context/

│   ├── Migrations/

│   ├── Repositories/

│   └── Configurations/

│

├── ShopStock.Infra.IOC/

│

└── ShopStock.Web/

&#x20;   ├── Areas/

&#x20;   │   ├── Admin/

&#x20;   │   └── UserPanel/

&#x20;   │

&#x20;   ├── Controllers/

&#x20;   ├── Views/

&#x20;   └── wwwroot/

```



\---



\## 🗄️ Data Access



The project uses \*\*Entity Framework Core\*\* for communication with SQL Server.



The data access layer is separated from the application layer through repositories and the infrastructure project.



Entity configurations are maintained separately from the domain models.



\---



\## 🔐 Authorization



ShopStock implements authorization at the application level using roles and permissions.



A custom permission-checking mechanism is used to restrict access to specific administrative actions and resources.



This approach helps keep authorization logic separated from the application's business logic.



\---



\## 🖼️ Product Management



Products can contain different types of information, including:



\* Product information

\* Categories

\* Product features

\* Product tags

\* Short descriptions

\* Detailed descriptions

\* Product reviews

\* Product gallery images

\* Main product images

\* Thumbnail images



Rich-text fields are managed using \*\*CKEditor\*\*.



\---



\## 🚀 Getting Started



\### Prerequisites



Make sure the following are installed:



\* .NET SDK

\* SQL Server

\* Visual Studio or another compatible IDE



\### 1. Clone the repository



```bash

git clone https://github.com/YOUR\_USERNAME/ShopStock.git

```



\### 2. Configure the database



Configure the SQL Server connection string in `appsettings.json`.



Example:



```json

{

&#x20; "ConnectionStrings": {

&#x20;   "DefaultConnection": "YOUR\_CONNECTION\_STRING"

&#x20; }

}

```



\### 3. Create a new migration



Since database migrations are not included in the repository, create a new migration based on the current project models:



```bash

dotnet ef migrations add InitialCreate

```



\### 4. Update the database



```bash

dotnet ef database update

```



\### 5. Run the application



```bash

dotnet run

```



Or run the project directly from Visual Studio.



\---



\## 🔧 Configuration



Before running the application:



1\. Configure the SQL Server connection string in `appsettings.json`.

2\. Create a new EF Core migration.

3\. Update the database.



\---



\## 📌 Architecture Overview



The main dependency flow of the application is:



```text

Web

&#x20;│

&#x20;▼

Application

&#x20;│

&#x20;▼

Infrastructure

&#x20;│

&#x20;▼

SQL Server

```



The \*\*Domain\*\* layer contains the core models and abstractions used throughout the application.



This separation helps keep business logic independent from presentation and data-access concerns.



\---



\## 🔮 Future Improvements



As the project is still under active development, planned improvements may include:



\* Unit and integration testing

\* Redis caching

\* Docker containerization

\* CI/CD pipeline

\* Improved logging and monitoring

\* Performance optimization

\* API development

\* Additional e-commerce features



\---



\## 📄 License



This project is developed for learning and portfolio purposes.



\---



\## 👨‍💻 Author



\*\*Mostapha\*\*



GitHub: `https://github.com/mostaphaaaa`




# 🚀 CRM-MicroServices

## 📌 Project Description
CRM-MicroServices is a **scalable CRM system** developed using **.NET** technology with a **microservices architecture**.  
This system enables the management of users, clients, products, sales, AI assistants, notifications, and payments.

🔹 **Key Features**:
- ✅ **Authentication** (JWT tokens, user authorization)
- ✅ **Client and lead management**
- ✅ **Product and sales management**
- ✅ **AI services** (ChatGPT, Gemini, DeepSeek) for analysis and responses
- ✅ **Email notifications**
- ✅ **Payments integrated with Stripe API**
- ✅ **Scalable architecture with Docker and Kubernetes support**

---

## ⚙️ **Technologies**
🔧 This project uses the following technologies:
- **C# .NET 8**
- **Entity Framework Core** (EF Core) – ORM for database operations
- **SQL Server** – Relational database
- **Docker & Kubernetes** – Deployment and scaling of microservices
- **Swagger** – API documentation
- **Stripe API** – Payment processing
- **Azure Storage** – Storing product images

---

## 🏗️ **Microservices**
CRM-MicroServices is divided into **7 microservices**, each with specific functionality:

| Microservice                | Functional Description |
|----------------------------|----------------------|
| **CRM.AuthenticationService** | User authentication management (JWT) |
| **CRM.CustomerService** | Client and lead management |
| **CRM.ProductService** | Creating, editing, and deleting products |
| **CRM.SalesService** | Creating and managing orders |
| **CRM.AIService** | AI assistants (ChatGPT, Gemini, DeepSeek) |
| **CRM.NotificationService** | Sending email notifications |
| **CRM.PaymentService** | Stripe integration for payments |

---

## 📥 **Installation and Setup**
To run the project, follow these steps:

### **Configure appsettings.json for each microservice**
Each microservice has its own `appsettings.json` file where you need to set up the appropriate database connection. Example for `CRM.LeadService`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server_name;Database=CRM.LeadService-0.0.1;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}

﻿# Patient Management API

## 📌 Project Description
The **Patient Management API** is a RESTful service designed to manage patient records efficiently. It provides endpoints for creating, retrieving, updating, and deleting patient data, medical records, and associated information. The API is built using **.NET 8** and follows clean architecture principles to ensure maintainability and scalability.

## 🚀 Features
- **Patient Records Management**: Create, update, and delete patient profiles.
- **Medical Records Handling**: Store and retrieve medical records linked to patients.
- **API Documentation**: Swagger UI for interactive API exploration.
- **Unit & Integration Testing**: Automated tests using xUnit.

---

## 🏗️ Project Structure
```
PatientManagement-API/
│── Controllers/            # API Controllers (Patient, Records)
│── Models/                 # Data Models (Entities)
│── Dto/                    # Data Transfer Objects (Dto)
│── Repositories/           # Data Access Layer (Interfaces, Implementations)
│── PatientManagement-API.Tests/  # Unit & Integration Tests
│── program.cs              # App Configuration & Middleware
│── appsettings.json        # Configuration File
│── README.md               # Project Documentation
```

---

## 🛠️ Setup & Installation
### **1️⃣ Prerequisites**
Ensure you have the following installed:
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

### **2️⃣ Clone Repository**
```sh
git clone https://github.com/BISHOPDAN/patient-management-api.git
cd patient-management-api
```

### **3️⃣ Install Dependencies**
```sh
dotnet restore
```

### **4️⃣ Database Migration**
Ensure you have your database connection string set in `appsettings.json`, then run:
```sh
dotnet ef database update
```

### **5️⃣ Run the Application**
```sh
dotnet run
```
API will be available at: `http://localhost:5003`

---

## ⚙️ API Endpoints
| Method | Endpoint | Description |
|--------|----------|--------------|
| `POST` | `/api/patient/CreatePatient` | Create a new patient |
| `GET`  | `/api/patient/ListPatients`| Retrieve all patient details |
| `GET`  | `/api/patient/GetPatientById/{id}` | Retrieve patient details |
| `PUT`  | `/api/patient/UpdatePatient/{id}` | Update patient details |
| `DELETE` | `/api/patient/SoftDeletePatient/{id}` | Remove a patient, soft Delete |
| `POST` | `/api/Record/CreateRecord` | Add medical record |
| `GET`  | `/api/Record/GetRecordList`| Retrieve all patient records |
| `GET`  | `/api/Record/GetRecordById/{id}` | Retrieve medical record |
| `PUT`  | `/api/Record/UpdateRecord/{id}` | update medical record |

---

## 🧪 Running Tests
### **1️⃣ Unit Tests**
Unit tests are written using **xUnit** to test repository methods, and logic, and controllers.  
To run unit tests, execute:
```sh
dotnet test
```

### **2️⃣ Integration Tests**
Integration tests validate the API endpoints using an in-memory database (e.g., SQLite in-memory).  
Run integration tests with:
```sh
dotnet test --filter Category=Integration
```

---

## 🎯 Key Design Decisions
### **🔹 Clean Architecture**
- **Separation of Concerns**: Business logic is encapsulated in repositories and as well data access, and API controllers handle request/response.
- **Dependency Injection**: Enhances testability and maintainability.

### **🔹 Database Choice**
- Chose **Sqlite** for its scalability as a simple level for testing, but the repository layer is flexible to support **SQL Server** if needed.

### **🔹 Error Handling & Logging**
- Implemented a global exception handler for consistent error responses.

---

### **3️⃣ Access the API**
Once running, API can be accessed at `http://localhost:5003`.

---

## 📜 License
This project is open-source and available under the **MIT License**.

---

## 💡 Future Improvements
- ✅ Add role-based access control (RBAC)
- ✅ Implement caching for frequently accessed data
- ✅ Deploy to **Azure** or **AWS Lambda**

---

### 🔗 **Contributing**
We welcome contributions! Feel free to submit PRs or report issues.

👨‍💻 **Developed by:** DANIEL SUCCESSFUL
📧 **Contact:** juliusdaniel554@gmail.com
📂 **GitHub:** [(https://github.com/BISHOPDAN)]

---
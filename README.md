# 📚 BookVerse — Backend API

> RESTful API cho nền tảng **BookVerse** — hệ thống quản lý và khám phá sách trực tuyến, xây dựng trên kiến trúc Clean Architecture.

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![EF Core](https://img.shields.io/badge/EF%20Core-10.0-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://learn.microsoft.com/en-us/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/sql-server)
[![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?style=flat-square&logo=swagger&logoColor=black)](https://swagger.io/)
[![License](https://img.shields.io/badge/License-MIT-green?style=flat-square)](LICENSE)

---

## 🏗️ Kiến trúc dự án (Clean Architecture)

```
BookVerse.Server/
├── BookVerse.API/               # Presentation Layer
│   ├── Controllers/             # API Controllers
│   ├── Configurations/          # Cấu hình ứng dụng (Cloudinary, ...)
│   ├── Extensions/              # Extension methods
│   ├── Models/                  # Request/Response models riêng API
│   ├── Services/                # Services riêng API (Cloudinary, ...)
│   ├── Properties/              # Launch settings
│   ├── Program.cs               # Entry point
│   ├── appsettings.json         # Cấu hình chung
│   └── appsettings.Development.json
│
├── BookVerse.Application/       # Application Layer
│   ├── DTOs/                    # Data Transfer Objects
│   │   ├── Books/               # Book DTOs
│   │   ├── Reviews/             # Review DTOs
│   │   └── Images/              # Image DTOs
│   ├── Features/                # Use cases / Business logic
│   ├── Interfaces/              # Abstractions / Contracts
│   ├── Models/                  # Application models (ApiResult, ...)
│   └── Services/                # Application services
│
├── BookVerse.Domain/            # Domain Layer
│   └── Entities/                # Domain entities
│       ├── Book.cs
│       ├── Review.cs
│       └── BookImage.cs
│
├── BookVerse.Infrastructure/    # Infrastructure Layer
│   ├── Dbcontext/               # EF Core DbContext
│   ├── Configurations/          # Entity configurations (Fluent API)
│   ├── Migrations/              # EF Core Migrations
│   └── Repositories/            # Repository implementations
│
└── BookVerse.slnx               # Solution file
```

### Luồng phụ thuộc

```
API  →  Application  →  Domain
 ↓
Infrastructure  →  Application  →  Domain
```

> **Domain** không phụ thuộc vào bất kỳ layer nào — đây là nguyên tắc cốt lõi của Clean Architecture.

## ⚙️ Công nghệ sử dụng

| Công nghệ                 | Phiên bản | Mô tả                              |
| ------------------------- | --------- | ----------------------------------- |
| **.NET**                   | 10.0      | Framework chính                     |
| **Entity Framework Core** | 10.0.8    | ORM — truy vấn & quản lý database  |
| **SQL Server**             | —         | Cơ sở dữ liệu quan hệ             |
| **Cloudinary**             | 1.29.1    | Dịch vụ lưu trữ & quản lý ảnh     |
| **Swagger / OpenAPI**      | 10.1.7    | Tài liệu hóa API tự động           |
| **Npgsql (PostgreSQL)**    | 10.0.1    | Hỗ trợ thêm PostgreSQL             |

## 📦 Domain Entities

### Book

| Thuộc tính   | Kiểu dữ liệu          | Mô tả              |
| ------------ | ---------------------- | ------------------- |
| `Id`         | `Guid`                 | Khóa chính          |
| `Title`      | `string`               | Tên sách            |
| `Author`     | `string`               | Tác giả             |
| `Year`       | `int`                  | Năm xuất bản        |
| `Content`    | `string`               | Nội dung / mô tả   |
| `CreatedAt`  | `DateTime`             | Ngày tạo (UTC)      |
| `Reviews`    | `ICollection<Review>`  | Danh sách đánh giá  |
| `Images`     | `ICollection<BookImage>` | Danh sách ảnh     |

### Review

| Thuộc tính       | Kiểu dữ liệu | Mô tả                |
| ---------------- | ------------- | --------------------- |
| `Id`             | `Guid`        | Khóa chính            |
| `ReviewerName`   | `string`      | Tên người đánh giá    |
| `ReviewContent`  | `string`      | Nội dung đánh giá     |
| `Rating`         | `int`         | Điểm đánh giá         |
| `CreatedAt`      | `DateTime`    | Ngày tạo (UTC)        |
| `BookId`         | `Guid`        | FK → Book             |

### BookImage

| Thuộc tính  | Kiểu dữ liệu | Mô tả                     |
| ----------- | ------------- | -------------------------- |
| `Id`        | `Guid`        | Khóa chính                 |
| `ImageUrl`  | `string`      | URL ảnh trên Cloudinary    |
| `PublicId`  | `string`      | Public ID trên Cloudinary  |
| `BookId`    | `Guid`        | FK → Book                  |

## 🔌 API Endpoints

### Books — `api/books`

| Method   | Endpoint          | Mô tả                         |
| -------- | ----------------- | ------------------------------ |
| `GET`    | `/api/books`      | Lấy danh sách tất cả sách     |
| `GET`    | `/api/books/{id}` | Lấy chi tiết sách (kèm reviews & images) |
| `POST`   | `/api/books`      | Tạo sách mới                  |
| `PUT`    | `/api/books/{id}` | Cập nhật thông tin sách        |
| `DELETE` | `/api/books/{id}` | Xóa sách                      |

### Reviews — `api/books/{bookId}/reviews`

| Method | Endpoint                          | Mô tả                        |
| ------ | --------------------------------- | ----------------------------- |
| `GET`  | `/api/books/{bookId}/reviews`     | Lấy danh sách đánh giá       |
| `POST` | `/api/books/{bookId}/reviews`     | Tạo đánh giá mới             |

### Images — `api/books/{bookId}/images`

| Method   | Endpoint                                  | Mô tả                        |
| -------- | ----------------------------------------- | ----------------------------- |
| `POST`   | `/api/books/{bookId}/images`              | Upload ảnh (max 10MB)         |
| `DELETE` | `/api/books/{bookId}/images/{imageId}`    | Xóa ảnh                      |

## 🔗 Frontend Client

Backend phục vụ cho frontend **BookVerse** (Angular):

| Môi trường  | URL                             |
| ----------- | ------------------------------- |
| HTTP        | `http://localhost:5034`         |
| HTTPS       | `https://localhost:7122`        |

> 📖 Frontend repo: [bookverse-clien](../bookverse-clien/)

## 🚀 Bắt đầu

### Yêu cầu hệ thống

- **.NET SDK** >= 10.0
- **SQL Server** (LocalDB hoặc full instance)
- **Cloudinary account** (để quản lý ảnh)

### Cài đặt

```bash
# Clone repository
git clone https://github.com/Truc5302/bookverse-server.git
cd bookverse-server
```

### Cấu hình

Chỉnh sửa `BookVerse.API/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BookVerseDb;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "CloudinarySettings": {
    "CloudName": "your-cloud-name",
    "ApiKey": "your-api-key",
    "ApiSecret": "your-api-secret"
  }
}
```

### Khởi tạo Database

```bash
# Áp dụng migrations
dotnet ef database update --project BookVerse.Infrastructure --startup-project BookVerse.API
```

### Chạy development server

```bash
dotnet run --project BookVerse.API
```

API sẽ khởi chạy tại:
- 👉 HTTP: [http://localhost:5034](http://localhost:5034)
- 👉 HTTPS: [https://localhost:7122](https://localhost:7122)

### Swagger UI

Truy cập tài liệu API tương tác tại:

👉 [http://localhost:5034/swagger](http://localhost:5034/swagger)

> Swagger UI chỉ khả dụng trong môi trường **Development**.

## 🧪 Testing

```bash
# Chạy tất cả unit tests
dotnet test
```

## 📁 Cấu hình CORS

Frontend Angular được cho phép truy cập API thông qua CORS policy:

```csharp
// Program.cs
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
```

> Cập nhật `WithOrigins(...)` nếu frontend chạy trên port khác.

## 🤝 Đóng góp

1. Fork repository
2. Tạo branch mới (`git checkout -b feature/ten-tinh-nang`)
3. Commit thay đổi (`git commit -m "feat: mô tả thay đổi"`)
4. Push lên branch (`git push origin feature/ten-tinh-nang`)
5. Tạo Pull Request

## 📄 License

Dự án được phân phối dưới giấy phép [MIT](LICENSE).

---

<p align="center">
  Made with ❤️ by <strong>BookVerse Team</strong>
</p>

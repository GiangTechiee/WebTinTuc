# Trang thông tin điện tử cho hiệp hội nghề nghiệp Vietnam codeCore

## Installation

Clone project.

```
git clone https://github.com/GiangTechiee/WebTinTuc.git
```

## Usage
- Mở terminal và trỏ đến thư mục project, chạy mã dưới để sinh databse bằng EF Core
```
dotnet ef migrations add CreateDB
dotnet ef database update
```
- Chạy project
```
dotnet run
```

Note: Hãy thêm tên quyền vào bảng Roles lần lượt là "Admin" và "User"

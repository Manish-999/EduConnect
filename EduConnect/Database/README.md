# Database Setup Instructions

## PostgreSQL Database Setup

### 1. Create Database
```sql
CREATE DATABASE academiceye;
```

### 2. Run Schema Script
Execute the `Schema.sql` file to create all tables:

```bash
psql -U postgres -d academiceye -f Schema.sql
```

Or using pgAdmin:
1. Connect to PostgreSQL server
2. Right-click on `academiceye` database
3. Select "Query Tool"
4. Open and execute `Schema.sql`

### 3. Verify Tables
```sql
\dt
```

You should see:
- schools
- teachers
- students

## Connection String Configuration

The connection string is configured in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "PostgreSQL": "Host=localhost;Port=5432;Database=academiceye;Username=postgres;Password=password;Pooling=true;MinPoolSize=5;MaxPoolSize=20;Connection Lifetime=300;Command Timeout=30;"
  }
}
```

## Example API Endpoints

### Get All Students (with SchoolId filtering)
**Endpoint**: `POST /tc/GetAllStudent`

**Request Body**:
```json
{
  "SchoolId": 1
}
```

**Response**:
```json
[
  {
    "id": 1,
    "schoolId": 1,
    "firstName": "John",
    "lastName": "Doe",
    "classApplyingFor": "Class 10th",
    ...
  }
]
```

### Save Student
**Endpoint**: `POST /tc/SaveStudent`

**Request**: `multipart/form-data`

**Form Fields**:
- All student fields (FirstName, LastName, etc.)
- SchoolId (optional, extracted from FormData if not provided)
- File uploads: Photo, BirthCertificate, etc.

**Response**:
```json
true
```

### Get All Teachers (with SchoolId filtering)
**Endpoint**: `POST /tc/GetAllTeacher`

**Request Body**:
```json
{
  "SchoolId": 1
}
```

**Response**:
```json
[
  {
    "id": 1,
    "schoolId": 1,
    "firstName": "Jane",
    "lastName": "Smith",
    "employeeId": "EMP001",
    ...
  }
]
```

### Save Teacher
**Endpoint**: `POST /tc/SaveTeacher`

**Request**: `multipart/form-data`

**Form Fields**:
- All teacher fields
- SchoolId (optional, extracted from FormData if not provided)
- File uploads: Photo, Resume, AadharCard, Certificates

**Response**:
```json
{
  "success": true,
  "teacher": {
    "id": 1,
    ...
  }
}
```

## File Storage

Uploaded files are stored in:
- `wwwroot/uploads/schools/` - School logos and certificates
- `wwwroot/uploads/teachers/` - Teacher documents
- `wwwroot/uploads/students/` - Student documents

File paths are stored in the database, and files are served via static file middleware.

## Connection Pooling

The application uses `NpgsqlDataSource` for connection pooling:
- Min Pool Size: 5 connections
- Max Pool Size: 20 connections
- Connection Lifetime: 300 seconds

This ensures efficient database connection management and better performance under load.


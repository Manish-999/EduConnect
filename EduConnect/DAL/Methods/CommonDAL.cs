using DAL.Interfaces;
using Dapper;
using Model;
using Npgsql;

namespace DAL.Methods
{
    public class CommonDAL : ICommonDAL
    {
        private readonly NpgsqlDataSource _dataSource;

        public CommonDAL(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));
        }

        public async Task<IEnumerable<T>> GetAllDataFromDbByFunNameAndClientName<T>(string funName, int projectId)
        {
            string query = $"SELECT * FROM {funName}(@projectId)";
            try
            {
                await using var connection = await _dataSource.OpenConnectionAsync();
                var projectWiseParameters = await connection.QueryAsync<T>(query, new { projectId });
                return projectWiseParameters;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing function {funName}: {ex.Message}");
                return Enumerable.Empty<T>();
            }
        }

        public async Task<bool> SaveSchool(SchoolRegistrationRequest request)
        {
            try
            {
                await using var connection = await _dataSource.OpenConnectionAsync();

                // Handle file uploads
                var schoolLogoPath = await DAL.Helpers.FileHelper.SaveFileAsync(request.SchoolLogo, "schools/logos");
                var affiliationCertPath = await DAL.Helpers.FileHelper.SaveFileAsync(request.AffiliationCertificate, "schools/certificates");

                // Hash password (in production, use proper password hashing like BCrypt)
                var passwordHash = request.Password; // TODO: Implement proper password hashing

                var sql = @"
                    INSERT INTO schools (
                        school_name, school_type, affiliation_number, school_code, medium_of_instruction,
                        total_students, total_teachers, academic_year_start, address_line, city, district,
                        state, pin_code, country, principal_name, designation, mobile, email, aadhar,
                        password_hash, terms_accepted, school_logo_path, affiliation_certificate_path
                    ) VALUES (
                        @SchoolName, @SchoolType, @AffiliationNumber, @SchoolCode, @MediumOfInstruction,
                        @TotalStudents, @TotalTeachers, @AcademicYearStart, @AddressLine, @City, @District,
                        @State, @PinCode, @Country, @PrincipalName, @Designation, @Mobile, @Email, @Aadhar,
                        @PasswordHash, @TermsAccepted, @SchoolLogoPath, @AffiliationCertificatePath
                    ) RETURNING id";

                var parameters = new
                {
                    request.SchoolName,
                    request.SchoolType,
                    request.AffiliationNumber,
                    request.SchoolCode,
                    request.MediumOfInstruction,
                    request.TotalStudents,
                    request.TotalTeachers,
                    request.AcademicYearStart,
                    request.AddressLine,
                    request.City,
                    request.District,
                    request.State,
                    request.PinCode,
                    request.Country,
                    request.PrincipalName,
                    request.Designation,
                    request.Mobile,
                    request.Email,
                    request.Aadhar,
                    PasswordHash = passwordHash,
                    request.TermsAccepted,
                    SchoolLogoPath = schoolLogoPath,
                    AffiliationCertificatePath = affiliationCertPath
                };

                await connection.ExecuteAsync(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving school: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SaveTeacher(TeacherRegistrationRequest request)
        {
            try
            {
                await using var connection = await _dataSource.OpenConnectionAsync();

                // Handle file uploads
                var photoPath = await DAL.Helpers.FileHelper.SaveFileAsync(request.Photo, "teachers/photos");
                var resumePath = await DAL.Helpers.FileHelper.SaveFileAsync(request.Resume, "teachers/resumes");
                var aadharCardPath = await DAL.Helpers.FileHelper.SaveFileAsync(request.AadharCard, "teachers/documents");
                var certificatesPath = await DAL.Helpers.FileHelper.SaveFileAsync(request.Certificates, "teachers/certificates");

                var sql = @"
                    INSERT INTO teachers (
                        school_id, employee_id, first_name, middle_name, last_name, date_of_birth, gender,
                        aadhar_number, nationality, blood_group, marital_status, country, email, mobile_number,
                        alternate_mobile, permanent_address, current_address, city, district, state, pin_code,
                        designation, department, subjects, qualification, specialization, experience, joining_date,
                        employment_type, highest_qualification, university, year_of_passing, percentage,
                        additional_certifications, previous_school, previous_designation, previous_experience,
                        reason_for_leaving, basic_salary, allowances, bank_name, account_number, ifsc_code,
                        pan_number, emergency_contact_name, emergency_relation, emergency_mobile, emergency_address,
                        terms_accepted, photo_path, resume_path, aadhar_card_path, certificates_path
                    ) VALUES (
                        @SchoolId, @EmployeeId, @FirstName, @MiddleName, @LastName, @DateOfBirth, @Gender,
                        @AadharNumber, @Nationality, @BloodGroup, @MaritalStatus, @Country, @Email, @MobileNumber,
                        @AlternateMobile, @PermanentAddress, @CurrentAddress, @City, @District, @State, @PinCode,
                        @Designation, @Department, @Subjects, @Qualification, @Specialization, @Experience, @JoiningDate,
                        @EmploymentType, @HighestQualification, @University, @YearOfPassing, @Percentage,
                        @AdditionalCertifications, @PreviousSchool, @PreviousDesignation, @PreviousExperience,
                        @ReasonForLeaving, @BasicSalary, @Allowances, @BankName, @AccountNumber, @IfscCode,
                        @PanNumber, @EmergencyContactName, @EmergencyRelation, @EmergencyMobile, @EmergencyAddress,
                        @TermsAccepted, @PhotoPath, @ResumePath, @AadharCardPath, @CertificatesPath
                    ) RETURNING id";

                var parameters = new
                {
                    request.SchoolId,
                    request.EmployeeId,
                    request.FirstName,
                    request.MiddleName,
                    request.LastName,
                    request.DateOfBirth,
                    request.Gender,
                    request.AadharNumber,
                    request.Nationality,
                    request.BloodGroup,
                    request.MaritalStatus,
                    request.Country,
                    request.Email,
                    request.MobileNumber,
                    request.AlternateMobile,
                    request.PermanentAddress,
                    request.CurrentAddress,
                    request.City,
                    request.District,
                    request.State,
                    request.PinCode,
                    request.Designation,
                    request.Department,
                    request.Subjects,
                    request.Qualification,
                    request.Specialization,
                    request.Experience,
                    request.JoiningDate,
                    request.EmploymentType,
                    request.HighestQualification,
                    request.University,
                    request.YearOfPassing,
                    request.Percentage,
                    request.AdditionalCertifications,
                    request.PreviousSchool,
                    request.PreviousDesignation,
                    request.PreviousExperience,
                    request.ReasonForLeaving,
                    request.BasicSalary,
                    request.Allowances,
                    request.BankName,
                    request.AccountNumber,
                    request.IfscCode,
                    request.PanNumber,
                    request.EmergencyContactName,
                    request.EmergencyRelation,
                    request.EmergencyMobile,
                    request.EmergencyAddress,
                    request.TermsAccepted,
                    PhotoPath = photoPath,
                    ResumePath = resumePath,
                    AadharCardPath = aadharCardPath,
                    CertificatesPath = certificatesPath
                };

                var id = await connection.QuerySingleAsync<int>(sql, parameters);
                request.Id = id; // Update the request object with the generated ID
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving teacher: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SaveStudent(StudentRegistrationRequest request)
        {
            try
            {
                await using var connection = await _dataSource.OpenConnectionAsync();

                // Handle file uploads
                var photoPath = await DAL.Helpers.FileHelper.SaveFileAsync(request.Photo, "students/photos");
                var birthCertPath = await DAL.Helpers.FileHelper.SaveFileAsync(request.BirthCertificate, "students/documents");
                var studentAadharPath = await DAL.Helpers.FileHelper.SaveFileAsync(request.StudentAadhar, "students/documents");
                var parentAadharDocPath = await DAL.Helpers.FileHelper.SaveFileAsync(request.ParentAadharDoc, "students/documents");
                var reportCardPath = await DAL.Helpers.FileHelper.SaveFileAsync(request.ReportCard, "students/documents");
                var transferCertPath = await DAL.Helpers.FileHelper.SaveFileAsync(request.TransferCertificate, "students/documents");
                var casteCertPath = await DAL.Helpers.FileHelper.SaveFileAsync(request.CasteCertificate, "students/documents");
                var incomeCertPath = await DAL.Helpers.FileHelper.SaveFileAsync(request.IncomeCertificate, "students/documents");

                var sql = @"
                    INSERT INTO students (
                        school_id, first_name, middle_name, last_name, date_of_birth, gender, aadhar_number,
                        category, religion, nationality, blood_group, mother_tongue, previous_school_name,
                        class_applying_for, medium_of_instruction, father_name, mother_name, guardian_name,
                        occupation, educational_qualification, annual_income, father_mobile, mother_mobile,
                        parent_email, parent_aadhar, country, permanent_address, current_address, city,
                        district, state, pin_code, previous_class_passed, previous_school, board, marks_obtained,
                        tc_number, migration_certificate, special_needs, special_needs_detail, emergency_contact,
                        sibling_in_school, transport_required, hostel_required, parent_signature,
                        photo_path, birth_certificate_path, student_aadhar_path, parent_aadhar_doc_path,
                        report_card_path, transfer_certificate_path, caste_certificate_path, income_certificate_path
                    ) VALUES (
                        @SchoolId, @FirstName, @MiddleName, @LastName, @DateOfBirth, @Gender, @AadharNumber,
                        @Category, @Religion, @Nationality, @BloodGroup, @MotherTongue, @PreviousSchoolName,
                        @ClassApplyingFor, @MediumOfInstruction, @FatherName, @MotherName, @GuardianName,
                        @Occupation, @EducationalQualification, @AnnualIncome, @FatherMobile, @MotherMobile,
                        @ParentEmail, @ParentAadhar, @Country, @PermanentAddress, @CurrentAddress, @City,
                        @District, @State, @PinCode, @PreviousClassPassed, @PreviousSchool, @Board, @MarksObtained,
                        @TcNumber, @MigrationCertificate, @SpecialNeeds, @SpecialNeedsDetail, @EmergencyContact,
                        @SiblingInSchool, @TransportRequired, @HostelRequired, @ParentSignature,
                        @PhotoPath, @BirthCertificatePath, @StudentAadharPath, @ParentAadharDocPath,
                        @ReportCardPath, @TransferCertificatePath, @CasteCertificatePath, @IncomeCertificatePath
                    ) RETURNING id";

                var parameters = new
                {
                    request.SchoolId,
                    request.FirstName,
                    request.MiddleName,
                    request.LastName,
                    request.DateOfBirth,
                    request.Gender,
                    request.AadharNumber,
                    request.Category,
                    request.Religion,
                    request.Nationality,
                    request.BloodGroup,
                    request.MotherTongue,
                    request.PreviousSchoolName,
                    request.ClassApplyingFor,
                    request.MediumOfInstruction,
                    request.FatherName,
                    request.MotherName,
                    request.GuardianName,
                    request.Occupation,
                    request.EducationalQualification,
                    request.AnnualIncome,
                    request.FatherMobile,
                    request.MotherMobile,
                    request.ParentEmail,
                    request.ParentAadhar,
                    request.Country,
                    request.PermanentAddress,
                    request.CurrentAddress,
                    request.City,
                    request.District,
                    request.State,
                    request.PinCode,
                    request.PreviousClassPassed,
                    request.PreviousSchool,
                    request.Board,
                    request.MarksObtained,
                    request.TcNumber,
                    request.MigrationCertificate,
                    request.SpecialNeeds,
                    request.SpecialNeedsDetail,
                    request.EmergencyContact,
                    request.SiblingInSchool,
                    request.TransportRequired,
                    request.HostelRequired,
                    request.ParentSignature,
                    PhotoPath = photoPath,
                    BirthCertificatePath = birthCertPath,
                    StudentAadharPath = studentAadharPath,
                    ParentAadharDocPath = parentAadharDocPath,
                    ReportCardPath = reportCardPath,
                    TransferCertificatePath = transferCertPath,
                    CasteCertificatePath = casteCertPath,
                    IncomeCertificatePath = incomeCertPath
                };

                await connection.ExecuteAsync(sql, parameters);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving student: {ex.Message}");
                return false;
            }
        }

        public async Task<List<SchoolRegistrationRequest>> GetAllSchool()
        {
            try
            {
                await using var connection = await _dataSource.OpenConnectionAsync();
                var sql = @"
                    SELECT 
                        id, school_name AS SchoolName, school_type AS SchoolType, affiliation_number AS AffiliationNumber,
                        school_code AS SchoolCode, medium_of_instruction AS MediumOfInstruction, total_students AS TotalStudents,
                        total_teachers AS TotalTeachers, academic_year_start AS AcademicYearStart, address_line AS AddressLine,
                        city AS City, district AS District, state AS State, pin_code AS PinCode, country AS Country,
                        principal_name AS PrincipalName, designation AS Designation, mobile AS Mobile, email AS Email,
                        aadhar AS Aadhar, terms_accepted AS TermsAccepted, school_logo_path AS SchoolLogoPath,
                        affiliation_certificate_path AS AffiliationCertificatePath, created_at AS CreatedAt, updated_at AS UpdatedAt
                    FROM schools
                    ORDER BY created_at DESC";

                var schools = await connection.QueryAsync<SchoolRegistrationRequest>(sql);
                return schools.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all schools: {ex.Message}");
                return new List<SchoolRegistrationRequest>();
            }
        }

        public async Task<List<TeacherRegistrationRequest>> GetAllTeacher()
        {
            try
            {
                await using var connection = await _dataSource.OpenConnectionAsync();
                var sql = @"
                    SELECT 
                        id AS Id, school_id AS SchoolId, employee_id AS EmployeeId, first_name AS FirstName,
                        middle_name AS MiddleName, last_name AS LastName, date_of_birth AS DateOfBirth, gender AS Gender,
                        aadhar_number AS AadharNumber, nationality AS Nationality, blood_group AS BloodGroup,
                        marital_status AS MaritalStatus, country AS Country, email AS Email, mobile_number AS MobileNumber,
                        alternate_mobile AS AlternateMobile, permanent_address AS PermanentAddress, current_address AS CurrentAddress,
                        city AS City, district AS District, state AS State, pin_code AS PinCode, designation AS Designation,
                        department AS Department, subjects AS Subjects, qualification AS Qualification, specialization AS Specialization,
                        experience AS Experience, joining_date AS JoiningDate, employment_type AS EmploymentType,
                        highest_qualification AS HighestQualification, university AS University, year_of_passing AS YearOfPassing,
                        percentage AS Percentage, additional_certifications AS AdditionalCertifications, previous_school AS PreviousSchool,
                        previous_designation AS PreviousDesignation, previous_experience AS PreviousExperience,
                        reason_for_leaving AS ReasonForLeaving, basic_salary AS BasicSalary, allowances AS Allowances,
                        bank_name AS BankName, account_number AS AccountNumber, ifsc_code AS IfscCode, pan_number AS PanNumber,
                        emergency_contact_name AS EmergencyContactName, emergency_relation AS EmergencyRelation,
                        emergency_mobile AS EmergencyMobile, emergency_address AS EmergencyAddress, terms_accepted AS TermsAccepted,
                        photo_path AS PhotoPath, resume_path AS ResumePath, aadhar_card_path AS AadharCardPath,
                        certificates_path AS CertificatesPath, created_at AS CreatedAt, updated_at AS UpdatedAt
                    FROM teachers
                    ORDER BY created_at DESC";

                var teachers = await connection.QueryAsync<TeacherRegistrationRequest>(sql);
                return teachers.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all teachers: {ex.Message}");
                return new List<TeacherRegistrationRequest>();
            }
        }

        public async Task<List<StudentRegistrationRequest>> GetAllStudent()
        {
            try
            {
                await using var connection = await _dataSource.OpenConnectionAsync();
                var sql = @"
                    SELECT 
                        id AS Id, school_id AS SchoolId, first_name AS FirstName, middle_name AS MiddleName,
                        last_name AS LastName, date_of_birth AS DateOfBirth, gender AS Gender, aadhar_number AS AadharNumber,
                        category AS Category, religion AS Religion, nationality AS Nationality, blood_group AS BloodGroup,
                        mother_tongue AS MotherTongue, previous_school_name AS PreviousSchoolName, class_applying_for AS ClassApplyingFor,
                        medium_of_instruction AS MediumOfInstruction, father_name AS FatherName, mother_name AS MotherName,
                        guardian_name AS GuardianName, occupation AS Occupation, educational_qualification AS EducationalQualification,
                        annual_income AS AnnualIncome, father_mobile AS FatherMobile, mother_mobile AS MotherMobile,
                        parent_email AS ParentEmail, parent_aadhar AS ParentAadhar, country AS Country,
                        permanent_address AS PermanentAddress, current_address AS CurrentAddress, city AS City,
                        district AS District, state AS State, pin_code AS PinCode, previous_class_passed AS PreviousClassPassed,
                        previous_school AS PreviousSchool, board AS Board, marks_obtained AS MarksObtained,
                        tc_number AS TcNumber, migration_certificate AS MigrationCertificate, special_needs AS SpecialNeeds,
                        special_needs_detail AS SpecialNeedsDetail, emergency_contact AS EmergencyContact,
                        sibling_in_school AS SiblingInSchool, transport_required AS TransportRequired,
                        hostel_required AS HostelRequired, parent_signature AS ParentSignature,
                        photo_path AS PhotoPath, birth_certificate_path AS BirthCertificatePath,
                        student_aadhar_path AS StudentAadharPath, parent_aadhar_doc_path AS ParentAadharDocPath,
                        report_card_path AS ReportCardPath, transfer_certificate_path AS TransferCertificatePath,
                        caste_certificate_path AS CasteCertificatePath, income_certificate_path AS IncomeCertificatePath,
                        created_at AS CreatedAt, updated_at AS UpdatedAt
                    FROM students
                    ORDER BY created_at DESC";

                var students = await connection.QueryAsync<StudentRegistrationRequest>(sql);
                return students.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all students: {ex.Message}");
                return new List<StudentRegistrationRequest>();
            }
        }
    }
}

using Model;

namespace Services.Interfaces
{
    public interface ISchoolService
    {
        Task<bool> SaveSchool(SchoolRegistrationRequest request);
        Task<bool> SaveTeacher(TeacherRegistrationRequest request);
        Task<bool> SaveStudent(StudentRegistrationRequest request);
        Task<List<SchoolRegistrationRequest>> GetAllSchool();
        Task<List<TeacherRegistrationRequest>> GetAllTeacher();
        Task<List<StudentRegistrationRequest>> GetAllStudent();
    }
}

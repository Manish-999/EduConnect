using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL.Interfaces
{
    public interface ICommonDAL
    {
        Task<IEnumerable<T>> GetAllDataFromDbByFunNameAndClientName<T>(string funName, int projectId);
        Task<bool> SaveSchool(SchoolRegistrationRequest request);
        Task<bool> SaveTeacher(TeacherRegistrationRequest request);
        Task<bool> SaveStudent(StudentRegistrationRequest request);
        Task<List<SchoolRegistrationRequest>> GetAllSchool();
        Task<List<TeacherRegistrationRequest>> GetAllTeacher();
        Task<List<StudentRegistrationRequest>> GetAllStudent();
    }
}

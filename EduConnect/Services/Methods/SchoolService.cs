using DAL.Interfaces;
using Model;
using Services.Interfaces;

namespace Services.Methods
{
    public class SchoolService: ISchoolService
    {
        public ICommonDAL _commonDal;
        public SchoolService(ICommonDAL commonDal) {
            _commonDal = commonDal;
        }

        public async Task<bool> SaveSchool(SchoolRegistrationRequest request)
        {
            await _commonDal.SaveSchool(request);
            return true;
        }
        public async Task<bool> SaveTeacher(TeacherRegistrationRequest request)
        {
            await _commonDal.SaveTeacher(request);
            return true;
        }
        public async Task<bool> SaveStudent(StudentRegistrationRequest request)
        {
            await _commonDal.SaveStudent(request);
            return true;
        }
        public async Task<List<SchoolRegistrationRequest>> GetAllSchool()
        {
            return await _commonDal.GetAllSchool();
        }
        public async Task<List<TeacherRegistrationRequest>> GetAllTeacher()
        {
            return await _commonDal.GetAllTeacher();
        }
        public async Task<List<StudentRegistrationRequest>> GetAllStudent()
        {
            return await _commonDal.GetAllStudent();
        }
    }
}

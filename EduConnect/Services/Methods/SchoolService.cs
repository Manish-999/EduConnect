using DAL.Interfaces;
using Model;
using Model.DTOs;
using Services.Interfaces;

namespace Services.Methods
{
    public class SchoolService: ISchoolService
    {
        public ICommonDAL _commonDal;
        public SchoolService(ICommonDAL commonDal) {
            _commonDal = commonDal;
        }

        //public async Task<bool> SaveSchool(SchoolCreateDto request)
        //{
        //    await _commonDal.SaveSchool(request);
        //    return true;
        //}
        //public async Task<bool> SaveTeacher(TeacherCreateDto request)
        //{
        //    await _commonDal.SaveTeacher(request);
        //    return true;
        //}
        //public async Task<bool> SaveStudent(StudentCreateDto request)
        //{
        //    await _commonDal.SaveStudent(request);
        //    return true;
        //}
        //public async Task<List<SchoolCreateDto>> GetAllSchool()
        //{
        //    return await _commonDal.GetAllSchool();
        //}
        //public async Task<List<TeacherCreateDto>> GetAllTeacher()
        //{
        //    return await _commonDal.GetAllTeacher();
        //}
        //public async Task<List<StudentCreateDto>> GetAllStudent()
        //{
        //    return await _commonDal.GetAllStudent();
        //}
    }
}

using DAL.Interfaces;
using Dapper;
using Model;
using Npgsql;

namespace DAL.Methods
{
    public class CommonDAL: ICommonDAL
    {
        private readonly List<SchoolRegistrationRequest> _schoolList = new();
        private readonly List<TeacherRegistrationRequest> _teacherList = new();
        private readonly List<StudentRegistrationRequest> _studentList = new();
        private int _nextTeacherId = 1; // Auto-increment teacher ID
        private readonly object _lockObject = new object(); // Thread safety lock
        public CommonDAL()
        {

        }
        public async Task<IEnumerable<T>> GetAllDataFromDbByFunNameAndClientName<T>(string funName, int projectId)
        {
            string query = $"SELECT * FROM {funName}('{projectId}')";
            try
            {
                using (var connection = new NpgsqlConnection(""))
                {
                    connection.Open();
                    var projectWiseParameters = await connection.QueryAsync<T>(query, null);

                    return projectWiseParameters;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        public async Task<bool> SaveSchool(SchoolRegistrationRequest request)
        {
            _schoolList.Add(request);
            return true;
        }
        public async Task<bool> SaveTeacher(TeacherRegistrationRequest request)
        {
            lock (_lockObject)
            {
                // Assign unique ID if not already set
                if (request.Id == 0)
                {
                    request.Id = _nextTeacherId++;
                }
                _teacherList.Add(request);
            }
            return true;
        }
        public async Task<bool> SaveStudent(StudentRegistrationRequest request)
        {
            _studentList.Add(request);
            return true;
        }
        public async Task<List<SchoolRegistrationRequest>> GetAllSchool()
        {
            return _schoolList;
        }
        public async Task<List<TeacherRegistrationRequest>> GetAllTeacher()
        {
            lock (_lockObject)
            {
                // Ensure we always return a valid list, never null
                var result = _teacherList ?? new List<TeacherRegistrationRequest>();
                return new List<TeacherRegistrationRequest>(result);
            }
        }
        public async Task<List<StudentRegistrationRequest>> GetAllStudent()
        {
            return _studentList;
        }
    }
}

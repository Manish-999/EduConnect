using DAL.Interfaces;
using Model;
using Model.DTOs;
using Services.Interfaces;

namespace Services.Methods
{
    public class InMemorySchoolStore : IInMemorySchoolStore
    {
        //private readonly List<ClassDto> _classes = new();
        private readonly List<SubjectDto> _subjects = new();
        private readonly List<TeacherDto> _teachers = new();
        private readonly List<ClassSubjectDto> _classSubjects = new();

        private int _nextClassId = 1;
        private int _nextTeacherId = 1;
        private int _nextSubjectId = 4; // Start from 4 since we have 3 pre-seeded subjects

        private readonly object _lockObject = new();
        private readonly ICommonDAL _commonDAL;

        public InMemorySchoolStore(ICommonDAL commonDAL)
        {
            _commonDAL = commonDAL;
            //InitializeData();
        }

        //private void InitializeData()
        //{
        //    lock (_lockObject)
        //    {
        //        // Pre-seed Subjects
        //        _subjects.Add(new SubjectDto { Id = 1, Name = "Maths" });
        //        _subjects.Add(new SubjectDto { Id = 2, Name = "English" });
        //        _subjects.Add(new SubjectDto { Id = 3, Name = "Science" });

        //        // Do NOT pre-seed teachers - only registered teachers should appear
        //        // Teachers will be added via the teacher registration system (SaveTeacher API)
        //        _nextTeacherId = 1; // Start from 1 for registered teachers
        //    }
        //}

        //// Subjects
        //public SubjectDto AddSubject(SubjectDto subjectDto)
        //{
        //    lock (_lockObject)
        //    {
        //        subjectDto.Id = _nextSubjectId++;
        //        _subjects.Add(subjectDto);
        //        return subjectDto;
        //    }
        //}

        //// Classes
        //public ClassDto AddClass(ClassDto classDto)
        //{
        //    lock (_lockObject)
        //    {
        //        classDto.Id = _nextClassId++;
        //        _classes.Add(classDto);
        //        return classDto;
        //    }
        //}

        //public List<ClassDto> GetAllClasses()
        //{
        //    lock (_lockObject)
        //    {
        //        return new List<ClassDto>(_classes);
        //    }
        //}

        //public ClassDto? GetClassById(int id)
        //{
        //    lock (_lockObject)
        //    {
        //        return _classes.FirstOrDefault(c => c.Id == id);
        //    }
        //}

        //// Subjects
        //public List<SubjectDto> GetAllSubjects()
        //{
        //    lock (_lockObject)
        //    {
        //        return new List<SubjectDto>(_subjects);
        //    }
        //}

        //public SubjectDto? GetSubjectById(int id)
        //{
        //    lock (_lockObject)
        //    {
        //        return _subjects.FirstOrDefault(s => s.Id == id);
        //    }
        //}

        //// Teachers
        //public TeacherDto AddTeacher(TeacherDto teacherDto)
        //{
        //    lock (_lockObject)
        //    {
        //        teacherDto.Id = _nextTeacherId++;
        //        _teachers.Add(teacherDto);
        //        return teacherDto;
        //    }
        //}

        //public List<TeacherDto> GetAllTeachers()
        //{
        //    lock (_lockObject)
        //    {
        //        return new List<TeacherDto>(_teachers);
        //    }
        //}

        //public TeacherDto? GetTeacherById(int id)
        //{
        //    lock (_lockObject)
        //    {
        //        // First check local _teachers list
        //        var localTeacher = _teachers.FirstOrDefault(t => t.Id == id);
        //        if (localTeacher != null)
        //        {
        //            return localTeacher;
        //        }

        //        // If not found locally, check registered teachers from CommonDAL
        //        var registeredTeachers = _commonDAL.GetAllTeacher().Result;
        //        var registeredTeacher = registeredTeachers.FirstOrDefault(t => t.Id == id);
        //        if (registeredTeacher != null)
        //        {
        //            // Convert TeacherRegistrationRequest to TeacherDto
        //            return new TeacherDto
        //            {
        //                Id = registeredTeacher.Id,
        //                Name = $"{registeredTeacher.FirstName} {registeredTeacher.LastName}".Trim()
        //            };
        //        }

        //        return null;
        //    }
        //}

        //// ClassSubjects
        //public List<ClassSubjectResponseDto> GetClassSubjects(int classId)
        //{
        //    lock (_lockObject)
        //    {
        //        var classSubjectMappings = _classSubjects
        //            .Where(cs => cs.ClassId == classId)
        //            .ToList();

        //        var result = new List<ClassSubjectResponseDto>();

        //        foreach (var mapping in classSubjectMappings)
        //        {
        //            var subject = _subjects.FirstOrDefault(s => s.Id == mapping.SubjectId);
        //            if (subject != null)
        //            {
        //                result.Add(new ClassSubjectResponseDto
        //                {
        //                    SubjectId = mapping.SubjectId,
        //                    SubjectName = subject.Name,
        //                    TeacherId = mapping.TeacherId
        //                });
        //            }
        //        }

        //        return result;
        //    }
        //}

        //public void AssignSubjectsToClass(int classId, List<int> subjectIds)
        //{
        //    lock (_lockObject)
        //    {
        //        // Preserve existing teacher assignments
        //        var existingMappings = _classSubjects
        //            .Where(cs => cs.ClassId == classId)
        //            .ToDictionary(cs => cs.SubjectId, cs => cs.TeacherId);

        //        // Remove existing mappings for this class
        //        _classSubjects.RemoveAll(cs => cs.ClassId == classId);

        //        // Add new mappings (prevent duplicates) and preserve teacher assignments
        //        var uniqueSubjectIds = subjectIds.Distinct().ToList();
        //        foreach (var subjectId in uniqueSubjectIds)
        //        {
        //            _classSubjects.Add(new ClassSubjectDto
        //            {
        //                ClassId = classId,
        //                SubjectId = subjectId,
        //                TeacherId = existingMappings.ContainsKey(subjectId) ? existingMappings[subjectId] : null
        //            });
        //        }
        //    }
        //}

        //public void AssignTeacherToClassSubject(int classId, int subjectId, int teacherId)
        //{
        //    lock (_lockObject)
        //    {
        //        var mapping = _classSubjects.FirstOrDefault(cs => cs.ClassId == classId && cs.SubjectId == subjectId);
        //        if (mapping != null)
        //        {
        //            mapping.TeacherId = teacherId;
        //        }
        //        else
        //        {
        //            // Create new mapping if it doesn't exist
        //            _classSubjects.Add(new ClassSubjectDto
        //            {
        //                ClassId = classId,
        //                SubjectId = subjectId,
        //                TeacherId = teacherId
        //            });
        //        }
        //    }
        //}

        //public ClassSubjectDto? GetClassSubject(int classId, int subjectId)
        //{
        //    lock (_lockObject)
        //    {
        //        return _classSubjects.FirstOrDefault(cs => cs.ClassId == classId && cs.SubjectId == subjectId);
        //    }
        //}
    }
}


using Model.DTOs;

namespace Services.Interfaces
{
    public interface IInMemorySchoolStore
    {
        // Classes
        ClassDto AddClass(ClassDto classDto);
        List<ClassDto> GetAllClasses();
        ClassDto? GetClassById(int id);

        // Subjects
        SubjectDto AddSubject(SubjectDto subjectDto);
        List<SubjectDto> GetAllSubjects();
        SubjectDto? GetSubjectById(int id);

        // Teachers
        TeacherDto AddTeacher(TeacherDto teacherDto);
        List<TeacherDto> GetAllTeachers();
        TeacherDto? GetTeacherById(int id);

        // ClassSubjects
        List<ClassSubjectResponseDto> GetClassSubjects(int classId);
        void AssignSubjectsToClass(int classId, List<int> subjectIds);
        void AssignTeacherToClassSubject(int classId, int subjectId, int teacherId);
        ClassSubjectDto? GetClassSubject(int classId, int subjectId);
    }
}


using Microsoft.EntityFrameworkCore;
using SimpleDeveloperCore.IRepository;
using SimpleDeveloperCore.Models;
using SimpleDeveloperCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDeveloperData.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolContext _schoolContext;
        public StudentRepository(SchoolContext schoolContext)
        {
            _schoolContext = schoolContext;
        }
        #region Get Methods
        public async Task<IEnumerable<Student>> GetAllStudentAsyc()
        {
            try
            {
                var studentList = await _schoolContext.Students.ToListAsync();
                if (studentList.Any())
                {
                    return studentList;
                }
                return new List<Student>();
            }
            catch (Exception ex)
            {
                return new List<Student>();
            }
        }
        public async Task<IEnumerable<IGrouping<string, Student>>> GetStudentsGroupedByClassListAsync()
        {
            if (!_schoolContext.Students.Any())
                return new List<IGrouping<string, Student>>();

            var studentsGroupedByClass = await _schoolContext.Students
                                                              .GroupBy(s => s.Class).AsNoTracking()
                                                              .ToListAsync();

            return studentsGroupedByClass ?? new List<IGrouping<string, Student>>();
        }
        public async Task<IEnumerable<StudentSubjectsTeachersViewModel>> GetStudentSubjectsandTeachersListAsync()
        {
            try
            {
                var data = await (from student in _schoolContext.Students
                                  join ssr in _schoolContext.StudentSubjectReferences on student.Id equals ssr.StudentId
                                  join subject in _schoolContext.Subjects on ssr.SubjectId equals subject.Id
                                  join str in _schoolContext.SubjectTeacherReferences on subject.Id equals str.SubjectId
                                  join teacher in _schoolContext.Teachers on str.TeacherId equals teacher.Id
                                  select new
                                  {
                                      Student = student,
                                      Subject = subject,
                                      Teacher = teacher
                                  })
                          .GroupBy(x => x.Student)
                          .Select(g => new StudentSubjectsTeachersViewModel
                          {
                              Student = g.Key,
                              SubjectsWithTeachers = g.GroupBy(st => st.Subject)
                                                      .Select(sg => new SubjectWithTeachersViewModel
                                                      {
                                                          Subject = sg.Key,
                                                          Teachers = sg.Select(st => st.Teacher).ToList()
                                                      })
                                                      .ToList()
                          })
                          .ToListAsync();

                return data;
            }
            catch (Exception ex)
            {
                return new List<StudentSubjectsTeachersViewModel>();
            }
        }
        #endregion
        #region Add method
        public async Task<string> AddNewStudentAsync(Student addStudent)
        {
            try
            {
                if (addStudent == null)
                {
                    return "Student data is null. Please provide valid student details.";
                }
                // Add the new student to the context
                await _schoolContext.Students.AddAsync(addStudent);
                await _schoolContext.SaveChangesAsync();
                // Split the subjects, parse them into integers, and create a list of StudentSubjectReference
                var studentSubjects = string.Join(",", addStudent.Subjects);
                var studentSubjectReferences = studentSubjects
                    .Split(',')
                    .Select(int.Parse)
                    .Select(subjectId => new StudentSubjectReference
                    {
                        StudentId = addStudent.Id, // Use the saved student ID
                        SubjectId = subjectId
                    }).ToList();

                // Add the list of StudentSubjectReferences to the context in one go
                await _schoolContext.StudentSubjectReferences.AddRangeAsync(studentSubjectReferences);
                await _schoolContext.SaveChangesAsync();

                return "Student added successfully.";
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
        }
        #endregion
        #region Filter Method
        public async Task<IEnumerable<Student>> SearchStudentsByNameAsync(string name)
        {
            return await _schoolContext.Students.Where(s => s.Name.Contains(name)).ToListAsync();
        }
        #endregion
    }
}

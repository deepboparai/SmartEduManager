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
    public class TeacherRepository : ITeacherRepository
    {
        private readonly SchoolContext _schoolContext;
        public TeacherRepository(SchoolContext schoolContext)
        {
            _schoolContext = schoolContext;
        }
        #region Get Methods
        public async Task<List<Subject>> GetSubjectListAsync()
        {
            try
            {
                var subjects = await _schoolContext.Subjects
               .Select(subject => new Subject
               {
                   Id = subject.Id,
                   Name = subject.Name
               }).ToListAsync();

                return subjects;
            }
            catch (Exception ex)
            {
                return new List<Subject>();
            }
        }
        #endregion
        #region Add method
        public async Task<string> AddNewTeacherAsync(Teacher addTeacher)
        {
            try
            {
                if (addTeacher == null)
                {
                    return "Teacher data is null. Please provide valid Teacher details.";
                }

                await _schoolContext.Teachers.AddAsync(addTeacher);
                await _schoolContext.SaveChangesAsync();

                // Split the subjects, parse them into integers, and create a list of SubjectTeacherReference
                var subjectTeacherReferences = addTeacher.SubjectId
                    .Split(',')
                    .Select(int.Parse)
                    .Select(subjectId => new SubjectTeacherReference
                    {
                        SubjectId = subjectId,
                        TeacherId = addTeacher.Id // Use the saved teacher ID
                    }).ToList();

                // Add the list of SubjectTeacherReferences to the context in one go
                await _schoolContext.SubjectTeacherReferences.AddRangeAsync(subjectTeacherReferences);
                await _schoolContext.SaveChangesAsync();

                return "Student added successfully.";
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
        }
        #endregion
    }
}

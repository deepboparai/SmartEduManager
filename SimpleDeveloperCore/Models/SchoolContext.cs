using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDeveloperCore.Models
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectTeacherReference> SubjectTeacherReferences { get; set; }
        public DbSet<StudentSubjectReference> StudentSubjectReferences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubjectTeacherReference>()
                .HasOne(str => str.Subject)
                .WithMany(s => s.SubjectTeacherReferences)
                .HasForeignKey(str => str.SubjectId);

            modelBuilder.Entity<SubjectTeacherReference>()
                .HasOne(str => str.Teacher)
                .WithMany(t => t.SubjectTeacherReferences)
                .HasForeignKey(str => str.TeacherId);

            modelBuilder.Entity<StudentSubjectReference>()
                .HasOne(ssr => ssr.Subject)
                .WithMany(s => s.StudentSubjectReferences)
                .HasForeignKey(ssr => ssr.SubjectId);

            modelBuilder.Entity<StudentSubjectReference>()
                .HasOne(ssr => ssr.Student)
                .WithMany(s => s.StudentSubjectReferences)
                .HasForeignKey(ssr => ssr.StudentId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektuppgiftStudentregistreringsprogram
{
    class School
    {
        private StudentServiceTerminal Terminal;
        internal StudentDbContext dbCtx = new StudentDbContext();

        public School()
        {
            this.Terminal = new StudentServiceTerminal(this);
            Terminal.PrintMenu();
        }

        public void AddStudentToDatabase(string firstName, string lastName, string city)
        {
            dbCtx.Students.Add(new Student(firstName, lastName, city));
            dbCtx.SaveChanges();
        }

        public Student? CheckExistingID(int studentID)
        {
            var student = dbCtx.Students.Find(studentID);
            if (student != null)
            {
                return student;
            }
            return null;
        }

        internal void UpdateStudent(Student student)
        {
            dbCtx.SaveChanges();
            Terminal.PrintMenu();
        }

        internal void DeleteStudent(Student student)
        {
            dbCtx.Students.Remove(student);
            dbCtx.SaveChanges();
            Terminal.PrintMenu();
        }

        internal List<Student> StudentList()
        {
            return dbCtx.Students.ToList();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektuppgiftStudentregistreringsprogram
{
    class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string City { get; set; } = "";

        public Student()
        {
            
        }

        public Student(string firstName, string lastName, string city)
        {
            FirstName = firstName;
            LastName = lastName;
            City = city;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektuppgiftStudentregistreringsprogram
{
    class StudentServiceTerminal
    {
        private School MySchool;

        public StudentServiceTerminal(School mySchool)
        {
            this.MySchool = mySchool;
        }

        public void PrintMenu()
        {
            Console.WriteLine("Välkommen till Student Service för hantering av studenter. Välj ett av följande menyval");
            Console.WriteLine("1. Registrera ny student");
            Console.WriteLine("2. Ändra en befintlig student");
            Console.WriteLine("3. Lista alla studenter");
            Console.WriteLine("4. Ta bort en student");
            Console.WriteLine("5. Avsluta");
            HandleMenuChoice();
        }
        public void HandleMenuChoice()
        {
            int menuChoice;
            bool validChoice = int.TryParse(Console.ReadLine(), out menuChoice);
            if (validChoice == false)
            {
                Console.WriteLine("Felaktigt val, ange giltigt alternativ med siffra\n");
                PrintMenu();
            }
            else
            {
                switch (menuChoice)
                {
                    case 1:
                        AddStudent();
                        break;
                    case 2: 
                        ChangeStudent();
                        break;
                    case 3:
                        ShowAllStudents();
                        Console.WriteLine();
                        PrintMenu();
                        break;
                    case 4:
                        RemoveStudent();
                        break;
                    case 5:
                        Console.WriteLine("Programmet avslutas");
                        break;
                    default:
                        Console.WriteLine("Felaktigt val, välj någon av de giltiga alternativen\n");
                        PrintMenu();
                        break;
                }
            }
        }
        public void AddStudent()
        {
            Console.WriteLine("Ange förnamn: ");
            string? firstname = Console.ReadLine();
            Console.WriteLine("Ange efternamn: ");
            string? lastName = Console.ReadLine();
            Console.WriteLine("Ange stad: ");
            string? city = Console.ReadLine();
            if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(city))
            {
                Console.WriteLine("Du har inte angett alla uppgifter, försök igen\n");
                PrintMenu();
            }
            Console.WriteLine($"Du har angett uppgifterna {firstname} {lastName}, {city}");
            Console.WriteLine("Om uppgifterna stämmer, tryck 1, tryck 2 för att ange uppgifter på nytt, tryck tre för att avbryta och återgå till huvudmenyn.");

            int choice;
            bool validchoice = int.TryParse(Console.ReadLine(),out choice);

            if ((choice <1 || choice >3) || validchoice == false)
            {
                Console.WriteLine("Felaktigt val, du skickas nu till huvudmenyn\n");
                PrintMenu();
            }
            else if (choice == 1)
            {
                MySchool.AddStudentToDatabase(firstname, lastName, city);
                Console.WriteLine("Studenten är tillagd\n");
                PrintMenu();
            }
            else if (choice == 2)
            {
                Console.WriteLine();
                AddStudent();
            }
            else if (choice == 3)
            {
                Console.WriteLine();
                PrintMenu();
            }
            else
            {
                Console.WriteLine("Ett fel har uppstått, du skickas nu till huvudmenyn\n");
                PrintMenu();
            }

        }
        public void ChangeStudent()
        {
            Console.WriteLine("Följande studenter finns tillgängliga");
            ShowAllStudents();

            Student? student = GetValidID();

            if (student != null)
            {
                GetNewStudentInformation(student);
                Console.WriteLine("Studentens uppgifter ändras\n");
                MySchool.UpdateStudent(student);
            }
            else
            {
                PrintMenu();
            }
        }
        public Student? GetValidID()
        {
            int studentID;
            int numberOfTries = 0;

            while (numberOfTries < 3)
            {
                Console.WriteLine("Ange ID nummer för den student du vill ändra uppgifter på.");
                bool validID = int.TryParse(Console.ReadLine(), out studentID);
                if (validID == false)
                {
                    Console.WriteLine("Du har inte angett ett nummer, försök igen");
                    numberOfTries++;
                    continue;
                }

                var student = MySchool.CheckExistingID(studentID);
                if (student != null)
                {
                    return student;
                }

                else
                {
                    Console.WriteLine("Ingen student med det ID:et hittades. försök igen");
                    numberOfTries++;
                }
            }
            Console.WriteLine("För många felaktiga försök, återgår till huvudmenyn\n");
            return null;
        }

        public void ShowAllStudents()
        {
            foreach (var student in MySchool.StudentList())
            {
                Console.WriteLine($"ID: {student.StudentID}, Namn: {student.FirstName} {student.LastName}, Stad: {student.City}");
            }
        }
        public void RemoveStudent()
        {
            Console.WriteLine("Följande studenter finns tillgängliga");
            ShowAllStudents();

            Student? student = GetValidID();

            if (student != null)
            {
                Console.WriteLine("Studenten tas bort ur systemet\n");
                MySchool.DeleteStudent(student);
            }
            else
            {
                PrintMenu();
            }
        }

        public Student GetNewStudentInformation(Student student)
        {
            Console.WriteLine($"Ändrar uppgifter för: {student.FirstName} {student.LastName}. Stad: {student.City}");
            Console.WriteLine("Lämna ett fält tomt för att behålla den tidigare uppgiften.");
            Console.WriteLine("Ange nytt förnamn: ");
            string? newFirstName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newFirstName))
            {
                student.FirstName = newFirstName;
            }
            Console.WriteLine("Ange nytt efternamn: ");
            string? newLastName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newLastName))
            {
                student.LastName = newLastName;
            }
            Console.WriteLine("Ange ny stad: ");
            string? newCity = Console.ReadLine();
            if (!string.IsNullOrEmpty(newCity))
            {
                student.City = newCity;
            }
            return student;

        }
    }
}

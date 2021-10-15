using System;

namespace Isu
{
    internal class Program
    {
        private static void Main()
        {
            var isuService = new IsuService();
            for (int i = 1; i < 10; i++)
            {
                isuService.AddGroup("M320" + i, 30);
            }

            isuService.AddStudent(isuService.FindGroup("M3209"), "Kirill");
            isuService.AddStudent(isuService.FindGroup("M3209"), "Anton");
            isuService.AddStudent(isuService.FindGroup("M3209"), "Nikita");
            isuService.AddStudent(isuService.FindGroup("M3209"), "Artem");
            isuService.AddStudent(isuService.FindGroup("M3209"), "Lev");
            isuService.AddStudent(isuService.FindGroup("M3209"), "Renat");
            isuService.AddStudent(isuService.FindGroup("M3209"), "Alexander");
            isuService.AddStudent(isuService.FindGroup("M3209"), "Alexey");

            foreach (Student student in isuService.FindStudents("M3209"))
            {
                Console.WriteLine(student.Name);
            }

            isuService.ChangeStudentGroup(isuService.FindStudent("Kirill"), isuService.FindGroup("M3208"));

            foreach (Student student in isuService.FindStudents((CourseNumber)2))
            {
                Console.WriteLine(student.ID);
            }

            Console.WriteLine(isuService.FindStudent("Kirill").Group);
            foreach (Student student in isuService.FindStudents("M3209"))
            {
                Console.WriteLine(student.Name);
            }
        }
    }
}

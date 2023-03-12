using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Tests.IntegrationTests.GradingStudent
{
    internal static class StudentsStore
    {
        public class Student
        {
            public string Name { get; set; }
            public int G1 { get; set; }
            public int G2 { get; set; }
            public int G3 { get; set; }
            public int G4 { get; set; } = -1; // not set

            public Student(string name, int g1, int g2, int g3)
            {
                Name = name;
                G1 = g1;
                G2 = g2;
                G3 = g3;
            }
        }

        public static void CreateStudentsStore(string filePath)
        {
            var students = new List<Student>()
            {
                new Student("A",3,4,3),
                new Student("B",5,3,4),
                new Student("C",2,2,3),
                new Student("D",4,4,3),
                new Student("E",2,4,4),
                new Student("F",2,5,5),
                new Student("G",4,4,4)
            };

            SaveStudentsStore(students, filePath);
        }

        public static void SaveStudentsStore(IEnumerable<Student> students, string filePath)
        {
            using (var sw = new StreamWriter(filePath))
            {
                foreach (var s in students)
                {
                    if (s.G4 == -1)
                    {
                        sw.WriteLine(string.Join(',', s.Name, s.G1, s.G2, s.G3));
                    }
                    else
                    {
                        sw.WriteLine(string.Join(',', s.Name, s.G1, s.G2, s.G3, s.G4));
                    }
                }
            }
        }

        public static List<Student> ReadStudentsStore(string filePath)
        {
            var students = new List<Student>();
            using (var sw = new StreamReader(filePath))
            {
                while (!sw.EndOfStream)
                {
                    string? line = sw.ReadLine();
                    if (line == null)
                    {
                        continue;
                    }

                    string[] parts = line.Split(',');

                    var student = new Student(
                        parts[0],
                        int.Parse(parts[1]),
                        int.Parse(parts[2]),
                        int.Parse(parts[3]));

                    if (parts.Length > 4)
                    {
                        student.G4 = int.Parse(parts[4]);
                    }

                    students.Add(student);
                }
            }

            return students;

        }

    }
}

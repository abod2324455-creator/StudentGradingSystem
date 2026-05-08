using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.IO;

namespace StudentGradingSystem
{
    internal class Student
    {
        private int _StudentID;
        private string _Name;
        private string _Department;
        public  List<Course> _Courses = new List<Course>();
        public List<Student> _Students = new List<Student>();


        // property
        public int StudentID
        {
            get { return _StudentID; }
            set
            {
                if (value > 0)
                    _StudentID = value;
                else
                    _StudentID = 1;
            }
        }
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
            }
        }
        public string Department
        {
            get { return _Department; }
            set
            {
                _Department = value;
            }
        }


        // constructors
        public Student ()
        {
            _StudentID = 1;
            _Name = "";
            _Department = "";
        }
        public Student(string name ,int id, string department , List<Course> courses)
        {
            Name = name;
            StudentID = id;  
            Department = department;
            _Courses = courses;
        }
        public Student (string name, int id , string department)
        {
            Name = name;
            StudentID = id;
            Department = department;
        }
        /* -------------------------------------------------*/


        /* Add students,courses,components to their list*/
        public void AddStudent ()
        {
            // Add student
            Console.Write("Enter student name : ");
            var studentname = Console.ReadLine();
            Console.Write("Enter student id : ");
            var studentid = int.Parse(Console.ReadLine());
            Console.Write("Enter department : ");
            var department = Console.ReadLine();
            Student student = new Student(studentname, studentid, department);
            // Add course
            char option;
            do
            {
                Console.Write("Enter course code : ");
                var coursecode = Console.ReadLine();
                Console.Write("Enter course name : ");
                var coursename = Console.ReadLine();
                Console.Write("Enter Credit hours : ");
                var credithours = int.Parse(Console.ReadLine());
                Course course = new Course(coursecode, coursename, credithours);
                // Add component
                char Answer;
                do
                {
                    Console.Write("Enter component name : ");
                    var componentname = Console.ReadLine();
                    Console.Write("Enter score : ");
                    var score = int.Parse(Console.ReadLine());
                    Console.Write("Enter max score : ");
                    var maxscore = int.Parse(Console.ReadLine());
                    Console.Write("Enter the weight of the component : ");
                    var weight = double.Parse(Console.ReadLine());
                    GradeComponent component = new GradeComponent(componentname, score, maxscore, weight);
                    course._GradeComponent.Add(component);
                    Console.Write("Add new component ------- [y for yes] and [n for no] ------- (y / n) : ");
                    Answer = char.Parse(Console.ReadLine());
                } while (Answer == 'y');
                student._Courses.Add(course); // After adding components to the course, attach it to the new student
                Console.Write("Add new course ------- [y for yes] and [n for no] ------- (y / n) : ");
                option = char.Parse(Console.ReadLine());
            } while (option == 'y');
            _Students.Add(student);
               SaveListInFile();
         }


        /* save list in file */
        public void SaveListInFile()
        {
            using (StreamWriter writer1 = File.CreateText("File.txt"))
            {
                foreach (var student in _Students)
                {
                    writer1.WriteLine(student.Name + "," + student.StudentID + "," + student.Department);
                    foreach (var course in student._Courses)
                    {
                        writer1.WriteLine(course.CourseCode + "," + course.CourseName + "," + course.CreditHours);
                        foreach (var component in course._GradeComponent)
                        {
                            writer1.WriteLine(component.ComponentName + "," + component.Score + "," + component.MaxScore);
                        }
                    }
                }
            }
        }


        // Read from file and put info in list
        public void LoadData(string path)
        {
            // Clear existing data
            _Students.Clear();
            // Check if file exists
            if (!File.Exists(path))
                return;
            // Temporary variables to hold current student and course
            Student currentStudent = null;
            Course currentCourse = null;
            // Read all lines from the file
            var lines = File.ReadAllLines(path);
            // Read each line
            foreach (var raw in lines)
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(raw))
                    continue;
                // Trim and split the line
                // trim to avoid extra spaces
                // split by comma to get parts
                var line = raw.Trim();
                var parts = line.Split(',');
                // if length is not 3, skip (invalid line)
                if (parts.Length != 3)
                    continue;
                // trim each part to remove extra spaces
                for (int i = 0; i < parts.Length; i++)
                    parts[i] = parts[i].Trim();

                // Determine whether this line is a student, a course or a component
                // Student: third part is non-numeric (department)
                // Course: third part is numeric (credit hours) and second part is non-numeric (course name)
                // Component: second and third parts are numeric (score, maxscore)

                bool thirdIsInt = int.TryParse(parts[2], out int thirdInt);
                bool secondIsInt = int.TryParse(parts[1], out int secondInt);

                if (!thirdIsInt)
                {
                    // Student line: name,id,department
                    if (int.TryParse(parts[1], out int id))
                    {
                        currentStudent = new Student(parts[0], id, parts[2]);
                        _Students.Add(currentStudent);
                        // reset current course
                        currentCourse = null;
                    }
                }
                else if (thirdIsInt && !secondIsInt)
                {
                    if (currentStudent == null)
                    {
                        // no student yet , create a default student to attach courses to it
                        currentStudent = new Student();
                        _Students.Add(currentStudent);
                    }
                    // Course line: code,name,creditHours
                    currentCourse = new Course(parts[0], parts[1], thirdInt);
                    // attach course to current student
                    currentStudent._Courses.Add(currentCourse);
                }
                else if (secondIsInt && thirdIsInt)
                {
                    // Component line: name,score,maxscore
                    if (currentCourse == null)
                    {
                        // no course to attach to; skip
                        continue;
                    }
                    if (double.TryParse(parts[1], out double score) && double.TryParse(parts[2], out double maxscore))
                    {
                        var component = new GradeComponent(parts[0], score, maxscore, 0);
                        currentCourse._GradeComponent.Add(component);
                    }
                }
            }
        }


        /* --------------   view report ------------------ */
        public void ViewFinalReportForStudent()
        {
            LoadData("File.txt");
            if (_Students.Count == 0)
            {
                Console.WriteLine("No students found.");
                return;
            }
            else
            {
                // show loaded students so the user knows which ID to enter
                Console.WriteLine("Available students:");
                foreach (var student in _Students)
                {
                    Console.WriteLine($"ID: {student.StudentID}, Name: {student.Name}, Department: {student.Department}");
                }

                Console.Write("Enter Student ID to view report : ");
                var id = int.Parse(Console.ReadLine());
                foreach (var student in _Students)
                {
                    if (student.StudentID == id)
                    {
                        Console.WriteLine("--------------------------------------------------------------------------------");
                        Console.WriteLine($"Student name = {student.Name}          Student ID = {student.StudentID}          Department = {student.Department}");
                        int countcourse = 0;
                        foreach (var course in student._Courses)
                        {
                            countcourse++;
                            Console.WriteLine($"{countcourse}  Course name = {course.CourseName}          Course code = {course.CourseCode}             Credit hours = {course.CreditHours}");
                            int countcomponent = 0;
                            foreach (var component in course._GradeComponent)
                            {
                                countcomponent++;
                                Console.WriteLine($"{countcomponent}  Component name = {component.ComponentName}           Score = {component.Score}             Max score = {component.MaxScore}");
                            }
                        }

                        double gpa = student.GPA(student._Courses);
                        Console.WriteLine($"GPA = {gpa:F2}");
                    }
                }
            }
        }
        

        /* ---------------- percentage ---------------- */
        public double Percentage (List <GradeComponent> gradeComponents)
        {
            PercentageBasedPolicy p = new PercentageBasedPolicy();
            double percentage= p.CalculateFinalGradePerCourse(gradeComponents);
            if (percentage >= 96)
                return 4;
            else if (percentage >= 92 && percentage < 96)
                return 3.7;
            else if (percentage >= 88 && percentage < 92)
                return 3.3;
            else if (percentage >= 84 && percentage < 88)
                return 3.0;
            else if (percentage >= 80 && percentage < 84)
                return 2.7;
            else if (percentage >= 76 && percentage < 80)
                return 2.3;
            else if (percentage >= 72 && percentage < 76)
                return 2.0;
            else if (percentage >= 68 && percentage < 72)
                return 1.7;
            else if (percentage >= 64 && percentage < 68)
                return 1.3;
            else if (percentage >= 60 && percentage < 64)
                return 1.0;
            else
                return 0;
        }


        /* ------------- GPA -------------- */
        public double GPA(List<Course> courses)
        {
            double totalpoint = 0;
            int hours = 0;
            foreach ( var course in courses )
            {
                totalpoint += Percentage(course._GradeComponent)*course.CreditHours;
                hours += course.CreditHours;
            }
            double gpa = totalpoint / hours;
            return gpa;
        }
    }
}

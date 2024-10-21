
using System;
using System.Collections;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        // Lecturer objects
        var lecturer1 = new Lecturer("Trudie");
        var lecturer2 = new Lecturer("Moyo");

        // Student objects
        var student1 = new Student("John");
        var student2 = new Student("Jack");
        var student3 = new Student("Jill");
        var student4 = new Student("Jane");

        // Module objects
        var module1 = new Module("ITDCA", lecturer1);  // data structures & algorithms in C#
        var module2 = new Module("ITUEA", lecturer1);  // usability engineering
        var module3 = new Module("ITJVA", lecturer2);  // java
        var module4 = new Module("ITDSA", lecturer2);














        // Add objects to Graph
        var graph = new Graph();
        graph.AddLecturer(lecturer1);
        graph.AddLecturer(lecturer2);
        graph.AddStudent(student1);
        graph.AddStudent(student2);
        graph.AddStudent(student3);
        graph.AddStudent(student4);
        graph.AddModule(module1, lecturer1);
        graph.AddModule(module2, lecturer1);
        graph.AddModule(module3, lecturer2);
        graph.AddModule(module4, lecturer2);


        // Link students to modules
        graph.EnrollStudentInModule(student1, module1);
        graph.EnrollStudentInModule(student1, module2);
        graph.EnrollStudentInModule(student1, module3);
        graph.EnrollStudentInModule(student2, module1);
        // graph.EnrollStudentInModule(student2, module2);
        graph.EnrollStudentInModule(student3, module2);
        graph.EnrollStudentInModule(student4, module2);
        graph.EnrollStudentInModule(student4, module3);
        graph.EnrollStudentInModule(student4, module4);

        graph.QueryModulesPerStudent(student1);





        // Display the graph
        graph.DisplayGraph();

        // Query Examples
        graph.QueryStudentsPerModule(module1);

        graph.QueryModulesPerStudent(student1);

        Console.WriteLine("\nAll student-module connections:");
        graph.QueryAllStudents();
    }
}

// Node classes
public class Student
{
    public string Name;
    public Student(string name)
    {
        Name = name;
    }
}

public class Module
{
    public string ModuleCode;
    private string LecturerID;
    public string LecturerIDFK { get => LecturerID; set => LecturerID = value; }
    public Module(string moduleCode, Lecturer lecturer)
    {
        ModuleCode = moduleCode;
        LecturerIDFK = lecturer.Name;
    }
}

public class Lecturer
{
    public string Name;
    public Lecturer(string name)
    {
        Name = name;
    }
}

// Graph structure
public class Graph
{
    private List<Student> Students = new List<Student>();
    private List<Module> Modules = new List<Module>();
    private ArrayList Lecturers = new ArrayList();
    private Dictionary<Student, LinkedList<Module>> Student_Modules = new Dictionary<Student, LinkedList<Module>>();

    public void AddStudent(Student student)
    {
        Students.Add(student);
        Student_Modules[student] = new LinkedList<Module>();
    }

    public void AddModule(Module module, Lecturer lecturer)
    {
        Modules.Add(module);
    }

    public void AddLecturer(Lecturer lecturer)
    {
        Lecturers.Add(lecturer);
    }

    public void EnrollStudentInModule(Student student, Module module)
    {
        if (Student_Modules.ContainsKey(student) && Modules.Contains(module))
        {
            // Assigning students to modules
            Student_Modules[student].AddLast(module);

        }
    }

    public void DisplayGraph()
    {
        Console.WriteLine("\nModules:");
        foreach (var module in Modules)
        {
            Console.WriteLine(module.ModuleCode);
        }

        Console.WriteLine("\nStudents and their enrolled modules:");
        foreach (var student in Students)
        {
            Console.Write(student.Name + " is enrolled in:\t");
            foreach (var module in Student_Modules[student])
            {
                Console.Write(module.ModuleCode + "\t");
            }
            Console.WriteLine();
        }
    }

    // Display students enrolled in a specific module
    public void QueryStudentsPerModule(Module module)
    {
        Console.WriteLine($"\nModule {module.ModuleCode} ({module.LecturerIDFK}):");
        foreach (var student in Students)
        {
            if (Student_Modules[student].Contains(module))
            {
                Console.WriteLine(student.Name);
            }
        }
        Console.WriteLine();
    }

    // Query: Find all modules for a specific student
    public void QueryModulesPerStudent(Student student)
    {
        Console.WriteLine($"{student.Name}:");
        if (Student_Modules.ContainsKey(student))
        {
            foreach (var module in Student_Modules[student])
            {
                Console.WriteLine(" - " + module.ModuleCode);
            }
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }

    public void QueryAllStudents()
    {
        foreach (var student in Students)
        {
            Console.Write($"\n{student.Name}: ");
            if (Student_Modules.ContainsKey(student))
            {
                foreach (var module in Student_Modules[student])
                {
                    Console.Write($" - {module.ModuleCode} ({module.LecturerIDFK})");
                }
                Console.WriteLine();
            }

        }
    }
}

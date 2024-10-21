// See https://aka.ms/new-console-template for more information
class Student
{
    public string StudentName { get; set; }

    public Student(string name)
    {
        StudentName = name;
    }

    public override string ToString()
    {
        return StudentName;
    }
}

class Program
{
    static List<Student> queue = new List<Student>();
    public static void Main()
    {
        queue.Add(new Student("John"));
        queue.Add(new Student("Jack"));
        queue.Add(new Student("Jill"));

        while (true)
        {
            Console.WriteLine("Operation (D) Display, (A) Add, (R) Remove first item\n>");
            var input = Console.ReadLine();
            input = input.ToUpper();

            if (input == "D")
            {
                Display();
            }
            if (input == "A")
            {
                AddUser();
            }
            if (input == "R")
            {
                RemoveUser();
                Display();

            }

            if (input == "Q")
            {
                break;
            }

        }
    }
    public static void Display()
    {
        Recursion(0, queue);
    }

    public static void AddUser()
    {
        Console.Write("Add User: ");
        var input = Console.ReadLine();
        queue.Add(new Student(input));
    }

    public static void RemoveUser()
    {
        var user = queue[0];
        queue.RemoveAt(0);

    }
    public static void Recursion(int index, List<Student> queue)
    {
        if (index == queue.Count)
        {
            return;
        }
        if (queue[index].StudentName.Reverse() == queue[index].StudentName)
        {

            Console.WriteLine(queue[index]);
        }
        Recursion(index + 1, queue);

    }

}




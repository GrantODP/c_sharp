class Program
{
  public static void Main()
  {
    var queue = new MyQueue<Student>();
    var queue2 = new MyQueue<Student>();
    var queue3 = new MyQueue<Student>();
    var queueMap = new Dictionary<int, MyQueue<Student>>();
    Console.WriteLine("Set each queue priority");

    Console.WriteLine("Queue 1?: ");
    var input = Int32.Parse(Console.ReadLine());
    SetQueuePriority(queueMap, input, queue);

    Console.WriteLine("Queue 2?: ");
    input = Int32.Parse(Console.ReadLine());
    SetQueuePriority(queueMap, input, queue2);

    Console.WriteLine("Queue 3?: ");
    input = Int32.Parse(Console.ReadLine());
    SetQueuePriority(queueMap, input, queue3);

    while (true)
    {
      Console.WriteLine("Operation (D) Display, (A) Add, (R) Remove first item\n>");
      var operation = Console.ReadLine();
      operation = operation.ToUpper();

      if (operation == "Q")
      {
        break;
      }


      Console.WriteLine('\n');
      Console.Write("Which Queue? (1-3)\n>");
      var selectedQueue = Int32.Parse(Console.ReadLine());
      if (!queueMap.ContainsKey(selectedQueue))
      {

        Console.WriteLine("Invalid queue selected");
        continue;
      }

      DoOperation(queueMap, operation, queueMap[selectedQueue]);
      // Console.WriteLine(selectedQueue);
      // switch (selectedQueue)
      // {
      //   case 1:
      //     DoOperation(queueMap, operation, queue);
      //     break;
      //   case 2:
      //     DoOperation(queueMap, operation, queue2);
      //     break;
      //   case 3:
      //     DoOperation(queueMap, operation, queue3);
      //     break;
      //   default:
      //     break;
      // }

    }
  }

  public static bool SetQueuePriority(Dictionary<int, MyQueue<Student>> map, int priority, MyQueue<Student> queue)
  {
    if (!map.ContainsKey(priority))
    {
      map.Add(priority, queue);
      return true;
    }
    return false;
  }


  public static bool DoOperation(Dictionary<int, MyQueue<Student>> map, string operation, MyQueue<Student> queue)
  {
    if (operation == "D")
    {
      queue.Display();
    }
    else if (operation == "A")
    {
      Console.Write("Add User: ");
      var input = Console.ReadLine();
      Console.Write("Priority: ");
      var prio = Int32.Parse(Console.ReadLine());
      if (map.ContainsKey(prio))
      {
        queue = map[prio];
      }
      queue.Add(new Student(input, prio));
    }
    else if (operation == "R")
    {
      queue.Remove();

    }

    return true;
  }
}

class MyQueue<T> where T : IComparable<T>
{

  private List<T> items = new List<T>();

  public MyQueue() { }

  public void Add(T item)
  {
    items.Add(item);
    MoveUpQueue(items.Count - 1);
  }

  public void MoveUpQueue(int index)
  {
    if (index == 0)
    {
      return;
    }
    if (items[index].CompareTo(items[index - 1]) > 0)
    {
      var temp = items[index];
      items[index] = items[index - 1];
      items[index - 1] = temp;
      MoveUpQueue(index - 1);
    }


  }

  public T Remove()
  {
    T value = items[0];
    items.RemoveAt(0);
    return value;
  }
  public void Display()
  {
    if (Count == 0)
    {
      return;
    }
    Console.WriteLine(items[0]);
  }
  public int Count { get { return items.Count; } }
}

class Student : IComparable<Student>
{
  public string StudentName { get; set; }
  public int StudentPriority { get; set; }

  public Student(string name, int priority)
  {
    StudentName = name;
  }
  public int CompareTo(Student rhs)
  {
    return StudentPriority.CompareTo(rhs.StudentPriority);
  }

  public override string ToString()
  {
    return StudentName;
  }
}

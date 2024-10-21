using Hospital.Collections;
using Hospital.Parser;

namespace Hospital.Care
{


  public enum CarePriority
  {
    CRITICAL = 1,
    URGENT = 2,
    OTHER = 3,
    DECEASED = 4,
  }

  class Patient : IComparable<Patient>
  {

    public CarePriority Priority { get; set; }
    public int RegisterNumber { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public int CompareTo(Patient rhs)
    {
      var prioCompare = Priority.CompareTo(rhs.Priority);
      if (prioCompare == 0)
      {
        return RegisterNumber.CompareTo(rhs.RegisterNumber);
      }
      return prioCompare;
    }


    public override string ToString()
    {
      return $"{RegisterNumber}: Patient: {Name} {Surname} Care: {Priority}";
    }

  }

  struct PerformanceMeasure
  {
    public string id;
    public long metric;


    public override string ToString()
    {
      return $"{id}: {metric}ms";
    }

  }

  class CareSystem
  {

    public CareSystem() { }

    public List<PerformanceMeasure> metrics = new();
    HPriorityQueue<Patient> queue = new HPriorityQueue<Patient>();
    List<Patient> patients = new();
    List<Patient> dead = new();

    public List<Patient> GetPatients(HPriorityQueue<Patient> patients)
    {

      var notDeadpatients = new List<Patient>();
      PerformanceMeasure pm = new PerformanceMeasure();
      pm.id = "GetPatients";
      pm.metric = 0;

      var stopWatch = System.Diagnostics.Stopwatch.StartNew();
      while (patients.Peek() != null && patients.Peek().Priority != CarePriority.DECEASED)
      {

        // var stopWatch = System.Diagnostics.Stopwatch.StartNew();
        var patient = patients.Pull();
        // stopWatch.Stop();
        // Console.WriteLine($"Pulled P: {patient}");
        // pm.metric += stopWatch.ElapsedMilliseconds;
        notDeadpatients.Add(patient);
      }

      stopWatch.Stop();
      pm.metric = stopWatch.ElapsedMilliseconds;
      metrics.Add(pm);
      return notDeadpatients;
    }
    public List<Patient> GetDead(HPriorityQueue<Patient> patients)
    {

      var deadpatients = new List<Patient>();
      PerformanceMeasure pm = new PerformanceMeasure();
      pm.id = "GetDead";
      pm.metric = 0;

      var stopWatch = System.Diagnostics.Stopwatch.StartNew();
      while (patients.Peek() != null && patients.Peek().Priority == CarePriority.DECEASED)
      {

        // var stopWatch = System.Diagnostics.Stopwatch.StartNew();
        var patient = patients.Pull();

        // Console.WriteLine($"Pulled D: {patient}");
        // stopWatch.Stop();
        // pm.metric += stopWatch.ElapsedMilliseconds * 1000000;
        deadpatients.Add(patient);
      }

      stopWatch.Stop();
      pm.metric = stopWatch.ElapsedMilliseconds;

      metrics.Add(pm);
      return deadpatients;
    }

    public void PrintMetrics()
    {
      foreach (var pm in metrics)
      {
        Console.WriteLine(pm);
      }
    }
    private void Load()
    {
      PerformanceMeasure pm = new PerformanceMeasure();
      pm.id = "Load patients";
      pm.metric = 0;
      var stopWatch = System.Diagnostics.Stopwatch.StartNew();

      queue = TxtParser.Parse("dummy_data.txt");
      stopWatch.Stop();
      pm.metric = stopWatch.ElapsedMilliseconds;

      metrics.Add(pm);
    }
    public void run()
    {

      Console.WriteLine("Hospital System");
      Load();
      patients = GetPatients(queue);
      dead = GetDead(queue);
      Console.WriteLine("Display (1) Patients(1-3), (2) Deceased patients(4) or (3) Display PerformanceMeasure (q) to Quit");

      while (true)
      {

        Console.Write(">");
        string input = Console.ReadLine();

        if (input == "1")
        {
          patients.ForEach(p => Console.WriteLine(p));

        }
        else if (input == "2")
        {
          dead.ForEach(p => Console.WriteLine(p));

        }
        else if (input == "3")
        {
          metrics.ForEach(p => Console.WriteLine(p));

        }
        else if (input == "q")
        {
          break;
        }
      }


    }
  }



}

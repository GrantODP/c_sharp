
public struct Player
{
  public string name = "";
  public Stack<int> values = new Stack<int>();

  public Player(string name, Stack<int> ints)
  {
    this.name = name;
    values = ints;
  }
}

class Snap
{

  private Random rnd = new Random();
  private Dictionary<string, Player> players = new Dictionary<string, Player>();
  Queue<Player> playerQueue = new Queue<Player>();
  private Stack<int> snaps = new Stack<int>();
  private Stack<int> snapHistory = new Stack<int>();

  public Snap(int n)
  {
    init(n);
  }
  public void init(int count)
  {
    count += 1;
    clearStacks();
    IEnumerable<int> range = Enumerable.Range(0, count);
    var numbers = range.ToList();

    ShuffleList(numbers);

    Player player = new Player("cpu", new Stack<int>(numbers));
    playerQueue.Enqueue(player);
    players.Add("cpu", player);

    ShuffleList(numbers);
    Player player2 = new Player("player", new Stack<int>(numbers));
    playerQueue.Enqueue(player2);
    players.Add("player", player2);

  }
  void ShuffleList(List<int> arr)
  {
    for (int i = 0; i < arr.Count; i++)
    {
      var randIndex = rnd.Next(0, arr.Count);
      var temp = arr[i];
      arr[i] = arr[randIndex];
      arr[randIndex] = temp;
    }
  }

  private void clearStacks()
  {
    foreach (var player in players)
    {
      player.Value.values.Clear();

    }
    snaps.Clear();
  }


  public void roundStart()
  {

    Console.Clear();
    Console.WriteLine("CPU vs Player 1");
    Wait(1000);
    Console.WriteLine("Press any key to snap. Example of Snap: 5 : 5");
    Wait(1000);
    Console.WriteLine("Press Any Key to Start");
    ReadKey();
    Console.Clear();
    Wait(1000);

    while (!IsStacksEmpty())
    {

      Wait(1000);
      var nextPlayer = playerQueue.Dequeue();
      playerQueue.Enqueue(nextPlayer);
      var nextValue = nextPlayer.values.Pop();

      Console.WriteLine($"{nextPlayer.name}: {nextValue}");
      if (snaps.Count == 0)
      {
        snaps.Push(nextValue);
        continue;
      }

      if (snaps.Peek() == nextValue)
      {
        Console.WriteLine($"Peek: {snaps.Peek()}");
        var winner = snapInProgress();
        snaps.Push(nextValue);
        pushOnWinnerStack(snaps, winner);
        snaps.Clear();

      }
      else
      {
        snaps.Push(nextValue);
      }






      //Console.WriteLine($"{cpu} : {p1}");

      //snaps.Enqueue(cpu);
      //snaps.Enqueue(p1);

      //if(cpu == p1)
      //{

      //    var winner = snapInProgress();
      //    pushOnWinnerStack(snaps, winner);
      //    snapHistory.Push(cpu);
      //}
      //else if(cpu == snaps.Last()) 
      //{

      //    Console.WriteLine($"{cpu}");
      //    var winner = snapInProgress();
      //    pushOnWinnerStack(snaps, winner);
      //    snapHistory.Push(cpu);
      //}
      //else if(p1 == snaps.Last()) 
      //{
      //    Console.WriteLine($"{p1}");
      //    var winner = snapInProgress();
      //    pushOnWinnerStack(snaps, winner);
      //    snapHistory.Push(cpu);
      //}


    }

    var winnerStr = GameWinner();
    winnerStr = (winnerStr.Length) == 0 ? "No one" : winnerStr;
    Console.WriteLine($"Winner!!: {GameWinner()}");
  }

  bool IsStacksEmpty()
  {
    foreach (var player in players)
    {
      if (player.Value.values.Count == 0) return true;
    }
    return false;

  }
  string GameWinner()
  {

    int maxValue = 0;
    string maxPlayer = "";
    foreach (var player in players)
    {
      if (player.Value.values.Count > maxValue)
      {
        maxValue = player.Value.values.Count;
        maxPlayer = player.Key.ToString();
      }
    }

    return maxPlayer;

  }

  void pushOnWinnerStack(Stack<int> snaps, string winner)
  {

    Console.WriteLine($"{winner} SNAPPED!");
    var winnerStack = players[winner];
    foreach (int s in snaps)
    {
      winnerStack.values.Push(s);
    }
  }

  private void Wait(int milliseconds)
  {
    Thread.Sleep(milliseconds);
  }
  string snapInProgress()
  {

    var user = Task.Run(() => { Console.ReadKey(true); });
    var cpu = Task.Delay(1000);
    var names = new[] { "cpu", "player" };
    var waitSnaps = new[] { cpu, user };
    int winnerIndex = Task.WaitAny(waitSnaps);

    return names[winnerIndex];


  }

  static async Task<string> ReadUser()
  {
    string s = await Task.Run(static () => Console.ReadLine());
    return s;
  }
  async Task<long> cpuSnap()
  {
    var watch = System.Diagnostics.Stopwatch.StartNew();
    await Task.Delay(5000);
    watch.Stop();
    return watch.ElapsedMilliseconds;
  }

  //async Task<long> userSnap()
  //{
  //     return await Task.Run(static () => Console.ReadKey(true));

  //}
  private void ReadKey()
  {
    var key = Console.ReadKey(true);
  }

  private string GetInput()
  {
    string userA;
    userA = Console.ReadLine();

    return userA;
  }
}

class Program
{

  static void Main()
  {
    Console.WriteLine("Welcome To Number Snap!!");

    while (true)
    {
      Console.WriteLine("How many values to generate:");
      var userInput = Console.ReadLine();
      try
      {
        int count = Convert.ToInt32(userInput);
        Snap snap = new Snap(count);
        snap.roundStart();

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace);
        Console.WriteLine($"Number not provided but {userInput} was given.");
      };

      Console.WriteLine("Go Again? (y/n) default(y):");
      var goAgain = Console.ReadLine();
      Console.Clear();
      if (goAgain == null | goAgain == "y")
      {

        continue;
      }

      if (goAgain == "n")
      {
        break;
      }

    }

  }
}

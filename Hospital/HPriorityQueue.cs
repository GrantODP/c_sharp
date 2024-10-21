
namespace Hospital.Collections
{

  public class HPriorityQueue<T> where T : IComparable<T>
  {

    private static int maxHeapChilds = 3;
    private MinDHeap<T> heap = new(maxHeapChilds);


    public HPriorityQueue() { }

    public void Push(T value)
    {
      heap.Insert(value);
    }

    public T Pull()
    {
      return heap.ExtractMin();
    }

    public T Peek()
    {
      return heap.PeekMin();
    }

    public void FromList(List<T> list)
    {
      heap.FromList(list);
    }

    public int Count
    {
      get { return heap.items.Count; }
    }
  }
}

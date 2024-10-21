using System.Diagnostics;

namespace Hospital.Collections
{
  public class MinDHeap<T> where T : IComparable<T>
  {


    private int dSize;
    public int MaxChilds
    {
      get { return dSize; }
      set
      {
        Debug.Assert(value >= 2);
        dSize = value;
      }
    }

    public List<T> items = new();
    public MinDHeap(int childCount)
    {
      MaxChilds = childCount;
    }

    public void Insert(T value)
    {
      items.Add(value);
      HeapifyUp(items.Count - 1);

    }

    private void HeapifyUp(int index)
    {

      while (index > 0)
      {
        int parentIndex = (index - 1) / dSize;
        T parent = items[parentIndex];
        T child = items[index];
        var compare = parent.CompareTo(child);


        if (compare > 0)
        {
          //swap
          T temp = parent;
          items[parentIndex] = child;
          items[index] = parent;
          index = parentIndex;
          continue;
        }
        break;

      }


    }
    private bool hasChild(int index)
    {
      return (index * dSize) + 1 < items.Count;
    }
    private void HeapifyDown(int index)
    {
      // Console.WriteLine($"Index:{index}");

      while (hasChild(index))
      {
        var parent = items[index];

        var childIndex = GetSmallestChildToSwap(index);
        if (childIndex == index)
        {
          return;
        }
        //swap
        items[index] = items[childIndex];
        items[childIndex] = parent;
        index = childIndex;




      }
    }

    private int GetSmallestChildToSwap(int index)
    {
      var currentNode = items[index];
      int firstChild = (index * dSize) + 1;
      int lastChild = Math.Min(items.Count - 1, firstChild + dSize - 1);
      // Console.WriteLine($"firstChild:{firstChild}, lastChild:{lastChild}");
      var minValue = currentNode;
      for (int i = lastChild; i >= firstChild; i--)
      {
        // Console.WriteLine($" Comparing i:{i}, to:{index}");
        var compare = minValue.CompareTo(items[i]);

        if (compare > 0)
        {
          index = i;
          minValue = items[i];
        }

      }
      return index;
    }
    public T ExtractMin()
    {

      var lastIndex = items.Count - 1;
      if (lastIndex < 0)
      {
        return default(T);
      }
      else if (lastIndex == 0)
      {

        var item = items[0];
        items.RemoveAt(lastIndex);

        return item;
      }
      //swap first and last
      var temp = items[0];
      items[0] = items[lastIndex];
      items[lastIndex] = temp;
      items.RemoveAt(lastIndex);
      HeapifyDown(0);
      return temp;
    }

    public T PeekMin()
    {
      if (items.Count == 0)
      {
        return default(T);
      }
      return items[0];
    }

    private void BuildHeap()
    {
      var last = items.Count - 1;
      var parent = (last - 1) / dSize;

      for (int i = parent; i >= 0; i--)
      {
        HeapifyDown(i);
      }

    }

    public void FromList(List<T> list)
    {
      items = list;
      BuildHeap();
    }
  }
}


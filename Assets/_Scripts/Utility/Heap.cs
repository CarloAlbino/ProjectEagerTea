using System;

// Interface for all items that will be held in the heap
// Doc: https://msdn.microsoft.com/en-us/library/system.icomparable.compareto(v=vs.110).aspx
public interface IHeapItem<T> : IComparable<T>
{
    int heapIndex
    {
        get;
        set;
    }
}

public class Heap<T> where T : IHeapItem<T>
{
    private T[] m_items;
    private int currentItemCount;

    // Create the heap, constructor
    public Heap(int maxHeapSize)
    {
        m_items = new T[maxHeapSize];
    }

    // Add a new item to the heap
    public void Add(T item)
    {
        item.heapIndex = currentItemCount;  // Set the index of the item
        m_items[currentItemCount] = item;   // Add the item to the heap
        SortUp(item);                       // Sort the heap
        currentItemCount++;                 // Increment the count
    }

    // Remove the first item in the heap
    public T RemoveFirst()
    {
        T firstItem = m_items[0];               // Save first item
        currentItemCount--;                     // Decrement the count of items
        m_items[0] = m_items[currentItemCount]; // Make the last item the first item
        m_items[0].heapIndex = 0;               
        SortDown(m_items[0]);                   // Sort down the new first item, this will re-sort the heap to be valid

        return firstItem;                       // Return the first item
    }

    // Does the heap contain the item passed in
    public bool Contains(T item)
    {
        return Equals(m_items[item.heapIndex], item);
    }

    // Re-sort heap after updating an item in the heap
    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    // Get the count of the heap
    public int count { get { return currentItemCount; } }

    // Sort up the heap, smaller value at the top
    private void SortUp(T item)
    {
        // This operation creates a heap tree format, since this is called everytime an item is added
        // the heap becomes a binary min heap when sorting up
        #region Sort Up Explanation
        // First item (10) is added at 0
        //          10      {10}
        //
        // Second item (6) [parentIndex is (1 - 1) / 2]:0, since 6 is less than 10 they are swapped
        //          6      {6, 10}
        //         / 
        //        10
        //
        // Third item (8) [parentIndex is (2 - 1) / 2]:0, since 8 is larger than 6 the array stays the same
        //          6      {6, 10, 8}
        //         /  \
        //        10   8
        //
        // Fourth item (3) [parentIndex is (3 - 1) / 2]:1, since 3 is less than 10 they are swapped
        //          6      {6, 3, 8, 10}
        //         /  \
        //        3    8
        //       /
        //      10   
        //           *New parentIndex [parentIndex is (1 - 1) / 2]:0, since 3 is less than 6 they are swapped
        //          3      {3, 6, 8, 10}
        //         /  \
        //        6    8
        //       /
        //      10   
        //
        // Fifth item (7) [parentIndex is (4 - 1) / 2]:1, since 7 is greater than 6 the array stays the same
        //          3      {3, 6, 8, 10, 7}
        //         /  \
        //        6    8
        //       / \
        //      10  7  
        #endregion Sort Up Explanation

        int parentIndex = (item.heapIndex - 1) / 2; 
        while(true)
        {
            T parentItem = m_items[parentIndex];
            if(item.CompareTo(parentItem) > 0)  // From docs: Greater than zero: This instance follows obj in the sort order
            {
                Swap(item, parentItem);
            }
            else
            {
                break;
            }
            parentIndex = (item.heapIndex - 1) / 2;
        }
    }

    // Sort down, sort the larger items at the bottom
    private void SortDown(T item)
    {
        while(true)
        {
            // Save the indexes
            int childIndexLeft = item.heapIndex * 2 + 1;
            int childIndexRight = item.heapIndex * 2 + 2;
            int swapIndex = 0;

            if(childIndexLeft < currentItemCount)       // If the index is valid
            {
                // Find the lowest leaf in the heap tree
                swapIndex = childIndexLeft;
                if(childIndexRight < currentItemCount)  // If the index is valid
                {
                    if(m_items[childIndexLeft].CompareTo(m_items[childIndexRight]) < 0) // From docs: Less than zero: This instance precedes obj in the sort order
                    {
                        swapIndex = childIndexRight;
                    }
                }
                if(item.CompareTo(m_items[swapIndex]) < 0)  // Swap if the passed in item is precedes the item at the swap index, swap them
                {
                    Swap(item, m_items[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else // No children
            {
                return;
            }
        }
    }

    // Swap items
    private void Swap(T itemA, T itemB)
    {
        m_items[itemA.heapIndex] = itemB;
        m_items[itemB.heapIndex] = itemA;
        int itemAIndex = itemA.heapIndex;
        itemA.heapIndex = itemB.heapIndex;
        itemB.heapIndex = itemAIndex;
    }
}



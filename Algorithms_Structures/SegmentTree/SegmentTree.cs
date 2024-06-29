namespace Algorithms_Structures.SegmentTree;

public class SegmentTree<T> : ISegmentTree<T> where T : IMergeableValue<T>
{
    private readonly T[] _items;
    private readonly int _size;

    /// <summary>
    /// Complexity :O(items.Count()) 
    /// </summary>
    public static SegmentTree<T> Build(IEnumerable<T> items)
    {
        var array = items.ToArray();
        var segmentTree = new SegmentTree<T>(array.Length);
        segmentTree.Build(array, 0, 0, segmentTree._size);
        return segmentTree;
    }

    private SegmentTree(int n)
    {
        _size = 1 << (int)Math.Ceiling(Math.Log2(n));
        _items = Enumerable.Repeat(T.NeutralElement, 2 * _size).ToArray();
    }

    private void Build(T[] data, int index, int left, int right)
    {
        if (right - left == 1)
        {
            if (data.Length > left)
                _items[index] = data[left];
            return;
        }

        var mid = (left + right) / 2;
        Build(data, GetLeftChildIndex(index), left, mid);
        Build(data, GetRightChildIndex(index), mid, right);

        _items[index] = T.Merge(_items[GetLeftChildIndex(index)], _items[GetRightChildIndex(index)]);
    }

    private void Set(int setIndex, T setValue, int index, int left, int right)
    {
        if (right - left == 1)
        {
            _items[index] = setValue;
            return;
        }

        var mid = (left + right) / 2;
        if (setIndex < mid)
            Set(setIndex, setValue, GetLeftChildIndex(index), left, mid);
        else
            Set(setIndex, setValue, GetRightChildIndex(index), mid, right);

        _items[index] = T.Merge(_items[GetLeftChildIndex(index)], _items[GetRightChildIndex(index)]);
    }

    private T Calculate(int calcLeft, int calcRight, int index, int left, int right)
    {
        if (calcLeft >= right || calcRight <= left) return T.NeutralElement;
        if (right - left == 1 || (calcLeft <= left && calcRight >= right)) return _items[index];

        var mid = (left + right) / 2;
        return T.Merge(
            Calculate(calcLeft, calcRight, GetLeftChildIndex(index), left, mid),
            Calculate(calcLeft, calcRight, GetRightChildIndex(index), mid, right));
    }

    private static int GetRightChildIndex(int i) => 2 * i + 2;
    private static int GetLeftChildIndex(int i) => 2 * i + 1;

    /// <summary>
    /// Complexity : O(log2(_items.Length))
    /// </summary>
    public void Set(int setIndex, T setValue) => Set(setIndex, setValue, 0, 0, _size);

    /// <summary>
    /// Complexity : O(log2(_items.Length))
    /// </summary>
    public T Calculate(int left, int right) => Calculate(left, right, 0, 0, _size);
}

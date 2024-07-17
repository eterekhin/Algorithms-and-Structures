using System.Text;

namespace Algorithms_Structures.SegmentTree.MassSegmentTree;

public class MassSegmentTree<T> : IMassSegmentTree<T> where T : IMassMergeableValue<T>
{
    private readonly T[] _items;
    private readonly int _size;

    public static MassSegmentTree<T> Build(int numberOfElements) => new(numberOfElements); 

    private MassSegmentTree(int n)
    {
        _size = 1 << (int)Math.Ceiling(Math.Log2(n));
        _items = new T[2 * _size];
        const int newValue = 10;

        for (var i = 0; i < _items.Length; i++)
            _items[i] = T.CreateDefaultValue;
    }

    private void Modify(int setLeft, int setRight, T val, int index, int left, int right)
    {
        if (setLeft >= right || setRight <= left) return;
        if (left >= setLeft && right <= setRight)
        {
            var currentItem = _items[index];
            _items[index] = T.ApplyValue(currentItem, val);
            return;
        }

        PropagateToChildren(index);

        var leftChildIndex = GetLeftChildIndex(index);
        var rightChildIndex = GetRightChildIndex(index);

        var mid = (left + right) / 2;
        Modify(setLeft, setRight, val, leftChildIndex, left, mid);
        Modify(setLeft, setRight, val, rightChildIndex, mid, right);
        _items[index] = T.MergeResults(_items[leftChildIndex], _items[rightChildIndex]);
    }

    private void PropagateToChildren(int index)
    {
        Propagate(index, GetLeftChildIndex(index));
        Propagate(index, GetRightChildIndex(index));
        
        // After propagation the value should be reassigned as it happens now in Modify and Get methods
        _items[index] = T.CreateDefaultValue;
    }

    private void Propagate(int parentIndex, int childIndex) =>
        _items[childIndex] = GetPropagatedToChildValue(_items[childIndex], _items[parentIndex]);

    private static T GetPropagatedToChildValue(T child, T parent) => 
        parent.HasNothingToPropagate() ? child : T.ApplyValue(child, parent);

    private T Get(int getLeft, int getRight, int index, int left, int right)
    {
        if (getLeft >= right || getRight <= left) return T.NeutralResultValue;
        if (left >= getLeft && right <= getRight) return _items[index];

        PropagateToChildren(index);

        var leftChildIndex = GetLeftChildIndex(index);
        var rightChildIndex = GetRightChildIndex(index);
        _items[index] = T.MergeResults(_items[leftChildIndex], _items[rightChildIndex]);

        var mid = (left + right) / 2;
        var leftChildResult = Get(getLeft, getRight, leftChildIndex, left, mid);
        var rightChildResult = Get(getLeft, getRight, rightChildIndex, mid, right);
        return T.MergeResults(leftChildResult, rightChildResult);
    }

    private static int GetRightChildIndex(int i) => 2 * i + 2;
    private static int GetLeftChildIndex(int i) => 2 * i + 1;
    
    public void Modify(int left, int right, T value) => Modify(left, right, value, 0, 0, _size);

    public T Get(int left, int right) => Get(left, right, 0, 0, _size);

    public override string ToString()
    {
        var builder = new StringBuilder();
        for (var powerOf2 = 0; 1 << powerOf2 < _items.Length; powerOf2++)
        {
            for (int start = (1 << powerOf2) - 1, end = (1 << (powerOf2 + 1)) - 1; start < end; start++)
                builder.Append(_items[start]);

            builder.AppendLine();
        }

        return builder.ToString();
    }
}
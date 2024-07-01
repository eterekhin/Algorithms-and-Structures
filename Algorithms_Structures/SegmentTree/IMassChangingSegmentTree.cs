namespace Algorithms_Structures.SegmentTree;

public interface IMassModifyingSegmentTree<T>
{
    T Get(int index);
    void Modify(int left, int right, T value);
}

public interface IMassModifyingAndTakingSegmentTree<T>
{
    T Get(int left, int right);
    void Modify(int left, int right, T value);
}

public class MassModifyingSegmentTree<T> : IMassModifyingSegmentTree<T>
{
    public T Get(int index)
    {
        throw new NotImplementedException();
    }

    public void Modify(int left, int right, T value)
    {
        throw new NotImplementedException();
    }
}

public class MassModifyingAndTakingSegmentTree<T> : IMassModifyingAndTakingSegmentTree<T>
{
    public T Get(int left, int right)
    {
        throw new NotImplementedException();
    }

    public void Modify(int left, int right, T value)
    {
        throw new NotImplementedException();
    }
}
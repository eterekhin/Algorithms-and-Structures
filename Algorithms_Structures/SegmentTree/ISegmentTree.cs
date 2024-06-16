
namespace Algorithms_Structures.SegmentTree;

public interface IMergeableValue<TSelf> where TSelf : IMergeableValue<TSelf>
{
    public static abstract TSelf Merge(TSelf x, TSelf y);
    public static abstract TSelf NeutralElement { get; }
}

public interface ISegmentTree<T> where T : IMergeableValue<T>
{
    /// <summary>
    /// Changes an element value and re-calculate the parent nodes' values
    /// </summary>
    void Set(int setIndex, T setValue);

    ///<summary>
    /// Calculates value on [left ... right)  segment
    /// </summary>
    T Calculate(int left, int right);
}
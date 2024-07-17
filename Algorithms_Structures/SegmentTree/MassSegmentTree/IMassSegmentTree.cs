namespace Algorithms_Structures.SegmentTree.MassSegmentTree;

public interface IMassMergeableValue<TSelf>
{
    public bool HasNothingToPropagate();
    public static abstract TSelf CreateDefaultValue { get; }
    public static abstract TSelf NeutralResultValue { get; }
    public static abstract TSelf MergeResults(TSelf left, TSelf right);
    public static abstract TSelf ApplyValue(TSelf a, TSelf value);

    public long ValueAppliedToSegment { get; }
    public long ResultValue { get; }
}

public interface IMassSegmentTree<T> where T : IMassMergeableValue<T>
{
    T Get(int left, int right);
    void Modify(int left, int right, T value);
}
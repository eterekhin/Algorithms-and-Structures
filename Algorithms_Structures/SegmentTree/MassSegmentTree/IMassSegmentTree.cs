namespace Algorithms_Structures.SegmentTree.MassSegmentTree;

public interface IMassMergeableValue<TSelf>
{
    bool HasNothingToPropagate();
    static abstract TSelf CreateDefaultValue { get; }
    static abstract TSelf NeutralResultValue { get; }
    static abstract TSelf MergeResults(TSelf left, TSelf right);
    static abstract TSelf ApplyValue(TSelf a, TSelf value);
}

public interface IMassSegmentTree<T> where T : IMassMergeableValue<T>
{
    T Get(int left, int right);
    void Modify(int left, int right, T value);
}
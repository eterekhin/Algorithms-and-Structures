using Algorithms_Structures.SegmentTree.MassSegmentTree;

namespace Algorithms_Structures.Tests;

public class MassSegmentTreeTests
{
    private record OrAndValue(long ValueAppliedToSegment, long ResultValue) : IMassMergeableValue<OrAndValue>
    {
        private const long OperationOnSegmentDefaultValue = 0;
        public static OrAndValue CreateDefaultValue { get; } = new(OperationOnSegmentDefaultValue, 0);
        public static OrAndValue NeutralResultValue { get; } = new(OperationOnSegmentDefaultValue, long.MaxValue);

        public static OrAndValue MergeResults(OrAndValue left, OrAndValue right) =>
            new(OperationOnSegmentDefaultValue, left.ResultValue & right.ResultValue);

        public static OrAndValue ApplyValue(OrAndValue a, OrAndValue value)
        {
            return new OrAndValue(OperateOnSegment(a.ValueAppliedToSegment, value.ValueAppliedToSegment),
                OperateOnSegment(a.ResultValue, value.ResultValue));

            static long OperateOnSegment(long a, long b) => a | b;
        }

        public static OrAndValue CreateResultValue(long result) => new(result, result);
        
        public bool HasNothingToPropagate() => ValueAppliedToSegment == OperationOnSegmentDefaultValue;

        public override string ToString() =>
            $"\"|\" to apply on segment: {ValueAppliedToSegment}, \"&\" result on segment: {ResultValue}    ";
    }

    [Fact]
    public void SimpleTest()
    {
        var tree = MassSegmentTree<OrAndValue>.Build(10);

        const int newValue = 10;
        tree.Modify(0, 10, OrAndValue.CreateResultValue(newValue));
        
        Assert.Equal(10, tree.Get(0, newValue).ResultValue);
    }

    [Fact]
    public void SequentialModificationTest()
    {
        var tree = MassSegmentTree<OrAndValue>.Build(10);

        var newVal = OrAndValue.CreateResultValue(10);
        for (var i = 1; i <= 10; i++)
            tree.Modify(i - 1, i, newVal);
        
        Assert.Equal(10, tree.Get(0, 10).ResultValue);
    }

    [Fact]
    public void PropagationTest()
    {
        var tree = MassSegmentTree<OrAndValue>.Build(10);
        
        tree.Modify(0, 4, OrAndValue.CreateResultValue(4));
        
        Assert.Equal(4, tree.Get(0, 2).ResultValue);
    }
}
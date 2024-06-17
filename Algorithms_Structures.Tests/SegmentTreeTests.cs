using Algorithms_Structures.SegmentTree;

namespace Algorithms_Structures.Tests;

public class SegmentTreeTests
{
    private record struct SumIntItem(int Value) : IMergeableValue<SumIntItem>
    {
        public static SumIntItem Merge(SumIntItem x, SumIntItem y) => x.Value + y.Value;

        public static SumIntItem NeutralElement => 0;

        public static implicit operator SumIntItem(int value) => new(value);
        public static implicit operator int(SumIntItem value) => value.Value;
    }

    private record struct MultIntItem(int Value) : IMergeableValue<MultIntItem>
    {
        public static MultIntItem Merge(MultIntItem x, MultIntItem y) => x.Value * y.Value;
        public static MultIntItem NeutralElement => 1;
        public static implicit operator MultIntItem(int value) => new(value);
        public static implicit operator int(MultIntItem value) => value.Value;
    }
    
    [Fact]
    public void SimpleTest()
    {
        var tree = SegmentTree<SumIntItem>.Build([1, 2, 3, 4, 5, 6]);

        Assert.Equal(21, tree.Calculate(0, 6).Value);
        Assert.Equal(15, tree.Calculate(0, 5).Value);
        Assert.Equal(20, tree.Calculate(1, 6).Value);
        Assert.Equal(7, tree.Calculate(2, 4).Value);
        Assert.Equal(1, tree.Calculate(0, 1).Value);
        Assert.Equal(5, tree.Calculate(4, 5).Value);

        tree.Set(0, 10);
        Assert.Equal(10, tree.Calculate(0, 1).Value);
        tree.Set(1, 20);
        Assert.Equal(30, tree.Calculate(0, 2).Value);
        tree.Set(2, 30);
        Assert.Equal(60, tree.Calculate(0, 3).Value);
        tree.Set(3, 40);
        Assert.Equal(100, tree.Calculate(0, 4).Value);
        tree.Set(4, 50);
        Assert.Equal(150, tree.Calculate(0, 5).Value);
        tree.Set(5, 60);
        Assert.Equal(210, tree.Calculate(0, 6).Value);
    }

    [Fact]
    public void RightAddedItemsInitializedWithNeutralElementTest()
    {
        var tree = SegmentTree<MultIntItem>.Build([1, 2, 3]);
        Assert.Equal(6, tree.Calculate(0, 4).Value);
    }
}
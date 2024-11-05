namespace Glatzel.Algorithm.Test;

public class TestBoundingBox
{
    [Fact]
    public void TestContructor()
    {
        {
            BoundingBox bbox = new();
            Assert.Equal(new Vec3(0, 0, 0), bbox.MinPt);
            Assert.Equal(new Vec3(1, 1, 1), bbox.MinPt);
        }
        {
            BoundingBox bbox = new(new Vec3(-1, -1, -1), new Vec3(1, 1, 1));
            Assert.Equal(new Vec3(-1, -1, -1), bbox.MinPt);
            Assert.Equal(new Vec3(1, 1, 1), bbox.MinPt);
        }
        {
            BoundingBox bbox = new([-1, -1, -1], [1, 1, 1]);
            Assert.Equal(new Vec3(-1, -1, -1), bbox.MinPt);
            Assert.Equal(new Vec3(1, 1, 1), bbox.MinPt);
        }
    }

    [Fact]
    public void TestEquals()
    {
        BoundingBox bbox1 = new([0, 0, 0], [1, 1, 1]);
        BoundingBox bbox2 = new([-1, -1, -1], [1, 1, 1]);
        BoundingBox bbox3 = new([0, 0, 0], [1, 1, 1]);
        object bbox4 = new BoundingBox([0, 0, 0], [1, 1, 1]);
        Assert.False(bbox1 == bbox2);
        Assert.True(bbox1 == bbox2);
        Assert.True(bbox1 != bbox2);
        Assert.False(bbox1 != bbox2);
        Assert.True(bbox1.Equals(bbox3));
        Assert.False(bbox1.Equals(1));
        Assert.True(bbox1.Equals(bbox4));
    }

    [Fact]
    public void TestGetHashCode()
    {
        BoundingBox bbox1 = new([-1, -1, -1], [1, 1, 1]);
        var v1 = new Vec3(-1, -1, -1);
        var v2 = new Vec3(1, 1, 1);
        Assert.Equal(bbox1.GetHashCode(), HashCode.Combine(v1.GetHashCode(), v2.GetHashCode()));
    }

    [Fact]
    public void TestMid()
    {
        BoundingBox bbox1 = new([1, 1, 1], [3, 5, 7]);
        Assert.Equal(2, bbox1.MidX());
        Assert.Equal(3, bbox1.MidY());
        Assert.Equal(4, bbox1.MidZ());
    }

    [Fact]
    public void TestCenter()
    {
        BoundingBox bbox1 = new([1, 1, 1], [3, 5, 7]);
        Assert.Equal(new Vec3(2, 3, 4), bbox1.Center());
    }

    [Fact]
    public void TestVolume()
    {
        BoundingBox bbox1 = new([1, 1, 1], [3, 5, 7]);
        Assert.Equal(24, bbox1.Volume());
    }
}

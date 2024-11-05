namespace Glatzel.Algorithm.Test;

public class TestBoundingBox
{
    [Fact]
    public void TestCenter()
    {
        BoundingBox bbox1 = new([1, 1, 1], [3, 5, 7]);
        Assert.Equal(new Vec3(2, 3, 4), bbox1.Center());
    }

    [Fact]
    public void TestCheck()
    {
        BoundingBox bbox1 = new([0, 0, 0], [1, 1, 1]);
        BoundingBox bbox2 = new([2, 0, 0], [1, 1, 1]);
        BoundingBox bbox3 = new([0, 2, 0], [1, 1, 1]);
        BoundingBox bbox4 = new([0, 0, 2], [1, 1, 1]);
        BoundingBox bbox5 = new([2, 2, 0], [1, 1, 1]);
        BoundingBox bbox6 = new([2, 0, 2], [1, 1, 1]);
        BoundingBox bbox7 = new([0, 2, 2], [1, 1, 1]);
        BoundingBox bbox8 = new([2, 2, 2], [1, 1, 1]);
        bbox1.Check();
        Assert.ThrowsAny<Exception>(bbox2.Check);
        Assert.ThrowsAny<Exception>(bbox3.Check);
        Assert.ThrowsAny<Exception>(bbox4.Check);
        Assert.ThrowsAny<Exception>(bbox5.Check);
        Assert.ThrowsAny<Exception>(bbox6.Check);
        Assert.ThrowsAny<Exception>(bbox7.Check);
        Assert.ThrowsAny<Exception>(bbox8.Check);
    }

    [Fact]
    public void TestContructor()
    {
        {
            BoundingBox bbox = new();
            Assert.Equal(new Vec3(0, 0, 0), bbox.MinPt);
            Assert.Equal(new Vec3(1, 1, 1), bbox.MaxPt);
        }
        {
            BoundingBox bbox = new(new Vec3(-1, -1, -1), new Vec3(1, 1, 1));
            Assert.Equal(new Vec3(-1, -1, -1), bbox.MinPt);
            Assert.Equal(new Vec3(1, 1, 1), bbox.MaxPt);
        }
        {
            BoundingBox bbox = new([-1, -1, -1], [1, 1, 1]);
            Assert.Equal(new Vec3(-1, -1, -1), bbox.MinPt);
            Assert.Equal(new Vec3(1, 1, 1), bbox.MaxPt);
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
        Assert.True(bbox1 == bbox3);
        Assert.True(bbox1 != bbox2);
        Assert.False(bbox1 != bbox3);
        Assert.True(bbox1.Equals(bbox3));
        Assert.False(bbox1.Equals(1));
        Assert.True(bbox1.Equals(bbox4));
    }

    [Fact]
    public void TestGetHashCode()
    {
        BoundingBox bbox1 = new([-1, -1, -1], [1, 1, 1]);
        Vec3 v1 = new(-1.0, -1.0, -1.0);
        Vec3 v2 = new(1.0, 1.0, 1.0);
        Assert.Equal(bbox1.GetHashCode(), HashCode.Combine(v1.GetHashCode(), v2.GetHashCode()));
    }

    [Fact]
    public void TestIntersect()
    {
        {
            BoundingBox bbox1 = new([1, 0, 2], [2, 5, 3]);
            BoundingBox bbox2 = new([0, 1, 0], [2, 4, 4]);
            BoundingBox bbox = BoundingBox.Intersect(bbox1, bbox2);
            Assert.Equal(new Vec3(1, 1, 2), bbox.MinPt);
            Assert.Equal(new Vec3(2, 4, 3), bbox.MaxPt);
        }
        {
            BoundingBox bbox1 = new([1, 0, 2], [2, 5, 3]);
            BoundingBox bbox2 = new([3, 1, 0], [2, 4, 4]);
            Assert.ThrowsAny<Exception>(() => BoundingBox.Intersect(bbox1, bbox2));
        }
    }

    [Fact]
    public void TestIsIntersect() { }

    [Fact]
    public void TestLength()
    {
        BoundingBox bbox1 = new([1, 1, 1], [3, 5, 7]);
        Assert.Equal(2, bbox1.LengthX());
        Assert.Equal(4, bbox1.LengthY());
        Assert.Equal(6, bbox1.LengthZ());
    }

    [Fact]
    public void TestMaxAxis()
    {
        BoundingBox bbox1 = new([0, 0, 0], [3, 2, 1]);
        BoundingBox bbox3 = new([0, 0, 0], [1, 2, 3]);
        BoundingBox bbox2 = new([0, 0, 0], [2, 3, 1]);
        Assert.Equal(Axis.X, bbox1.MaxAxis());
        Assert.Equal(Axis.Y, bbox2.MaxAxis());
        Assert.Equal(Axis.Z, bbox3.MaxAxis());
    }

    [Fact]
    public void TestMinAxis()
    {
        BoundingBox bbox1 = new([0, 0, 0], [1, 2, 3]);
        BoundingBox bbox3 = new([0, 0, 0], [2, 1, 3]);
        BoundingBox bbox2 = new([0, 0, 0], [3, 2, 1]);
        Assert.Equal(Axis.X, bbox1.MinAxis());
        Assert.Equal(Axis.Y, bbox2.MinAxis());
        Assert.Equal(Axis.Z, bbox3.MinAxis());
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
    public void TestOffset()
    {
        {
            BoundingBox bbox0 = new([0, 0, 0], [1, 1, 1]);
            var bbox = BoundingBox.Offset(bbox0, 2);
            Assert.Equal(new Vec3(-1, -1, -1), bbox.MinPt);
            Assert.Equal(new Vec3(2, 2, 2), bbox.MaxPt);
        }
        {
            BoundingBox bbox0 = new([0, 0, 0], [1, 1, 1]);
            var bbox = BoundingBox.Offset(bbox0, new Vec3(-2, -3, -1), new Vec3(2, 3, 4));
            Assert.Equal(new Vec3(-2, -3, -1), bbox.MinPt);
            Assert.Equal(new Vec3(3, 4, 5), bbox.MaxPt);
        }
        {
            BoundingBox bbox = new([0, 0, 0], [1, 1, 1]);
            bbox.Offset(2);
            Assert.Equal(new Vec3(-1, -1, -1), bbox.MinPt);
            Assert.Equal(new Vec3(2, 2, 2), bbox.MaxPt);
        }
        {
            BoundingBox bbox = new([0, 0, 0], [1, 1, 1]);
            bbox.Offset(new Vec3(-2, -3, -1), new Vec3(2, 3, 4));
            Assert.Equal(new Vec3(-2, -3, -1), bbox.MinPt);
            Assert.Equal(new Vec3(3, 4, 5), bbox.MaxPt);
        }
    }

    [Fact]
    public void TestScale()
    {
        {
            BoundingBox bbox = new([0, 0, 0], [2, 2, 2]);
            bbox.Scale(2);
            Assert.Equal(new Vec3(-1, -1, -1), bbox.MinPt);
            Assert.Equal(new Vec3(3, 3, 3), bbox.MaxPt);
        }
        {
            BoundingBox bbox = new([0, 0, 0], [2, 2, 2]);
            bbox.Scale(new Vec3(2, 3, 4));
            Assert.Equal(new Vec3(-1, -2, -3), bbox.MinPt);
            Assert.Equal(new Vec3(3, 4, 5), bbox.MaxPt);
        }
        {
            BoundingBox bbox0 = new([0, 0, 0], [2, 2, 2]);
            var bbox = BoundingBox.Scale(bbox0, 2);
            Assert.Equal(new Vec3(-1, -1, -1), bbox.MinPt);
            Assert.Equal(new Vec3(3, 3, 3), bbox.MaxPt);
        }
        {
            BoundingBox bbox0 = new([0, 0, 0], [2, 2, 2]);
            var bbox = BoundingBox.Scale(bbox0, new Vec3(2, 3, 4));
            Assert.Equal(new Vec3(-1, -2, -3), bbox.MinPt);
            Assert.Equal(new Vec3(3, 4, 5), bbox.MaxPt);
        }
    }

    [Fact]
    public void TestUnion()
    {
        BoundingBox bbox1 = new([1, 2, 3], [7, 8, 9]);
        BoundingBox bbox2 = new([3, 2, 1], [7, 8, 9]);
        BoundingBox bbox3 = new([2, 3, 1], [7, 8, 9]);
        BoundingBox bbox4 = new([1, 1, 3], [7, 8, 9]);
        BoundingBox bbox5 = new([1, 2, 3], [9, 8, 7]);
        BoundingBox bbox6 = new([1, 2, 3], [8, 9, 7]);
        BoundingBox bbox = BoundingBox.Union(bbox1, bbox2, bbox3, bbox4, bbox5, bbox6);
        Assert.Equal(new Vec3(1, 1, 1), bbox.MinPt);
        Assert.Equal(new Vec3(9, 9, 9), bbox.MaxPt);
    }

    [Fact]
    public void TestVolume()
    {
        BoundingBox bbox1 = new([1, 1, 1], [3, 5, 7]);
        Assert.Equal(48, bbox1.Volume());
    }
}

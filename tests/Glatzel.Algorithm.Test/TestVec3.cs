namespace Glatzel.Algorithm.Test;

public class TestVec3
{
    [Fact]
    public void TestConstructor()
    {
        {
            Vec3 v = new();
            Assert.Equal(0, v.X);
            Assert.Equal(0, v.Y);
            Assert.Equal(0, v.Z);
        }
        {
            Vec3 v = new(1, 2, 3);
            Assert.Equal(1, v.X);
            Assert.Equal(2, v.Y);
            Assert.Equal(3, v.Z);
        }
        {
            Vec3 v = new([1, 2, 3]);
            Assert.Equal(1, v.X);
            Assert.Equal(2, v.Y);
            Assert.Equal(3, v.Z);
        }
        {
            Assert.Throws<ArgumentException>(() => new Vec3([1, 2]));
        }
    }

    [Fact]
    public void TesOne()
    {
        Vec3 v = Vec3.One;
        Assert.Equal(1, v.X);
        Assert.Equal(1, v.Y);
        Assert.Equal(1, v.Z);
    }

    [Fact]
    public void TestUnitX()
    {
        Vec3 v = Vec3.UnitX;
        Assert.Equal(1, v.X);
        Assert.Equal(0, v.Y);
        Assert.Equal(0, v.Z);
    }

    [Fact]
    public void TestUnitY()
    {
        Vec3 v = Vec3.UnitY;
        Assert.Equal(0, v.X);
        Assert.Equal(1, v.Y);
        Assert.Equal(0, v.Z);
    }

    [Fact]
    public void TestUnitZ()
    {
        Vec3 v = Vec3.UnitZ;
        Assert.Equal(0, v.X);
        Assert.Equal(0, v.Y);
        Assert.Equal(1, v.Z);
    }

    [Fact]
    public void TestZero()
    {
        Vec3 v = Vec3.Zero;
        Assert.Equal(0, v.X);
        Assert.Equal(0, v.Y);
        Assert.Equal(0, v.Z);
    }

    [Fact]
    public void TestIndex()
    {
        {
            Vec3 v = new(1, 2, 3);
            Assert.Equal(1, v[0]);
            Assert.Equal(2, v[1]);
            Assert.Equal(3, v[2]);
            Assert.Throws<ArgumentException>(() => v[3]);
        }
        {
            Vec3 v = new(1, 1, 1);
            v[0] = 2;
            v[1] = 3;
            v[2] = 4;
            Assert.Equal(2, v[0]);
            Assert.Equal(3, v[1]);
            Assert.Equal(4, v[2]);
            Assert.Throws<ArgumentException>(() => v[3] = 5);
        }
    }

    [Fact]
    public void TestAdd()
    {
        {
            Vec3 u = new(1, 2, 3);
            Vec3 v = new(1, 2, 3);
            Vec3 result = Vec3.Add(u, v);
            Assert.Equal(2, result.X);
            Assert.Equal(4, result.Y);
            Assert.Equal(6, result.Z);
        }
        {
            Vec3 u = new(1, 2, 3);
            Vec3 result = Vec3.Add(u, 4);
            Assert.Equal(5, result.X);
            Assert.Equal(6, result.Y);
            Assert.Equal(7, result.Z);
        }
        {
            Vec3 u = new(1, 2, 3);
            Vec3 v = new(1, 2, 3);
            Vec3 result = new();
            Vec3.Add(u, v, ref result);
            Assert.Equal(2, result.X);
            Assert.Equal(4, result.Y);
            Assert.Equal(6, result.Z);
        }
        {
            Vec3 u = new(1, 2, 3);
            Vec3.Add(u, 4, ref u);
            Assert.Equal(5, u.X);
            Assert.Equal(6, u.Y);
            Assert.Equal(7, u.Z);
        }
    }

    [Fact]
    public void TestCross()
    {
        Vec3 u = new(1, 2, 3);
        Vec3 v = new(2, 4, 7);
        Vec3 result = Vec3.Cross(u, v);
        Assert.Equal(2, result.X);
        Assert.Equal(-1, result.Y);
        Assert.Equal(0, result.Z);
    }

    [Fact]
    public void TestDivide()
    {
        {
            Vec3 u = new(4, 6, 15);
            Vec3 v = new(1, 2, 3);
            Vec3 result = Vec3.Divide(u, v);
            Assert.Equal(4, result.X);
            Assert.Equal(3, result.Y);
            Assert.Equal(5, result.Z);
        }
        {
            Vec3 u = new(3, 9, 15);
            Vec3 result = Vec3.Divide(u, 3);
            Assert.Equal(1, result.X);
            Assert.Equal(3, result.Y);
            Assert.Equal(5, result.Z);
        }
        {
            Vec3 u = new(4, 6, 15);
            Vec3 v = new(1, 2, 3);
            Vec3 result = new();
            Vec3.Divide(u, v, ref result);
            Assert.Equal(4, result.X);
            Assert.Equal(3, result.Y);
            Assert.Equal(5, result.Z);
        }
        {
            Vec3 u = new(3, 9, 15);
            Vec3.Divide(u, 3, ref u);
            Assert.Equal(1, u.X);
            Assert.Equal(3, u.Y);
            Assert.Equal(5, u.Z);
        }
    }

    [Fact]
    public void TestDot()
    {
        Vec3 u = new(1, 2, 3);
        Vec3 v = new(3, 4, 5);
        double result = Vec3.Dot(u, v);
        Assert.Equal(26, result);
    }
}

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
    public void TestIndex()
    {
        Vec3 v = new(1, 2, 3);
        Assert.Equal(1, v[0]);
        Assert.Equal(2, v[1]);
        Assert.Equal(3, v[2]);
        Assert.Throws<ArgumentOutOfRangeException>(() => v[3]);
    }
}

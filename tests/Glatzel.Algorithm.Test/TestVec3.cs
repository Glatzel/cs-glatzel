namespace Glatzel.Algorithm.Test;

public class TestVec3
{
    [Fact]
    public void PassingTest()
    {
        Assert.Equal(2, new Vec3(1, 2, 3).Y);
    }

    [Fact]
    public void FailTest()
    {
        Assert.Equal(1, new Vec3(1, 2, 3).Y);
    }
}

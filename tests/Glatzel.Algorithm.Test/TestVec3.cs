namespace Glatzel.Algorithm.Test;

public class TestVec3
{
    [Fact]
    public void TestConstructor()
    {
        {
            var v = new Vec3();
            Assert.Equal(0, v.X);
            Assert.Equal(0, v.Y);
            Assert.Equal(0, v.Z);
        }
        {
            var v = new Vec3(1, 2, 3);
            Assert.Equal(1, v.X);
            Assert.Equal(2, v.Y);
            Assert.Equal(3, v.Z);
        }
        {
            var v = new Vec3([1, 2, 3]);
            Assert.Equal(1, v.X);
            Assert.Equal(2, v.Y);
            Assert.Equal(3, v.Z);
        }
        {
            Assert.Throws<ArgumentException>(() => new Vec3([1, 2]));
        }
    }
}

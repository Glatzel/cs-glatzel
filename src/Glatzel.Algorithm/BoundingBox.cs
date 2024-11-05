using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Glatzel.Algorithm;

public struct BoundingBox : IEquatable<BoundingBox>
{
    private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

    public BoundingBox()
    {
        MinPt = new Vec3();
        MaxPt = new Vec3(1, 1, 1);
    }

    public BoundingBox(Vec3 minPt, Vec3 maxPt)
    {
        MinPt = minPt;
        MaxPt = maxPt;
    }

    public BoundingBox(double[] minPt, double[] maxPt)
    {
        MinPt = new Vec3(minPt);
        MaxPt = new Vec3(maxPt);
    }

    public Vec3 MaxPt { get; set; }
    public Vec3 MinPt { get; set; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BoundingBox Intersect(params BoundingBox[] bboxs)
    {
        Vec3 maxpt = new();
        Vec3 minpt = new();
        List<BoundingBox> listBBox = [.. bboxs];
        maxpt.X = listBBox.Min(p => p.MaxPt.X);
        maxpt.Y = listBBox.Min(p => p.MaxPt.Y);
        maxpt.Z = listBBox.Min(p => p.MaxPt.Z);

        minpt.X = listBBox.Max(p => p.MinPt.X);
        minpt.Y = listBBox.Max(p => p.MinPt.Y);
        minpt.Z = listBBox.Max(p => p.MinPt.Z);

        BoundingBox outbbox = new(minpt, maxpt);
        outbbox.Check();
        return outbbox;
    }

    //https://developer.mozilla.org/en-US/docs/Games/Techniques/3D_collision_detection#aabb_vs._aabb
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsIntersect(BoundingBox bbox1, BoundingBox bbox2) =>
        bbox1.MinPt.X <= bbox2.MaxPt.X
        && bbox1.MaxPt.X >= bbox2.MinPt.X
        && bbox1.MinPt.Y <= bbox2.MaxPt.Y
        && bbox1.MaxPt.Y >= bbox2.MinPt.Y
        && bbox1.MinPt.Z <= bbox2.MaxPt.Z
        && bbox1.MaxPt.Z >= bbox2.MinPt.Z;

    public static bool operator !=(BoundingBox left, BoundingBox right)
    {
        return !(left == right);
    }

    public static bool operator ==(BoundingBox left, BoundingBox right)
    {
        return left.Equals(right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BoundingBox Union(params BoundingBox[] bboxs)
    {
        Vec3 maxpt = new(x: double.MinValue, double.MinValue, double.MinValue);
        Vec3 minpt = new(double.MaxValue, double.MaxValue, double.MaxValue);
        List<BoundingBox> listBBox = [.. bboxs];
        maxpt.X = listBBox.Max(p => p.MaxPt.X);
        maxpt.Y = listBBox.Max(p => p.MaxPt.Y);
        maxpt.Z = listBBox.Max(p => p.MaxPt.Z);

        minpt.X = listBBox.Min(p => p.MinPt.X);
        minpt.Y = listBBox.Min(p => p.MinPt.Y);
        minpt.Z = listBBox.Min(p => p.MinPt.Z);

        return new BoundingBox(minpt, maxpt);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Vec3 Center() => new(MidX(), MidY(), MidZ());

    public readonly void Check()
    {
        if (MaxPt.X < MinPt.X || MaxPt.Y < MinPt.Y || MaxPt.Z < MinPt.Z)
        {
            string msg =
                $"MaxPt({MaxPt.X}, {MaxPt.Y}, {MaxPt.Z})< MinPt({MinPt.X}, {MinPt.Y}, {MinPt.Z})";
            Log.Error(msg);
            throw new ArithmeticException(msg);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool Equals(BoundingBox other) => MinPt == other.MinPt && MaxPt == other.MaxPt;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override readonly bool Equals(object obj) =>
        obj is BoundingBox boundingBox && Equals(boundingBox);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override readonly int GetHashCode() =>
        HashCode.Combine(MinPt.GetHashCode(), MaxPt.GetHashCode());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly double LengthX() => MaxPt.X - MinPt.X;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly double LengthY() => MaxPt.Y - MinPt.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly double LengthZ() => MaxPt.Z - MinPt.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Axis MaxAxis()
    {
        if (LengthX() >= LengthY() && LengthX() >= LengthZ())
            return Axis.X;
        else if (LengthY() >= LengthZ())
            return Axis.Y;
        else
            return Axis.Z;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly double MidX() => (MaxPt.X + MinPt.X) / 2.0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly double MidY() => (MaxPt.Y + MinPt.Y) / 2.0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly double MidZ() => (MaxPt.Z + MinPt.Z) / 2.0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly BoundingBox Offset(double offset)
    {
        MinPt.Add(-offset / 2.0);
        MaxPt.Add(offset / 2.0);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BoundingBox Scale(double scale)
    {
        Vec3 center = Center();
        scale--;
        MinPt = new Vec3(
            MinPt.X + ((MinPt.X - center.X) * scale),
            MinPt.Y + ((MinPt.Y - center.Y) * scale),
            MinPt.Z + ((MinPt.Z - center.Z) * scale)
        );

        MaxPt = new Vec3(
            MaxPt.X + ((MaxPt.X - center.X) * scale),
            MaxPt.Y + ((MaxPt.Y - center.Y) * scale),
            MaxPt.Z + ((MaxPt.Z - center.Z) * scale)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly double Volume() => LengthX() * LengthY() * LengthZ();
}

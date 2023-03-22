using System.Drawing;

public static class PointExtensions
{
    public static Point Multiply(this Point point, float scalar)
    {
        return new Point((int)(point.X * scalar), (int)(point.Y * scalar));
    }

    public static Point Divide(this Point point, float scalar)
    {
        return new Point((int)(point.X / scalar), (int)(point.Y / scalar));
    }
    public static Point Multiply(this Point point, double scalar)
    {
        return new Point((int)(point.X * scalar), (int)(point.Y * scalar));
    }

    public static Point Divide(this Point point, double scalar)
    {
        return new Point((int)(point.X / scalar), (int)(point.Y / scalar));
    }
}
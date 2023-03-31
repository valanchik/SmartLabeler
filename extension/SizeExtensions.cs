using System.Drawing;


public static class SizeExtensions
{
    public static Size Multiply(this Size size, float scalar)
    {
        return new Size((int)(size.Width * scalar), (int)(size.Height * scalar));
    }

    public static Size Divide(this Size size, float scalar)
    {
        return new Size((int)(size.Width / scalar), (int)(size.Height / scalar));
    }
    public static Size Multiply(this Size size, double scalar)
    {
        return new Size((int)(size.Width * scalar), (int)(size.Height * scalar));
    }

    public static Size Divide(this Size size, double scalar)
    {
        return new Size((int)(size.Width / scalar), (int)(size.Height / scalar));
    }
}



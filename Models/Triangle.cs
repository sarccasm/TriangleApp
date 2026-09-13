using System.Globalization;

namespace TriangleApp.Models;

public class Triangle
{
    public double Side1 { get; set; }
    public double Side2 { get; set; }
    public double Side3 { get; set; }

    public Triangle()
    {
    }

    public Triangle(double side1, double side2, double side3)
    {
        Side1 = side1;
        Side2 = side2;
        Side3 = side3;
    }

    public double[] GetOrderedSides()
    {
        var sides = new[] { Side1, Side2, Side3 };
        Array.Sort(sides);
        return sides;
    }

    public bool IsValid()
    {
        var sides = GetOrderedSides();

        if (sides.Any(side => double.IsNaN(side) || double.IsInfinity(side) || side <= 0))
            return false;

        return sides[0] + sides[1] > sides[2];
    }

    public double GetPerimeter() => Side1 + Side2 + Side3;

    public double GetArea()
    {
        var s = GetPerimeter() / 2.0;
        return Math.Sqrt(s * (s - Side1) * (s - Side2) * (s - Side3));
    }

    public bool IsRightAngled()
    {
        var sides = GetOrderedSides();
        var a = sides[0];
        var b = sides[1];
        var c = sides[2];

        return NearlyEqual(a * a + b * b, c * c);
    }

    public bool IsEquilateral() =>
        NearlyEqual(Side1, Side2) && NearlyEqual(Side1, Side3);

    public bool IsIsosceles() =>
        NearlyEqual(Side1, Side2) ||
        NearlyEqual(Side1, Side3) ||
        NearlyEqual(Side2, Side3);

    public bool IsCongruentTo(Triangle other)
    {
        var first = GetOrderedSides();
        var second = other.GetOrderedSides();

        return NearlyEqual(first[0], second[0]) &&
               NearlyEqual(first[1], second[1]) &&
               NearlyEqual(first[2], second[2]);
    }

    public bool IsSimilarTo(Triangle other)
    {
        var first = GetOrderedSides();
        var second = other.GetOrderedSides();

        return NearlyEqual(first[0] / second[0], first[1] / second[1]) &&
               NearlyEqual(first[0] / second[0], first[2] / second[2]);
    }

    public string GetInfo()
    {
        var sides = GetOrderedSides();
        var perimeter = GetPerimeter();

        return
            "------------------------------------------------------------\n" +
            "Triangle:\n" +
            $"({Format(sides[0])}, {Format(sides[1])}, {Format(sides[2])})\n" +
            "Reduced:\n" +
            $"({Format(sides[0] / perimeter)}, {Format(sides[1] / perimeter)}, {Format(sides[2] / perimeter)})\n\n" +
            $"Area = {Format(GetArea())}\n" +
            $"Perimeter = {Format(perimeter)}\n" +
            "------------------------------------------------------------";
    }

    public static bool NearlyEqual(double first, double second)
    {
        if (first == second)
            return true;

        var tolerance = Math.Abs(first) * 0.00001; // 0.001% = 0.00001
        return Math.Abs(first - second) < tolerance;
    }

    public static string Format(double value) =>
        value.ToString("0.####", CultureInfo.InvariantCulture);
}

using System.Text;
using Microsoft.AspNetCore.Mvc;
using TriangleApp.Models;

namespace TriangleApp.Controllers;

[Route("triangle")]
public class TriangleController : Controller
{
    [HttpGet("Info")]
    public IActionResult Info(double side1, double side2, double side3)
    {
        var triangle = new Triangle(side1, side2, side3);
        if (!triangle.IsValid()) return TriangleError();

        return Content(triangle.GetInfo(), "text/plain");
    }

    [HttpGet("Area")]
    public IActionResult Area(double side1, double side2, double side3)
    {
        var triangle = new Triangle(side1, side2, side3);
        if (!triangle.IsValid()) return TriangleError();

        return Content(Triangle.Format(triangle.GetArea()), "text/plain");
    }

    [HttpGet("Perimeter")]
    public IActionResult Perimeter(double side1, double side2, double side3)
    {
        var triangle = new Triangle(side1, side2, side3);
        if (!triangle.IsValid()) return TriangleError();

        return Content(Triangle.Format(triangle.GetPerimeter()), "text/plain");
    }

    [HttpGet("IsRightAngled")]
    public IActionResult IsRightAngled(double side1, double side2, double side3)
    {
        var triangle = new Triangle(side1, side2, side3);
        if (!triangle.IsValid()) return TriangleError();

        return BoolResult(triangle.IsRightAngled());
    }

    [HttpGet("IsEquilateral")]
    public IActionResult IsEquilateral(double side1, double side2, double side3)
    {
        var triangle = new Triangle(side1, side2, side3);
        if (!triangle.IsValid()) return TriangleError();

        return BoolResult(triangle.IsEquilateral());
    }

    [HttpGet("IsIsosceles")]
    public IActionResult IsIsosceles(double side1, double side2, double side3)
    {
        var triangle = new Triangle(side1, side2, side3);
        if (!triangle.IsValid()) return TriangleError();

        return BoolResult(triangle.IsIsosceles());
    }

    [HttpGet("arecongruent")]
    public IActionResult AreCongruent([FromQuery] Triangle tr1, [FromQuery] Triangle tr2)
    {
        if (!tr1.IsValid() || !tr2.IsValid()) return TriangleError();

        return BoolResult(tr1.IsCongruentTo(tr2));
    }

    [HttpGet("aresimilar")]
    public IActionResult AreSimilar([FromQuery] Triangle tr1, [FromQuery] Triangle tr2)
    {
        if (!tr1.IsValid() || !tr2.IsValid()) return TriangleError();

        return BoolResult(tr1.IsSimilarTo(tr2));
    }

    [HttpGet("GreatesByPerimeter")]
    public IActionResult GreatesByPerimeter([FromQuery] List<Triangle> tr)
    {
        if (!ValidateTriangles(tr)) return TriangleError();

        var greatest = tr.OrderByDescending(t => t.GetPerimeter()).First();
        return Content(greatest.GetInfo(), "text/plain");
    }

    [HttpGet("GreatestByPerimeter")]
    public IActionResult GreatestByPerimeter([FromQuery] List<Triangle> tr) =>
        GreatesByPerimeter(tr);

    [HttpGet("GreatestByArea")]
    public IActionResult GreatestByArea([FromQuery] List<Triangle> tr)
    {
        if (!ValidateTriangles(tr)) return TriangleError();

        var greatest = tr.OrderByDescending(t => t.GetArea()).First();
        return Content(greatest.GetInfo(), "text/plain");
    }

    [HttpGet("PairwiseNonSimilar")]
    public IActionResult PairwiseNonSimilar([FromQuery] List<Triangle> tr)
    {
        if (!ValidateTriangles(tr)) return TriangleError();

        var selected = new List<Triangle>();

        foreach (var triangle in tr)
        {
            if (selected.All(existing => !triangle.IsSimilarTo(existing)))
                selected.Add(triangle);
        }

        var result = new StringBuilder();
        foreach (var triangle in selected)
        {
            result.AppendLine(triangle.GetInfo());
            result.AppendLine();
        }

        return Content(result.ToString().TrimEnd(), "text/plain");
    }

    private IActionResult BoolResult(bool value) =>
        Content(value ? "true" : "false", "text/plain");

    private static bool ValidateTriangles(List<Triangle>? triangles) =>
        triangles is { Count: > 0 } && triangles.All(t => t.IsValid());

    private IActionResult TriangleError()
    {
        Response.StatusCode = StatusCodes.Status400BadRequest;
        return View("~/Views/Shared/TriangleError.cshtml");
    }
}

# TriangleApp

ASP.NET Core MVC solution for the triangle controller assignment.

## Run

```bash
dotnet restore
dotnet run
```

Default HTTP profile: `http://localhost:5090`

## Example requests

- `/triangle/Info?side1=10&side2=20&side3=25`
- `/triangle/Area?side1=10&side2=20&side3=25`
- `/triangle/Perimeter?side1=10&side2=20&side3=25`
- `/triangle/IsRightAngled?side1=3&side2=5&side3=4`
- `/triangle/IsEquilateral?side1=10&side2=10&side3=10`
- `/triangle/IsIsosceles?side1=15&side2=20&side3=15`
- `/triangle/arecongruent?tr1.side1=10&tr1.side2=20&tr1.side3=15&tr2.side1=15&tr2.side2=10&tr2.side3=20`
- `/triangle/aresimilar?tr1.side1=10&tr1.side2=20&tr1.side3=15&tr2.side1=30&tr2.side2=20&tr2.side3=40`
- `/triangle/GreatesByPerimeter?tr[0].side1=10&tr[0].side2=20&tr[0].side3=12&tr[1].side1=10&tr[1].side2=20&tr[1].side3=25&tr[2].side1=10&tr[2].side2=18&tr[2].side3=22`
- `/triangle/GreatestByArea?tr[0].side1=10&tr[0].side2=20&tr[0].side3=12&tr[1].side1=10&tr[1].side2=20&tr[1].side3=25&tr[2].side1=10&tr[2].side2=18&tr[2].side3=22`
- `/triangle/PairwiseNonSimilar?tr[0].side1=10&tr[0].side2=20&tr[0].side3=12&tr[1].side1=10&tr[1].side2=20&tr[1].side3=25&tr[2].side1=10&tr[2].side2=18&tr[2].side3=22`

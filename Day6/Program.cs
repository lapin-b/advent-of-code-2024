namespace Day6;

class Program
{
    static void Main(string[] args)
    {
        var mapLines = File.ReadAllLines("files/test.txt");
        var map = new GuardPatrolingMap(mapLines);
        var guard = new LabGuard(map);

        while (guard.CanStep())
        {
            guard.Step();
        }

        var distinctSpotsVisited = map.VisitedPoints.Count;
        Console.WriteLine($"Part 1: Guard has visites {distinctSpotsVisited} distinct spots before getting out of the zone");
    }
}
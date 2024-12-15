using System.Diagnostics;

namespace AOCUtils;

public class StopwatchUtils
{
    public static TimeSpan WithTimer(Action callback)
    {
        var stopwatch = Stopwatch.StartNew();
        callback();
        stopwatch.Stop();

        return stopwatch.Elapsed;
    }
}
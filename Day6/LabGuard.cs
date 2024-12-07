using AOCUtils;

namespace Day6;

public class LabGuard
{
    private const char OBSTACLE = '#';
    
    private readonly GuardPatrolingMap _map;
    private GridPoint _currentCoords;
    private GridRelativeVector _currentDirection;

    public LabGuard(GuardPatrolingMap map)
    {
        _map = map;

        var initialPosition = _map
            .Traverse()
            .First(item => item.Item == '<' || item.Item == '>' || item.Item == '^' || item.Item == 'v');
        
        _currentCoords = initialPosition.Point;
        _currentDirection = initialPosition.Item switch
        {
            '<' => GridRelativeVector.Left,
            '>' => GridRelativeVector.Right,
            '^' => GridRelativeVector.Top,
            'v' => GridRelativeVector.Down,
            _ => throw new ArgumentOutOfRangeException(nameof(initialPosition.Item), initialPosition.Item, null)
        };
    }

    public bool CanStep() => _map[_currentCoords] != null;

    public void Step()
    {
        if (NextStepIsObstacle())
        {
            Rotate();
            return;
        }

        _map.MarkAsVisited(_currentCoords);
        _currentCoords = _currentCoords.AddVector(_currentDirection, 1);
    }

    private void Rotate()
    {
        _currentDirection = _currentDirection switch
        {
            GridRelativeVector.Top => GridRelativeVector.Right,
            GridRelativeVector.Right => GridRelativeVector.Down,
            GridRelativeVector.Down => GridRelativeVector.Left,
            GridRelativeVector.Left => GridRelativeVector.Top,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private bool NextStepIsObstacle() => _map.ElementAtRelative(_currentCoords, _currentDirection, 1) == OBSTACLE;
}
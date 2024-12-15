using System.IO.MemoryMappedFiles;

namespace Day9;

public class Diskmap : IDisposable
{
    private MemoryMappedFile MappedFile { get; }
    public MemoryMappedViewAccessor MappedView { get; }
    
    public const int EmptyBlockId = 1;
    public const int BlockIdOffset = 64;
    private const int Alignment = sizeof(int);
    
    private readonly int _expandedMapSize;

    public Diskmap(string fileName, int expandedMapSize)
    {
        _expandedMapSize = expandedMapSize;

        MappedFile = MemoryMappedFile.CreateFromFile(fileName);
        MappedView = MappedFile.CreateViewAccessor();
    }

    public int Read(int position) => MappedView.ReadInt32(position * Alignment);
    public void Write(int position, int what) => MappedView.Write(position * Alignment, what);

    public int? LastDataBlock(int searchLimit)
    {
        for (var i = _expandedMapSize - 1; i >= searchLimit; i--)
        {
            if (Read(i) != EmptyBlockId)
            {
                return i;
            }
        }

        return null;
    }

    public IEnumerable<int?> BlocksInMap()
    {
        for (var i = 0; i < _expandedMapSize; i++)
        {
            var rawBlock = Read(i);
            yield return rawBlock == EmptyBlockId ? null : rawBlock - BlockIdOffset;
        }
    }

    public void DefragmentPart1()
    {
        for (var startOffset = 0; startOffset < _expandedMapSize; startOffset++)
        {
            // Check what we have
            if (Read(startOffset) != EmptyBlockId)
            {
                // Console.WriteLine($"Existing block at {startOffset}");
                continue;
            }

            // Console.WriteLine($"Empty block at {startOffset}");
            // If we do have a hole, check from the end what data lives there and move it
            var lastBlockPosition = LastDataBlock(startOffset);
            if (lastBlockPosition == null)
            {
                // We're done defragmenting, the rest should be empty blocks
                break;
            }
            
            // Move the block from end to the block hole
            // Not atomic. What is data consistency anyway ?
            // Console.WriteLine($"Move {lastBlockPosition} to {startOffset}");
            Write(startOffset, Read((int)lastBlockPosition));
            Write((int)lastBlockPosition, EmptyBlockId);
        }
    }
    
    public void Dispose()
    {
        MappedFile.Dispose();
        MappedView.Dispose();
    }
}
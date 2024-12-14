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
    
    public void Dispose()
    {
        MappedFile.Dispose();
        MappedView.Dispose();
    }
}
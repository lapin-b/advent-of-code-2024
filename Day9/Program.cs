using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using AOCUtils;

namespace Day9;

class Program
{
    private const string DiskFilename = "disk_repr.dat";
    private const string DiskFilenamePart1 = "disk_repr.part1.dat";
    
    static void Main(string[] args)
    {
        var inputMap = File.ReadAllText("files/input.txt")
            .Trim()
            .Select(c => c - '0')
            .ToArray();

        var inputExpandedSize = inputMap.Sum();

        Console.WriteLine("Expanding disk map");
        using (var file = new BinaryWriter(File.Open(DiskFilename, FileMode.Create)))
        {
            file.Seek(0, SeekOrigin.Begin);

            // "Domain expansion"
            var isBlankSpace = false;
            var blockId = Diskmap.BlockIdOffset;
            
            foreach (var size in inputMap)
            {
                for (var i = 0; i < size; i++)
                {
                    file.Write(isBlankSpace ? Diskmap.EmptyBlockId : blockId);
                }

                if (!isBlankSpace) blockId++;
                isBlankSpace = !isBlankSpace;
            }
        }

        Console.WriteLine("Disk map expanded");

        if (File.Exists(DiskFilenamePart1))
            File.Delete(DiskFilenamePart1);
        
        File.Copy(DiskFilename, DiskFilenamePart1);
        using (var diskMap = new Diskmap(DiskFilenamePart1, inputExpandedSize))
        {
            var defragTimeTaken = StopwatchUtils.WithTimer(() => diskMap.DefragmentPart1());
            Console.WriteLine($"Part 1: Time taken for defrag {defragTimeTaken.TotalSeconds} seconds");
            
            // Calculate filesystem checksum
            var part1Checksum = diskMap.BlocksInMap()
                .Where(b => b is not null)
                .Select((block, position) => (long)block! * position)
                .Sum();

            Console.WriteLine($"Part 1 checksum: {part1Checksum}");    
        }
    }
}
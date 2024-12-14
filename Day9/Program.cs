using System.IO.MemoryMappedFiles;

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

        if (File.Exists(DiskFilenamePart1))
            File.Delete(DiskFilenamePart1);
        
        File.Copy(DiskFilename, DiskFilenamePart1);
        using var diskMap = new Diskmap(DiskFilenamePart1, inputExpandedSize);

        for (var startOffset = 0; startOffset < inputExpandedSize; startOffset++)
        {
            // Check what we have
            if (diskMap.Read(startOffset) != Diskmap.EmptyBlockId)
            {
                // Console.WriteLine($"Existing block at {startOffset}");
                continue;
            }

            // Console.WriteLine($"Empty block at {startOffset}");
            // If we do have a hole, check from the end what data lives there and move it
            var lastBlockPosition = diskMap.LastDataBlock(startOffset);
            if (lastBlockPosition == null)
            {
                // We're done defragmenting, the rest should be empty blocks
                break;
            }
            
            // Move the block from end to the block hole
            // Not atomic. What is data consistency anyway ?
            // Console.WriteLine($"Move {lastBlockPosition} to {startOffset}");
            diskMap.Write(startOffset, diskMap.Read((int)lastBlockPosition));
            diskMap.Write((int)lastBlockPosition, Diskmap.EmptyBlockId);
        }
        
        // Calculate filesystem checksum
        var part1Checksum = diskMap.BlocksInMap()
            .Where(b => b is not null)
            .Select((block, position) => (long)block! * position)
            .Sum();

        Console.WriteLine($"Part 1 checksum: {part1Checksum}");
    }
}
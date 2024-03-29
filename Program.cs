using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Diagnostics;

public class Program
{
    public static void Main()
    {
        string projectRoot = Directory.GetCurrentDirectory();
        string inputDirectory = Path.Combine(projectRoot, "entries");
        string outputDirectory = Path.Combine(projectRoot, "resized");

        Directory.CreateDirectory(outputDirectory);
        List<string> imagePaths = new List<string>(Directory.GetFiles(inputDirectory, "*.jpg"));

        Console.WriteLine($"Total number of images to process: {imagePaths.Count}");

        // Parallel processing
        Console.WriteLine("Starting parallel processing...");
        Stopwatch totalParallelStopwatch = Stopwatch.StartNew();
        Parallel.ForEach(imagePaths, (currentImagePath) =>
        {
            string fileName = Path.GetFileNameWithoutExtension(currentImagePath) + "_parallel" + Path.GetExtension(currentImagePath);
            string outputFilePath = Path.Combine(outputDirectory, fileName);

            using (Image image = Image.Load(currentImagePath))
            {
                image.Mutate(x => x.Resize(800, 600));
                image.Save(outputFilePath);
            }
        });
        totalParallelStopwatch.Stop();
        Console.WriteLine($"Total time for parallel processing: {totalParallelStopwatch.ElapsedMilliseconds} ms");

        // Sequential processing
        Console.WriteLine("Starting sequential processing...");
        Stopwatch totalSequentialStopwatch = Stopwatch.StartNew();
        foreach (string currentImagePath in imagePaths)
        {
            string fileName = Path.GetFileNameWithoutExtension(currentImagePath) + "_sequential" + Path.GetExtension(currentImagePath);
            string outputFilePath = Path.Combine(outputDirectory, fileName);

            using (Image image = Image.Load(currentImagePath))
            {
                image.Mutate(x => x.Resize(800, 600));
                image.Save(outputFilePath);
            }
        }
        totalSequentialStopwatch.Stop();
        Console.WriteLine($"Total time for sequential processing: {totalSequentialStopwatch.ElapsedMilliseconds} ms");

        // Performance gain
        double performanceGain = (double)totalSequentialStopwatch.ElapsedMilliseconds / totalParallelStopwatch.ElapsedMilliseconds;
        Console.WriteLine($"Performance gain (Sequential/Parallel): {performanceGain}x");
    }
}

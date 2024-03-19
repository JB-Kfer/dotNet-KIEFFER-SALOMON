using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

public class Program
{
    public static void Main()
    {
        // Define the directory path relative to the project root
        string projectRoot = Directory.GetCurrentDirectory();
        string inputDirectory = Path.Combine(projectRoot, "entries");
        string outputDirectory = Path.Combine(projectRoot, "entries", "resized");

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Get all JPEG images from the input directory
        List<string> imagePaths = new List<string>(Directory.GetFiles(inputDirectory, "*.jpg"));

        Parallel.ForEach(imagePaths, (currentImagePath) =>
        {
            string outputFilePath = Path.Combine(outputDirectory, Path.GetFileName(currentImagePath));

            using (Image image = Image.Load(currentImagePath))
            {
                image.Mutate(x => x.Resize(800, 600));
                image.Save(outputFilePath);
            }

            Console.WriteLine($"Image successfully resized and saved to {outputFilePath}");
        });
    }
}

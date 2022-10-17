using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace AsthaconsoleApp1
{
    class Program
    {
        static string key = "6d6235c12e2b4e9b86414a6e84699b84";
        static string endpoint = "https://imager666.cognitiveservices.azure.com/";
        static void Main(string[] args)
        {
            List<string> imagePaths = new List<string>
            {

             @"E:\image\tajmahal.jpg",
             @"E:\image\rollsroyce.jpg"

            };
            //client object
            var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(key)) { Endpoint = endpoint };
            foreach (var imagePath in imagePaths)
            {
                AnalyzerImage(client, imagePath).Wait();
            }
        }
        private static async Task AnalyzerImage(ComputerVisionClient client, string imagePath)
        {
            var features = new List<VisualFeatureTypes?>()
            {
                VisualFeatureTypes.Description,
                VisualFeatureTypes.Tags,
                VisualFeatureTypes.Categories,
                VisualFeatureTypes.Color,
            };
            using (Stream stream = File.OpenRead(imagePath))
            {
                var result = await client.AnalyzeImageInStreamAsync(stream, visualFeatures: features);

                Console.WriteLine("\nDescription:");
                foreach (var caption in result.Description.Captions)
                {
                    Console.WriteLine($"{caption.Text} and confidence{caption.Confidence}");
                }
                Console.WriteLine("\nTags:");
                foreach (var tag in result.Tags)
                {
                    Console.WriteLine($"{tag.Name}");
                   
                }
                Console.WriteLine("\nCategories :");
                foreach (var category in result.Categories)
                { 
                    Console.WriteLine($"{category.Name} confidence {category.Score}");
                }

            }
        }

    }
}

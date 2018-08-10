using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using Microsoft.WindowsAzure.Storage.Blob;

namespace FaceApiDemo
{
    public static class ImageAnalysis
    {
        [FunctionName("ImageAnalysis")]
        public static async Task Run(
            [BlobTrigger("images/{name}", Connection = "ImagesStorageConnectionString")]CloudBlockBlob blob, 
            string name, 
            TraceWriter log,
            [CosmosDB("facedb", "images", ConnectionStringSetting = "CosmoDbConnectionString")]
            IAsyncCollector<ImageAnalysisResult> result)
        {
            log.Info($"C# Blob trigger function Processed blob\n Name:{blob.Name} \n Size: {blob.Properties.Length} Bytes");

            var sas = GetSas(blob);
            var url = blob.Uri + sas;
            log.Info($"Blob url is {url}");

            var faces = await GetImageAnalysisAsync(url);
            log.Info($"\n Number of faces found: {faces.Length}");

            await result.AddAsync(
                new ImageAnalysisResult
                {
                    ImageId = blob.Name,
                    Faces = faces
                });
        }

        public static async Task<Face[]> GetImageAnalysisAsync(string url)
        {
            var client = new FaceServiceClient(GetEnvironmentVariable("FaceApiKey"), GetEnvironmentVariable("FaceApiEndpoint"));
            var types = new List<FaceAttributeType>() {
                FaceAttributeType.Emotion,
                FaceAttributeType.Age,
                FaceAttributeType.FacialHair,
                FaceAttributeType.Gender,
                FaceAttributeType.Glasses,
                FaceAttributeType.HeadPose,
                FaceAttributeType.Smile
            };

            var result = await client.DetectAsync(url, true, true, types);
            return result;
        }

        public static string GetSas(CloudBlockBlob blob)
        {
            var sasPolicy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-15),
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(15)
            };

            var sas = blob.GetSharedAccessSignature(sasPolicy);
            return sas;
        }

        public static string GetEnvironmentVariable(string name)
        {
            return System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }
    }
}

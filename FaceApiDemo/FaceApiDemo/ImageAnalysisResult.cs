using Microsoft.ProjectOxford.Face.Contract;

namespace FaceApiDemo
{
    public class ImageAnalysisResult
    {
        public string ImageId { get; set; }
        public Face[] Faces { get; set; }
    }
}

using System.IO;
using System.Reflection;
using ImageTagger.Core;

namespace ImageTagger.UI.Service;

public class ModelInference
{
    private readonly ModelPrediction _modelPrediction;
    private const string TaggingModelCategoriesPath = "AIModels/categories.txt";
    private const string TaggingModelPath = "AIModels/resnet50.onnx";

    public ModelInference()
    {
        // Get assembly base path
        var assemblyBasePath = Assembly.GetExecutingAssembly().Location;
        var assemblyPath = Path.GetDirectoryName(assemblyBasePath);
        _modelPrediction = new ModelPrediction(
            Path.Join(assemblyPath, TaggingModelPath),
            Path.Join(assemblyPath, TaggingModelCategoriesPath));
    }

    public string PredictTags(string imagePath, string separator)
    {
        var tags = _modelPrediction.PredictTags(imagePath);
        return string.Join(separator, tags);
    }
}
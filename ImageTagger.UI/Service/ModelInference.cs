using System.IO;
using System.Linq;
using System.Reflection;
using ImageTagger.Core;

namespace ImageTagger.UI.Service;

public class ModelInference
{
    private const string TaggingModelCategoriesPath = "AIModels/resnet50_categories.txt";
    private const string TaggingModelPath = "AIModels/resnet50_10_epochs.onnx";
    private readonly ModelPrediction _modelPrediction;

    public ModelInference()
    {
        // Get assembly base path
        var assemblyBasePath = Assembly.GetExecutingAssembly().Location;
        var assemblyPath = Path.GetDirectoryName(assemblyBasePath);
        // Create model prediction instance
        _modelPrediction = new ModelPrediction(
            Path.Join(assemblyPath, TaggingModelPath),
            Path.Join(assemblyPath, TaggingModelCategoriesPath));
    }

    public string PredictTags(string imagePath, string separator)
    {
        var tags = _modelPrediction.PredictTags(imagePath);
        var predictionTags = tags.Where(tag => tag.Confidence > 0.5).Select(tag => tag.Label).ToList();
        return string.Join(separator, predictionTags);
    }
}
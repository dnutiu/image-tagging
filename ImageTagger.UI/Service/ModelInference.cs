using System.IO;
using System.Linq;
using System.Reflection;
using ImageTagger.Core;

namespace ImageTagger.UI.Service;

/// <summary>
///     ModelInference class is used to predict tags for given images.
/// </summary>
public class ModelInference
{
    private const string TaggingModelCategoriesPath = "AIModels/resnet50_categories.txt";
    private const string TaggingModelPath = "AIModels/resnet50_10_epochs.onnx";
    private readonly ModelPrediction _modelPrediction;

    /// <summary>
    ///     Constructs a new instance of ModelInference.
    /// </summary>
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

    /// <summary>
    ///     Returns the predicted tags for the given image.
    /// </summary>
    /// <param name="imagePath">The absolute path to the image for predicting the tags.</param>
    /// <param name="separator">The separator for the predicted tags</param>
    /// <returns>The string with the predicted tags.</returns>
    public string PredictTags(string imagePath, string separator)
    {
        var tags = _modelPrediction.PredictTags(imagePath);
        var predictionTags = tags.Where(tag => tag.Confidence > 0.5).Select(tag => tag.Label).ToList();
        return string.Join(separator, predictionTags);
    }
}
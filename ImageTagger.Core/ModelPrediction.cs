using Microsoft.ML.OnnxRuntime;

namespace ImageTagger.Core;

/// <summary>
///     ModelPrediction class is used to predict tags for given images.
/// </summary>
public class ModelPrediction
{
    /// <summary>
    ///     The <see cref="ImageProcessor" /> instance.
    /// </summary>
    private readonly ImageProcessor _imageProcessor;

    /// <summary>
    ///     The ONNX <see cref="InferenceSession" /> instance.
    /// </summary>
    private readonly InferenceSession _inferenceSession;

    /// <summary>
    ///     The LabelsRegistry instance.
    /// </summary>
    private readonly LabelsRegistry _labelsRegistry;

    /// <summary>
    ///     Constructs a new instance of ModelPrediction.
    /// </summary>
    /// <param name="modelPath">The path to the model.</param>
    /// <param name="labelsPath">The path to the labels file..</param>
    public ModelPrediction(string modelPath, string labelsPath)
    {
        _inferenceSession = new InferenceSession(modelPath);
        _imageProcessor = new ImageProcessor();
        _labelsRegistry = new LabelsRegistry(labelsPath);
    }

    /// <summary>
    ///     PredictTags predicts an image's tags.
    /// </summary>
    /// <param name="imagePath">The path to the image file.</param>
    /// <returns>A <see cref="Prediction" /> enumerable.</returns>
    public IEnumerable<Prediction> PredictTags(string imagePath)
    {
        // Setup inputs
        var inputs = new List<NamedOnnxValue>
        {
            NamedOnnxValue.CreateFromTensor("input.1", _imageProcessor.ProcessImage(imagePath))
        };

        using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = _inferenceSession.Run(inputs);

        // Post process output vector.
        var output = results.First().AsEnumerable<float>();

        var predictionResults = output
            .Select((confidence, labelIndex) => new Prediction(_labelsRegistry[labelIndex], confidence))
            .OrderBy(x => x.Confidence).ToList();

        return predictionResults;
    }
}
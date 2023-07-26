using System.Reflection;

namespace ImageTagger.Core.Tests;

public class ModelPredictionTests
{
    private readonly ModelPrediction _modelPrediction;
    private readonly string _testArchitectureImagePath;

    public ModelPredictionTests()
    {
        var assemblyBasePath = Assembly.GetExecutingAssembly().Location;
        var assemblyPath = Path.GetDirectoryName(assemblyBasePath);

        _testArchitectureImagePath = Path.Join(assemblyPath, "resources", "test-image.jpg");
        _modelPrediction = new ModelPrediction(
            Path.Join(assemblyPath, "AIModels", "resnet50.onnx"),
            Path.Join(assemblyPath, "AIModels", "resnet50_categories.txt")
        );
    }

    [Fact]
    public void Test_PredictTags_Architecture()
    {
        // Test: Predict the tags.
        var tags = _modelPrediction.PredictTags(_testArchitectureImagePath);

        // Assert
        Assert.NotNull(tags);
        var architectureTag = tags.FirstOrDefault(x => x.Label == "architecture" && x.Confidence > -0.5);
        Assert.NotNull(architectureTag);
    }
}
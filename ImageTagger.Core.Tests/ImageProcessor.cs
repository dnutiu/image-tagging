using System.Reflection;

namespace ImageTagger.Core.Tests;

public class ImageProcessor
{
    private readonly Core.ImageProcessor _imageProcessor;
    private readonly string _testImagePath;

    public ImageProcessor()
    {
        var assemblyBasePath = Assembly.GetExecutingAssembly().Location;
        var assemblyPath = Path.GetDirectoryName(assemblyBasePath);
        _testImagePath = Path.Join(assemblyPath, "resources", "test-image.jpg");
        _imageProcessor = new Core.ImageProcessor();
    }

    [Fact]
    public void Test_ProcessImage_CorrectShape()
    {
        // Test: Process the image.
        var image = _imageProcessor.ProcessImage(_testImagePath);
        
        // Assert: The image is not null.
        Assert.NotNull(image);
        
        // Assert: The tensor has the correct shape.
        var expectedShape = new [] {1, 3, 224, 224};
        for (var i = 0; i < expectedShape.Length; i++)
        {
            Assert.True(expectedShape[i] == image.Dimensions[i]);
        }
    }
    
}
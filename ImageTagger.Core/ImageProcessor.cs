using Microsoft.ML.OnnxRuntime.Tensors;

namespace ImageTagger.Core;

/// <summary>
///     ImageProcessor is a class for processing images.
/// </summary>
public class ImageProcessor
{
    private readonly float[] _mean = { 0.485f, 0.456f, 0.406f };
    private readonly float[] _standardDeviation = { 0.229f, 0.224f, 0.225f };


    /// <summary>
    ///     Given an image it applies necessary transformations and returns a tensor.
    /// </summary>
    /// <param name="image">The image object.</param>
    /// <returns>The DenseTensor of dimension {1, 3, 224, 224}</returns>
    private Tensor<float> ImageToTensor(Image<Rgb24> image)
    {
        Tensor<float> outputTensor = new DenseTensor<float>(new[] { 1, 3, 224, 224 });
        // Resize image to the correct width.
        image.Mutate(x =>
        {
            x.Resize(new ResizeOptions
            {
                Size = new Size(224, 224),
                Mode = ResizeMode.Pad
            });
        });

        image.ProcessPixelRows(accessor =>
        {
            for (var y = 0; y < accessor.Height; y++)
            {
                var pixelSpan = accessor.GetRowSpan(y);
                for (var x = 0; x < accessor.Width; x++)
                {
                    outputTensor[0, 0, y, x] = (pixelSpan[x].R / 255f - _mean[0]) / _standardDeviation[0];
                    outputTensor[0, 1, y, x] = (pixelSpan[x].G / 255f - _mean[1]) / _standardDeviation[1];
                    outputTensor[0, 2, y, x] = (pixelSpan[x].B / 255f - _mean[2]) / _standardDeviation[2];
                }
            }
        });
        return outputTensor;
    }

    /// <summary>
    ///     ProcessImage transforms an image into a tensor given it's path.
    /// </summary>
    /// <param name="filePath">The image path.</param>
    /// <returns>The image tensor.</returns>
    public Tensor<float> ProcessImage(string filePath)
    {
        using var image = Image.Load<Rgb24>(filePath);
        var outputTensor = ImageToTensor(image);
        return outputTensor;
    }
}
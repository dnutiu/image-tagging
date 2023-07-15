using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media.Imaging;
using Avalonia.Metadata;

namespace ImageTagger.UI.Controls;

/// <summary>
///     ImagePredictionRow is a custom control used to display an image and its predicted tags.
/// </summary>
public class ImagePredictionRow : TemplatedControl
{
    public static readonly AvaloniaProperty<string> PredictedImageTagsProperty =
        AvaloniaProperty.Register<ImagePredictionRow, string>(nameof(PredictedImageTagsProperty));

    public static readonly AvaloniaProperty<Bitmap> ImageProperty =
        AvaloniaProperty.Register<ImagePredictionRow, Bitmap>(nameof(ImageProperty));

    /// <summary>
    ///     Constructs a new instance of ImagePredictionRow.
    /// </summary>
    public ImagePredictionRow()
    {
    }

    /// <summary>
    ///     Constructs a new instance of ImagePredictionRow.
    /// </summary>
    /// <param name="predictedImageTags">The predicted image tags text.</param>
    /// <param name="image">The <see cref="Bitmap" /> image.</param>
    public ImagePredictionRow(string predictedImageTags, Bitmap image)
    {
        PredictedImageTags = predictedImageTags;
        Image = image;
    }

    [Content]
    public string? PredictedImageTags
    {
        get => GetValue(PredictedImageTagsProperty) as string;
        set => SetValue(PredictedImageTagsProperty, value);
    }

    [Content]
    public Bitmap? Image
    {
        get => GetValue(ImageProperty) as Bitmap;
        set => SetValue(ImageProperty, value);
    }
}
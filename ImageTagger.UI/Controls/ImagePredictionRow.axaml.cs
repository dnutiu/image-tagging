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
    /// <summary>
    ///     Defines the PredictedImageTags property.
    /// </summary>
    public static readonly AvaloniaProperty<string> PredictedImageTagsProperty =
        AvaloniaProperty.Register<ImagePredictionRow, string>(nameof(PredictedImageTagsProperty));

    /// <summary>
    ///     Defines the Image property.
    /// </summary>
    public static readonly AvaloniaProperty<Bitmap> ImageProperty =
        AvaloniaProperty.Register<ImagePredictionRow, Bitmap>(nameof(ImageProperty));

    /// <summary>
    ///     Defines the ImageFileName property.
    /// </summary>
    public static readonly AvaloniaProperty<string> ImageFileNameProperty =
        AvaloniaProperty.Register<ImagePredictionRow, string>(nameof(ImageFileNameProperty));

    private readonly string _imageFilePath;

    /// <summary>
    ///     Constructs a new instance of ImagePredictionRow.
    /// </summary>
    /// <param name="predictedImageTags">The predicted image tags text.</param>
    /// <param name="imagePath">The image file path.</param>
    public ImagePredictionRow(string predictedImageTags, string imagePath)
    {
        PredictedImageTags = predictedImageTags;
        Image = new Bitmap(imagePath);
        _imageFilePath = imagePath;
        // Set the image file name to the file name of the image path.
        ImageFileName = System.IO.Path.GetFileName(imagePath);
    }

    /// <summary>
    /// The predicted image tags text.
    /// </summary>
    [Content]
    public string? PredictedImageTags
    {
        get => GetValue(PredictedImageTagsProperty) as string;
        set => SetValue(PredictedImageTagsProperty, value);
    }

    /// <summary>
    /// The Bitmap image.
    /// </summary>
    [Content]
    public Bitmap? Image
    {
        get => GetValue(ImageProperty) as Bitmap;
        set => SetValue(ImageProperty, value);
    }

    /// <summary>
    /// The image file name.
    /// </summary>
    [Content]
    public string? ImageFileName
    {
        get => GetValue(ImageFileNameProperty) as string;
        set => SetValue(ImageFileNameProperty, value);
    }
}
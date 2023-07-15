using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media.Imaging;
using Avalonia.Metadata;

namespace ImageTagger.UI.Controls;

public class ImagePredictionRow : TemplatedControl
{
    public static readonly AvaloniaProperty<string> PredictedImageTagsProperty =
        AvaloniaProperty.Register<ImagePredictionRow, string>(nameof(PredictedImageTagsProperty));

    public static readonly AvaloniaProperty<Bitmap> ImageProperty =
        AvaloniaProperty.Register<ImagePredictionRow, Bitmap>(nameof(ImageProperty));

    public ImagePredictionRow()
    {
    }

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
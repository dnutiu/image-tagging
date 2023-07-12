using Avalonia.Media.Imaging;

namespace ImageTagger.UI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public string ImageTags { get; set; } =
        "Image tag1, Image tag2, Image tag3, Image tag4, Image tag5, Image tag6, Image tag7, Image tag8, Image tag9, Image tag10";

    public Bitmap Image => new("C:\\Users\\nutiu\\Documents\\i\\5.JPG");
}
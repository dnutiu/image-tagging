using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using ImageTagger.UI.Views;

namespace ImageTagger.UI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly MainWindow _mainWindow;

    public MainWindowViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
    }

    public string ImageTags { get; set; } =
        "Image tag1, Image tag2, Image tag3, Image tag4, Image tag5, Image tag6, Image tag7, Image tag8, Image tag9, Image tag10";

    public Bitmap Image => new("/Users/dnutiu/Pictures/picture.webp");

    public async Task OnLoadImagesClick()
    {
        _mainWindow.OnLoadImages_Click();
    }
}
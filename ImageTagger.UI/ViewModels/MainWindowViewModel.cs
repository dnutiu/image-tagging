using System.Threading.Tasks;
using ImageTagger.UI.Views;

namespace ImageTagger.UI.ViewModels;

/// <summary>
///     MainWindowViewModel is the view model for the MainWindow.
/// </summary>
public class MainWindowViewModel : ViewModelBase
{
    private readonly MainWindow _mainWindow;

    public MainWindowViewModel(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
    }

    public async Task OnLoadImagesClick()
    {
        _mainWindow.OnLoadImages_Click();
    }
}
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

    /// <summary>
    /// OnLoadImagesClick is called when the Load Images button is clicked.
    /// </summary>
    public async Task OnLoadImagesClick()
    {
        await _mainWindow.OnLoadImages_Click();
    }
}
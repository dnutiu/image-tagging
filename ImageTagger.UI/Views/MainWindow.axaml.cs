using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using ImageTagger.UI.Controls;
using ImageTagger.UI.ViewModels;

namespace ImageTagger.UI.Views;

public partial class MainWindow : Window
{
    private readonly StackPanel? _imagePredictionStackPanel;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel(this);
        _imagePredictionStackPanel = this.FindControl<StackPanel>("MainStackPanel");
    }

    public async Task OnLoadImages_Click()
    {
        // Create an instance of OpenFileDialog
        var openFileDialog = new OpenFileDialog
        {
            // Set the dialog's properties as needed
            Title = "Open Files",
            AllowMultiple = true,
            Filters = new List<FileDialogFilter>
            {
                new() { Name = "Image Files", Extensions = { "png", "jpg", "jpeg", "webp" } }
            }
        };

        // Open the dialog and wait for the result
        var result = await openFileDialog.ShowAsync(this);

        // Process the selected file(s) if the dialog was not cancelled
        if (result is { Length: > 0 })
        {
            // Access the selected file(s)
            foreach (var file in result.Where(file =>
                         file.EndsWith(".png") || 
                         file.EndsWith(".jpg") || 
                         file.EndsWith(".jpeg") ||
                         file.EndsWith(".webp")
                         ))
            {
                Dispatcher.UIThread.Post(() =>
                {
                    // Add image to stack panel
                    _imagePredictionStackPanel?.Children.Add(new ImagePredictionRow(file, new Bitmap(file)));
                });
            }
        }
    }
}
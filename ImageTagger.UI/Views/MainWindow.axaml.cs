using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using ImageTagger.UI.Controls;
using ImageTagger.UI.Service;
using ImageTagger.UI.ViewModels;

namespace ImageTagger.UI.Views;

/// <summary>
///     The MainWindow class is the main window of the application.
/// </summary>
public partial class MainWindow : Window
{
    private readonly StackPanel? _imagePredictionStackPanel;
    private readonly ModelInference _modelInference;

    /// <summary>
    ///     Constructs a new instance of MainWindow.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel(this);
        _imagePredictionStackPanel = this.FindControl<StackPanel>("MainStackPanel");
        _modelInference = new ModelInference();
    }

    /// <summary>
    ///     OnLoadImages_Click is the event handler for the Load Images button.
    ///     It opens a file dialog and loads the selected images.
    ///     Then it calls the model inference service to predict the image tags.
    ///     Finally it adds the image and the predicted tags to the UI's stack panel.
    /// </summary>
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
                    var imageTags = _modelInference.PredictTags(file, ",");
                    _imagePredictionStackPanel?.Children.Add(
                        new ImagePredictionRow(imageTags, new Bitmap(file))
                    );
                });
            }
        }
    }
}
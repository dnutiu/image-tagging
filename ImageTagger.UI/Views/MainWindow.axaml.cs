using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            // Start new thread
            var thread = new Thread(() =>
            {
                var imagePredictions = new List<Tuple<string, Bitmap>>();
                // For each selected file, filter out non-image files and predict image tags.
                foreach (var file in result.Where(file =>
                             file.ToLower().EndsWith(".png") ||
                             file.ToLower().EndsWith(".jpg") ||
                             file.ToLower().EndsWith(".jpeg") ||
                             file.ToLower().EndsWith(".webp")
                         ))
                {
                    // Predict image tags
                    var imageTags = _modelInference.PredictTags(file, ",");
                    imagePredictions.Add(new Tuple<string, Bitmap>(imageTags, new Bitmap(file)));
                }

                // Update UI thread.
                Dispatcher.UIThread.Post(() =>
                {
                    // Add images to stack panel
                    _imagePredictionStackPanel?.Children.AddRange(
                        imagePredictions.Select(item => new ImagePredictionRow(item.Item1, item.Item2))
                    );
                });
            });
            thread.Start();
        }
    }
}
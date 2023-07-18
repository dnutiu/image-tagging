using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
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
    private readonly StackPanel _imagePredictionStackPanel;
    private readonly ModelInference _modelInference;
    private readonly ProgressBar _progressBar;

    /// <summary>
    ///     Constructs a new instance of MainWindow.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel(this);
        
        _imagePredictionStackPanel = this.FindControl<StackPanel>("MainStackPanel") ??
                                     throw new InvalidOperationException("MainStackPanel could not be found");
        _progressBar = this.FindControl<ProgressBar>("ProgressBar") ??
                       throw new InvalidOperationException("ProgressBar could not be found!");
        _modelInference = new ModelInference();
    }

    /// <summary>
    ///     Constructs a new instance of MainWindow with the given image files.
    /// </summary>
    /// <param name="imageFiles">The image files paths.</param>
    public MainWindow(IEnumerable<string> imageFiles) : this()
    {
        var imageFilesArray = imageFiles.ToArray();
        if (imageFilesArray.Any())
        {
            var thread = new Thread(() => PredictTagsForFiles(imageFilesArray));
            thread.Start();
        }
    }

    /// <summary>
    ///     Predicts the tags for the given image files.
    /// </summary>
    /// <param name="files">The image file paths.</param>
    public void PredictTagsForFiles(IEnumerable<string> files)
    {
        var imagePredictions = new List<Tuple<string, string>>();
        var imageFiles = files.Where(file =>
            file.ToLower().EndsWith(".png") ||
            file.ToLower().EndsWith(".jpg") ||
            file.ToLower().EndsWith(".jpeg") ||
            file.ToLower().EndsWith(".webp")
        ).ToList();
        // For each selected file, filter out non-image files and predict image tags.
        foreach (var file in imageFiles)
        {
            // Predict image tags
            var imageTags = _modelInference.PredictTags(file, ",");
            imagePredictions.Add(new Tuple<string, string>(imageTags, file));
            Dispatcher.UIThread.Post(() =>
            {
                // Update progress bar
                _progressBar.IsVisible = true;
                _progressBar.Value = (double)((imagePredictions.Count - 1) * 100) / imageFiles.Count;
            });
        }

        // Update UI thread.
        Dispatcher.UIThread.Post(() =>
        {
            // Add images to stack panel
            _imagePredictionStackPanel.Children.AddRange(
                imagePredictions.Select(item => new ImagePredictionRow(item.Item1, item.Item2))
            );
            _progressBar.IsVisible = false;
        });
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
            AllowMultiple = true
        };

        // Open the dialog and wait for the result
        var result = await openFileDialog.ShowAsync(this);

        // Process the selected file(s) if the dialog was not cancelled
        if (result is { Length: > 0 })
        {
            // Start new thread
            var thread = new Thread(() => { PredictTagsForFiles(result); });
            thread.Start();
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Threading;


namespace ImageTagger.UI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public string ImageTags { get; set; } =
        "Image tag1, Image tag2, Image tag3, Image tag4, Image tag5, Image tag6, Image tag7, Image tag8, Image tag9, Image tag10";

    public Bitmap Image => new("/Users/dnutiu/Pictures/picture.webp");
    
    public async Task OnLoadImagesClick(Window parentWindow)
    {
        // Create an instance of OpenFileDialog
        var openFileDialog = new OpenFileDialog();

        // Set the dialog's properties as needed
        openFileDialog.Title = "Open File";
        openFileDialog.Filters = new List<FileDialogFilter>
        {
            new() { Name = "Image Files", Extensions = { "png", "jpg", "jpeg" } }
        };

        // Open the dialog and wait for the result
        var result = await openFileDialog.ShowAsync(parentWindow);

        // Process the selected file(s) if the dialog was not cancelled
        if (result != null && result.Length > 0)
        {
            // Access the selected file(s)
            foreach (var file in result)
            {
                // Do something with the file path
                // e.g., display the selected file path in a text box
                Dispatcher.UIThread.Post(() =>
                {
                    // Assuming you have a TextBox named "filePathTextBox"
                });
            }
        }
    }
}
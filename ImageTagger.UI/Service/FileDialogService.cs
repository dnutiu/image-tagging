using System.Threading.Tasks;
using Avalonia.Controls;

namespace ImageTagger.UI.Service;

/// <summary>
///     FileDialogService is a service that provides a way to open a file dialog.
/// </summary>
public class FileDialogService
{
    private readonly bool _allowMultiple;
    private readonly string _dialogTitle;

    /// <summary>
    ///     Instantiates a new FileDialogService with the default dialog title.
    /// </summary>
    public FileDialogService() : this("Select Files")
    {
    }

    /// <summary>
    ///     Instantiates a new FileDialogService with the given dialog title.
    /// </summary>
    /// <param name="dialogTitle">The given dialog title.</param>
    /// <param name="allowMultiple">If true allows selection of multiple files.</param>
    public FileDialogService(string dialogTitle, bool allowMultiple = true)
    {
        _dialogTitle = dialogTitle;
        _allowMultiple = allowMultiple;
    }

    /// <summary>
    ///     Opens a file dialog with the given parent window.
    /// </summary>
    /// <param name="parentWindow">The window.</param>
    /// <returns>Returns an array of file names.</returns>
    public async Task<string[]?> OpenDialog(Window parentWindow)
    {
        // Create an instance of OpenFileDialog
        var openFileDialog = new OpenFileDialog
        {
            // Set the dialog's properties as needed
            Title = _dialogTitle,
            AllowMultiple = _allowMultiple
        };

        // Open the dialog and wait for the result
        return await openFileDialog.ShowAsync(parentWindow);
    }
}
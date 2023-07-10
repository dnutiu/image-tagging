namespace ImageTagger.Core;

/// <summary>
///     LabelsRegistry holds the model labels.
/// </summary>
public class LabelsRegistry
{
    private readonly List<string> labels;

    /// <summary>
    /// Instantiates a new instance of LabelsRegistry.
    /// </summary>
    /// <param name="labelsFile">The labelsFile path.</param>
    public LabelsRegistry(string labelsFile)
    {
        labels = new List<string>();
        // Read the labels file and add the labels to the list.
        using var sr = new StreamReader(labelsFile);
        while (!sr.EndOfStream)
        {
            labels.Add(sr.ReadLine());
        }
    }

    public string this[int i]
    {
        get => labels[i];
        private set => labels[i] = value;
    }
}
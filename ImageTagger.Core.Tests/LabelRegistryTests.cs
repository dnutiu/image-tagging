namespace ImageTagger.Core.Tests;

public class LabelRegistryTests
{
    [Fact]
    public void Test_LabelRegistry_FileOk()
    {
        // Setup: Create a temporary labels file.
        var labels = new List<string>
        {
            "label1",
            "label2",
            "label3"
        };
        var labelsFile = Path.GetTempFileName();
        File.WriteAllLines(labelsFile, labels);
        
        // Test: Create a new LabelsRegistry instance.
        var labelsRegistry = new LabelsRegistry(labelsFile);
        
        // Verify: The labels are the same.
        Assert.Equal(3, labelsRegistry.Count);
        for (var i = 0; i < labels.Count; i++)
        {
            Assert.Equal(labels[i], labelsRegistry[i]);
        }
    }
    
    [Fact]
    public void Test_LabelRegistry_FileEmpty()
    {
        // Setup: Create a temporary labels file.
        var labelsFile = Path.GetTempFileName();

        // Test: Create a new LabelsRegistry instance.
        var labelsRegistry = new LabelsRegistry(labelsFile);
        
        // Verify: The labels are the same.
        Assert.Equal(0, labelsRegistry.Count);
    }
    
    [Fact]
    public void Test_LabelRegistry_FileDoesNotExist()
    {
        // Setup: Create a temporary labels file.
        var labelsFile = Path.GetTempFileName();
        File.Delete(labelsFile);

        // Test: Create a new LabelsRegistry instance.
        Assert.Throws<FileNotFoundException>(() => new LabelsRegistry(labelsFile));
    }
}
using ImageTagger.Core;

/*
 * Example use of ImageTagger core library.
 */


// Specify the paths to the model and labels files.
const string modelPath = @"RiderProjects\ImageTagger\ImageTagger.Core\AIModels\resnet50.onnx";
const string labelsPath = @"RiderProjects\ImageTagger\ImageTagger.Core\AIModels\resnet50_categories.txt";

// Create a new instance of ModelPrediction.
var modelPrediction = new ModelPrediction(modelPath, labelsPath);

// Predict tags for the images from 1.jpg to 12.jpg.
for (var i = 1; i <= 12; i++)
{
    Console.WriteLine($"Predicting... {i}.jpg");
    var prediction = modelPrediction.PredictTags($@"Documents\i\{i}.jpg");

    // Print the predictions and their confidence.
    // Filter out the predictions with confidence lower than -0.5.
    foreach (var pred in prediction.Where(prediction => prediction.Confidence > -0.5))
    {
        Console.WriteLine(pred.Label, pred.Confidence);
    }
}
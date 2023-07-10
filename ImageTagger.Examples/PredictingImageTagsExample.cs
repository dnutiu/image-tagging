using ImageTagger.Core;

/*
 * Example use of ImageTagger core library.
 */


const string modelPath = @"C:\Users\nutiu\RiderProjects\ImageTagger\data\resnet50.onnx";
const string labelsPath = @"C:\Users\nutiu\RiderProjects\ImageTagger\data\categories.txt";
var modelPrediction = new ModelPrediction(modelPath, labelsPath);

for (var i = 1; i <= 12; i++)
{
    Console.WriteLine($"Predicting... {i}.jpg");
    var prediction = modelPrediction.PredictTags($@"C:\Users\nutiu\Documents\i\{i}.jpg");

    foreach (var pred in prediction.Where(prediction => prediction.Confidence > -0.5))
    {
        Console.WriteLine(pred.Label, pred.Confidence);
    }
}
namespace ImageTagger.Core;

/// <summary>
/// Prediction is a record that represents a prediction made by a model.
/// </summary>
/// <param name="Label">The prediction label / category.</param>
/// <param name="Confidence">The confidence level of the prediction. Higher is better.</param>
public record Prediction(string Label, float Confidence);
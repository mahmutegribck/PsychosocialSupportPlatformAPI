using Microsoft.ML;
using PsychosocialSupportPlatformAPI.Business.MLModel.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.MLModel
{
    public class MLModelService : IMLModelService
    {
        public async Task CreateAIModel(UploadDataSetDTO uploadDataSetDTO)
        {
            string? rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            if (rootPath == null || uploadDataSetDTO.DataSetFile == null || uploadDataSetDTO.DataSetFile.Length == 0)
            {
                throw new Exception();
            }
            if (Path.GetExtension(uploadDataSetDTO.DataSetFile.FileName) != ".csv")
            {
                throw new Exception("Lütfen .csv Uzantılı Dosya Yükleyiniz");
            }

            if (System.IO.File.Exists(rootPath + "\\MLDataSet\\DataSet.csv"))
            {
                File.Delete(rootPath + "\\MLDataSet\\DataSet.csv");
            }

            string basePath = rootPath + "\\MLDataSet\\";

            if (!System.IO.Directory.Exists(basePath))
            {
                System.IO.Directory.CreateDirectory(basePath);
            }
            string newFileName = Path.ChangeExtension("DataSet", Path.GetExtension(uploadDataSetDTO.DataSetFile.FileName));

            string filePath = string.Concat($"{basePath}", newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await uploadDataSetDTO.DataSetFile.CopyToAsync(stream);
            }

            var mlContext = new MLContext();
            var dataView = mlContext.Data.LoadFromTextFile<SentimentData>(
              filePath, separatorChar: ',', hasHeader: false);

            var trainTestSplit = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);
            var trainSet = trainTestSplit.TrainSet;
            var testSet = trainTestSplit.TestSet;

            var dataProcessPipeline = mlContext.Transforms.Conversion.MapValueToKey("Label")
                .Append(mlContext.Transforms.Text.FeaturizeText(
                  outputColumnName: "Features",
                  inputColumnName: nameof(SentimentData.SentimentText)));

            var trainer = mlContext.MulticlassClassification
              .Trainers
              .SdcaMaximumEntropy(
                labelColumnName: "Label", featureColumnName: "Features");

            var trainingPipeline = dataProcessPipeline
              .Append(trainer)
              .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var trainedModel = trainingPipeline.Fit(trainSet);

            var predictions = trainedModel.Transform(testSet);

            var metrics = mlContext.MulticlassClassification.Evaluate(predictions, "Label");

            Console.WriteLine($"Macro accuracy: {metrics.MacroAccuracy:P2}");
            Console.WriteLine($"Micro accuracy: {metrics.MicroAccuracy:P2}");
            Console.WriteLine($"Log loss: {metrics.LogLoss:P2}");

            var modelPath = rootPath + "\\MLDataSet\\DataSet.zip";

            if (System.IO.File.Exists(modelPath))
            {
                File.Delete(modelPath);
            }
            mlContext.Model.Save(trainedModel, dataView.Schema, modelPath);

        }

        public async Task<string?> GetMessagePrediction(string message)
        {
            string modelPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MLDataSet", "DataSet.zip");

            if (!System.IO.File.Exists(modelPath))
                return null;

            MLContext mLContext = new();
            var loadedModel = mLContext.Model.Load(modelPath, out _);

            var predEngine = mLContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(loadedModel);

            var predictionResult = predEngine.Predict(new SentimentData { SentimentText = message });

            return await Task.FromResult(predictionResult.Prediction.ToLower());
        }
    }
}

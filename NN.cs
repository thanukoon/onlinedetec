using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using System.Data;
using System.Windows.Documents;
using System.Threading;

namespace Microsoft.Samples.Kinect.BodyBasics
{
    
    class NN
    {
        private static readonly string sourceFile = Path.Combine(Environment.CurrentDirectory, "testnn2.csv"); //breast-cancer-wisconsin
                                                                                                               // private static readonly string scource = 
                                                                                                               // Number of input neurons, hidden neurons and output neurons
        private static readonly int[] inputColumns = { 0, 1, 2, 3, 4, 5, 6, 7, 8 }; // ไว้เพิ่มcolumn
        private static readonly int numInput = inputColumns.Length;
        private const int numHidden = 7;
        private const int numOutput = 2;

        // Parameters for NN training
        private const int maxEpochs = 2000;
        private const double learnRate = 0.05;
        private const double momentum = 0.01;
        private const double weightDecay = 0.0001;
        List<double[]> count5 = new List<double[]>();
        List<double[]> data = new List<double[]>();
        ArrayList item = new ArrayList();
        double[] array;
        int d = 0;
        NeuralNetwork nn;
        // public var data = new List<double[]>();
        
        public async Task getAsync(double ab)
        {
            
            
            if (d < 1000)
            {
                item.Add(ab);
                double[] dd = { ab };
                count5.Add(dd);
                d++;
               // getCount();
            }
            else
            {
                //Console.WriteLine(data.Count);
                foreach (var it in count5)
                {
                    Console.WriteLine(count5.Count);
                }
              //  trian(count5);
               // Console.WriteLine("123lasd");
               // trian(data);
              //  item.Clear();

                
                // Console.WriteLine("stop");
                // count5.Clear();
                d =0;
            }

            getCount();
        }

        void getCount()
        {
            Console.WriteLine(count5.Count);


        }

        public void yes()
        {
            Console.WriteLine(inputColumns);
          //  double[] it2 = {123.123,123.23,32.23,934.234,5645.342 }; 
    
            Console.WriteLine("Neural Network Demo using .NET by Sebastian Brandes");
            Console.WriteLine("Data Set: Breast Cancer Wisconsin (Diagnostic), November 1995");
            // Source: httdp://archive.ics.uci.edu/ml/machine-learning-databases/breast-cancer-wisconsin/
            Console.WriteLine();
            //C:\Users\Goon\source\repos\BreastCancerNeuralNetwork\BreastCancerNeuralNetwork\Data\breast-cancer-wisconsin.csv
            #region Data Generation
            Console.WriteLine("Loading source file and generating data sets...");
            var rows = File.ReadAllLines(sourceFile);

       
            foreach (var row in rows)
            {
               

                var values = row.Split(',');
               
                var observation = new double[values.Length];
          
                
                for (int i = 0; i < values.Length; i++)
                {
                    double.TryParse(values[i], result: out observation[i]);


               
                }
     
               
                data.Add(observation);
                
              count5.Add(observation);
  
            }
            


            List<double[]> trainData;
            List<double[]> testData;
            Helpers.GenerateDataSets(data, out trainData, out testData, 0.8);

            Console.WriteLine("Done!");
            Console.WriteLine();
            #endregion

            #region Normalization
            Console.WriteLine("Normalizing data...");
            List<double[]> normalizedTrainData = Helpers.NormalizeData(trainData, inputColumns);
            List<double[]> normalizedTestData = Helpers.NormalizeData(testData, inputColumns);

            Console.WriteLine("Done!");
            Console.WriteLine();
            #endregion

            #region Initializing the Neural Network
            Console.WriteLine("Creating a new {0}-input, {1}-hidden, {2}-output neural network...", numInput, numHidden, numOutput);
            nn = new NeuralNetwork(numInput, numHidden, numOutput);

            Console.WriteLine("Initializing weights and bias to small random values...");
            nn.InitializeWeights();

            Console.WriteLine("Done!");
            Console.WriteLine();
            #endregion

            #region Training
            Console.WriteLine("Beginning training using incremental back-propagation...");
            nn.Train(normalizedTrainData.ToArray(), maxEpochs, learnRate, momentum, weightDecay);

            Console.WriteLine("Done!");
            Console.WriteLine();
            #endregion

            #region Results
            double[] weights = nn.GetWeights();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Final neural network weights and bias values:");
            Console.ResetColor();
            Helpers.ShowVector(weights, 10, 3, true);
            Console.WriteLine();

            double trainAcc = nn.Accuracy(normalizedTrainData.ToArray());
            Console.WriteLine("Accuracy on training data = " + trainAcc.ToString("F4"));
            double testAcc = nn.Accuracy(normalizedTestData.ToArray());
            Console.WriteLine("Accuracy on test data = " + testAcc.ToString("F4"));
            Console.WriteLine();

            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine("Raw results:");
            //Console.ResetColor();
            Console.WriteLine(nn.ToString());
            #endregion

             trian(data);
           // getCount();
            


        }

        public void trian(List<double[]> count52)
        {
            

            List<double[]> trainData;
            List<double[]> testData;
            Helpers.GenerateDataSets(count52, out trainData, out testData, 0.8);

            Console.WriteLine("Done!");
            Console.WriteLine();


            #region Normalization
            Console.WriteLine("Normalizing data...");
            List<double[]> normalizedTrainData = Helpers.NormalizeData(trainData, inputColumns);
            List<double[]> normalizedTestData = Helpers.NormalizeData(testData, inputColumns);

            //Console.WriteLine("Done!");
            //Console.WriteLine();
            //#endregion

            //#region Initializing the Neural Network
            //Console.WriteLine("Creating a new {0}-input, {1}-hidden, {2}-output neural network...", numInput, numHidden, numOutput);
            //var nn = new NeuralNetwork(numInput, numHidden, numOutput);

            //Console.WriteLine("Initializing weights and bias to small random values...");
            //nn.InitializeWeights();

            //Console.WriteLine("Done!");
            //Console.WriteLine();
            //#endregion

            //#region Training
            //Console.WriteLine("Beginning training using incremental back-propagation...");
            //nn.Train(normalizedTrainData.ToArray(), maxEpochs, learnRate, momentum, weightDecay);

            //Console.WriteLine("Done!");
            ////Console.WriteLine();
            //#endregion

            //#region Results
            //double[] weights = nn.GetWeights();
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine("Final neural network weights and bias values:");
            //Console.ResetColor();
            //Helpers.ShowVector(weights, 10, 3, true);
            //Console.WriteLine();

            double trainAcc = nn.Accuracy(normalizedTrainData.ToArray());
            Console.WriteLine("Accuracy on training data = " + trainAcc.ToString("F4"));
            double testAcc = nn.Accuracy(normalizedTestData.ToArray());
            Console.WriteLine("Accuracy on test data = " + testAcc.ToString("F4"));
            Console.WriteLine();

            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine("Raw results:");
            //Console.ResetColor();
            // Console.WriteLine(nn.ToString());
            #endregion
            Console.WriteLine(data.Count);


        }
    }
}

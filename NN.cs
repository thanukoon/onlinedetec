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
        private static readonly string sourceFile = Path.Combine(Environment.CurrentDirectory, "fall1.csv"); //breast-cancer-wisconsin
                                                                                                               // private static readonly string scource = 
                                                                                                               // Number of input neurons, hidden neurons and output neurons
        private static readonly int[] inputColumns = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54 }; // ไว้เพิ่มcolumn
        private static readonly int numInput = inputColumns.Length;
        private const int numHidden = 20;
        private const int numOutput = 2;

        // Parameters for NN training
        private const int maxEpochs = 2000;
        private const double learnRate = 0.05;
        private const double momentum = 0.01;
        private const double weightDecay = 0.0001;
        List<double[]> count5 = new List<double[]>();
        List<double[]> data = new List<double[]>();
        ArrayList item = new ArrayList();
       public double[] array = new double[1000];
        int d = 0;
        NeuralNetwork nn;
        // public var data = new List<double[]>();
        
        public void getdata(double ab)
        {
            
            
            if (d < 56)
            {
                array[d] = ab;
                Console.WriteLine(array[d]);
                d++;
                /*  item.Add(ab);
                  double[] dd = { ab };
                  count5.Add(dd);
                  d++;
                 // getCount();*/
                // getCount();
                
                
            }
            else
            {

                   for (int i = 0; i <=55; i++)
                   {

                       count5[0][i] = array[i];
                   } 
               

                d = 0;
               count5 = count5.Distinct().ToList();
                trian(count5);
            }

           // getCount();
        }

        void getCount()
        {
            Random rnd = new Random();
            //  Console.WriteLine(count5.Count);
            Console.WriteLine();
            //Random rnd = new Random();
            for (int i = 0;i<54;i++)
            {
                array[i] = 0.214 ;
                /*  count5[0][1] = (new double[][]
                 {
                      array[0]
                  });*/
                count5[0][i] = array[i];

            }
            
           
            




            //  trian(count5);
           // trian(count5);
            //  Console.WriteLine(data.Count);


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

            // trian(data);
            // getCount();
           // yes();
           // getCount();


        }

        public void trian(List<double[]> count52)
        {
           foreach (var it in count52)
            {
                Console.WriteLine(it[0]);
            }

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
             Console.WriteLine(nn.ToString());
            #endregion
            d = 0;
           // Console.WriteLine(data.Count);


        }
    }
}

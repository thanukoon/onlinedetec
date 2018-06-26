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
using System.Net;
using System.Windows;

namespace Microsoft.Samples.Kinect.BodyBasics
{

    class NN
    {
        private static readonly string sourceFile = Path.Combine(Environment.CurrentDirectory, "datatest.csv"); //breast-cancer-wisconsin
                                                                                                                // private static readonly string scource = 
                                                                                                                // Number of input neurons, hidden neurons and output neurons

        private static readonly string sourceFile2 = Path.Combine(Environment.CurrentDirectory, "wei.csv");


        private static readonly int[] inputColumns = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54 }; // ไว้เพิ่มcolumn
        private static readonly int numInput = inputColumns.Length;
        private const int numHidden = 20;
        private const int numOutput = 2;

        // Parameters for NN training
        private const int maxEpochs = 500;
        private const double learnRate = 0.05;
        private const double momentum = 0.01;
        private const double weightDecay = 0.0001;
        static List<double[]> cameraData = new List<double[]>();
        List<double[]> dataFile = new List<double[]>();
        public double[] wei;
        List<double> test = new List<double>();



        //  double[] array = new double[1000];

        NeuralNetwork nn;
        //  Train n2 = new Train();



        public void getdata(double[] ab)
        {
            for (int i = 0; i < ab.Length; i++)
            {
                cameraData[0][i] = ab[i];
            }
            // Console.WriteLine(ab.Length);
            trian(cameraData);

        }

        public void getCount()
        {
            var rows = File.ReadAllLines(sourceFile);
            var ro2 = File.ReadAllLines(sourceFile2);

            //paraell 2นิวรอน
            foreach (var row in ro2)
            {


                var values = row.Split(',');


                var observation = new double[values.Length];


                for (int i = 0; i < values.Length; i++)
                {
                    double.TryParse(values[i], result: out observation[i]); //เป็นการเปลี่ยนค่าให้เป็น doubleและ สามารถแปลงเป็นชนิดข้อมูลที่เราต้องการได้หรือไม่ 
                                                                            //  Console.WriteLine(observation[i]);


                }
                test.AddRange(observation);




            }
            


            foreach (var row in rows)
            {


                var values = row.Split(',');

                var observation = new double[values.Length];


                for (int i = 0; i < values.Length; i++)
                {
                    double.TryParse(values[i], result: out observation[i]); //เป็นการเปลี่ยนค่าให้เป็น doubleและ สามารถแปลงเป็นชนิดข้อมูลที่เราต้องการได้หรือไม่ 
                                                                            //  Console.WriteLine(observation[i]);


                }


                dataFile.Add(observation);


                cameraData.Add(observation);


            }
            //  cameraData.RemoveRange(0, 0);
            Random rnd = new Random();
            // double[] test ={1.416,1.415,1.414,1.413,1.412,1.411,1.41,1.408,1.406,1.405,1.4,1.395,1.387,1.373,1.355,1.326,1.297,1.292,1.248,1.165,1.118,1.062,0.997,0.94,0.882,0.82,0.755,0.702,0.624,0.687,0.644,0.597,0.585,0.498,0.458,0.426,0.391,0.346,0.328,0.297,0.221,0.21,0.197,0.17,0.182,0.191,0.19,0.194,0.214,0.223,0.213,0.208,0.216,0.235,0.278};
            // Console.WriteLine(cameraData.Count);
            for (int i = 0; i < 54; i++)
            {
                cameraData[0][i] = 1.425;

            }
            // Console.WriteLine(cameraData.Count);

            trian(cameraData);
        }



        public void getData()
        {
            //    Console.WriteLine(inputColumns);
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
                    double.TryParse(values[i], result: out observation[i]); //เป็นการเปลี่ยนค่าให้เป็น doubleและ สามารถแปลงเป็นชนิดข้อมูลที่เราต้องการได้หรือไม่ 
                                                                            //  Console.WriteLine(observation[i]);


                }


                dataFile.Add(observation);


                cameraData.Add(observation);


            }



            List<double[]> trainData;
            List<double[]> testData;
            Helpers.GenerateDataSets(dataFile, out trainData, out testData, 0.8); // เป็นการสร้าง data weight ของแต่ละตัว โดยการrandom เข้า traindata และ testdata

            Console.WriteLine("Done!");
            Console.WriteLine();
            #endregion

            #region Normalization
            Console.WriteLine("Normalizing data...");
            List<double[]> normalizedTrainData = Helpers.NormalizeData(trainData, inputColumns); //
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
            //   wei = nn.GetWeights();
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
            /*  if (testAcc<0.8)
               {
                   double testac = testAcc*100;
                   string messageBoxText = "Accuracy น้อยกว่า 90%"+" "+"และค่าพยากรณ์ครั้งนี้คือ "+testac.ToString("F4")+"%";
                   string caption = "Word Processor" ;
                   MessageBoxImage icon = MessageBoxImage.Warning;
                   MessageBoxButton button = MessageBoxButton.OK;
                   MessageBox.Show(messageBoxText, caption, button, icon);
                   Application.Current.Dispatcher.Invoke((Action)delegate {
                       Application.Current.Shutdown();
                      System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                   });
                   // Application.Current.Exit();
               } */

            var ro2 = File.ReadAllLines(sourceFile2);


            foreach (var row in ro2)
            {


                var values = row.Split(',');


                var observation = new double[values.Length];


                for (int i = 0; i < values.Length; i++)
                {
                    double.TryParse(values[i], result: out observation[i]); //เป็นการเปลี่ยนค่าให้เป็น doubleและ สามารถแปลงเป็นชนิดข้อมูลที่เราต้องการได้หรือไม่ 
                                                                            //  Console.WriteLine(observation[i]);


                }
                test.AddRange(observation);




            }

            getCount();
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine("Raw results:");
            //Console.ResetColor();
            //  Console.WriteLine(nn.ToString());
            //double[] a = nn.output();
            Console.WriteLine("asd");
            //   Console.WriteLine(a[0] +" "+a[1]);
            //getCount();


            #endregion
            //mainwin.getarray(cameraData);

            // trian(data);
            // getCount(cameraData);
            // yes();
            // n2.getlist(cameraData);




        }

        public void trian(List<double[]> cameraData2)
        {

            

            for (int i = 0; i < test.Count; i++)
            {
                //    Console.WriteLine(Math.Round(test[i] , 3)); //online
            }

            //ใช้ส่วนตอนกำลังออนไลน์ทำงาาน:)


            List<double[]> trainData;
            List<double[]> testData;
            Helpers.GenerateDataSets(cameraData2, out trainData, out testData, 0.8);

            //  Console.WriteLine("Done!");
            Console.WriteLine();


            #region Normalization
            // Console.WriteLine("Normalizing data...");
            List<double[]> normalizedTrainData = Helpers.NormalizeData(trainData, inputColumns);
            List<double[]> normalizedTestData = Helpers.NormalizeData(testData, inputColumns);

            //Console.WriteLine("Done!");
            Console.WriteLine();
            //#endregion

            //#region Initializing the Neural Network
            // Console.WriteLine("Creating a new {0}-input, {1}-hidden, {2}-output neural network...", numInput, numHidden, numOutput);
            var nn = new NeuralNetwork(numInput, numHidden, numOutput);

            nn.SetWeights(test.ToArray());
            //Console.WriteLine("Initializing weights and bias to small random values...");
            //  nn.InitializeWeights();

            //Console.WriteLine("Done!");
            //Console.WriteLine();
            //#endregion

            //#region Training
            //Console.WriteLine("Beginning training using incremental back-propagation...");
            //    nn.Train(normalizedTrainData.ToArray(), maxEpochs, learnRate, momentum, weightDecay);

            //Console.WriteLine("Done!");
            ////Console.WriteLine();
            //#endregion

            //#region Results
            //    double[] weights = nn.GetWeights();
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
            //  Console.WriteLine(nn.ToString());

            double[] checkacc = nn.output();

            for (int i = 0; i < checkacc.Length; i++)
            {
                checkacc[i] = Math.Round(checkacc[i], 0);
                //     out1[i] = Convert.ToInt32(a[i],0);

            }

            Console.WriteLine(checkacc[0] + " " + checkacc[1]);
            if (checkacc[0] == 1 && checkacc[1] == 0)
            {
                //       Console.WriteLine("falldetection");  //alert lineS
                //     string UTL = "http://localhost/line/index.php";
                //     WebClient client = new WebClient();
                //     client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                //      Stream data = client.OpenRead(UTL);
                //     StreamReader reader = new StreamReader(data);
                //     string s = reader.ReadToEnd(); 
                Console.WriteLine("fall2");

            }
            else
            {

                /* Console.WriteLine("falldetection");  //alert lineS
                 string UTL = "http://localhost/line/index.php";
                 WebClient client = new WebClient();
                 client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                 Stream data = client.OpenRead(UTL);
                 StreamReader reader = new StreamReader(data);
                 string s = reader.ReadToEnd(); */

                Console.WriteLine("no fall");

            }



            #endregion
            // d = 0;
            // Console.WriteLine(data.Count);
            getCount();
        }


    }
}
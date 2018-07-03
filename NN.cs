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
        private static readonly string sourceFile = Path.Combine(Environment.CurrentDirectory, "ttest.csv"); //breast-cancer-wisconsin
                                                                                                                // private static readonly string scource = 
                                                                                                                // Number of input neurons, hidden neurons and output neurons

        private static readonly string sourceFilehead = Path.Combine(Environment.CurrentDirectory, "weihead.csv");
        private static readonly string sourceFilespine = Path.Combine(Environment.CurrentDirectory, "weispine.csv");


        private static readonly int[] inputColumns = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99 }; // ไว้เพิ่มcolumn
        private static readonly int numInput = inputColumns.Length;
        private const int numHidden = 30;
        private const int numOutput = 2;

        // Parameters for NN training
        private const int maxEpochs = 500;
        private const double learnRate = 0.05;
        private const double momentum = 0.01;
        private const double weightDecay = 0.0001;
        static List<double[]> cameraData = new List<double[]>();
       public static List<double[]> dataFile = new List<double[]>();
        public static List<double[]> datahead = new List<double[]>();
        public static List<double[]> dataspine = new List<double[]>();
        public static double[] checkacc;
        public static double[] checkaccspine;
        public double[] sliding = new double[100];
        public static double[] slidingspine;
        public static double[] slidinghead;

        List<double> weihead = new List<double>();

        List<double> weispine = new List<double>();
        public static int cc;




        //  double[] array = new double[1000];

        NeuralNetwork nn;
        //  Train n2 = new Train();



        public void getdata(double[] abhead, double[] abspine)
        {

            var rows = File.ReadAllLines(sourceFile);
            foreach (var row in rows)
            {
                var values = row.Split(',');
                cc = values.Length;
                var observation = new double[values.Length];


                for (int i = 0; i < values.Length; i++)
                {
                    double.TryParse(values[i], result: out observation[i]); //เป็นการเปลี่ยนค่าให้เป็น doubleและ สามารถแปลงเป็นชนิดข้อมูลที่เราต้องการได้หรือไม่ 
                                                                            //  Console.WriteLine(observation[i]);


                }


                dataFile.Add(observation);
                cameraData.Add(observation);
            }
            datahead.AddRange(dataFile);
            int remove = dataFile.Count / 2;
            datahead.RemoveRange(0, remove);

            dataspine.AddRange(datahead);
            int head = 0;
            int spine = 0;
            int counthead = 0;
            int k = 15;
            for (int i = 0; i < dataFile.Count; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < cc; j++)
                    {
                        datahead[head][j] = dataFile[i][j];
                        //         Console.WriteLine(datahead[head][j]);
                        counthead++;
                    }

                    counthead = 0;
                    //       Console.WriteLine("asd");
                    head++;
                }
                else
                {
                    for (int j = 0; j < cc; j++)
                    {
                        dataspine[spine][j] = dataFile[i][j];
                        //    Console.WriteLine(dataspine[spine][j]);

                    }
                    spine++;
                }


            }
            slidinghead = printKMin(abhead, abhead.Length);
            slidingspine = printKMin(abspine, abspine.Length); //เทสๆๆๆ


            for (int i = 0; i < slidinghead.Count(); i++)
            {
                datahead[0][i] = slidinghead[i];
                
                dataspine[0][i] = slidingspine[i];
            }


           

            // Console.WriteLine(ab.Length);
            //Console.WriteLine(datahead.Count);
            //Console.WriteLine(dataspine.Count);

            trian(datahead, dataspine);
            datahead.Clear();
            dataspine.Clear();

        }

        public double[] printKMin(double[] arr, int n)
        {
            int k = 20;
            int bfslide = 80;
            int count = 0;
            int j;
            double min;
            for (int i = 0; i < n - k; i++)
            {
                min = arr[i];
                for (j = 1; j < k; j++)
                {
                    if (arr[i + j] < min)
                    {
                        min = arr[i + j];
                    }
                }
                sliding[i] = Math.Round(min, 3);
            }
            for (int i = arr.Length - count; i < arr.Length; i++)
            {
                arr[i] = sliding[i - bfslide];
            }
            return arr;
        }



        public void getCount()
        {
           

            //  cameraData.RemoveRange(0, 0);
            Random rnd = new Random();
            // double[] test ={1.416,1.415,1.414,1.413,1.412,1.411,1.41,1.408,1.406,1.405,1.4,1.395,1.387,1.373,1.355,1.326,1.297,1.292,1.248,1.165,1.118,1.062,0.997,0.94,0.882,0.82,0.755,0.702,0.624,0.687,0.644,0.597,0.585,0.498,0.458,0.426,0.391,0.346,0.328,0.297,0.221,0.21,0.197,0.17,0.182,0.191,0.19,0.194,0.214,0.223,0.213,0.208,0.216,0.235,0.278};
            // Console.WriteLine(cameraData.Count);
            for (int i = 0; i < 54; i++)
            {
                cameraData[0][i] = 1.425;

            }
            // Console.WriteLine(cameraData.Count);

            trian(datahead ,dataspine);
        }



        public void getData()
        {



            //var rows = File.ReadAllLines(sourceFile);
            //foreach (var row in rows)
            //{
            //    var values = row.Split(',');
            //    cc = values.Length;
            //    var observation = new double[values.Length];


            //    for (int i = 0; i < values.Length; i++)
            //    {
            //        double.TryParse(values[i], result: out observation[i]); //เป็นการเปลี่ยนค่าให้เป็น doubleและ สามารถแปลงเป็นชนิดข้อมูลที่เราต้องการได้หรือไม่ 
            //      //  Console.WriteLine(observation[i]);


            //    }


            //    dataFile.Add(observation);
            //    cameraData.Add(observation);
            //}
            //datahead.AddRange(dataFile);
            //int remove = dataFile.Count / 2;
            //datahead.RemoveRange(0, remove);
          
            //dataspine.AddRange(datahead);
            //int head = 0;
            //int spine = 0;
            //int counthead = 0;
            //int k = 15;
            //for (int i = 0; i < dataFile.Count; i++)
            //{
            //    if (i % 2 == 0)
            //    {
            //        for (int j = 0; j < cc; j++)
            //        {
            //            datahead[head][j] = dataFile[i][j];
            //   //         Console.WriteLine(datahead[head][j]);
            //            counthead++;
            //        }
                   
            //        counthead = 0;
            //        //       Console.WriteLine("asd");
            //        head++;
            //    }
            //    else
            //    {
            //        for (int j = 0; j < cc; j++)
            //        {
            //            dataspine[spine][j] = dataFile[i][j];
            //        //    Console.WriteLine(dataspine[spine][j]);

            //        }
            //        spine++;
            //    }


            //}
         //   Console.WriteLine(datahead.Count);
          //  getCount();

        }

        public void trian(List<double[]> cameraData2head , List<double[]> cameraDataspine)
        {

            
            var ro2 = File.ReadAllLines(sourceFilehead);
            var ro3 = File.ReadAllLines(sourceFilespine);

            //paraell 2นิวรอน
            foreach (var row in ro2)
            {


                var values = row.Split(',');


                var observation = new double[values.Length];


                for (int i = 0; i < values.Length; i++)
                {
                    double.TryParse(values[i], result: out observation[i]); //เป็นการเปลี่ยนค่าให้เป็น doubleและ สามารถแปลงเป็นชนิดข้อมูลที่เราต้องการได้หรือไม่ 
//                    Console.WriteLine(observation[i]);                                                        //  Console.WriteLine(observation[i]);


                }
                weihead.AddRange(observation);




            }

            foreach (var row in ro3)
            {


                var values = row.Split(',');


                var observation = new double[values.Length];


                for (int i = 0; i < values.Length; i++)
                {
                    double.TryParse(values[i], result: out observation[i]); //เป็นการเปลี่ยนค่าให้เป็น doubleและ สามารถแปลงเป็นชนิดข้อมูลที่เราต้องการได้หรือไม่ 
                                                                            //  Console.WriteLine(observation[i]);


                }
                weispine.AddRange(observation);




            }
            
            List<double[]> trainData;
            List<double[]> testData;
            List<double[]> trainDataspine;
            List<double[]> testDataspine;
            try
            {

                Parallel.Invoke(() =>
                {
                    Helpers.GenerateDataSets(cameraData2head, out trainData, out testData, 0.8);

                    Helperspine.GenerateDataSets(cameraDataspine, out trainDataspine, out testDataspine, 0.8);

               //     Console.WriteLine();

                    List<double[]> normalizedTrainData = Helpers.NormalizeData(trainData, inputColumns);
                    List<double[]> normalizedTestData = Helpers.NormalizeData(testData, inputColumns);

                    List<double[]> normalizedTrainDataspine = Helperspine.NormalizeData(trainDataspine, inputColumns);
                    List<double[]> normalizedTestDataspine = Helperspine.NormalizeData(testDataspine, inputColumns);

                 //   Console.WriteLine();
                  
                    var nn = new NeuralNetwork(numInput, numHidden, numOutput);
                    var nn2 = new NeuralNetworkspine(numInput, numHidden, numOutput);

                    nn.SetWeights(weihead.ToArray());
                    nn2.SetWeights(weispine.ToArray());
                   

                    double trainAcc = nn.Accuracy(normalizedTrainData.ToArray());
                    Console.WriteLine("Accuracy on training data = " + trainAcc.ToString("F4"));
                    double testAcc = nn.Accuracy(normalizedTestData.ToArray());
                    Console.WriteLine("Accuracy on test data = " + testAcc.ToString("F4"));
                    Console.WriteLine();

                    double trainAccspine = nn2.Accuracy(normalizedTrainDataspine.ToArray());
                    Console.WriteLine("Accuracy on training data = " + trainAccspine.ToString("F4"));
                    double testAccspine = nn2.Accuracy(normalizedTestDataspine.ToArray());
                    Console.WriteLine("Accuracy on test data = " + testAccspine.ToString("F4"));
                    Console.WriteLine();


                    checkacc = nn.output();
                    checkaccspine = nn2.output();
                    for (int i = 0; i < checkacc.Length; i++)
                    {
                        checkacc[i] = Math.Round(checkacc[i], 0);
                    }
                    for (int i = 0; i < checkaccspine.Length; i++)
                    {
                        checkaccspine[i] = Math.Round(checkaccspine[i], 0);
                    }


                });
                
            }
            catch (AggregateException a)
            {
                Console.WriteLine(a);
            }

            ///Parael


          /*  Helpers.GenerateDataSets(cameraData2head, out trainData, out testData, 0.8);

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

            nn.SetWeights(weihead.ToArray());
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

            } */

            Console.WriteLine(checkacc[0] + " " + checkacc[1]);
            Console.WriteLine();
            Console.WriteLine(checkaccspine[0] + " " + checkaccspine[1]);
            if (checkacc[0] == 1 && checkacc[1] == 0 &&checkaccspine[0] == 1 && checkaccspine[1] == 0)
            {
                //       Console.WriteLine("falldetection");  //alert lineS
                //     string UTL = "http://localhost/line/index.php";
                //     WebClient client = new WebClient();
                //     client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                //      Stream data = client.OpenRead(UTL);
                //     StreamReader reader = new StreamReader(data);
                //     string s = reader.ReadToEnd(); 
                Console.WriteLine("fall");

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
            weihead.Clear();
            weispine.Clear();



           
            // d = 0;
            // Console.WriteLine(data.Count);
          // getCount();
        }


    }
}
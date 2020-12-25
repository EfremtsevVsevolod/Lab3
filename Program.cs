using System;
using System.IO;

namespace Lab2
{
    class Program
    {
        static void Main()
        {
            var v5MainCollectionInstance = new V5MainCollection();
            v5MainCollectionInstance.AddDefaults();

            Console.WriteLine(v5MainCollectionInstance.ToString());

            v5MainCollectionInstance.DataChanged += DataChangedEventHandler;

            var grid = new Grid2D(3, 3, 1.0f, 1.0f);
            var v5DataOnGridInstance =
                new V5DataOnGrid("Service info", new DateTime(2020, 12, 20), grid);

            v5MainCollectionInstance.Add(v5DataOnGridInstance);
            v5MainCollectionInstance[3] = new V5DataCollection("ababa", DateTime.Now);
            v5MainCollectionInstance[0].ServiceInfo = "Service info";
            v5MainCollectionInstance[0].MeasurementTime = new DateTime(2020, 12, 20);

            Console.WriteLine("======");
            v5MainCollectionInstance[3].ServiceInfo = "Service info";
            v5MainCollectionInstance[3].MeasurementTime = new DateTime(2020, 12, 20);

            v5MainCollectionInstance.Remove("Service info", new DateTime(2020, 12, 20));
        }

        static void DataChangedEventHandler(object sender, DataChangedEventArgs args)
        {
            Console.WriteLine(args.ToString());
        }
    }
}
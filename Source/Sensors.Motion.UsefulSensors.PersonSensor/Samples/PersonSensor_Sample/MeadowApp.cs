using Meadow;
using Meadow.Devices;
using Meadow.Foundation.Sensors;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sensors.PersonSensor_Sample
{
    public class MeadowApp : App<F7CoreComputeV2>
    {
        //<!=SNIP=>
        PersonSensor personSensor;


        public override Task Initialize()
        {
            Console.WriteLine("Initialize...");

            personSensor = new PersonSensor(Device.CreateI2cBus());


            return Task.CompletedTask;
        }

        public override Task Run()
        {
            while (true)
            {
                var sensorData = personSensor.GetSensorData();
                DisplaySensorData(sensorData);

                Thread.Sleep(2000);
            }
        }

        private void DisplaySensorData(PersonSensorResults sensorData)
        {
            Console.WriteLine("********");
            Console.WriteLine($"{sensorData.NumberOfFaces} faces found");

            for (int i = 0; i < sensorData.NumberOfFaces && i < 4; ++i)
            {
                var face = sensorData.FaceData[i];
                Console.Write($"Face #{i}: ");
                Console.Write($"{face.BoxConfidence} confidence, ");
                Console.Write($"({face.BoxLeft}, {face.BoxTop}), ");
                Console.Write($"({face.BoxRight}, {face.BoxBottom}), ");
                Console.WriteLine(face.IsFacing == 1 ? "facing" : "not facing");
            }
        }

        //<!=SNOP=>
    }
}
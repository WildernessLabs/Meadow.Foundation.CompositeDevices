using Meadow;
using Meadow.Devices;
using Meadow.Foundation.Sensors.Camera;
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
            Resolver.Log.Info("Initialize...");

            personSensor = new PersonSensor(Device.CreateI2cBus());


            return Task.CompletedTask;
        }

        public override Task Run()
        {
            while (true)
            {
                var sensorData = personSensor.GetSensorData();
                DisplaySensorData(sensorData);

                Thread.Sleep(1500);
            }
        }

        private void DisplaySensorData(PersonSensorResults sensorData)
        {
            if (sensorData.NumberOfFaces == 0)
            {
                Resolver.Log.Info("No faces found");
                return;
            }

            for (int i = 0; i < sensorData.NumberOfFaces; ++i)
            {
                var face = sensorData.FaceData[i];
                Resolver.Log.Info($"Face #{i}: {face.BoxConfidence} confidence, ({face.BoxLeft}, {face.BoxTop}), ({face.BoxRight}, {face.BoxBottom}), facing: {face.IsFacing}");
            }
        }

        //<!=SNOP=>
    }
}
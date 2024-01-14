using Meadow;
using Meadow.Devices;
using Meadow.Foundation.Sensors;
using System;
using System.Threading.Tasks;

namespace Sensors.TinyCodeReader_Sample
{
    public class MeadowApp : App<F7CoreComputeV2>
    {
        //<!=SNIP=>
        TinyCodeReader tinyCodeReader;


        public override Task Initialize()
        {
            Resolver.Log.Info("Initialize...");

            tinyCodeReader = new TinyCodeReader(Device.CreateI2cBus());


            return Task.CompletedTask;
        }

        public override Task Run()
        {
            //one time read 
            var qrCode = tinyCodeReader.ReadCode();

            if (qrCode != null)
            {
                Resolver.Log.Info($"QR Code: {qrCode}");
            }
            else
            {
                Resolver.Log.Info("No QR Code Found");
            }

            //continuous read
            tinyCodeReader.CodeRead += TinyCodeReader_CodeRead;
            tinyCodeReader.StartUpdating(TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        private void TinyCodeReader_CodeRead(object sender, string e)
        {
            Resolver.Log.Info($"QRCode message: {e} ({DateTime.Now})");
        }

        //<!=SNOP=>
    }
}
using Meadow;
using Meadow.Devices;
using Meadow.Foundation.Relays;
using System;
using System.Threading.Tasks;

namespace Relays.ElectronmagneticRelayModule_Sample
{
    public class MeadowApp : App<F7FeatherV2>
    {
        //<!=SNIP=>

        private ElectromagneticRelayModule module;

        public override Task Initialize()
        {
            Console.WriteLine("Initialize...");

            module = new ElectromagneticRelayModule(Device.CreateI2cBus(), 0x20);

            return Task.CompletedTask;
        }

        public override Task Run()
        {

            return Task.CompletedTask;
        }

        //<!=SNOP=>
    }
}
using Meadow;
using Meadow.Devices;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Relays.ElectronmagneticRelayModule_Sample
{
    public class MeadowApp : App<F7FeatherV2>
    {
        //<!=SNIP=>


        public override Task Initialize()
        {
            Console.WriteLine("Initialize...");


            return Task.CompletedTask;
        }

        public override Task Run()
        {

            return Task.CompletedTask;
        }

        //<!=SNOP=>
    }
}
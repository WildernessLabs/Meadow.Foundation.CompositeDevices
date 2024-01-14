using Meadow;
using Meadow.Devices;
using Meadow.Foundation.Relays;
using Meadow.Peripherals.Relays;
using System.Threading;
using System.Threading.Tasks;
using static Meadow.Foundation.Relays.ElectromagneticRelayModule;

namespace Relays.ElectronmagneticRelayModule_Sample
{
    public class MeadowApp : App<F7FeatherV2>
    {
        //<!=SNIP=>

        private ElectromagneticRelayModule module;

        public override Task Initialize()
        {
            Resolver.Log.Info("Initialize...");

            module = new ElectromagneticRelayModule(Device.CreateI2cBus(), ElectromagneticRelayModule.GetAddressFromPins(false, false, false));

            return Task.CompletedTask;
        }

        public override Task Run()
        {
            for (int i = 0; i < 5; i++)
            {
                Resolver.Log.Info("All on (closed)");
                module.SetAllOn();

                Thread.Sleep(1000);

                Resolver.Log.Info("All off (open)");
                module.SetAllOff();

                Thread.Sleep(1000);

                for (int j = 0; j < (int)RelayIndex.Relay4; j++)
                {
                    Resolver.Log.Info($"{(RelayIndex)j} on (closed)");
                    module.Relays[j].State = RelayState.Closed;
                    Thread.Sleep(1000);
                }

                for (int j = 0; j < (int)RelayIndex.Relay4; j++)
                {
                    Resolver.Log.Info($"{(RelayIndex)j} off (open)");
                    module.Relays[j].State = RelayState.Open;
                    Thread.Sleep(1000);
                }
            }

            return Task.CompletedTask;
        }

        //<!=SNOP=>
    }
}
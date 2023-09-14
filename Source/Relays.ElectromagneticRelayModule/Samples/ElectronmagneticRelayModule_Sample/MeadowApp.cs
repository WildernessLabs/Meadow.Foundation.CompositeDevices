﻿using Meadow;
using Meadow.Devices;
using Meadow.Foundation.Relays;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Meadow.Foundation.Relays.ElectromagneticRelayModule;

namespace Relays.ElectronmagneticRelayModule_Sample
{
    public class MeadowApp : App<F7CoreComputeV2>
    {
        //<!=SNIP=>

        private ElectromagneticRelayModule module;

        public override Task Initialize()
        {
            Console.WriteLine("Initialize...");

            module = new ElectromagneticRelayModule(Device.CreateI2cBus(), ElectromagneticRelayModule.GetAddressFromPins(false, false, false));

            return Task.CompletedTask;
        }

        public override Task Run()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.Write("All on");
                module.SetAllOn();

                Thread.Sleep(1000);

                Console.Write("All off");
                module.SetAllOff();

                Thread.Sleep(1000);

                for (int j = 0; j < 4; j++)
                {
                    Console.Write($"{(RelayIndex)j} on");
                    module.SetRelayState((RelayIndex)j, true);
                    Thread.Sleep(1000);
                }

                for (int j = 0; j < 4; j++)
                {
                    Console.Write($"{(RelayIndex)j} off");
                    module.SetRelayState((RelayIndex)j, false);
                    Thread.Sleep(1000);
                }
            }

            return Task.CompletedTask;
        }

        //<!=SNOP=>
    }
}
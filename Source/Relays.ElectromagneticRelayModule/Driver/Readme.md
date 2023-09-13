# Meadow.Foundation.Relays.ElectromagneticRelayModule

**I2C 4 Channel Electromagnetic Relay Module**

The **ElectromagneticRelayModule** library is designed for the [Wilderness Labs](www.wildernesslabs.co) Meadow .NET IoT platform and is part of [Meadow.Foundation](https://developer.wildernesslabs.co/Meadow/Meadow.Foundation/)

The **Meadow.Foundation** peripherals library is an open-source repository of drivers and libraries that streamline and simplify adding hardware to your C# .NET Meadow IoT application.

For more information on developing for Meadow, visit [developer.wildernesslabs.co](http://developer.wildernesslabs.co/), to view all Wilderness Labs open-source projects, including samples, visit [github.com/wildernesslabs](https://github.com/wildernesslabs/)

## Usage

```
private ElectromagneticRelayModule module;

public override Task Initialize()
{
    Console.WriteLine("Initialize...");

    module = new ElectromagneticRelayModule(Device.CreateI2cBus(), 0x20);

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

        for(int j = 0; j < 4; j++)
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

```

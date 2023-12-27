# Meadow.Foundation.Relays.ElectromagneticRelayModule

**I2C 4 Channel Electromagnetic Relay Module**

The **ElectromagneticRelayModule** library is designed for the [Wilderness Labs](www.wildernesslabs.co) Meadow .NET IoT platform and is part of [Meadow.Foundation](https://developer.wildernesslabs.co/Meadow/Meadow.Foundation/).

The **Meadow.Foundation** peripherals library is an open-source repository of drivers and libraries that streamline and simplify adding hardware to your C# .NET Meadow IoT application.

For more information on developing for Meadow, visit [developer.wildernesslabs.co](http://developer.wildernesslabs.co/).

To view all Wilderness Labs open-source projects, including samples, visit [github.com/wildernesslabs](https://github.com/wildernesslabs/).

## Usage

```csharp
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
        Console.Write("All on (closed)");
        module.SetAllOn();

        Thread.Sleep(1000);

        Console.Write("All off (open)");
        module.SetAllOff();

        Thread.Sleep(1000);

        for (int j = 0; j < (int)RelayIndex.Relay4; j++)
        {
            Console.Write($"{(RelayIndex)j} on (closed)");
            module.Relays[j].State = RelayState.Closed;
            Thread.Sleep(1000);
        }

        for (int j = 0; j < (int)RelayIndex.Relay4; j++)
        {
            Console.Write($"{(RelayIndex)j} off (open)");
            module.Relays[j].State = RelayState.Open;
            Thread.Sleep(1000);
        }
    }

    return Task.CompletedTask;
}

```
## How to Contribute

- **Found a bug?** [Report an issue](https://github.com/WildernessLabs/Meadow_Issues/issues)
- Have a **feature idea or driver request?** [Open a new feature request](https://github.com/WildernessLabs/Meadow_Issues/issues)
- Want to **contribute code?** Fork the [Meadow.Foundation.CompositeDevices](https://github.com/WildernessLabs/Meadow.Foundation.CompositeDevices) repository and submit a pull request against the `develop` branch


## Need Help?

If you have questions or need assistance, please join the Wilderness Labs [community on Slack](http://slackinvite.wildernesslabs.co/).

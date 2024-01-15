# Meadow.Foundation.Sensors.Cameras.UsefulSensors.TinyCodeReader

**Useful Sensor's Tiny Code Reader I2C optical QR code reader**

The **UsefulSensorsTinyCodeReader** library is designed for the [Wilderness Labs](www.wildernesslabs.co) Meadow .NET IoT platform and is part of [Meadow.Foundation](https://developer.wildernesslabs.co/Meadow/Meadow.Foundation/).

The **Meadow.Foundation** peripherals library is an open-source repository of drivers and libraries that streamline and simplify adding hardware to your C# .NET Meadow IoT application.

For more information on developing for Meadow, visit [developer.wildernesslabs.co](http://developer.wildernesslabs.co/).

To view all Wilderness Labs open-source projects, including samples, visit [github.com/wildernesslabs](https://github.com/wildernesslabs/).

## Usage

```csharp
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

```
## How to Contribute

- **Found a bug?** [Report an issue](https://github.com/WildernessLabs/Meadow_Issues/issues)
- Have a **feature idea or driver request?** [Open a new feature request](https://github.com/WildernessLabs/Meadow_Issues/issues)
- Want to **contribute code?** Fork the [Meadow.Foundation.CompositeDevices](https://github.com/WildernessLabs/Meadow.Foundation.CompositeDevices) repository and submit a pull request against the `develop` branch


## Need Help?

If you have questions or need assistance, please join the Wilderness Labs [community on Slack](http://slackinvite.wildernesslabs.co/).

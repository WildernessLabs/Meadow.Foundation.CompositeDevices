# Meadow.Foundation.Sensors.PersonSensor

**Useful Sensor's I2C optical person sensor**

The **UsefulSensorsPersonSensor** library is designed for the [Wilderness Labs](www.wildernesslabs.co) Meadow .NET IoT platform and is part of [Meadow.Foundation](https://developer.wildernesslabs.co/Meadow/Meadow.Foundation/).

The **Meadow.Foundation** peripherals library is an open-source repository of drivers and libraries that streamline and simplify adding hardware to your C# .NET Meadow IoT application.

For more information on developing for Meadow, visit [developer.wildernesslabs.co](http://developer.wildernesslabs.co/).

To view all Wilderness Labs open-source projects, including samples, visit [github.com/wildernesslabs](https://github.com/wildernesslabs/).

## Usage

```csharp
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

```
## How to Contribute

- **Found a bug?** [Report an issue](https://github.com/WildernessLabs/Meadow_Issues/issues)
- Have a **feature idea or driver request?** [Open a new feature request](https://github.com/WildernessLabs/Meadow_Issues/issues)
- Want to **contribute code?** Fork the [Meadow.Foundation.CompositeDevices](https://github.com/WildernessLabs/Meadow.Foundation.CompositeDevices) repository and submit a pull request against the `develop` branch


## Need Help?

If you have questions or need assistance, please join the Wilderness Labs [community on Slack](http://slackinvite.wildernesslabs.co/).

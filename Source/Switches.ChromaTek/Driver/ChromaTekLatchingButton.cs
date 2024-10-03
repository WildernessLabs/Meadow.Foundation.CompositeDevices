using Meadow.Foundation.Leds;
using Meadow.Foundation.Sensors.Switches;
using Meadow.Hardware;

namespace Meadow.Foundation.Switches;

/// <summary>
/// Represents a latching push button with a WS2812 color LED
/// </summary>
public class ChromaTekLatchingButton : SpstSwitch
{
    private Ws2812 _leds = default!;
    private int _index;

    /// <summary>
    /// Creates an instance of a ChromaTekLatchingButton
    /// </summary>
    /// <param name="bus">The SPI bus COPI line connected to the WS2812 data line</param>
    /// <param name="inputPort">The interrupt port connected to the switch</param>
    /// <param name="buttonIndex">The index of the button LED for daisy-chained button panels</param>
    public ChromaTekLatchingButton(ISpiBus bus, IDigitalInterruptPort inputPort, int buttonIndex = 0)
        : base(inputPort)
    {
        Initialize(bus, buttonIndex);
    }

    /// <summary>
    /// Creates an instance of a ChromaTekLatchingButton
    /// </summary>
    /// <param name="bus">The SPI bus COPI line connected to the WS2812 data line</param>
    /// <param name="pin">The IPin connected to the switch</param>
    /// <param name="interruptMode">The interrupt mode for the switch pin</param>
    /// <param name="resistorMode">The resistor mode for the switch pin</param>
    /// <param name="buttonIndex">The index of the button LED for daisy-chained button panels</param>
    public ChromaTekLatchingButton(ISpiBus bus, IPin pin, InterruptMode interruptMode, ResistorMode resistorMode, int buttonIndex = 0)
        : base(pin, interruptMode, resistorMode)
    {
        Initialize(bus, buttonIndex);
    }

    private void Initialize(ISpiBus spiBus, int buttonIndex = 0)
    {
        _leds = new Ws2812(spiBus, 20);
        _index = buttonIndex;
    }

    /// <summary>
    /// Sets the LED color of the button
    /// </summary>
    /// <param name="color">The color to set</param>
    public void SetColor(Color color)
    {
        // HACK: this is a hack due to SPI bus weirdness on the F7
        for (var i = _leds.NumberOfLeds - 1; i > _index; i--)
        {
            _leds.SetLed(i, Color.Black);
        }
        _leds.SetLed(_index, color);
        _leds.Show();
    }
}

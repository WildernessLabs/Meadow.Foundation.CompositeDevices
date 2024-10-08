using Meadow.Foundation.Leds;
using Meadow.Foundation.Sensors.Switches;
using Meadow.Hardware;
using System;

namespace Meadow.Foundation.Switches.ChromaTek;

/// <summary>
/// Represents a latching push button with a WS2812 color LED
/// </summary>
public class LatchingButton : SpstSwitch, IChromaTekButton
{
    private ISpiBus? _bus = null;

    internal Ws2812? LedController { get; set; } = default!;
    internal int ButtonIndex { get; set; } = 0;

    /// <summary>
    /// Creates an instance of a ChromaTekLatchingButton
    /// </summary>
    /// <param name="bus">The SPI bus COPI line connected to the WS2812 data line</param>
    /// <param name="inputPort">The interrupt port connected to the switch</param>
    public LatchingButton(IDigitalInterruptPort inputPort, ISpiBus? bus = null)
        : base(inputPort)
    {
        _bus = bus;
    }

    /// <summary>
    /// Creates an instance of a ChromaTekLatchingButton
    /// </summary>
    /// <param name="bus">The SPI bus COPI line connected to the WS2812 data line</param>
    /// <param name="pin">The IPin connected to the switch</param>
    /// <param name="interruptMode">The interrupt mode for the switch pin</param>
    /// <param name="resistorMode">The resistor mode for the switch pin</param>
    public LatchingButton(IPin pin, InterruptMode interruptMode, ResistorMode resistorMode, ISpiBus? bus = null)
        : base(pin, interruptMode, resistorMode)
    {
        _bus = bus;
    }

    private void Initialize()
    {
        if (LedController != null) return;

        if (_bus == null)
        {
            throw new Exception("This button must either be constructed withan ISpiBus or added to a ButtonCollection");
        }

        LedController = new Ws2812(_bus, 1);
    }

    /// <summary>
    /// Sets the LED color of the button
    /// </summary>
    /// <param name="color">The color to set</param>
    public void SetColor(Color color)
    {
        if (LedController == null)
        {
            Initialize();
        }

        LedController?.SetLed(ButtonIndex, color);
        LedController?.Show();
    }
}

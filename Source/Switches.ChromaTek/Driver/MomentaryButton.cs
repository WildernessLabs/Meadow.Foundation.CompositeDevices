using Meadow.Foundation.Leds;
using Meadow.Foundation.Sensors.Buttons;
using Meadow.Hardware;
using System;

namespace Meadow.Foundation.Switches.ChromaTek;

/// <summary>
/// Represents a momentary push button with a WS2812 color LED
/// </summary>
public class MomentaryButton : PushButton, IChromaTekButton
{
    private ISpiBus? _bus = null;

    internal Ws2812 LedController { get; set; } = default!;
    internal int ButtonIndex { get; set; } = 0;

    /// <summary>
    /// Creates an instance of a ChromaTekMomentaryButton
    /// </summary>
    /// <param name="bus">The SPI bus COPI line connected to the WS2812 data line</param>
    /// <param name="inputPort">The interrupt port connected to the switch</param>
    public MomentaryButton(IDigitalInterruptPort inputPort, ISpiBus? bus = null)
        : base(inputPort)
    {
        _bus = bus;
    }

    /// <summary>
    /// Creates an instance of a ChromaTekMomentaryButton
    /// </summary>
    /// <param name="bus">The SPI bus COPI line connected to the WS2812 data line</param>
    /// <param name="pin">The IPin connected to the switch</param>
    /// <param name="resistorMode">The resistor mode for the switch pin</param>
    public MomentaryButton(IPin pin, ResistorMode resistorMode, ISpiBus? bus = null)
        : base(pin, resistorMode)
    {
        _bus = bus;
    }

    /// <summary>
    /// Creates an instance of a ChromaTekMomentaryButton
    /// </summary>
    /// <param name="bus">The SPI bus COPI line connected to the WS2812 data line</param>
    /// <param name="pin">The IPin connected to the switch</param>
    /// <param name="resistorMode">The resistor mode for the switch pin</param>
    /// <param name="debounceDuration">Debounce duration for the interrupt pin</param>
    public MomentaryButton(IPin pin, ResistorMode resistorMode, TimeSpan debounceDuration, ISpiBus? bus = null)
        : base(pin, resistorMode, debounceDuration)
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

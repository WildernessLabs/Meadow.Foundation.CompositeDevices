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
    private ISpiBus? bus = null;
    private Color color = Color.Black;

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
        this.bus = bus;
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
        this.bus = bus;
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
        this.bus = bus;
    }

    private void Initialize()
    {
        if (LedController != null) return;

        if (bus == null)
        {
            throw new Exception("This button must either be constructed withan ISpiBus or added to a ButtonCollection");
        }

        LedController = new Ws2812(bus, 1);
    }

    /// <inheritdoc/>
    public void SetColor(Color color)
    {
        if (LedController == null)
        {
            Initialize();
        }

        LedController?.SetLed(ButtonIndex, color);
        LedController?.Show();
        this.color = color;
    }

    /// <inheritdoc/>
    public Color GetColor() => color;
}

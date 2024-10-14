using Meadow.Foundation.Leds;
using Meadow.Hardware;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Meadow.Foundation.Switches.ChromaTek;

/// <summary>
/// Represents a collection of ChromaTek buttons with WS2812 color LEDs
/// </summary>
public class ButtonCollection : IEnumerable<IChromaTekButton>
{
    private Ws2812 ws2812 = default!;
    private List<IChromaTekButton> buttons = new();

    /// <summary>
    /// Creates a collection of buttons
    /// </summary>
    /// <param name="bus">The SPI bus that all buttons are connected to</param>
    /// <param name="buttons">The list of button on the bus</param>
    public ButtonCollection(ISpiBus bus, params IChromaTekButton[] buttons)
    {
        ws2812 = new Ws2812(bus, buttons.Length);
        var index = 0;

        foreach (var button in buttons)
        {
            switch (button)
            {
                case MomentaryButton mb:
                    mb.LedController = ws2812;
                    mb.ButtonIndex = index;
                    this.buttons.Add(mb);
                    break;
                case LatchingButton lb:
                    lb.LedController = ws2812;
                    lb.ButtonIndex = index;
                    this.buttons.Add(lb);
                    break;
                default: throw new ArgumentException("Button is not a ChromaTek button");

            };
            index++;
        }
    }

    /// <inheritdoc/>
    public IChromaTekButton this[int index]
    {
        get => buttons[index];
    }

    /// <inheritdoc/>
    public IEnumerator<IChromaTekButton> GetEnumerator()
    {
        return buttons.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

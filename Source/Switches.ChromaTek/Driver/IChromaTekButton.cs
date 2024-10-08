namespace Meadow.Foundation.Switches.ChromaTek;

/// <summary>
/// Represents a ChromaTek button with a WS2812 color LED
/// </summary>
public interface IChromaTekButton
{
    /// <summary>
    /// Sets the LED color of the button
    /// </summary>
    /// <param name="color">The color to set</param>
    void SetColor(Color color);
}

using Meadow.Hardware;
using Meadow.Peripherals.Sensors;

namespace Meadow.Foundation.Switches.ChromaTek;

/// <summary>
/// Represents a ChromaTek button with a WS2812 color LED
/// </summary>
public interface IChromaTekButton : IColorable, ISensor<bool>
{
}

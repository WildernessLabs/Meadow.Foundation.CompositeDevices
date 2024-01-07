namespace Meadow.Foundation.Sensors
{
    public partial class PersonSensor
    {
        enum Commands : byte
        {
            MODE = 0x01,
            ENABLE_ID = 0x02,
            SINGLE_SHOT = 0x03,
            CALIBRATE_ID = 0x04,
            PERSIST_IDS = 0x05,
            ERASE_IDS = 0x06,
            DEBUG_MODE = 0x07,
        }
    }
}
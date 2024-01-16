namespace Meadow.Foundation.Sensors.Camera
{
    public partial class PersonSensor
    {
        /// <summary>
        /// Enumeration of configuration commands for the Person Sensor.
        /// </summary>
        enum Commands : byte
        {
            /// <summary>
            /// Mode configuration command. Default: 0x01 (continuous).
            /// </summary>
            MODE = 0x01,
            /// <summary>
            /// Enable/Disable ID model configuration command. Default: 0x00 (False).
            /// With this flag set to False, only capture bounding boxes.
            /// </summary>
            ENABLE_ID = 0x02,
            /// <summary>
            /// Single-shot inference configuration command. Default: 0x00.
            /// Trigger a single-shot inference. Only works if the sensor is in standby mode.
            /// </summary>
            SINGLE_SHOT = 0x03,
            /// <summary>
            /// Calibrate the next identified frame as person N, from 0 to 7.
            /// If two frames pass with no person, this label is discarded. Default: 0x00.
            /// </summary>
            CALIBRATE_ID = 0x04,
            /// <summary>
            /// Store any recognized IDs even when unpowered configuration command. Default: 0x01 (True).
            /// </summary>
            PERSIST_IDS = 0x05,
            /// <summary>
            /// Wipe any recognized IDs from storage configuration command. Default: 0x00.
            /// </summary>
            ERASE_IDS = 0x06,
            /// <summary>
            /// Debug mode configuration command. Default: 0x01 (True).
            /// Whether to enable the LED indicator on the sensor.
            /// </summary>
            DEBUG_MODE = 0x07,
        }
    }
}
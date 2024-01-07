using Meadow.Hardware;

namespace Meadow.Foundation.Sensors
{
    /// <summary>
    /// Represents a Useful Sensor's Person Sensor
    /// </summary>
    public partial class PersonSensor : II2cPeripheral
    {
        /// <inheritdoc/>
        public byte DefaultI2cAddress => 0x62;

        /// <summary>
        /// The maximun number of faces the sensor can recognize
        /// </summary>
        public int MAX_FACE_COUNT => 4;

        /// <summary>
        /// The max number of specific face IDs the sensor can track
        /// </summary>
        public int MAX_IDS_COUNT => 7;

        int DATA_LENGTH = 39;

        II2cCommunications i2cComms;

        /// <summary>
        /// Initializes a new instance of the ElectroMagneticRelayModule device
        /// </summary>
        /// <param name="i2cBus">The I2C bus the peripheral is connected to</param>
        /// <param name="address">The bus address of the peripheral</param>
        public PersonSensor(II2cBus i2cBus, byte address)
        {
            i2cComms = new I2cCommunications(i2cBus, address, 40, 8);

            Initialize();
        }

        void Initialize()
        {
        }

        void Read()
        {

        }

        struct PersonFace
        {
            public byte boxConfidence;
            public byte boxLeft;
            public byte boxTop;
            public byte boxRight;
            public byte boxBottom;
            public byte idConfidence; //this is signed
            public byte id; //this is signed
            public byte isFacing;
        }

        struct SensorResults
        {
            public byte[] header; //5 bytes (0-4)
            byte numberOfFaces; //byte 5
            PersonFace[] faceData; //bytes 6-37
            ushort checkSum;    //bytes 38-39
        }
    }
}
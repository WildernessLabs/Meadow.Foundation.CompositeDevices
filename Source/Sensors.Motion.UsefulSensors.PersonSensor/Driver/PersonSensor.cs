using Meadow.Hardware;
using System;
using System.Linq;

namespace Meadow.Foundation.Sensors;

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

    private readonly int DATA_LENGTH = 40;
    private byte[] readBuffer = default!;
    private readonly II2cCommunications i2cComms;


    /// <summary>
    /// Initializes a new instance of the ElectroMagneticRelayModule device
    /// </summary>
    /// <param name="i2cBus">The I2C bus the peripheral is connected to</param>
    /// <param name="address">The bus address of the peripheral</param>
    public PersonSensor(II2cBus i2cBus, byte address = 0x62)
    {
        i2cComms = new I2cCommunications(i2cBus, address, DATA_LENGTH);
        Initialize();
    }

    void Initialize()
    {
        readBuffer = new byte[DATA_LENGTH];
    }

    /// <summary>
    /// Reads data from the sensor
    /// </summary>
    private byte[] Read()
    {
        i2cComms.Read(readBuffer);
        return readBuffer;
    }

    /// <summary>
    /// Gets the structured sensor data.
    /// </summary>
    /// <returns>The structured sensor data.</returns>
    public PersonSensorResults GetSensorData()
    {
        var data = Read();
        return ParseSensorResults(data);
    }

    /// <summary>
    /// Parses the sensor results from the provided byte array.
    /// </summary>
    /// <param name="data">The byte array containing sensor data.</param>
    /// <returns>The structured sensor data.</returns>
    public PersonSensorResults ParseSensorResults(byte[] data)
    {
        PersonSensorResults results = new();
        results.Header = data.Take(5).ToArray();
        results.NumberOfFaces = data[5];
        results.FaceData = new PersonFace[MAX_FACE_COUNT];

        for (int i = 0; i < MAX_FACE_COUNT; ++i)
        {
            var faceStartIndex = 6 + i * 8;
            results.FaceData[i] = new PersonFace
            {
                BoxConfidence = data[faceStartIndex],
                BoxLeft = data[faceStartIndex + 1],
                BoxTop = data[faceStartIndex + 2],
                BoxRight = data[faceStartIndex + 3],
                BoxBottom = data[faceStartIndex + 4],
                IdConfidence = (sbyte)data[faceStartIndex + 5],
                Id = (sbyte)data[faceStartIndex + 6],
                IsFacing = data[faceStartIndex + 7]
            };
        }

        results.CheckSum = BitConverter.ToUInt16(data.Skip(38).Take(2).ToArray(), 0);
        return results;
    }
}
using Meadow.Hardware;
using System;
using System.Linq;

namespace Meadow.Foundation.Sensors.Camera.UsefulSensors;

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
    public int MaxFaceCount => 4;

    /// <summary>
    /// The max number of specific face IDs the sensor can track
    /// </summary>
    public int MaxIdsCount => 7;

    private const int HeaderLength = 4;
    private const int DataLength = 40;
    private readonly byte[] readBuffer;
    private readonly II2cCommunications i2cComms;


    /// <summary>
    /// Initializes a new instance of the Useful Sensor's Person Sensor device
    /// </summary>
    /// <param name="i2cBus">The I2C bus the peripheral is connected to</param>
    public PersonSensor(II2cBus i2cBus)
    {
        i2cComms = new I2cCommunications(i2cBus, DefaultI2cAddress, DataLength);
        readBuffer = new byte[DataLength];
    }

    /// <summary>
    /// Sets the capture mode of the sensor
    /// </summary>
    /// <param name="enable">Continuous if true</param>
    public void SetContinuousMode(bool enable)
    {
        i2cComms.WriteRegister((byte)Commands.MODE, (byte)(enable ? 0x0 : 0x1));
    }

    /// <summary>
    /// Sets the Person Sensor to single-shot mode for inference.
    /// </summary>
    public void SetSingleShotMode()
    {
        i2cComms.WriteRegister((byte)Commands.SINGLE_SHOT, 0x1);
    }

    /// <summary>
    /// Sets the debug mode for the Person Sensor.
    /// </summary>
    /// <param name="enable">True to enable debug mode, False to disable.</param>
    public void SetDebugMode(bool enable)
    {
        i2cComms.WriteRegister((byte)Commands.DEBUG_MODE, (byte)(enable ? 0x1 : 0x0));
    }

    /// <summary>
    /// Enables or disables the ID model for the Person Sensor.
    /// </summary>
    /// <param name="enable">True to enable the ID model, False to disable. 
    /// With this flag set to False, only bounding boxes are captured.</param>
    public void SetEnableId(bool enable)
    {
        i2cComms.WriteRegister((byte)Commands.ENABLE_ID, (byte)(enable ? 0x1 : 0x0));
    }

    /// <summary>
    /// Sets whether to persist recognized IDs even when unpowered for the Person Sensor.
    /// </summary>
    /// <param name="enable">True to store recognized IDs even when unpowered, False otherwise.</param>
    public void SetPersistIds(bool enable)
    {
        i2cComms.WriteRegister((byte)Commands.PERSIST_IDS, (byte)(enable ? 0x1 : 0x0));
    }

    /// <summary>
    /// Initiates wiping recognized IDs from storage for the Person Sensor.
    /// </summary>
    public void SetEraseIds()
    {
        i2cComms.WriteRegister((byte)Commands.ERASE_IDS, 0x1);
    }

    /// <summary>
    /// Initiates calibration for the next identified frame as a specific person ID (0 to 7) for the Person Sensor.
    /// </summary>
    /// <param name="id">The person ID to calibrate.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the specified ID exceeds the maximum number of IDs.</exception>
    public void SetCalibrateId(byte id)
    {
        if (id > MaxIdsCount)
        {
            throw new ArgumentOutOfRangeException(nameof(id), $"ID ({id}) exceeds the maximum number of IDs ({MaxIdsCount})");
        }

        i2cComms.WriteRegister((byte)Commands.CALIBRATE_ID, id);
    }

    /// <summary>
    /// Gets the structured sensor data.
    /// </summary>
    /// <returns>The structured sensor data.</returns>
    public PersonSensorResults GetSensorData()
    {
        i2cComms.Read(readBuffer);
        return ParseSensorResults(readBuffer);
    }

    /// <summary>
    /// Parses the sensor results from the provided byte array.
    /// </summary>
    /// <param name="data">The byte array containing sensor data.</param>
    /// <returns>The structured sensor data.</returns>
    public PersonSensorResults ParseSensorResults(byte[] data)
    {
        PersonSensorResults results = new();
        results.Header = data.Take(HeaderLength).ToArray();

        results.NumberOfFaces = (sbyte)data[HeaderLength];

        if (results.NumberOfFaces < 0)
        {
            throw new InvalidOperationException($"Number of faces detected ({results.NumberOfFaces}) is less than zero");
        }

        if (results.NumberOfFaces > MaxFaceCount)
        {
            throw new InvalidOperationException($"Number of faces detected ({results.NumberOfFaces}) exceeds the maximum number of faces ({MaxFaceCount})");
        }

        results.FaceData = new PersonFace[MaxFaceCount];

        for (int i = 0; i < MaxFaceCount; ++i)
        {
            var faceStartIndex = i * 8 + HeaderLength + 1;
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
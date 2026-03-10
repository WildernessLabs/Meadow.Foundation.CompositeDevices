using Meadow.Hardware;
using System;
using System.Threading.Tasks;

namespace Meadow.Foundation.Sensors.Camera.UsefulSensors;

/// <summary>
/// Represents a Useful Sensor's Tiny Code Reader
/// </summary>
public class TinyCodeReader : II2cPeripheral
{
    /// <summary>
    /// Event raised when a QR code is read
    /// </summary>
    public event EventHandler<string>? CodeRead;

    /// <summary>
    /// Gets a value indicating whether the sensor is sampling/running
    /// </summary>
    public bool IsRunning => _isRunning;

    private volatile bool _isRunning;

    /// <summary>
    /// The sample period of the sensor (default 200ms)
    /// </summary>
    public TimeSpan SamplePeriod { get; set; } = TimeSpan.FromMilliseconds(200);

    /// <inheritdoc/>
    public byte DefaultI2cAddress => 0x0C;

    private const int ContentByteCount = 254;
    private const int ContentByteLengthCount = 2;
    private const int LedRegister = 0x01;

    private readonly byte[] readBuffer;
    private readonly II2cCommunications i2cComms;

    /// <summary>
    /// Initializes a new instance of the ElectroMagneticRelayModule device
    /// </summary>
    /// <param name="i2cBus">The I2C bus the peripheral is connected to</param>
    public TinyCodeReader(II2cBus i2cBus)
    {
        i2cComms = new I2cCommunications(i2cBus, DefaultI2cAddress, ContentByteCount + ContentByteLengthCount);
        readBuffer = new byte[ContentByteCount + ContentByteLengthCount];
    }

    /// <summary>
    /// Sets the LED on the Tiny Code Reader 
    /// </summary>
    /// <param name="enable">enable if true, disable if false</param>
    public void SetLed(bool enable)
    {
        i2cComms.WriteRegister((byte)LedRegister, (byte)(enable ? 0x01 : 0x00));
    }

    /// <summary>
    /// Reads the string value of the QR code from the Tiny Code Reader
    /// </summary>
    /// <returns>the code as a string if avaliable, null if no code found</returns>
    public string? ReadCode()
    {
        i2cComms.ReadRegister(0x00, readBuffer);

        if (readBuffer[0] == 0)
        {
            return null;
        }
        else
        {
            var length = Math.Min((int)readBuffer[0], ContentByteCount);
            return System.Text.Encoding.UTF8.GetString(readBuffer, 2, length);
        }
    }

    /// <summary>
    /// Start sampling the sensor
    /// </summary>
    public void StartUpdating(TimeSpan? samplePeriod = null)
    {
        if (_isRunning)
        {
            return;
        }

        _isRunning = true;

        if (samplePeriod != null)
        {
            SamplePeriod = samplePeriod.Value;
        }

        Task.Run(async () =>
        {
            while (_isRunning)
            {
                try
                {
                    var code = ReadCode();
                    if (code != null)
                    {
                        CodeRead?.Invoke(this, code);
                    }
                }
                catch (Exception)
                {
                    // suppress per-sample errors to keep the loop running
                }

                await Task.Delay(SamplePeriod);
            }
        });
    }

    /// <summary>
    /// Stop sampling the sensor
    /// </summary>
    public void StopUpdating()
    {
        _isRunning = false;
    }
}
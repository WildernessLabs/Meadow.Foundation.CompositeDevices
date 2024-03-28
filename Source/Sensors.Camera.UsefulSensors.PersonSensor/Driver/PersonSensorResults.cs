using static Meadow.Foundation.Sensors.Camera.UsefulSensors.PersonSensor;

namespace Meadow.Foundation.Sensors.Camera.UsefulSensors;

/// <summary>
/// Represents the structured results returned from the person sensor.
/// </summary>
public class PersonSensorResults
{
    /// <summary>
    /// Header information of the data packet.
    /// </summary>
    public byte[] Header { get; set; } = default!;

    /// <summary>
    /// Number of faces detected.
    /// </summary>
    public sbyte NumberOfFaces { get; set; }

    /// <summary>
    /// Information about detected faces.
    /// </summary>
    public PersonFace[] FaceData { get; set; } = new PersonFace[4];

    /// <summary>
    /// Checksum value of the data packet.
    /// </summary>
    public ushort CheckSum { get; set; }
}
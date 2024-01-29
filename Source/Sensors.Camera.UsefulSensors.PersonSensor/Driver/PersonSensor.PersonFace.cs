namespace Meadow.Foundation.Sensors.Camera;

public partial class PersonSensor
{
    /// <summary>
    /// Represents information about a detected face.
    /// </summary>
    public struct PersonFace
    {
        /// <summary>
        /// Confidence level for the detected face.
        /// </summary>
        public byte BoxConfidence { get; set; }

        /// <summary>
        /// X coordinate of the left side of the detected face.
        /// </summary>
        public byte BoxLeft { get; set; }

        /// <summary>
        /// Y coordinate of the top edge of the detected face.
        /// </summary>
        public byte BoxTop { get; set; }

        /// <summary>
        /// X coordinate of the right side of the detected face.
        /// </summary>
        public byte BoxRight { get; set; }

        /// <summary>
        /// Y coordinate of the bottom edge of the detected face.
        /// </summary>
        public byte BoxBottom { get; set; }

        /// <summary>
        /// Confidence level for the assigned ID of the detected face.
        /// </summary>
        public sbyte IdConfidence { get; set; }

        /// <summary>
        /// Numerical ID assigned to the detected face.
        /// </summary>
        public sbyte Id { get; set; }

        /// <summary>
        /// Indicates whether the person is facing the camera (1) or not (0).
        /// </summary>
        public byte IsFacing { get; set; }
    }
}


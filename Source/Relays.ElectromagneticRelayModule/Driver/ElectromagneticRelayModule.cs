using Meadow.Foundation.ICs.IOExpanders;
using Meadow.Hardware;
using Meadow.Peripherals.Relays;
using System;

namespace Meadow.Foundation.Relays
{
    /// <summary>
    /// Represents a 4 Channel I2C Electromagnetic Relay Module powered by a PCF8574 IO expander
    /// </summary>
    public partial class ElectromagneticRelayModule : II2cPeripheral, IDisposable
    {
        /// <inheritdoc/>
        public byte DefaultI2cAddress => 0x20;

        /// <summary>
        /// The relays
        /// </summary>
        public readonly Relay[] Relays = new Relay[4];

        /// <summary>
        /// PCF8574 object
        /// </summary>
        protected readonly Pcf8574 ioExpander;

        /// <summary>
        /// The digital output ports for the relays
        /// </summary>
        protected readonly IDigitalOutputPort[] ports = new IDigitalOutputPort[4];

        private bool isDisposed;

        /// <summary>
        /// Initializes a new instance of the ElectroMagneticRelayModule device
        /// </summary>
        /// <param name="i2cBus">The I2C bus the peripheral is connected to</param>
        /// <param name="address">The bus address of the peripheral</param>
        public ElectromagneticRelayModule(II2cBus i2cBus, byte address)
        {
            ioExpander = new Pcf8574(i2cBus, address);

            //Relay logic is inverted, this sets all relays to off
            ioExpander.AllOn();

            Initialize();
        }

        void Initialize()
        {
            ports[0] = ioExpander.Pins.P4.CreateDigitalOutputPort(true);
            ports[1] = ioExpander.Pins.P5.CreateDigitalOutputPort(true);
            ports[2] = ioExpander.Pins.P6.CreateDigitalOutputPort(true);
            ports[3] = ioExpander.Pins.P7.CreateDigitalOutputPort(true);

            Relays[0] = new Relay(ports[0], RelayType.NormallyClosed);
            Relays[1] = new Relay(ports[1], RelayType.NormallyClosed);
            Relays[2] = new Relay(ports[2], RelayType.NormallyClosed);
            Relays[3] = new Relay(ports[3], RelayType.NormallyClosed);
        }

        /// <summary>
        /// Set all relays on
        /// </summary>
        public void SetAllOn()
        {
            foreach (var relay in Relays)
            {
                relay.IsOn = true;
            }
        }

        /// <summary>
        /// Set all relays off
        /// </summary>
        public void SetAllOff()
        {
            foreach (var relay in Relays)
            {
                relay.IsOn = false;
            }
        }

        /// <summary>
        /// Disposes the instances resources
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    foreach (var port in ports)
                    {
                        port?.Dispose();
                    }
                }
                isDisposed = true;
            }
        }

        /// <summary>
        /// Disposes the instances resources
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
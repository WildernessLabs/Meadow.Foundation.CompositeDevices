using Meadow.Foundation.ICs.IOExpanders;
using Meadow.Hardware;
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

            InitializePorts();
        }

        void InitializePorts()
        {
            ports[0] = ioExpander.Pins.P4.CreateDigitalOutputPort();
            ports[1] = ioExpander.Pins.P5.CreateDigitalOutputPort();
            ports[2] = ioExpander.Pins.P6.CreateDigitalOutputPort();
            ports[3] = ioExpander.Pins.P7.CreateDigitalOutputPort();
        }

        /// <summary>
        /// Get the relay state
        /// </summary>
        /// <param name="relay">The relay (1-4)</param>
        /// <returns>True if closed/connected, fase if open/disconnected</returns>
        public bool GetRelayState(RelayIndex relay)
        {
            return !ports[(int)relay].State;
        }

        /// <summary>
        /// Set the relay state
        /// </summary>
        /// <param name="relay">The relay (1-4)</param>
        /// <param name="state">True for closed/connected, fase if open/disconnected</param>
        public void SetRelayState(RelayIndex relay, bool state) 
        {
            ports[(int)relay].State = !state;
        }

        /// <summary>
        /// Set all relays on
        /// </summary>
        public void SetAllOn()
        {
            foreach(var port in ports)
            {
                port.State = false;
            }
        }

        /// <summary>
        /// Set all relays off
        /// </summary>
        public void SetAllOff()
        {
            foreach (var port in ports)
            {
                port.State = true;
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
                    foreach(var port in ports)
                    {
                        if(port != null)
                        {
                            port.Dispose();
                        }
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
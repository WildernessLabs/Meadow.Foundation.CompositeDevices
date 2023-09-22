namespace Meadow.Foundation.Relays
{
    public partial class ElectromagneticRelayModule
    {
        /// <summary>
        /// Helper method to get address from address pin configuation
        /// </summary>
        /// <param name="pinA0">State of A0 address pin - true if high</param>
        /// <param name="pinA1">State of A1 address pin - true if high</param>
        /// <param name="pinA2">State of A2 address pin - true if high</param>
        /// <param name="isATypeDevice">Is an A hardware variant, this shifts the address returned by 24</param>
        /// <returns>The device address</returns>
        public static byte GetAddressFromPins(bool pinA0, bool pinA1, bool pinA2, bool isATypeDevice = false)
        {
            /*
            A2  A1  A0   HexAddress DecimalAddress
            1	1	1    0x20       32
            1	1	0    0x21       33
            1	0	1    0x22       34
            1	0	0    0x23       35
            0	1	1    0x24       36
            0	1	0    0x25       37
            0	0	1    0x26       38
            0	0	0    0x27       39
            */
            int baseAddress = isATypeDevice ? 0x38 : 0x20;
            int address = baseAddress;

            address |= (pinA0 ? 1 : 0);
            address |= (pinA1 ? 2 : 0);
            address |= (pinA2 ? 4 : 0);

            return (byte)(address & 0xff);
        }
    }
}
using Meadow.Modbus;
using Meadow.Peripherals.Relays;
using System;

namespace Meadow.Foundation.Modbus.Temco;

public partial class T38O
{

    private IModbusBusClient _client;
    private byte _address;

    private Lazy<IRelay> _d01;
    private Lazy<IRelay> _d02;
    private Lazy<IRelay> _d03;
    private Lazy<IRelay> _d04;
    private Lazy<IRelay> _d05;
    private Lazy<IRelay> _d06;

    public T38O(IModbusBusClient client, byte address = 150)
    {
        _address = address;
        _client = client;

        if (!_client.IsConnected)
        {
            _ = _client.Connect();
        }

        _d01 = new Lazy<IRelay>(new T3Relay(_client, _address, (ushort)Registers.DO_CH0));
        _d02 = new Lazy<IRelay>(new T3Relay(_client, _address, (ushort)Registers.DO_CH1));
        _d03 = new Lazy<IRelay>(new T3Relay(_client, _address, (ushort)Registers.DO_CH2));
        _d04 = new Lazy<IRelay>(new T3Relay(_client, _address, (ushort)Registers.DO_CH3));
        _d05 = new Lazy<IRelay>(new T3Relay(_client, _address, (ushort)Registers.DO_CH4));
        _d06 = new Lazy<IRelay>(new T3Relay(_client, _address, (ushort)Registers.DO_CH5));
    }

    public IRelay D01 => _d01.Value;
    public IRelay D02 => _d02.Value;
    public IRelay D03 => _d03.Value;
    public IRelay D04 => _d04.Value;
    public IRelay D05 => _d05.Value;
    public IRelay D06 => _d06.Value;
}


using Meadow.Modbus;
using Meadow.Peripherals.Relays;
using System;

namespace Meadow.Foundation.Modbus.Temco;

internal class T3Relay : IRelay
{
    public event EventHandler<RelayState>? OnChanged;

    private readonly IModbusBusClient modbusBusClient;
    private readonly byte modbusAddress;
    private readonly ushort modbusRegister;

    public RelayType Type => RelayType.NormallyOpen;

    internal T3Relay(IModbusBusClient modbusBusClient, byte modbusAddress, ushort modbusRegister)
    {
        this.modbusBusClient = modbusBusClient;
        this.modbusAddress = modbusAddress;
        this.modbusRegister = modbusRegister;
    }

    public RelayState State
    {
        get => modbusBusClient.ReadHoldingRegisters(modbusAddress, modbusRegister, 1).Result[0] == 1 ? RelayState.Open : RelayState.Closed;
        set => modbusBusClient.WriteHoldingRegister(modbusAddress, modbusRegister, (ushort)(value == RelayState.Closed ? 1 : 0)).Wait();
    }

    public void Toggle()
    {
        State = State switch
        {
            RelayState.Open => RelayState.Closed,
            _ => RelayState.Open
        };
    }
}


using Meadow.Foundation.Modbus.Temco;
using Meadow.Modbus;
using Meadow.Peripherals.Relays;

namespace T38ORelay_Sample;

internal class Program
{
    public static string T3IPAddress = "192.168.10.95";

    private static async Task Main(string[] args)
    {
        var modbusClient = new ModbusTcpClient(T3IPAddress);
        Console.WriteLine("connecting");
        await modbusClient.Connect();
        var t38o = new T38O(modbusClient, 254);

        while (true)
        {
            t38o.D01.State = RelayState.Closed;
            Thread.Sleep(200);
            t38o.D02.State = RelayState.Closed;
            Thread.Sleep(200);
            t38o.D03.State = RelayState.Closed;
            Thread.Sleep(200);
            t38o.D04.State = RelayState.Closed;
            Thread.Sleep(200);
            t38o.D05.State = RelayState.Closed;
            Thread.Sleep(200);
            t38o.D06.State = RelayState.Closed;
            Thread.Sleep(500);

            t38o.D01.State = RelayState.Open;
            Thread.Sleep(200);
            t38o.D02.State = RelayState.Open;
            Thread.Sleep(200);
            t38o.D03.State = RelayState.Open;
            Thread.Sleep(200);
            t38o.D04.State = RelayState.Open;
            Thread.Sleep(200);
            t38o.D05.State = RelayState.Open;
            Thread.Sleep(200);
            t38o.D06.State = RelayState.Open;
            Thread.Sleep(500);
        }
    }
}

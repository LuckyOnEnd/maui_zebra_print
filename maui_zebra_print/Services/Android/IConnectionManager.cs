using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer.Discovery;

namespace MauiApp3.Services.Android
{
    public interface IConnectionManager
    {
        Task<Connection> BuildBluetoothConnectionChannelsString(string macAddress);
        Task DisconnectDevice(Connection connection);

        void FindBluetoothPrinters(DiscoveryHandler discoveryHandler);

        Connection GetBluetoothConnection(string macAddress);

        StatusConnection GetBluetoothStatusConnection(string macAddress);

        MultichannelConnection GetMultichannelBluetoothConnection(string macAddress);

    }
}

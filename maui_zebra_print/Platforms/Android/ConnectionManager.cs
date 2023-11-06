using Android.App;
using Android.Content;
using Android.Hardware.Usb;
using MauiApp3.Services.Android;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer.Discovery;
using Application = Android.App.Application;
using Android.Bluetooth;
using Android.Graphics;
using Android.Telecom;
using Connection = Zebra.Sdk.Comm.Connection;

namespace MauiApp3.Platforms.Android
{
    public class ConnectionManager : IConnectionManager
    {
        private const string ActionUsbPermission = "com.zebra.XamarinDevDemo.USB_PERMISSION";
        private const int UsbPermissionTimeout = 30000;

        private static readonly object UsbConnectionLock = new object();

        private readonly IntentFilter filter = new IntentFilter("ACTION_USB_PERMISSION");

        public async Task<Connection> BuildBluetoothConnectionChannelsString(string macAddress)
        {
            var connection  = new BluetoothConnectionInsecure(macAddress);

            try
            {
                connection.Open();
                if (connection.Connected)
                {
                    return connection;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public async Task DisconnectDevice(Connection connection)
        {
            connection.Close();
        }

        public void FindBluetoothPrinters(DiscoveryHandler discoveryHandler)
        {
            throw new NotImplementedException();
        }

        public Connection GetBluetoothConnection(string macAddress)
        {
            throw new NotImplementedException();
        }

        public StatusConnection GetBluetoothStatusConnection(string macAddress)
        {
            return new BluetoothStatusConnection(macAddress);
        }

        public MultichannelConnection GetMultichannelBluetoothConnection(string macAddress)
        {
            return new MultichannelBluetoothConnection(macAddress);
        }

    }

}
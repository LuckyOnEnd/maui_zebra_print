using MauiApp3.Services.Android;
using System.Text;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;

namespace maui_zebra_print;

public partial class MainPage : ContentPage
{
    private const string firstPrinterMacAdress = "90:7B:C6:3D:8B:7D";
    private const string secondPrinterMacAdress = "606E4117C6DA";
    private string SelectedPrinter = string.Empty;

    private bool _isBusy = false;
    private Connection connection = null;

    public bool _isConnected = false;
    public string TextToPrint { get; set; }

    public bool IsBusy
    {
        get { return _isBusy; }
        set
        {
            if (_isBusy != value)
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
    }

    public bool IsConnected
    {
        get { return _isConnected; }
        set
        {
            if (_isConnected != value)
            {
                _isConnected = value;
                OnPropertyChanged();
            }
        }
    }


    public MainPage()
	{
		InitializeComponent();
	}


    private async void SelectFirstPrinter(object sender, EventArgs e)
    {
        SelectedPrinter = firstPrinterMacAdress;
    }

    private async void SelectSecondPrinter(object sender, EventArgs e)
    {
        SelectedPrinter = secondPrinterMacAdress;
    }

    private async void Print(object sender, EventArgs e)
    {
        try
        {

            IsBusy = true;
            TextToPrint = textInput.Text;
            PrinterLanguage printerLanguage = ZebraPrinterFactory.GetInstance(connection).PrinterControlLanguage;
            connection.Write(GetTestLabelBytes(printerLanguage));
            IsBusy = false;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Ok");
            return;
        }
    }

    private async void DisconnectClick(object sender, EventArgs e)
    {
        IsBusy = true;
        var service = DependencyService.Get<IConnectionManager>();
        await service.DisconnectDevice(connection);
        IsConnected = false;
        IsBusy = false;
    }

    private async void OnConnectClick(object sender, EventArgs e)
    {
        IsBusy = true;
        try
        {
            var service = DependencyService.Get<IConnectionManager>();
            var connectionChannels = await service.BuildBluetoothConnectionChannelsString(SelectedPrinter);
            if (connectionChannels is null)
            {
                await DisplayAlert("Error", "Problem podczas podłączenia bluetooth", "Ok");
                return;
            }

            connection = connectionChannels;
            IsConnected = true;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Ok");
            IsBusy = false;

        }
        IsBusy = false;

    }

    private byte[] GetTestLabelBytes(PrinterLanguage printerLanguage)
    {
        if (printerLanguage == PrinterLanguage.ZPL)
        {
            return Encoding.UTF8.GetBytes(TextToPrint);
        }
        else if (printerLanguage == PrinterLanguage.CPCL || printerLanguage == PrinterLanguage.LINE_PRINT)
        {
            var printText = TextToPrint;

            return Encoding.UTF8.GetBytes(printText);
        }
        else
        {
            throw new ZebraPrinterLanguageUnknownException();
        }
    }
}


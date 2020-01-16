using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

using Opc.UaFx;
using Opc.UaFx.Client;

using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Client.Uwp
{
    /// <summary>
    /// This sample demonstrates how to implement a simple OPC UA client in an UWP Application.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void HandleButtonClick(object sender, RoutedEventArgs e)
        {
            // ATTENTION:
            // In UWP it is not possible to take use of the automatic certificate creation.
            // Therefore you will need to create a certificate for the client manually.
            // A possible way to go is to use the code in the CreateCertificate method
            // at the end of this file.
            // 
            // Query the file information of the client certificate stored in the assets.
            var certificateFile = await Package.Current.InstalledLocation.GetFileAsync(@"Assets\Client.Uwp.pfx");

            try {
                using (var client = new OpcClient("opc.tcp://opcuaserver.com:48484")) {
                    client.Certificate = OpcCertificateManager.LoadCertificate(certificateFile.Path);
                    client.Connect();

                    var value = client.ReadNode("ns=1;s=Countries.DE.DEBB049.Temperature");

                    if (value.Status.IsGood)
                        this.textBox.Text = $"Temperature: {value.As<string>()} °C";
                    else
                        this.textBox.Text = value.Status.Description;
                }
            }
            catch (Exception ex) {
                this.textBox.Text = ex.Message;
            }
        }

        // This method creates a new certificate which can be used for the UWP application.
        // Note that the certificate is saved at Downloads/Client.Uwp/Client.Uwp.pfx.
        // To take use of the certificate you may copy it to the Assets of the project and
        // change the "Build Action"-Property of the file (in Visual Studio) to "Content".
        private async void CreateCertificate()
        {
            var settings = new OpcCertificateSettings("Client.Uwp");

            var certificate = OpcCertificateManager.CreateCertificate(settings);
            var newFile = await DownloadsFolder.CreateFileAsync("Client.Uwp.pfx");

            using (var stream = await newFile.OpenStreamForWriteAsync()) {
                var data = certificate.Export(X509ContentType.Pfx);
                stream.Write(data, 0, data.Length);
            }
        }
    }
}

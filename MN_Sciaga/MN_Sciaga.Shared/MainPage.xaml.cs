using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MN_Sciaga
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public void LoadMNClicked(object sender, RoutedEventArgs e)
        {
            var lines = QuestionsMocks.QM_MN2022.data.Replace("\r", "").Split('\n');

            Frame.Navigate(typeof(ViewerPage), lines);
        }

        public async void LoadButtonClicked(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.List;
            picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add(".txt");
            picker.FileTypeFilter.Add(".mn");

            var file = await picker.PickSingleFileAsync();
            if(file == null)
            {
                await new MessageDialog("Nie wybrano pliku", "Błąd").ShowAsync();
                return;
            }

            IBuffer buffer = await FileIO.ReadBufferAsync(file);
            DataReader reader = DataReader.FromBuffer(buffer);
            byte[] fileContent = new byte[reader.UnconsumedBufferLength];
            reader.ReadBytes(fileContent);
            string text = Encoding.UTF8.GetString(fileContent, 0, fileContent.Length);
            var lines = text.Replace("\r", "").Split('\n');

            Frame.Navigate(typeof(ViewerPage), lines);
        }
    }
}

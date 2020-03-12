using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Exercise_5
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Data newData = new Data();

        public MainPage()
        {
            this.InitializeComponent();
            CreateData();
            this.DataContext = newData;
        }

        public void CreateData()
        {
            newData.FirstName = "Lilyan";
            newData.LastName = "Vivian";
            newData.BirthDate = "05/08/196";
            newData.Email = "Lilyan.viv@gmail.com";
        }
    }
}

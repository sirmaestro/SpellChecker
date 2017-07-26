using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module2.Services;
using Module2.Models;
using Microsoft.WindowsAzure.MobileServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Module2.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class History : ContentPage
	{
        public History ()
		{
			InitializeComponent ();
		}

        async void Handle_ClickedAsync(object sender, System.EventArgs e)
        {
            loading.IsRunning = true;
            List<HistoryModel> historyInfo = await AzureManager.AzureManagerInstance.GetHistoryInfo();

            HistoryList.ItemsSource = historyInfo;
            loading.IsRunning = false;
        }
    }
}
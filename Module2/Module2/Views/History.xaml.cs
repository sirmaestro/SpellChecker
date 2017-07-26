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

        //async void Handle_ClickedAsync(object sender, System.EventArgs e)
        //{
        //    List<HistoryModel> historyInfo = await AzureManager.AzureManagerInstance.GetHistoryInfo();
        //    historyInfo.Reverse();
        //    HistoryList.ItemsSource = historyInfo;
        //}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            List<HistoryModel> historyInfo = await AzureManager.AzureManagerInstance.GetHistoryInfo();
            historyInfo.Reverse();
            HistoryList.ItemsSource = historyInfo;
        }

        async void OnRefresh(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            //put your refreshing logic here
            List<HistoryModel> historyInfo = await AzureManager.AzureManagerInstance.GetHistoryInfo();
            historyInfo.Reverse();
            HistoryList.ItemsSource = historyInfo;
            //make sure to end the refresh state
            list.IsRefreshing = false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Module2.Models;

namespace Module2.Services
{
    class AzureManager
    {
        private static AzureManager instance;
        private MobileServiceClient client;
        private IMobileServiceTable<HistoryModel> historyTable;

        private AzureManager()
        {
            this.client = new MobileServiceClient("http://mobileapp2017.azurewebsites.net");
            this.historyTable = this.client.GetTable<HistoryModel>();
        }

        public MobileServiceClient AzureClient
        {
            get { return client; }
        }

        public static AzureManager AzureManagerInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AzureManager();
                }

                return instance;
            }
        }

        public async Task<List<HistoryModel>> GetHistoryInfo()
        {
            return await this.historyTable.ToListAsync();
        }

        public async Task PostHistoryInfo(HistoryModel historyModel)
        {
            await this.historyTable.InsertAsync(historyModel);
        }
    }
}

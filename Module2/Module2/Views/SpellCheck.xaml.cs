using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Module2.Services;
using System.Diagnostics;
using Module2.Models;

namespace Module2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpellCheck : ContentPage
    {
        public static readonly BindableProperty IsProcessingProperty =
            BindableProperty.Create("IsProcessing", typeof(bool), typeof(SpellCheck), false);

        public bool IsProcessing
        {
            get { return (bool)GetValue(IsProcessingProperty); }
            set { SetValue(IsProcessingProperty, value); }
        }
        public SpellCheck()
        {
            InitializeComponent();
        }

        async void OnCheckClicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(CheckWord.Text))
                {
                    //IsProcessing = true;
                    loading.IsRunning = true;
                    string tempword = CheckWord.Text;

                    var spellCheckResult = await SpellCheckService.SpellCheckTextAsync(CheckWord.Text);
                    var proofCheckResult = await SpellCheckService.ProofCheckTextAsync(CheckWord.Text);
                    foreach (var flaggedToken in spellCheckResult.FlaggedTokens)
                    {
                        ReplaceWord.Text = CheckWord.Text.Replace(flaggedToken.Token, flaggedToken.Suggestions.FirstOrDefault().Suggestion);
                    }
                    if (!spellCheckResult.FlaggedTokens.Any())
                    {
                        foreach (var flaggedToken in proofCheckResult.FlaggedTokens)
                        {
                            ReplaceWord.Text = CheckWord.Text.Replace(flaggedToken.Token, flaggedToken.Suggestions.FirstOrDefault().Suggestion);
                        }
                    }
                    if ((!proofCheckResult.FlaggedTokens.Any()) && (!spellCheckResult.FlaggedTokens.Any()))
                    {
                        ReplaceWord.Text = "Error no replacement word, please try again with a different word";
                    }
                    else
                    {
                        await postWordsAsync();
                    }
                    //if ((ReplaceWord.Text == "Suggested replacement") || (ReplaceWord.Text == "Error please try again with differnent word"))
                    //{
                    //    foreach (var flaggedToken in proofCheckResult.FlaggedTokens)
                    //    {
                    //        ReplaceWord.Text = CheckWord.Text.Replace(flaggedToken.Token, flaggedToken.Suggestions.FirstOrDefault().Suggestion);
                    //    }
                    //    if ((ReplaceWord.Text == "Suggested replacement") || (ReplaceWord.Text == "Error please try again with differnent word"))
                    //    {
                    //        ReplaceWord.Text = "Error please try again with differnent word";
                    //    }
                    //}
                    //if ((ReplaceWord.Text != "Suggested replacement") && (ReplaceWord.Text != "Error please try again with differnent word"))
                    //{
                    //    await postWordsAsync();
                    //}

                    loading.IsRunning = false;
                    //IsProcessing = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        async Task postWordsAsync()
        {


            HistoryModel model = new HistoryModel()
            {
                DateUtc = DateTime.UtcNow,
                Unchecked = CheckWord.Text,
                Checked = ReplaceWord.Text
            };

            await AzureManager.AzureManagerInstance.PostHistoryInfo(model);
        }
    }
}
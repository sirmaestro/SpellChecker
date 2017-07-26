using Module2.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module2.Services
{
    class SpellCheckService
    {
        public static async Task<SpellCheckResult> SpellCheckTextAsync(string text)
        {
            string requestUri = GenerateRequestUri(Constants.BingSpellCheckEndpoint, text, SpellCheckMode.Spell);
            var response = await SendRequestAsync(requestUri, Constants.BingSpellCheckApiKey);
            var spellCheckResults = JsonConvert.DeserializeObject<SpellCheckResult>(response);
            return spellCheckResults;
        }

        public static async Task<SpellCheckResult> ProofCheckTextAsync(string text)
        {
            string requestUri = GenerateRequestUri(Constants.BingSpellCheckEndpoint, text, SpellCheckMode.Proof);
            var response = await SendRequestAsync(requestUri, Constants.BingSpellCheckApiKey);
            var spellCheckResults = JsonConvert.DeserializeObject<SpellCheckResult>(response);
            return spellCheckResults;
        }

        static string GenerateRequestUri(string spellCheckEndpoint, string text, SpellCheckMode mode)
        {
            string requestUri = spellCheckEndpoint;
            requestUri += string.Format("?text={0}", text);                         // text to spell check
            requestUri += string.Format("&mode={0}", mode.ToString().ToLower());    // spellcheck mode - proof or spell
            return requestUri;
        }

        static async Task<string> SendRequestAsync(string url, string apiKey)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiKey);
                var response = await httpClient.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}

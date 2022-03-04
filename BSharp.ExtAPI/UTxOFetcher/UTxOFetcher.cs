using BSharp.ExtAPI.UTxOFetcher.UTxO;

namespace BSharp.ExtAPI.UTxOFetcher
{
    public class UTxOFetcher
    {
        private readonly HttpClient _httpClient;

        public UTxOFetcher()
        {
            this._httpClient = new HttpClient();
        }

        public async Task<IUTxO[]?> FetchUTxOsAsync(string networkUrl, string address)
        {
            var apiCall = await _httpClient.GetAsync(UTxOUrls.UrlFor(networkUrl) + address);
            var asStr = await apiCall.Content.ReadAsStringAsync();
            var UTxOs = System.Text.Json.JsonSerializer.Deserialize<Data>(asStr);
            return UTxOs?.data?.txs;
        }
    }
}

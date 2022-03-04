namespace BSharp.ExtAPI.UTxOFetcher
{
    internal static class UTxOUrls
    {
        private static readonly Dictionary<string, string> _urls = new()
        {
            { "BtcM", "https://sochain.com/api/v2/get_tx_unspent/BTC/" },
            { "LtcM", "https://sochain.com/api/v2/get_tx_unspent/LTC/" },
            { "DogeM", "https://sochain.com/api/v2/get_tx_unspent/DOGE/" },

            { "BtcT", "https://sochain.com/api/v2/get_tx_unspent/BTCTEST/" },
            { "LtcT", "https://sochain.com/api/v2/get_tx_unspent/LTCTEST/" },
            { "DogeT", "https://sochain.com/api/v2/get_tx_unspent/DOGETEST/" }
        };

        public static string UrlFor(string network)
        {
            return _urls[network];
        }
    }
}

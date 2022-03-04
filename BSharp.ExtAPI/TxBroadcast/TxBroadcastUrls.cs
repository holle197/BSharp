namespace BSharp.ExtAPI.TxBroadcast
{
    internal static class TxBroadcastUrls
    {
        private static readonly Dictionary<string, string> _urls = new()
        {
            { "BtcM", "https://sochain.com/api/v2/send_tx/BTC" },
            { "LtcM", "https://sochain.com/api/v2/send_tx/LTC" },
            { "DogeM", "https://sochain.com/api/v2/send_tx/DOGE" },

            { "BtcT", "https://sochain.com/api/v2/send_tx/BTCTEST" },
            { "LtcT", "https://sochain.com/api/v2/send_tx/LTCTEST" },
            { "DogeT", "https://sochain.com/api/v2/send_tx/DOGETEST" },
        };

        public static string UrlFor(string network)
        {
            return _urls[network];
        }
    }
}

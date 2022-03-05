namespace BSharp.ExtAPI.TxBroadcast
{
    internal static class TxBroadcastUrls
    {
        private static readonly Dictionary<Networks, string> _urls = new()
        {
            { Networks.BtcMainnet, "https://sochain.com/api/v2/send_tx/BTC" },
            { Networks.LtcMainnet, "https://sochain.com/api/v2/send_tx/LTC" },
            { Networks.DogeMainnet, "https://sochain.com/api/v2/send_tx/DOGE" },

            { Networks.BtcTestnet, "https://sochain.com/api/v2/send_tx/BTCTEST" },
            { Networks.LtcTestnet, "https://sochain.com/api/v2/send_tx/LTCTEST" },
            { Networks.DogeTestnet, "https://sochain.com/api/v2/send_tx/DOGETEST" },
        };

        public static string UrlFor(Networks network)
        {
            return _urls[network];
        }
    }
}

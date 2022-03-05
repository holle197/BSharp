namespace BSharp.ExtAPI.UTxOFetcher
{
    internal static class UTxOUrls
    {
        private static readonly Dictionary<Networks, string> _urls = new()
        {
            { Networks.BtcMainnet, "https://sochain.com/api/v2/get_tx_unspent/BTC/" },
            { Networks.LtcMainnet, "https://sochain.com/api/v2/get_tx_unspent/LTC/" },
            { Networks.DogeMainnet, "https://sochain.com/api/v2/get_tx_unspent/DOGE/" },

            { Networks.BtcTestnet, "https://sochain.com/api/v2/get_tx_unspent/BTCTEST/" },
            { Networks.LtcTestnet, "https://sochain.com/api/v2/get_tx_unspent/LTCTEST/" },
            { Networks.DogeTestnet, "https://sochain.com/api/v2/get_tx_unspent/DOGETEST/" }
        };

        public static string UrlFor(Networks network)
        {
            return _urls[network];
        }
    }
}

using BSharp.ExtAPI.TxBroadcast.TransactionResult;
using RestSharp;

namespace BSharp.ExtAPI.TxBroadcast
{
    public class TxPusher
    {
        public static async Task<string?> BroadcastRawTxAsync(string networkUrl, string txHex)
        {
            try
            {
                if (string.IsNullOrEmpty(txHex)) return null;

                var client = new RestClient(TxBroadcastUrls.UrlFor(networkUrl))
                {
                    Timeout = -1
                };
                var request = new RestRequest(Method.POST);
                request.AddJsonBody(new { tx_hex = txHex });
                var response = await client.ExecuteAsync(request);
                var txHash = System.Text.Json.JsonSerializer.Deserialize<TxData>(response.Content);

                return txHash?.data?.txid;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

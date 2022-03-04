using BSharp.ExtAPI.TxBroadcast;
using BSharp.ExtAPI.UTxOFetcher;
using BSharp.ExtAPI.UTxOFetcher.UTxO;
using BSharp.Wallet.SHA;
using BSharp.Wallet.Transactions;
using NBitcoin;
using NBitcoin.Altcoins;

namespace BSharp.Wallet.Wallet
{
    public class Wallet
    {
        private Network Network { get; }
        private Key Key { get; }
        private ScriptPubKeyType AddrType { get; }
        private Networks Networks { get; }

        public Wallet(string secret, Networks network, ScriptPubKeyType addrTypee)
        {
            var ntwrk = SetNetwork(network);
            this.Network = ntwrk;
            this.Key = new Key(SHA256.GenerateHashBytes(secret));
            this.AddrType = addrTypee;
            this.Networks = network;
        }

        // Returns Private Key (Wallet Import Format)
        public string GetWIF()
        {
            return Key.GetWif(Network).ToString();
        }

        public string GetAddress()
        {
            return Key.GetAddress(AddrType, Network).ToString();
        }

        public async Task<string?> PushTx(string destinationAddr, decimal amount, decimal fee)
        {
            var utxos = await FetchAllUtxosAsync();
            if (utxos is null) return "Cannot Fetch UTxOs.";

            var txModel = new TxModel(Network, Key, GetAddress(), AddrType, utxos, destinationAddr, amount, fee);
            Console.WriteLine(txModel.Fee);
            Console.WriteLine(txModel.Amount);

            if (TxInputValidation(txModel) is not null) return TxInputValidation(txModel);
            if (!HaveEnoughFunds(txModel)) return "Insufficient Funds.";
            var signTx = RawTx.CreateAndSignTx(txModel);

            return await BroadcastTx(signTx);
        }

        //Returns total balance of current address
        public async Task<decimal> GetTotalBalance()
        {
            var utxos = await FetchAllUtxosAsync();

            decimal total = 0m;
            if (utxos is not null)
            {
                foreach (var utxo in utxos)
                {
                    total += Convert.ToDecimal(utxo.GetValue());
                }
            }
            return total;
        }

        private async Task<IUTxO[]?> FetchAllUtxosAsync()
        {
            var fetcher = new UTxOFetcher();
            var network = ChooseNetwork(Networks);
            return await fetcher.FetchUTxOsAsync(network, GetAddress());
        }

        private async Task<string?> BroadcastTx(string txHex)
        {
            return await TxPusher.BroadcastRawTxAsync(ChooseNetwork(Networks), txHex);
        }
#nullable enable
        private string? TxInputValidation(TxModel txModel)
        {
            if (txModel.Amount < 0.00000001m) return "The smallest transfer amount must be greater or equal to 0.00000001";
            if (txModel.Fee < 0.00000001m) return "The smallest fee must be greater or equal to 0.00000001";
            if (txModel.DestinationAddr == Key.GetAddress(AddrType, Network)) return "You cannot send a transaction to your own address";

            return null;
        }
#nullable disable   

        //Checks the account for sufficient balances for the next transaction
        private static bool HaveEnoughFunds(TxModel txModel)
        {
            decimal total = 0m;
            foreach (var utxo in txModel.Utxos)
            {
                total += Convert.ToDecimal(utxo.GetValue());
            }
            return total >= txModel.GetTotalAmount();
        }

        private static Network SetNetwork(Networks network)
        {
            return network switch
            {
                Networks.BtcMainnet => Bitcoin.Instance.Mainnet,
                Networks.LtcMainnet => Litecoin.Instance.Mainnet,
                Networks.DogeMainnet => Dogecoin.Instance.Mainnet,
                Networks.BtcTestnet => Bitcoin.Instance.Testnet,
                Networks.LtcTestnet => Litecoin.Instance.Testnet,
                Networks.DogeTestnet => Dogecoin.Instance.Testnet,
                _ => Bitcoin.Instance.Testnet,
            };
        }

        private static string ChooseNetwork(Networks network)
        {
            return network switch
            {
                Networks.BtcMainnet => "BtcM",
                Networks.LtcMainnet => "LtcM",
                Networks.DogeMainnet => "DogeM",
                Networks.BtcTestnet => "BtcT",
                Networks.LtcTestnet => "LtcT",
                Networks.DogeTestnet => "DogeT",
                _ => "BtcT",
            };
        }
    }
}

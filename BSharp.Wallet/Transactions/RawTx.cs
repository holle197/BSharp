using BSharp.ExtAPI.UTxOFetcher.UTxO;
using NBitcoin;

namespace BSharp.Wallet.Transactions
{
    internal class RawTx
    {
        public static string CreateAndSignTx(TxModel txModel)
        {
            var bestUtxosForTx = PickBestUtxos(txModel.Utxos, txModel.GetTotalAmount());
            var tx = Transaction.Create(txModel.Network);
            GenerateOutpoints(tx, bestUtxosForTx);
            GenerateMoney(tx, bestUtxosForTx, txModel);
            SignTx(tx, bestUtxosForTx, txModel);
            return tx.ToHex();
        }


        private static IUTxO[] PickBestUtxos(IUTxO[] utxos, decimal totalAmount)
        {
            decimal total = 0;
            List<IUTxO> bestUtxos = new();
            foreach (var utxo in utxos)
            {
                if (total >= totalAmount) break;
                total += Convert.ToDecimal(utxo.GetValue());
                bestUtxos.Add(utxo);
            }
            return bestUtxos.ToArray();
        }

        private static void GenerateOutpoints(Transaction tx, IUTxO[] utxos)
        {
            foreach (var utxo in utxos)
            {
                OutPoint otp = OutPoint.Parse(utxo.GetTxId() + ":" + utxo.GetOutputNo());
                tx.Inputs.Add(otp);
            }
        }

        private static void GenerateMoney(Transaction tx, IUTxO[] bestUtxosForTx, TxModel txModel)
        {
            decimal rest = TotalBalanceInBestUtxos(bestUtxosForTx) - txModel.GetTotalAmount();
            var moneyToSend = new Money(txModel.Amount, MoneyUnit.BTC);

            if (rest > 0)
            {
                var restMoney = new Money(rest, MoneyUnit.BTC);
                tx.Outputs.Add(moneyToSend, txModel.DestinationAddr.ScriptPubKey);
                tx.Outputs.Add(restMoney, txModel.Address.ScriptPubKey);
                SignInputs(tx, bestUtxosForTx, txModel);
                return;
            }

            tx.Outputs.Add(moneyToSend, txModel.DestinationAddr.ScriptPubKey);
            SignInputs(tx, bestUtxosForTx, txModel);
        }

        private static decimal TotalBalanceInBestUtxos(IUTxO[] utxos)
        {
            decimal total = 0;
            foreach (var utxo in utxos)
            {
                total += Convert.ToDecimal(utxo.GetValue());
            }
            return total;
        }

        private static void SignInputs(Transaction tx, IUTxO[] utxos, TxModel txModel)
        {
            for (int i = 0; i < utxos.Length; i++)
            {
                tx.Inputs[i].ScriptSig = txModel.Address.ScriptPubKey;
            }
        }

        private static void SignTx(Transaction tx, IUTxO[] utxos, TxModel txModel)
        {
            List<ICoin> coins = new();
            foreach (var utxo in utxos)
            {
                var txInString = uint256.Parse(utxo.GetTxId());
                var coin = new Coin(txInString, utxo.GetOutputNo(), new Money(Convert.ToDecimal(utxo.GetValue()), MoneyUnit.BTC),
                                    txModel.Address.ScriptPubKey);
                coins.Add(coin);
            }
            tx.Sign(txModel.Key.GetWif(txModel.Network), coins);
        }

    }
}

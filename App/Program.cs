// See https://aka.ms/new-console-template for more information
using BSharp.ExtAPI;
using BSharp.Wallet.Wallet;
using NBitcoin;

var wallet1 = new Wallet("Hollee1997@", Networks.BtcTestnet, ScriptPubKeyType.Legacy);
string toAddr = "tb1q9zczqxd0fvy8grzfwrs3qjywrsfwm0fcdmdy38";
var tx = await wallet1.PushTxAsync(toAddr, 0.0000001m, 0.000001m);
Console.WriteLine(tx);
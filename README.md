# BSharp 
BSharp is a project that enables the simple creation of bitcoin, and bitcoin-based currencies wallets. The project is free for all types of use.

• Supported Currencies : Bitcoin,Litecoin,Dogecoin.
• Supported Networks: BitcoinMainnet,BitcoinTestnet,LitecoinMainnet,LitecoinTestnet,DogecoinMainnet,DogecoinTestmet



Example of how to creeate and use BSharp wallet:

//Creating Wallet
var wallet = new Wallet("YourSecret",Networks.BtcTestnet,ScriptPubKeyType.Legacy,NBitcoin.ScriptPubKeyType.Legacy);
//Returns Private Key Of Wallet
Console.WriteLine(wallet.GetWIF());
//Returns Address Of Wallet
Console.WriteLine(wallet.GetAddress());
//Current Balance Of Wallet
var balance = await wallet.GetTotalBalance();

//Send Transaction
var tx = await wallet.PushTxAsync("tb1qrgw3m5zt8yvy6mcrag4252hm8tpvwr7qyp3l6m", 0.01m, 0.00001m);
//Log Tx Hash
Console.WriteLine(tx);


• Wallet takes 3 arguments: "YourSecret",Networks and ScriptPubKeyType.
  "YourSecret" is string that is hashed using SHA256.This bytes are used for creating private key.
  Networks represent which network you want to use.BtcMainnet,BtcTestnet etc.
  ScriptPubKeyType is the type of address used to create different types of addresses.Legacy,SegWit etc.
  
  
• wallet.PushTxAsync takes 3 arguments: Destination Address (address where you send coins), Amount (the total amount of coins you send), and Fee (commission that goes to the miners).

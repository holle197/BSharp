namespace BSharp.ExtAPI.UTxOFetcher.UTxO
{
    internal class UTxO : IUTxO
    {
        public string? txid { get; set; }
        public uint output_no { get; set; }
        public string? value { get; set; }

        public uint GetOutputNo()
        {
            return output_no;
        }

        public string? GetTxId()
        {
            return txid;
        }

        public string? GetValue()
        {
            return value;
        }
    }
}

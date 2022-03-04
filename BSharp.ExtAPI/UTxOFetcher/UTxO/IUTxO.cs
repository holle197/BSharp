namespace BSharp.ExtAPI.UTxOFetcher.UTxO
{
    public interface IUTxO
    {
        public string? GetTxId();
        public string? GetValue();
        public uint GetOutputNo();
    }
}

namespace SubtitleDownloader.Data.Client
{
    public interface ISubtitleClientFactory
    {
        ISubtitleClient BuildClient();
    }
}
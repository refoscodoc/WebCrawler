namespace WebCrawler.Tools;

public interface IDownloadPage
{
    Task<string> CallUrl(string fullUrl);
}
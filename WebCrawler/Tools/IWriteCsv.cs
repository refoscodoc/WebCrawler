using WebCrawler.Models;

namespace WebCrawler.Tools;

public interface IWriteCsv
{
    void WriteToCsv(List<RouteModel> links);
    List<RouteModel> ReadCsv();
}
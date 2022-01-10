using WebCrawler.Models;

namespace WebCrawler.Tools;

public interface IParsePage
{
    List<RouteModel> ParseFiveForty(string html);
}
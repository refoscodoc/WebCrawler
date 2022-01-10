
using Microsoft.AspNetCore.Mvc;
using WebCrawler.Models;
using WebCrawler.Tools;


namespace WebCrawler.Controllers;

[Route("[controller]")]
public class FlyFiveFortyController : Controller
{
    private readonly ILogger<FlyFiveFortyController> _logger;
    private readonly IParsePage _parser;
    private readonly IDownloadPage _download;
    private readonly IWriteCsv _csv;

    public FlyFiveFortyController(ILogger<FlyFiveFortyController> logger, IParsePage parser, IDownloadPage download, IWriteCsv csv)
    {
        _logger = logger;
        _parser = parser;
        _download = download;
        _csv = csv;
    }
    
    [HttpGet("{date?}")]
    public IActionResult Index(string? date)
    {
        DateTime departure;

        List<RouteModel> allFlights = new();
        
        if (string.IsNullOrEmpty(date) || date == "{date}")
        {
            date = DateTime.Now.ToString("dd-MM-yyyy");
            departure = DateTime.Parse(date).AddDays(10);
        }
        else
        {
            departure = DateTime.Parse(date).AddDays(10);
        }
        
        DateTime returnFlight = departure.AddDays(7);

        List<string> urls = new List<string>();
        
        urls.Add($"https://www.fly540.com/flights/nairobi-to-mombasa?isoneway=0&currency=KES&depairportcode=NBO&arrvairportcode=MBA&date_from={departure.Day}+{DatePicker.Picker(departure.Month)}+{departure.Year}&date_to={returnFlight.Day}+{DatePicker.Picker(returnFlight.Month)}+{returnFlight.Year}&adult_no=1&children_no=0&infant_no=0&searchFlight=&change_flight=");
        urls.Add($"https://www.fly540.com/flights/nairobi-to-mombasa?isoneway=0&currency=KES&depairportcode=NBO&arrvairportcode=MBA&date_from={departure.AddDays(10).Day}+{DatePicker.Picker(departure.AddDays(10).Month)}+{departure.AddDays(10).Year}&date_to={returnFlight.AddDays(10).Day}+{DatePicker.Picker(returnFlight.AddDays(10).Month)}+{returnFlight.AddDays(10).Year}&adult_no=1&children_no=0&infant_no=0&searchFlight=&change_flight=");
        
        foreach (var url in urls)
        {
            allFlights.AddRange(_parser.ParseFiveForty(_download.CallUrl(url).Result));
        }

        GetLowestFare.GetLowest(allFlights);
        
        _csv.WriteToCsv(allFlights);
        
        return Accepted();
    }
}
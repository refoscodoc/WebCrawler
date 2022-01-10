namespace WebCrawler.Models;

public class RouteModel
{
    public string OutboundDepartureIata { get; set; }
    public string OutboundArrivalIata { get; set; }
    public string OutboundDepartureTime { get; set; }
    public string OutboundArrivalTime { get; set; }
    
    public string InboundDepartureIata { get; set; }
    public string InboundArrivalIata { get; set; }
    public string InboundDepartureTime { get; set; }
    public string InboundArrivalTime { get; set; }
    
    public float FareOut { get; set; }
    public float FareIn { get; set; }
    
    public float TaxesFlexOut { get; set; }
    public float TaxesFlexPlusOut { get; set; }
    public float TaxesFlexIn { get; set; }
    public float TaxesFlexPlusIn { get; set; }
    public float FinalPrice { get; set; }
}
using WebCrawler.Models;

namespace WebCrawler.Tools;

public static class GetLowestFare
{
    public static void GetLowest(List<RouteModel> allFlights)
    {
        
        var lowestFare = allFlights[0];
        foreach (var flight in allFlights)
        {
            if (flight.FinalPrice < lowestFare.FinalPrice)
            {
                lowestFare = flight;
            }
        }

        Console.WriteLine($"The lowest flight will be: {lowestFare.OutboundDepartureIata} to {lowestFare.InboundDepartureIata} at {lowestFare.OutboundDepartureTime}, landing at {lowestFare.OutboundArrivalTime}, and returning on {lowestFare.InboundDepartureTime}, and landing at {lowestFare.InboundArrivalTime}");
    }
}
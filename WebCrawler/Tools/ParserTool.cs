using HtmlAgilityPack;
using WebCrawler.Models;

namespace WebCrawler.Tools;

public class ParserTool : IParsePage
{
    public List<RouteModel> ParseFiveForty(string html)
    {
        List<RouteModel> dailyFlights = new();

        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        foreach (HtmlNode node in htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'fly5-flights fly5-depart th')]/div[contains(@class, 'fly5-results')]/div[contains(@class, 'fly5-result')]"))
        {
            foreach (HtmlNode returnNode in htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'fly5-flights fly5-return th')]/div[contains(@class, 'fly5-results')]/div[contains(@class, 'fly5-result')]"))
            {
                var newFlight = new RouteModel
                {
                    OutboundDepartureIata = node.SelectSingleNode("//div[contains(@class, 'fly5-flights fly5-depart th')]/div/div[contains(concat(' ', @class, ' '), 'fly5-flfrom')]/span").InnerText,
                    OutboundArrivalIata = node.SelectSingleNode("//div[contains(@class, 'fly5-flights fly5-depart th')]/div/div[contains(concat(' ', @class, ' '), 'fly5-flto')]/span").InnerText,
                    
                    OutboundDepartureTime = node.SelectSingleNode("./table/tbody/tr/td[contains(concat(' ', @data-title, ' '), 'Departs')]/span[contains(concat(' ', @class, ' '), 'fldate')]").InnerText
                        // .Remove(0, 5)
                        .Replace(",", "")
                        .Replace("-", " ") + "2022 UTC+3 " + node.SelectSingleNode("./table/tbody/tr/td[contains(concat(' ', @data-title, ' '), 'Departs')]/span[contains(@class, 'fltime')]").InnerText,
                    OutboundArrivalTime = node.SelectSingleNode("./table/tbody/tr/td[contains(concat(' ', @data-title, ' '), 'Arrives')]/span[contains(concat(' ', @class, ' '), 'fldate')]").InnerText
                        // .Remove(0, 5)
                        .Replace(",", "")
                        .Replace("-", " ") + "2022 UTC+3 " + node.SelectSingleNode("./table/tbody/tr/td[contains(concat(' ', @data-title, ' '), 'Arrives')]/span[contains(concat(' ', @class, ' '), 'fltime')]").InnerText,
                    
                    FareOut = float.Parse(node.SelectSingleNode("./table/tbody/tr/td[contains(concat(' ', @id, ' '), 'fdflight')]/span[contains(concat(' ', @class, ' '), 'flprice')]").InnerText),
                    TaxesFlexOut = float.Parse(node.SelectNodes(".//span[contains(concat(' ', @class, ' '), 'pkgprice')]")[1].InnerText.Remove(0, 4)),
                    TaxesFlexPlusOut = float.Parse(node.SelectNodes(".//span[contains(concat(' ', @class, ' '), 'pkgprice')]")[2].InnerText.Remove(0, 4)),
                    
                    InboundDepartureIata = returnNode.SelectSingleNode("//div[contains(@class, 'fly5-flights fly5-return th')]/div/div[contains(concat(' ', @class, ' '), 'fly5-flfrom')]/span").InnerText,
                    InboundArrivalIata = returnNode.SelectSingleNode("//div[contains(@class, 'fly5-flights fly5-return th')]/div/div[contains(concat(' ', @class, ' '), 'fly5-flto')]/span").InnerText,

                    InboundDepartureTime = returnNode.SelectSingleNode("./table/tbody/tr/td[contains(concat(' ', @data-title, ' '), 'Departs')]/span[contains(concat(' ', @class, ' '), 'fldate')]").InnerText
                        // .Remove(0, 5)
                        .Replace(",", "")
                        .Replace("-", " ") + "2022 UTC+3 " + returnNode.SelectSingleNode("./table/tbody/tr/td[contains(concat(' ', @data-title, ' '), 'Departs')]/span[contains(concat(' ', @class, ' '), 'fltime')]").InnerText,
                    InboundArrivalTime = returnNode.SelectSingleNode("./table/tbody/tr/td[contains(concat(' ', @data-title, ' '), 'Arrives')]/span[contains(concat(' ', @class, ' '), 'fldate')]").InnerText
                        // .Remove(0, 5)
                        .Replace(",", "")
                        .Replace("-", " ") + "2022 UTC+3 " + returnNode.SelectSingleNode("./table/tbody/tr/td[contains(concat(' ', @data-title, ' '), 'Arrives')]/span[contains(concat(' ', @class, ' '), 'fltime')]").InnerText,
                    
                    FareIn = float.Parse(returnNode.SelectSingleNode("./table/tbody/tr/td[contains(concat(' ', @id, ' '), 'fdflight')]/span[contains(concat(' ', @class, ' '), 'flprice')]").InnerText),
                    TaxesFlexIn = float.Parse(node.SelectNodes(".//span[contains(concat(' ', @class, ' '), 'pkgprice')]")[1].InnerText.Remove(0, 4)),
                    TaxesFlexPlusIn = float.Parse(node.SelectNodes(".//span[contains(concat(' ', @class, ' '), 'pkgprice')]")[2].InnerText.Remove(0, 4)),
                    
                    FinalPrice = float.Parse(node.SelectSingleNode("./table/tbody/tr/td[contains(concat(' ', @id, ' '), 'fdflight')]/span[contains(concat(' ', @class, ' '), 'flprice')]").InnerText) +float.Parse(returnNode.SelectSingleNode("./table/tbody/tr/td[contains(concat(' ', @id, ' '), 'fdflight')]/span[contains(concat(' ', @class, ' '), 'flprice')]").InnerText), 
                };
                
                dailyFlights.Add(newFlight);
            }
        }

        return dailyFlights;
        
    }
}
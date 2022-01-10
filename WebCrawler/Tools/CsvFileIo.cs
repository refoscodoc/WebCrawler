using System.ComponentModel;
using System.Text;
using WebCrawler.Models;

namespace WebCrawler.Tools;

public class CsvFileIo : IWriteCsv
{
    public void WriteToCsv(List<RouteModel> allFlights)
    {
        // very many thanks to https://gist.github.com/luisdeol/c2c276796a92c8e3246ce2cd3e17e1df
        
        var sb = new StringBuilder();
        // var basePath = AppDomain.CurrentDomain.BaseDirectory;
        // var finalPath = Path.Combine(basePath, "bestfares.csv");
        var finalPath = Path.Combine("./", "bestfares.csv");
        var header = "";
        var info = typeof(RouteModel).GetProperties();
        if (!File.Exists(finalPath))
        {
            var file = File.Create(finalPath);
            file.Close();
            foreach (var prop in typeof(RouteModel).GetProperties())
            {
                header += prop.Name + "; ";
            }
            header = header.Substring(0, header.Length - 2);
            sb.AppendLine(header);
            TextWriter sw = new StreamWriter(finalPath, true);
            sw.Write(sb.ToString());
            sw.Close();
        }
        foreach (var obj in allFlights)
        {
            sb = new StringBuilder();
            var line = "";
            foreach (var prop in info)
            {
                line += prop.GetValue(obj, null) + "; ";
            }
            line = line.Substring(0, line.Length - 2);
            sb.AppendLine(line);
            TextWriter sw = new StreamWriter(finalPath, true);
            sw.Write(sb.ToString());
            sw.Close(); 
        }
    }

    public List<RouteModel> ReadCsv()
    {
        return null;
    }
}
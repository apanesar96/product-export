using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VerifyXunit;
using Xunit;

namespace ProductExport;

[UsesVerify]
public class XmlExporterTest
{
    [Fact]
    public Task Something()
    {
        var json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(),"Orders.json"));
        var orders = JsonConvert.DeserializeObject<List<Order>>(json);

        var xml = XmlExporter.ExportFull(orders);

        return Verifier.VerifyXml(xml);
    }
    
    [Fact]
    public void Price()
    {
        var json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(),"Temp.json"));
        var price = JsonConvert.DeserializeObject<Product>(json);
    }
}
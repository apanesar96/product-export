using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VerifyXunit;
using Xunit;

namespace ProductExport;

[UsesVerify]
public class XmlExporterTest
{
    [Fact]
    public Task GivenACompleteOrder_VerifyOrder()
    {
        var json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(),"Orders.json"));
        var orders = JsonConvert.DeserializeObject<List<Order>>(json);

        var xml = XmlExporter.ExportFull(orders);

        return Verifier.VerifyXml(xml);
    }
    
    [Fact]
    public Task GivenACompleteOrder_VerifyTaxDetails()
    {
        var json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(),"Orders.json"));
        var orders = JsonConvert.DeserializeObject<List<Order>>(json);

        var xml = XmlExporter.ExportTaxDetails(orders);

        return Verifier.VerifyXml(xml);
    }

    [Fact]
    public Task GivenAStore_VerifyStore()
    {
        var json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(),"Orders.json"));
        var orders = JsonConvert.DeserializeObject<List<Order>>(json);

        var store = orders.Select(x => x.Store).FirstOrDefault();
        
        var xml = XmlExporter.ExportStore(store);

        return Verifier.VerifyXml(xml);
    }
    
    [Fact]
    public Task GivenAStore_VerifyHistory()
    {
        var json = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(),"Orders.json"));
        var orders = JsonConvert.DeserializeObject<List<Order>>(json);
        
        var xml = XmlExporter.ExportHistory(orders);
    
        return Verifier.VerifyXml(xml)
            .ScrubLinesContaining("createdAt");
    }
}
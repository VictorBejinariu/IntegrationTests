using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Connector.Int.Tests.TestModels;

public class PaymentAllTestData:IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var fileData = File.ReadAllText("PaymentTestData.json");
        
        var items = JsonConvert.DeserializeObject<List<PaymentTest>>(fileData);

        foreach (var item in items)
        {
            yield return new object[]{item};
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
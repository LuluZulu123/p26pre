using System.Text.Json.Serialization;
using DB;
using Extensions;
using Newtonsoft.Json;
using Handler = Handlers.Hansi;

namespace Hansi;

public class Controller
{
	public static async Task GetOrders (Handler handler) {
		using var conn = await ConnectorBase.GetConnection("va6"); // Hansi
		
		var deb2 = conn.LoadGeneric(@"SELECT 
                                        o.*
                                    FROM
                                        hansi.v_orders o
        ;", (x) => new {
            Id = x["id"],
		});
		
		await foreach (var row in deb2)
		{
            await handler.Send(JsonConvert.SerializeObject(row));
		}
		
		await conn.CloseAsync();
    }
}
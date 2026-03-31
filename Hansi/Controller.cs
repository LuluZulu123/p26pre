using System.Text.Json.Serialization;
using DB;
using Extensions;
using Newtonsoft.Json;
using Handler = Handlers.Hansi;

namespace Hansi;

public class Controller
{
	private class Instruction {
		public string Type;
		public object Data;
	}
	private class Order {
		public string Id;
		public object ClientId;
	}
	public static async Task HandleSave (Handler handler, string data) {
		using var conn = await ConnectorBase.GetConnection("va6"); // Hansi
		
		var instruction = JsonConvert.DeserializeObject<Instruction>(data);

		switch (instruction.Type) {
			case "order":
				var order = (Order)instruction.Data;
				conn.LoadGeneric(@"REPLACE INTO hansi.order 
									(id, client)
								SELECT
									@Id,
									@Client,
        						;", (x) => 1, 
									("Id", order.Id),
									("Client", order.ClientId)
								);
				break;
		}

		// {"Type":"order","Data":{"CreateTime":"2026-03-31T10:20:17.606+02:00","Id":"47ccb48b-b245-4871-93c2-327b0de7518f","Cart":{"ArticleCodes":{}},"ClientId":"031b8cef-ce4f-4558-bd9d-6fbd4ef62ec9"}}

		await conn.CloseAsync();
    }
	public static async Task GetOrders (Handler handler) {
		using var conn = await ConnectorBase.GetConnection("va6"); // Hansi
		
		var deb2 = conn.LoadGeneric(@"SELECT 
                                        o.*
                                    FROM
                                        hansi.v_orders o
        ;", (x) => new {
            Id = x["id"],
            Client = x["client"],
		});
		
		await foreach (var row in deb2)
		{
            await handler.Send(JsonConvert.SerializeObject(row));
		}
		
		await conn.CloseAsync();
    }
}
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
		public GenericData Data;
	}
	private class GenericData {
		public string Id;
		public string Data;
	}
	// public static async Task HandleSaveOrder (Handler handler, string data) {
	// 	try {
	// 		using var conn = await ConnectorBase.GetConnection("va6"); // Hansi
			
	// 		var instruction = JsonConvert.DeserializeObject<Instruction>(data);

	// 		switch (instruction.Type) {
	// 			case "order":
	// 				await conn.Exec(@"REPLACE INTO hansi.order 
	// 									(id, client)
	// 								SELECT
	// 									@Id,
	// 									@Client
	// 								;",  
	// 									("Id", instruction.Data.Id),
	// 									("Client", instruction.Data.ClientId)
	// 								);
	// 				break;
	// 		}

	// 		// {"Type":"order","Data":{"CreateTime":"2026-03-31T10:20:17.606+02:00","Id":"47ccb48b-b245-4871-93c2-327b0de7518f","Cart":{"ArticleCodes":{}},"ClientId":"031b8cef-ce4f-4558-bd9d-6fbd4ef62ec9"}}

	// 		await conn.CloseAsync();
	// 		} 
	// 	catch (System.Exception e)
	// 	{
	// 		Console.WriteLine($"web error");
	// 		Console.WriteLine(e);
	// 	}
    // }
	public static async Task HandleSave (Handler handler, string data) {
		try {
			using var conn = await ConnectorBase.GetConnection("va6"); // Hansi
			
			var instruction = JsonConvert.DeserializeObject<GenericData>(data);

			await conn.Exec(@"REPLACE INTO hansi.data 
								(id, data, user)
							SELECT
								@Id,
								@Data,
								@User
							;",  
								("Id", instruction.Id),
								("Data", instruction.Data),
								("User", handler.Name)
							);
							
			await conn.CloseAsync();
			} 
		catch (System.Exception e)
		{
			Console.WriteLine($"web error");
			Console.WriteLine(e);
		}
    }
	public static async Task GetChanges (Handler handler, string refTime) {
		using var conn = await ConnectorBase.GetConnection("va6"); // Hansi
		
		var deb2 = conn.LoadGeneric(@"SELECT 
                                        d.*
                                    FROM
                                        hansi.v_data d
									WHERE
										d.modify_time > @refTime
        ;", (x) => new {
            Id = x["id"],
            Data = x["data"],
            ModifyTime = x["modify_time"],
		}, ("refTime", refTime));
		
		await foreach (var row in deb2)
		{
            await handler.Send(JsonConvert.SerializeObject(row));
		}
		
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
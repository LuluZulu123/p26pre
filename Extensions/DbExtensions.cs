using DB;
using MySql.Data.MySqlClient;

namespace Extensions;

public static class DB
{
	public static async IAsyncEnumerable<Tout> Exec<Tout> (this MySqlConnection conn, string cmd, params (string, object)[] placeholder)
	{
		var command = new MySqlCommand();
		
		command.Connection = conn;
		command.CommandText = cmd;
		
		foreach (var pair in placeholder)
		{
			command.Parameters.AddWithValue(pair.Item1, pair.Item2);
		}
		
		await command.ExecuteNonQuery();
	}
	public static async IAsyncEnumerable<Tout> LoadGeneric<Tout> (this MySqlConnection conn, string cmd, Func<MySqlDataReader, Tout> builder, params (string, object)[] placeholder)
	{
		var command = new MySqlCommand();
		
		command.Connection = conn;
		command.CommandText = cmd;
		
		foreach (var pair in placeholder)
		{
			command.Parameters.AddWithValue(pair.Item1, pair.Item2);
		}
		
		using var reader = (MySqlDataReader)await command.ExecuteReaderAsync();
		
		while (await reader.ReadAsync())
		{
			yield return builder(reader);
		}
	}
	public static async IAsyncEnumerable<T> Load<T> (this MySqlConnection conn, string cmd) where T : DbEntity, new ()
	{
		ConnectorBase.ReadFields(typeof(T));
		
		var fields = ConnectorBase.FieldInfoCache[typeof(T)];
		var command = new MySqlCommand();
		
		command.Connection = conn;
		command.CommandText = cmd;
		
		using var reader = (MySqlDataReader)await command.ExecuteReaderAsync();
		
		while (await reader.ReadAsync())
		{
			var row = new T();
			
			foreach (var field in fields)
			{
				switch (field.FieldType.ToString())
				{
					case "System.String":
						field.SetValue(row, reader.GetString(field.Name));
						break;
					
					default:
						Console.WriteLine($"unmapped type {field.FieldType}");
						break;
				}
			}
			
			yield return row;
		}
	}
}

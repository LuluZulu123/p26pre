using System.Reflection;
using System.Runtime.CompilerServices;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509.Qualified;

namespace DB;

public abstract class DbEntity
{
}
public class Unit : DbEntity
{
	private string unit_class = "";
	public string Class => unit_class;
}
public class ConnectorBase
{
	public const string identifier = "8cZ7m7FOQHhpFgdAkBj0QH7c6ZgCebXJVt1e";
	public const string host = "127.0.0.1";
	// public const string host = "82.165.217.93";
	public const string uid = "web";
	public const string dabb = "va6";
	private static string ConnectionString => $"server={host};uid={uid};pwd={identifier};database={dabb}";
	private static Dictionary<Type, List<(FieldInfo,Type)>> FieldInfoCache = new Dictionary<Type, List<(FieldInfo,Type)>>();
	public static void ReadFields (Type type)
	{
		if (FieldInfoCache.ContainsKey(type))
			return;
		
		var fields = type
						.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
						.Select(x => (x, x.FieldType))
						.ToList()
						;
		
		FieldInfoCache.Add(type, fields);
	}
	public static async IEnumerable<T> GetResults<T> (string cmd) where T : DbEntity, new () {
		ReadFields(typeof(T));
		
		var fields = FieldInfoCache[typeof(T)];
		
		var connection = new MySqlConnection(ConnectionString);
		
		connection.Open();
		
		MySqlCommand command = new MySqlCommand();
		
		command.Connection = connection;
		command.CommandText = cmd;
		
		using var reader = await command.ExecuteReaderAsync();
		
		while (reader.Read()) // async????
		{
			var row = new T();
			
			foreach (var field in fields)
			{
				switch (field.Item2.ToString())
				{
					case "System.String":
						field.Item1.SetValue(row, reader.GetString(field.Item1.Name));
						break;
					
					default:
						Console.WriteLine($"unmapped type {field.Item2}");
						break;
				}
			}
			
			yield return row;
		}
		
		connection.Close();
	} 
}
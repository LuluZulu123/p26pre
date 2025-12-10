using System.Reflection;
using MySql.Data.MySqlClient;

namespace DB;
public class ConnectorBase
{
	public static string identifier => Program.IsDev ? "lermi" : "8cZ7m7FOQHhpFgdAkBj0QH7c6ZgCebXJVt1e";
	public static string host => Program.IsDev ? "82.165.217.93" : "127.0.0.1";
	public const string uid = "web";
	private static string ConnectionString (string db) => Program.IsDev ? $"server={host};uid={uid};pwd=gei{identifier}chl;database={db}" : $"server={host};uid={uid};pwd={identifier};database={db}";
	public static Dictionary<Type, List<FieldInfo>> FieldInfoCache {get; private set;} = new Dictionary<Type, List<FieldInfo>>();
	public static void ReadFields (Type type)
	{
		if (FieldInfoCache.ContainsKey(type))
			return;
		
		var fields = type
						.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
						.ToList()
						;
		
		FieldInfoCache.Add(type, fields);
	}
	public static async Task<MySqlConnection> GetConnection (string db)
	{
		var connection = new MySqlConnection(ConnectionString(db));
		await connection.OpenAsync();
		return connection;
	}
}
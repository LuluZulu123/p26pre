using System.Linq;
using System.Threading.Tasks;

namespace Handlers;

public class Plans : SocketHandler
{
	private static HashSet<Plans> connections = new HashSet<Plans>();
	public string Name { get; private set; } = string.Empty;
	public override string ToString() => Name;
	protected override async Task Unregister()
	{
		await PrintOthers($"{this} left");
		connections.Remove(this);
	}
	protected override async Task HanldeInput(string raw)
	{
		var parts = raw.Split('|', 2);
		
		var cmd = parts[0];
		var data = () => parts[1];
		
		switch (cmd)
		{
			case "register":
				Name = data();
				connections.Add(this);
				await PrintOwn($"{this} registered");
				break;
			
			case "w":
				await PrintOthers($"{this} says: {data()}");
				break;
			
			case "lookup":
				await GetCurrentClients();
				break;
		}
	}
	private async Task GetCurrentClients ()
	{
		foreach (var connection in connections)
		{
			var isMe = connection == this ? " (it's you!)" : string.Empty;
			await PrintOwn($"{connection}{isMe}");
		}
	}
	protected override async Task<bool> HanldeInput(byte[] raw)
	{
		if (raw == null)
			return false;
		
		if (raw.Length == 0)
			return false;
		
		var data = raw.Skip(1).ToArray();
		switch (raw[0])
		{
			case (byte)'d':
				await PrintRoom($">>>{raw}");
				return true;
			
			default:
				return false;
		}
	}
}
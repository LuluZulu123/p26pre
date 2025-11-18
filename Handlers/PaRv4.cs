namespace Handlers;

public class PaRv4 : SocketHandler
{
	private static HashSet<PaRv4> connections = new HashSet<PaRv4>();
	public string Name { get; private set; } = string.Empty;
	public override string ToString() => Name;

	protected override async Task Unregister()
	{
		await PrintOthers($"{this} left PAR");
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
				await PrintOwn($"{this} registered PAR");
				break;
			
			case "w":
				await PrintOthers($"{this} says: {data()}");
				break;
			
			case "lookup":
				await GetCurrentClients();
				break;
		}

		await PushUnlock();
	}
	private async Task GetCurrentClients ()
	{
		foreach (var connection in connections)
		{
			var isMe = connection == this ? " (it's you!) (PAR)" : string.Empty;
			await PrintOwn($"{connection}{isMe}");
		}
	}
}
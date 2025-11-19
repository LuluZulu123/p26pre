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
	protected async Task SendOutput (string msg)
	{
		await Socket?.Send($"o|{msg}");
	}
	protected async Task SendOutput (string typeKey, string msg)
	{
		await Socket?.Send($"{typeKey}}|{msg}");
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
				await SendOutput($"{this} registered PAR");
				await Socket?.Send($"m");
				break;
			
			case "w":
				await SendOutput($"{this} says: {data()}");
				break;
			
			case "v":
				await SendOutput($"{this} does: {data()}");
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
			await SendOutput($"{connection}{isMe}");
		}
	}
}
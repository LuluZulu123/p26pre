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
		await Socket?.Send($"{typeKey}|{msg}");
	}
	protected async Task SendLog (string msg)
	{
		await Socket?.Send($"o|log|{msg}");
	}
	protected async Task SendRoll (int diff)
	{
		await Socket?.Send($"r|{diff}");
	}
	protected async Task HandleVerb (string msg)
	{
		var parts = raw.Split('|', 2);
		
		var verb = parts[0];
		var data = () => parts[1];
		
		switch (verb) {
			case "hit":
				await SendLog($"{this} hit {data()}");
				await PushUnlock();
				break;
			
			case "roll":
				await SendRoll(5);
				break;
		}
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
				await Socket?.Send($"m");
				await PushUnlock();
				break;
			
			case "w":
				await SendOutput($"{this} says: {data()}");
				await PushUnlock();
				break;
			
			case "v":
				await HandleVerb(data());
				break;
			
			case "lookup":
				await GetCurrentClients();
				await PushUnlock();
				break;
		}
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
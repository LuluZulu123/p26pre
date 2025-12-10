using DB;
using VA6;

namespace Handlers;

public class VA6 : SocketHandler
{
	private static HashSet<VA6> connections = new HashSet<VA6>();
	public string Name { get; private set; } = string.Empty;
	public int ProfileId;
	
	public override string ToString() => Name;

	protected override async Task Unregister()
	{
		await PrintOthers($"{this} left VA6");
		connections.Remove(this);
	}
	protected override async Task HanldeInput(string raw)
	{
		if (Program.IsDev)
		{
			Console.WriteLine($"< {raw}");
		}
		
		var parts = raw.Split('|', 2);
		
		var cmd = parts[0];
		var data = () => parts[1];
		
		switch (cmd)
		{
			case "register":
				Name = data();
				connections.Add(this);
				await PushUnlock();
				break;
			
			case "p":
				int.TryParse(data(), out ProfileId);
				await Send($"ProfileId {ProfileId}");
				await PushUnlock();
				break;
			
			case "u":
				await Controller.GetUnits(this);
				await PushUnlock();
				break;
		}
	}
}
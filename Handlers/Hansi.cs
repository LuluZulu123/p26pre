using DB;
using Hansi;

namespace Handlers;

public class Hansi : SocketHandler
{
	private static HashSet<Hansi> connections = new HashSet<Hansi>();
	public string Name { get; private set; } = string.Empty;
	
	public override string ToString() => Name;

	protected override async Task Unregister()
	{
		await PrintOthers($"{this} left VA6");
		connections.Remove(this);
	}
	protected override async Task HanldeInput(string raw)
	{
		var parts = raw.Split('|', 2);
		
		var cmd = parts[0];
		var data = () => parts[1];
		
		switch (cmd)
		{
			case "r": // register
				Name = data();
				connections.Add(this);
				await PushUnlock();
				break;
			
			case "rename":
				Name = data();
				await PushUnlock();
				break;
			
			case "s": // save
				// Name = data();
				await Controller.HandleSave(this, data());
				await PushUnlock();
				break;
			
			case "l": // load
				await Controller.GetChanges(this, data());
				await PushUnlock();
				break;
			
			case "a": // autocomplete
				await Controller.GetArticles(this, data());
				await PushUnlock();
				break;
			
			case "o": // deprecated
				await Controller.GetOrders(this);
				await PushUnlock();
				break;
			
			case "u":
				await Unregister();
				await PushUnlock();
				break;
		}
	}
}
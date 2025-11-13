namespace Handlers;

public class Generic : SocketHandler
{
	protected override async Task HanldeInput(string raw)
	{
		Console.WriteLine($"HanldeInput in {GetType()} > \"{raw}\"");
		await PushUnlock();
	}
}
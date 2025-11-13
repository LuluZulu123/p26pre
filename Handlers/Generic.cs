namespace Handlers;

public class Generic : SocketHandler
{
	protected override Task HanldeInput(string raw)
	{
		Console.WriteLine($"HanldeInput in {GetType()} > \"{raw}\"");
		return Task.CompletedTask;
	}
}
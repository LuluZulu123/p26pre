namespace Handlers;

public class PaR : SocketHandler
{
	protected override Task HanldeInput(string raw)
	{
		Console.WriteLine($"HanldeInput in {GetType()} > \"{raw}\"");
		return Task.CompletedTask;
	}
}
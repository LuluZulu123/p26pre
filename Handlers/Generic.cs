namespace Handlers;

public class Generic : SocketHandler
{
	protected override void HanldeInput(string raw)
	{
		Console.WriteLine($"HanldeInput in {this} > \"{raw}\"");
	}
}
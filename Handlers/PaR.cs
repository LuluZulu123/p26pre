namespace Handlers;

public class PaR : SocketHandler
{
	protected override void HanldeInput(string raw)
	{
		Console.WriteLine($"HanldeInput in {this} > \"{raw}\"");
	}
}
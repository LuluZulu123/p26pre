using System.Threading.Tasks;

namespace Handlers;

public class Chat : SocketHandler
{
	protected override async Task HanldeInput(string raw)
	{
		Console.WriteLine($"HanldeInput in {GetType()} > \"{raw}\" by client {identifier}");
		await PrintRoom($">>>{raw}");
	}
}
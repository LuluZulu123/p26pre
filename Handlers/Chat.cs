using System.Threading.Tasks;

namespace Handlers;

public class Chat : SocketHandler
{
	protected override async Task HanldeInput(string raw)
	{
		// await PrintGlobal($">>>{raw}");
		await PrintRoom($">>>{raw}");
	}
}
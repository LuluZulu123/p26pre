using System.Net.WebSockets;
using System.Text;
using cx3web.WorldObjects;

public abstract class SocketHandler
{
	protected abstract void HanldeInput (string raw);
	public async Task AddClinet(GameCharacter localPlayer)
	{
		var webSocket = localPlayer.socket;
		
		Console.WriteLine($"socket for {localPlayer} == {webSocket}");
		
		if (webSocket == null)
			return;

		Console.WriteLine($"socket state for {localPlayer} == {webSocket.State}");
		
		if (webSocket.State != WebSocketState.Open)
			return;

		var buffer = new byte[1024 * 4];
		
		var first = true;
		
		WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
		
		while (!result.CloseStatus.HasValue)
		{
			if (!first)
				result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
			
			first = false;
			
			var input = Encoding.UTF8.GetString(buffer, 0, result.Count).Trim();
			
			HanldeInput(input);
			
			// Console.WriteLine($"recieved {input} from {localPlayer}");

			// if (string.IsNullOrEmpty(input)) {
			// 	localPlayer.PushUnlock();
			// 	continue;
			// }

						// if (localPlayer.CheckProgressAction(input)) {
						// 	localPlayer.PushUnlock();
						// 	continue;
						// }

						// if (localPlayer.CheckContinue(input)) {
						// 	localPlayer.PushUnlock();
						// 	continue;
						// }
						
						// if (localPlayer.diceRoll.CheckRolling(input))
						// {
						// 	localPlayer.PushUnlock();
						// 	continue;
						// }
			
			// localPlayer.mechanics.HandleInput(localPlayer, input);
			
			// localPlayer.PushUnlock();
		}
		
		await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
	}
}

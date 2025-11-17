using System.Net.WebSockets;
using System.Text;

public static class Socket {
	public static ArraySegment<byte> ToByteArray(this string message) {
		return new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
	}
	public static async Task Send (this WebSocket webSocket, string message) {
		await webSocket.SendAsync(
			message.ToByteArray(),
			WebSocketMessageType.Text, 
			true, 
			CancellationToken.None
		);
	}
}
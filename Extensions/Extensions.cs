using System.Net.WebSockets;
using System.Text;

public static class Socket {
	public static ArraySegment<byte> ToByteArray(this string message) {
		return new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
	}
	public static async Task SendOutput (this WebSocket webSocket, string message, bool own = true) {
		var key = own ? "oo" : "of";
		await webSocket.Send($"{key}|{message}");
	}
	public static async Task SendStatsMain (this WebSocket webSocket, string message) {
		await webSocket.Send($"sm|{message.Trim().Replace(Environment.NewLine, "<br />")}");
	}
	public static async Task SendStatsWeapon (this WebSocket webSocket, string message) {
		await webSocket.Send($"sw|{message.Trim().Replace(Environment.NewLine, "<br />")}");
	}
	public static async Task SendStatsAttr (this WebSocket webSocket, string message) {
		await webSocket.Send($"sa|{message.Trim().Replace(Environment.NewLine, "<br />")}");
	}
	public static async Task SendDiceData (this WebSocket webSocket, string jsArray) {
		await webSocket.Send($"r|{jsArray}");
	}
	public static async Task SendContinue (this WebSocket webSocket, string text) {
		await webSocket.Send($"c|{text}");
	}
	public static async Task SendProgressAction (this WebSocket webSocket, string text, int restSeconds, float skipPenalty, bool abortable) {
		var time = TimeSpan.FromSeconds(restSeconds).ToString("mm':'ss");
		var costs = skipPenalty.ToString("0.00");
		await webSocket.Send($"p|{text}|{time}|{costs}|{abortable}");
	}
	public static async Task SendDefaultInput (this WebSocket webSocket) {
		await webSocket.Send($"i|-");
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
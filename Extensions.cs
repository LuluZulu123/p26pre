using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace cx3web
{
	public static class Extensions {
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
		// public static ConditionalString OnlyIf (this string val, Func<bool> cond) {
		// 	return new ConditionalString(val, cond);
		// }
		public static string AsCommand (this string val) {
			return $"<i class=\"cmd\">{val}</i>";
		}
		public static string AsKeyword (this string val) {
			return $"<i class=\"keyword\">{val}</i>";
		}
		public static string AsError (this string val) {
			return $"<i class=\"error\">{val}</i>";
		}
		public static string AsWarning (this string val) {
			return $"<i class=\"warn\">{val}</i>";
		}
		public static int Range (this Random rnd, int from, int to) {
			return (int)Math.Floor(rnd.NextDouble() * (to - from));
		}
		public static string CamelToSeperated (this string input) {
			var output = "";
			foreach (var character in input)
			{
				var charString = character.ToString();
				var isLowerCase = charString.ToLower() == charString;

				if (!isLowerCase)
					output += " ";

				output += charString;
			}
			return output;
		}
	}
}

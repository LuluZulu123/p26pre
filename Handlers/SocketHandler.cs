using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

public abstract class SocketHandler
{
	protected virtual Task HanldeInput (string raw) => Task.CompletedTask;
	protected virtual Task<bool> HanldeInput (byte[] raw) => Task.FromResult(false);
	public string identifier => GetHashCode().ToString();
	public override string ToString() {
		return identifier;
	}
    public object? roomIdentifier;
	private static HashSet<SocketHandler> clients = new HashSet<SocketHandler>();
	public WebSocket? Socket {get; private set;}
	public async Task AddClinet(WebSocket Socket) {
		this.Socket = Socket;
		
		if (Socket == null)
			return;
		
		if (Socket.State != WebSocketState.Open)
			return;

		clients.Add(this);
		
		var buffer = new byte[1024 * 4];
		
		var first = true;
		
		WebSocketReceiveResult result = await Socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
		
		while (!result.CloseStatus.HasValue)
		{
			if (!first)
				result = await Socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
			
			first = false;
			
			var binaryUsed = await HanldeInput(buffer);
			
			if (binaryUsed)
				continue;
			
			var input = Encoding.UTF8.GetString(buffer, 0, result.Count).Trim();
			
			await HanldeInput(input);
		}
		
		await Socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
	}
	public async Task PrintGlobal(string text)
	{
		await Print(new Output(text, OutputType.Global, this));
	}
	public async Task PrintRoom(string text)
	{
		await Print(new Output(text, OutputType.Room, this));
	}
	public async Task PrintOthers(string text)
	{
		await Print(new Output(text, OutputType.OtherOnly, this));
	}
	public async Task PrintOwn(string text)
	{
		await Print(new Output(text, OutputType.OwnOnly, this));
	}
	public async Task Print(Output output)
	{
		if (output.type == OutputType.Global) {
			foreach (var client in clients)
			{
				if (client.Socket == null)
					continue;
				
				await client.Socket.Send(output.text);
			}
			return;
		}

		if (output.type != OutputType.OtherOnly)
			output.actor.Socket?.Send(output.text);
		
		if (output.type == OutputType.OwnOnly)
			return;
		
		foreach (var client in clients.Where(x => x.roomIdentifier == output.actor.roomIdentifier))
		{
			if (client == output.actor)
				continue;
			
			client.Socket?.Send(output.text);
		}
	}
	protected async Task PushUnlock () {
		if (Socket == null)
			return;
		
		await Socket.Send("u|-");
	}
}

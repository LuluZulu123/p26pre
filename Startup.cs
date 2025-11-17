public class Startup
{
	public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
		app.UseWebSockets();
		app.UseDefaultFiles();
		app.UseStaticFiles();
		app.UseSession();
		app.Run(Run);
	}
	public static async Task Run (HttpContext context) {
		if (!context.Request.Path.StartsWithSegments("/o"))
			return;
			
		if (!context.WebSockets.IsWebSocketRequest)
			return;
		
		var socket = await context.WebSockets.AcceptWebSocketAsync();
		
		var parts = context.Request.Path.Value!.Split('/');
		var project = parts.Last();
		
		var newType = Type.GetType($"Handlers.{project}", false, true);
		
		if (newType == null)
		{
			newType = typeof(Handlers.Generic);
			Console.WriteLine($"handler not found {project}. Fallback to generic");
		}
		
		var handler = Activator.CreateInstance(newType) as SocketHandler;
		
		if (handler == null)
		{
			throw new ArgumentException($"{newType} does not implement SocketHandler");
		}
		
		await handler.AddClinet(socket);
	}
}

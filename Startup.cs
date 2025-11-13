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
		if (context.Request.Path.StartsWithSegments("/o"))
		{
			if (context.WebSockets.IsWebSocketRequest)
			{
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
		
		// if (context.WebSockets.IsWebSocketRequest)
		// 	return;
		
		await context.Response.WriteAsync($@"<!DOCTYPE html>
												<head>
												<title>cx3web</title>
												<link rel=""manifest"" href=""/manifest.json"">
												<link rel=""shortcut icon"" sizes=""256x256"" href=""/fav.png"" />
												<meta name=""msapplication-TileColor"" content=""#000000""/>
												<meta name=""theme-color"" content=""#000000"">
												<meta name=""viewport"" content=""width=device-width,initial-scale=1,maximum-scale=1"">
												<link media=""all"" rel=""stylesheet"" href=""/main.css"" type=""text/css"">
												</head>
												<body data-input=""loading"">
												<div class=""mainContainer"">
													<div class=""terminal"">
														<div class=""scrollhelper""></div>
													</div>
													<div class=""status"">
														<div class=""main"">
														</div>
														<div class=""weapon"">
														</div>
														<div class=""attr"">
														</div>
													</div>
												</div>
												<div class=""form"">

													<div class=""singleRow editable inpDef fakeinput input""></div>
													
													<div onclick=""sendRoll();"" class=""singleRow inpRoll rollButton btn""></div>
													
													<div onclick=""sendCont();"" class=""singleRow inpCont contButton btn""></div>
													
													<div class=""inpProg progContainer"">
													
														<div class=""full progText""></div>
														<div onclick=""send('abort');"" class=""third Button progAbort btn"">>ABORT<</div>
														<div class=""third"">WAIT (<span class=""progRestTime""></span>)</div>
														<div onclick=""send('skip');"" class=""third Button btn"">>SKIP (<span class=""progSkipCosts""></span>)<</div>
													
													</div>

												</div>
												<script type=""text/javascript"" src=""/main.js""></script>
												</body>
											");
	}
}

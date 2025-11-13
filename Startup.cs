using System;
using System.Net.WebSockets;
using System.Text;
using cx3web;
using cx3web.WorldObjects;

public class Startup
{
	public const string playerNameSessionKey = "playerName";
	public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
		app.UseWebSockets();
		
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}
		
		app.UseDefaultFiles();
		app.UseStaticFiles();
		app.UseSession();

		app.Run(Run);
	}
	public static async Task Run (HttpContext context) {
		// GamsObject.contextFun = () => { return context; };

		Func<string, string> TryGetCookie = (key) =>
		{
			key = key.Trim();

			var val = string.Empty;

			if (string.IsNullOrEmpty(key))
				return val;

			if (!context.Request.Cookies.ContainsKey(key))
				return val;

			return context.Request.Cookies[playerNameSessionKey]!.Trim();
		};

		var charname = "";
		var pass = "";

		if (context.Request.Method == "POST")
		{
			if (context.Request.Form.Any(x => x.Key == "charname"))
			{
				charname = context.Request.Form["charname"].FirstOrDefault();
			}
			if (context.Request.Form.Any(x => x.Key == "pass"))
			{
				pass = context.Request.Form["pass"].FirstOrDefault() ?? string.Empty;
			}
		}

		if (!string.IsNullOrEmpty(charname))
		{
			context.Session.SetString(playerNameSessionKey, charname);
			context.Response.Cookies.Append(playerNameSessionKey, charname);
		}

		charname = context.Session.GetString(playerNameSessionKey);

		if (!string.IsNullOrEmpty(charname))
		{
			// var mechanics = Mechanics.Get();
			var mechanics = new Mechanics();
			
			mechanics.terminal.mechanics = mechanics;

			mechanics.Cleanup();
			
			// var localPlayer = mechanics.GetSetPlayer(charname, pass);

			// localPlayer.lastAction = DateTime.Now;
			
			if (context.Request.Path.HasValue)
			{
				// if (context.Request.Path.StartsWithSegments("s") == "/o/websocket")
				if (context.Request.Path.StartsWithSegments("/o"))
				{
					if (context.WebSockets.IsWebSocketRequest)
					{
						// context.Response.Headers.Add("proxy_http_version", "1.1");
						// context.Response.Headers.Add("proxy_set_header", new string[2]{"Upgrade \"websocket\"", "Connection \"upgrade\""});
						// context.Response.Headers.Add("accept-encoding", "gzip;q=0,deflate,sdch");
						// context.Request.Headers.Add("proxy_http_version", "1.1");
						// context.Request.Headers.Add("proxy_set_header", new string[2]{"Upgrade \"websocket\"", "Connection \"upgrade\""});
						// context.Request.Headers["accept-encoding"] = "gzip;q=0,deflate,sdch";
						
						var socket = await context.WebSockets.AcceptWebSocketAsync();
						
						var parts = context.Request.Path.Value.Split('/');
						var project = parts.Last();
						
						var newType = System.Type.GetType($"Handlers.{project}", false, true);
						
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
						
						// await SocketHandler(localPlayer);
					}
				}
			}

			if (context.WebSockets.IsWebSocketRequest)
				return;
			
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
		else
		{
			await context.Response.WriteAsync($@"<!DOCTYPE html>
												<head>
												<title>cx3web</title>
												<link rel=""shortcut icon"" sizes=""256x256"" href=""/fav.png"" />
												<meta name=""msapplication-TileColor"" content=""#000000""/>
												<meta name=""theme-color"" content=""#000000"">
												<meta name=""viewport"" content=""width=device-width,initial-scale=1,maximum-scale=1"">
												<link media=""all"" rel=""stylesheet"" href=""/main.css"" type=""text/css"">
												</head>
												<body data-input=""def"">
												<form class=""form"" method=""post"" action="""">
												<input class=""input"" autofocus name=""charname"" min=""3"" max=""8"" value=""{TryGetCookie.Invoke(playerNameSessionKey)}"" placeholder=""charname""/>
												<input class=""submit"" type=""submit"" value=""enter""/></form>
												</body>
												");
		}
	}
}

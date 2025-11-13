using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using cx3web;
using cx3web.WorldObjects;

public partial class TerminalConsole
{
	public Mechanics mechanics;
	List<Output> ConsoleOutput = new List<Output>();
	public void RunCommand(GameCharacter localPlayer, string text)
	{
		// var success = mechanics.navigation.TryToAct(localPlayer, text);
		
		// if (!success)
		// 	mechanics.logger.Log(localPlayer, text);
		
		// if (localPlayer.diceRoll.rolling)
		// 	return;

		// localPlayer.parser.ClearSelection();
	}
	public const string inputCursor = "> ";
	public const string delimeter = "********************";
	public const int maxlines = 150;
	public void PrintDelimeter()
	{
		// PrintGlobal(delimeter.AsCommand());
	}
	private string lastPrintedTime = "";
	public async Task PrintGlobal(string text)
	{
		await mechanics.terminal.Print(new Output(text, OutputType.Global));
	}
	public async Task Print(Output output)
	{
		if (output.type == OutputType.Global) {
			foreach (var player in mechanics.players)
			{
				if (player.socket == null)
					continue;
				
				await player.socket.SendOutput(output.text, player == output.actor);
			}
			return;
		}

		if (output.type != OutputType.OtherOnly)
			output.actor.socket?.SendOutput(output.text);
		
		if (output.type == OutputType.OwnOnly)
			return;
		
		// foreach (var player in mechanics.world.GetRoommatesOf(output.actor))
		// {
		// 	player.socket?.SendOutput(output.text, false);
		// }

		// ConsoleOutput.Add(output);

		// if (ConsoleOutput.Count >= maxlines)
		// 	ConsoleOutput.RemoveAt(0);
	}
	public string GetOutput(GameCharacter localPlayer)
	{
		var text = new StringBuilder();
		
		foreach (var item in ConsoleOutput)
		{
			if (
				item.type == OutputType.Global ||
				// (item.type == OutputType.Public && item.container == (localPlayer != null ? localPlayer.Container : null)) ||
				(item.type == OutputType.OtherOnly && item.actor != localPlayer) ||
				(item.type == OutputType.OwnOnly && item.actor == localPlayer)
			)
			{
				text.Append($"<log>{item.text}</log>");
			}
		}
		
		return $"<div class=\"scrollhelper\">{text}</div>";
	}
}
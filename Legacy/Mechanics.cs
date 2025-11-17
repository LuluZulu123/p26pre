using cx3web.WorldObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace cx3web
{
	public class Mechanics
	{
		// public Actions.Verb verbs { get; private set; }
		// public GameTime gameTime { get; private set; }
		public List<GameCharacter> players = new List<GameCharacter>();
		// public List<GameCharacter> savedPlayers = new List<GameCharacter>();
		// public Logger logger { get; private set; }
		// public Navigation navigation { get; private set; }
		public TerminalConsole terminal { get; private set; } = new TerminalConsole();
		// public World world { get; private set; }
		// public static Mechanics _singleton;
		// public WorldSaveModel saveModel = new WorldSaveModel();
		// private void FillSaveModel () {
		// 	foreach (var obj in world.Objects)
		// 	{
		// 		var npc = obj as GameCharacter;
				
		// 		if (npc != null) {
		// 			saveModel.objects.Add((PlayerModel)npc);
					
		// 			if (!npc.IsPlayerControlled)
		// 				continue;
					
		// 			if (saveModel.players.Contains(npc))
		// 				saveModel.players.Remove(npc);
					
		// 			saveModel.players.Add(npc);
		// 		} else {
		// 			if (obj.Container == obj.OriginalContainer)
		// 				continue;
					
		// 			if (saveModel.objects.Contains(obj))
		// 				saveModel.objects.Remove(obj);
					
		// 			saveModel.objects.Add(obj);
		// 		}
		// 	}
		// }
		// public void SaveToFile()
		// {
		// 	var formatter = new BinaryFormatter();
		// 	var memoryStream = new MemoryStream();
		// 	FillSaveModel();
		// 	formatter.Serialize(memoryStream, saveModel);
		// 	using (FileStream file = new FileStream(GetSaveFilePath(), FileMode.Create, FileAccess.Write))
		// 	{
		// 		memoryStream.Seek(0, SeekOrigin.Begin);
		// 		memoryStream.CopyTo(file);
		// 	}
		// }
		// private static string GetSaveFilePath (int saveFile = 0) {
		// 	return $"save{saveFile}.bin";
		// }
		// private static string GetPlayerSaveFilePath (string playerName) {
		// 	return $"player{playerName}.bin";
		// }
		// private static Mechanics LoadFromFile()
		// {
		// 	if (!File.Exists(GetSaveFilePath()))
		// 		return null;
			
		// 	var loadedMech = new Mechanics();

		// 	var formatter = new BinaryFormatter();

		// 	using (FileStream file = new FileStream(GetSaveFilePath(), FileMode.Open, FileAccess.Read))
		// 	{
		// 		if (file.Length == 0)
		// 			return null;

		// 		loadedMech.saveModel = (WorldSaveModel)formatter.Deserialize(file);
		// 	}

		// 	foreach (var obj in loadedMech.saveModel.objects)
		// 	{
		// 		// var wo = loadedMech.world.Objects.FirstOrDefault(x => x.Key == obj.Key);
		// 		var wo = loadedMech.world.GetObject(obj.Key);

		// 		if (wo == null)
		// 			continue;
				
		// 		wo.saveModel = obj;
				
		// 		// var woC = loadedMech.world.Objects.FirstOrDefault(x => x.Key == obj.ContainerKey);
		// 		var woC = loadedMech.world.GetObject(obj.ContainerKey);
		// 		wo.Container = woC;

		// 		var npc = wo as GameCharacter;
		// 		if (npc != null) {
		// 			var playerModel = (PlayerModel)obj;
		// 			npc.alignments = playerModel.alignments;
		// 			npc.stats = playerModel.stats;
		// 			npc.stats.player = npc;
		// 			npc.SetImpressions(playerModel.impressions);
		// 		}
		// 	}

		// 	return loadedMech;
		// }
		// public static Mechanics Get()
		// {
		// 	if (_singleton == null)
		// 		_singleton = LoadFromFile();

		// 	if (_singleton == null)
		// 		_singleton = new Mechanics();

		// 	return _singleton;
		// }
		// public Mechanics()
		// {
		// 	verbs = new Actions.Verb();
		// 	gameTime = new GameTime();
		// 	logger = new Logger();
		// 	navigation = new Navigation();
		// 	terminal = new TerminalConsole();
		// 	// world = new Worlds.Demo();
		// 	world = new World();

		// 	verbs.Setup(this);
		// 	gameTime.Setup(this);
		// 	terminal.Setup(this);
		// 	logger.Setup(this);
		// 	navigation.Setup(this);
		// 	world.Setup(this);
			
		// 	logger.Log("mech", "constructor");
		// }
		public void HandleInput(GameCharacter localPlayer, string text)
		{
			// localPlayer.parser.CheckInput(text);
			// terminal.RunCommand(localPlayer, text);
			
			// // hack
			// cmds++;
			// if (cmds < 5)
			// 	return;

			// SavePlayer(localPlayer);
			// SaveToFile();
			// cmds = 0;
		}
		// private int cmds = 0;
		// public void SavePlayer(GameCharacter localPlayer)
		// {
		// 	var formatter = new BinaryFormatter();
		// 	var memoryStream = new MemoryStream();
		// 	formatter.Serialize(memoryStream, (PlayerModel)localPlayer);
		// 	using (FileStream file = new FileStream(GetPlayerSaveFilePath(localPlayer), FileMode.Create, FileAccess.Write))
		// 	{
		// 		memoryStream.Seek(0, SeekOrigin.Begin);
		// 		memoryStream.CopyTo(file);
		// 	}
		// }
		// private GameCharacter TryLoadPlayer (string name, string pass) {
		// 	var filePath = GetPlayerSaveFilePath(name);

		// 	if (!File.Exists(filePath))
		// 		return null;

		// 	PlayerModel loadData;
		// 	var loadedPlayer = new GameCharacter();
		// 	loadedPlayer.Setup(this);

		// 	var formatter = new BinaryFormatter();

		// 	using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
		// 	{
		// 		if (file.Length == 0)
		// 			return null;

		// 		loadData = (PlayerModel)formatter.Deserialize(file);
		// 	}
			
		// 	loadedPlayer.saveModel = loadData;
			
		// 	// if (loadedPlayer.GetSaveString("pass") != pass)
		// 	// 	return null;
			
		// 	// loadedPlayer.SetSaveData("pass", pass);
			
		// 	loadedPlayer.Name = name;
		// 	loadedPlayer.IsPlayerControlled = true;
		// 	loadedPlayer.stats = loadData.stats;
		// 	loadedPlayer.stats.player = loadedPlayer;
		// 	// loadedPlayer.Container = world.Objects.FirstOrDefault(x => x.Key == loadData.ContainerKey);
		// 	loadedPlayer.Container = world.GetObject(loadData.ContainerKey);
		// 	players.Add(loadedPlayer);

		// 	world.Objects.Add(loadedPlayer);

		// 	return loadedPlayer;
		// }
		public GameCharacter GetSetPlayer(string name, string pass = "", string preset = "")
		{
			return SetNewPlayer(name, preset);
			// name = name.Replace(" ", "");
			// name = name.Replace("/", "");
			// name = name.Replace(".", "");
			// name = name.Replace("?", "");
			// name = name.Replace("\\", "");
			// name = name.Replace("$", "");
			// name = name.Replace("§", "");
			// name = name.Replace("\"", "");
			// name = name.Substring(0, Math.Min(name.Length, 10));

			// var existingPlayer = players.FirstOrDefault(x => x.Name == name);

			// if (existingPlayer == null)
			// 	existingPlayer = TryLoadPlayer(name, pass);

			// if (existingPlayer == null)
			// 	existingPlayer = SetNewPlayer(name, preset);

			// return existingPlayer;
		}
		public GameCharacter SetNewPlayer(string name, string preset)
		{
			var player = new GameCharacter();
			// player.mechanics = this;
			// player.Setup(this);
			// player.LoadPreset(preset);
			// player.Name = name;
			player.IsPlayerControlled = true;
			// player.Container = world.start;
			players.Add(player);
			// world.Objects.Add(player);
			
			return player;
		}
		// public void Restart()
		// {
		// 	_singleton = new Mechanics();
		// 	_singleton.SaveToFile();
		// 	context.Response.Redirect("", false);
		// }
		// private Dictionary<GameCharacter, string> playersToCleanUp = new Dictionary<GameCharacter, string>();
		// private const double connectionTimeout = 5d;
		public void Cleanup () {
			// foreach (var item in players)
			// {
			// 	var diffInSeconds = Math.Abs((item.lastAction - DateTime.Now).TotalSeconds);

			// 	if (diffInSeconds < connectionTimeout)
			// 		continue;
				
			// 	if (playersToCleanUp.ContainsKey(item))
			// 		continue;
				
			// 	playersToCleanUp.Add(item, item);
			// }
			// foreach (var item in playersToCleanUp)
			// {
			// 	terminal.PrintGlobal($"{item.Value} disconnected");
			// 	world.Objects.Remove(item.Key);
			// 	players.Remove(item.Key);
			// }
			// playersToCleanUp.Clear();
		}
	}
}

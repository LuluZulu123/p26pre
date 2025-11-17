using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace cx3web.WorldObjects
{
	public class WorldObject 
	{}
}
namespace cx3web.WorldObjects {
	public class FixedObject : WorldObject {
		// public override bool canBePickedUp => false;
	}
}
namespace cx3web.WorldObjects
{
	public partial class GameCharacter : FixedObject
	{
		private const int AttackCooldown = 60;
		private const int DefenseCooldown = 60;
		// public InputModes inputMode = InputModes.CommandMode;
		// public Parser parser { get; private set; }
		// public DiceRoll diceRoll { get; private set; }
		public bool IsPlayerControlled = false;
		public DateTime lastAction = DateTime.Now;
		private DateTime lastAttack = DateTime.Now;
		public int attackerCooldown => (int)Math.Max((lastAttack - DateTime.Now).TotalSeconds, 0);
		public DateTime lastDefense = DateTime.Now;
		public int defenseCooldown => (int)Math.Max((lastDefense - DateTime.Now).TotalSeconds, 0);
		// public ActionScoping actionScoping = ActionScoping.GlobalAndDefault;
		// private Weapon equippedWeapon;
		// public List<ScopedAction> scopedActions = new List<ScopedAction>();
		// public List<Spell> spells = new List<Spell>();
		// public CharacterStats stats = new CharacterStats();
		// public CharacterAlignments alignments = new CharacterAlignments();
		// private Dictionary<GameCharacter, CharacterAlignments> impressions = new Dictionary<GameCharacter, CharacterAlignments>();
		public bool Looted = false;
		// public bool Alive {
		// 	get {
		// 		if (stats == null)
		// 			return false;

		// 		return stats.Hp > 0;
		// 	}
		// }
		// public float GetAccordance (GameCharacter other) {
		// 	return GetImpression(other).GetAccordance(alignments);
		// }
		// public Weapon EquippedWeapon
		// {
		// 	get
		// 	{
		// 		if (equippedWeapon == null)
		// 			// equippedWeapon = mechanics.world.Objects.FirstOrDefault(x => x.Key == GetSaveString("EquippedWeapon")) as Weapon;
		// 			equippedWeapon = mechanics.world.GetObject(GetSaveString("EquippedWeapon")) as Weapon;
				
		// 		if (equippedWeapon == null)
		// 			equippedWeapon = Weapon.Unarmed;

		// 		return equippedWeapon;
		// 	}
		// 	set
		// 	{
		// 		equippedWeapon = value;
		// 		SetSaveString("EquippedWeapon", value.Key);
		// 		PushStatsWeapon();
		// 	}
		// }
		public float turnProgression;
		// public override void Setup(Mechanics mechanics)
		// {
		// 	base.Setup(mechanics);
			
		// 	stats.Setup(mechanics);
		// 	stats.player = this;
			
		// 	diceRoll = new DiceRoll();
		// 	diceRoll.Setup(mechanics);
		// 	diceRoll.player = this;
			
		// 	parser = new Parser();
		// 	parser.Setup(mechanics);
		// 	parser.player = this;
		// }
		public void SetAttackerCooldown () {
			lastAttack = DateTime.Now;
		}
		public void SetDefenseCooldown () {
			lastDefense = DateTime.Now;
		}
		// public void EndTurn(
		// 	float progress = World.turnProgressionLimit,
		// 	float impression = 0,
		// 	int notability = 10,
		// 	float rolePlayRating = 0
		// )
		// {
        //     var mod = parser.selectedModifier;
            
        //     if (mod != null) {
        //         progress *= mod.timeModifier;
        //         impression += mod.alignmentExtra;
        //         notability += mod.notabilityExtra;
        //     }
            
        //     if (impression != 0)
    	// 		MakeImpression(impression, notability);
			
        //     turnProgression += progress;
			
		// 	System.Console.WriteLine($"EndTurn for {this} (turnProgression={turnProgression}) (progress={progress})");
			
		// 	stats.RolePlayRating += rolePlayRating;
			
		// 	if (IsPlayerControlled && Room != null)
		// 		Room.progression += progress;
			
		// 	mechanics.world.CheckTurnEnd(this);
		// }
		// save
		// public static implicit operator PlayerModel (GameCharacter npc) {
		// 	var wo = npc as WorldObject;
		// 	var wom = (WorldObjectModel)wo;
		// 	var save = new PlayerModel();
			
		// 	if (npc == null)
		// 		return save;
			
		// 	if (npc.Container != null)
		// 		save.ContainerKey = npc.Container.Key;
			
		// 	save.name = npc.Name;
		// 	save.alignments = npc.alignments;
		// 	save.stats = npc.stats;
		// 	save.impressions = npc.GetImpressions();
			
		// 	return save;
		// }
		// public void PrintNewAlignmentInfo (GameCharacter other) {
		// 	var impr = GetImpression(other);

		// 	var newAcc = impr.GetAccordance(alignments);

		// 	if (newAcc > .8f) {
		// 		other.PrintOwn($"{this} loves {other}");
		// 	} else if (newAcc > .6f) {
		// 		other.PrintOwn($"{this} likes {other}");
		// 	} else if (newAcc > .3f) {
		// 		other.PrintOwn($"{this} dislikes {other}");
		// 	} else {
		// 		other.PrintOwn($"{this} hates {other}");
		// 	}
		// }
// 		public void SetImpression(GameCharacter other, float goodEvil, float offset = 0)
// 		{
// 			if (!Alive)
// 				return;
			
// 			if (IsPlayerControlled)
// 				return;

// 			var impr = GetImpression(other);

// 			var oldAcc = impr.GetAccordance(alignments);

// 			impr.GoodEvil += goodEvil;
// 			impr.PersonalOffset += offset;

// 			var newAcc = impr.GetAccordance(alignments);
			
// 			if (newAcc - oldAcc > .4f) {
// 				other.PrintOwn($"{this} loved this");
// 			} else if (newAcc - oldAcc > .2f) {
// 				other.PrintOwn($"{this} liked this");
// 			} else if (newAcc - oldAcc < -.2f) {
// 				other.PrintOwn($"{this} disliked this");
// 			} else if (newAcc - oldAcc < -.4f) {
// 				other.PrintOwn($"{this} hated this");
// 			}
// 		}
// 		public void MakeImpression(float goodEvil, int notability)
// 		{
// 			foreach (var npc in mechanics.world.GetRoommatesOf(this))
// 			{
// 				if (!npc.Alive)
// 					continue;
                
// 				if (notability < npc.stats.Get(StatTypes.Awareness))
// 					continue;

// 				npc.SetImpression(this, goodEvil);
// 			}
// 		}
// 		public void SetImpressions (Dictionary<string, AlignmentModel> impressions) {
// 			foreach (var imp in impressions)
// 			{
// 				// var npc = mechanics.world.Objects.FirstOrDefault(x => x.Name == imp.Key) as GameCharacter;
// 				var npc = mechanics.world.GetObject(imp.Key) as GameCharacter;

// 				if (npc == null)
// 					continue;

// 				this.impressions.Add(npc, imp.Value);
// 			}
// 		}
// 		public Dictionary<string, AlignmentModel> GetImpressions() {
// 			var data = new Dictionary<string, AlignmentModel>();
// 			foreach (var imp in impressions)
// 			{
// 				if (data.ContainsKey(imp.Key.Name))
// 					continue;
				
// 				data.Add(imp.Key.Name, imp.Value);
// 			}
// 			return data;
// 		}
// 		public CharacterAlignments GetImpression(GameCharacter other)
// 		{
// 			if (!impressions.ContainsKey(other)) {
// 				impressions.Add(other, new CharacterAlignments());
// 				impressions[other].GoodEvil = other.alignments.GoodEvil;
// 			}

// 			return impressions[other];
// 		}
// 		public void AddSpell (Spell newSpell) {
// 			if (spells.Any(x => x.Name == newSpell.Name))
// 				return;

// 			spells.Add(newSpell);

// 			Print($"{this} learned {newSpell}.");
// 		}
// 		public void EnterOrLook () {
// 			Room?.OnEnter(this);
			
// 			if (Room != null)
// 				return;
			
// 			mechanics.verbs.look.TryInvoke(this);
// 		}
// 		private ProgressAction progressAction;
// 		public void SetProgressAction (string text, int seconds, Action<GameCharacter> ready) {
// 			var action = new ProgressAction();
			
// 			action.text = text;
// 			action.ready = ready;
// 			action.totalSeconds = seconds;
			
// 			SetProgressAction(action);
// 		}
// 		public void SetProgressAction (ProgressAction action) {
// 			progressAction = action;
// 			progressAction.startTime = DateTime.Now;
// 			progressAction.start?.Invoke(this);
// 			PushProgressAction(action);
// 		}
// 		private void ProgressActionEnd () {
// 			progressAction.ready?.Invoke(this);
// 			progressAction = null;
// 			PushDefaultInput();
// 		}
// 		public bool CheckProgressAction (string input) {
// 			if (progressAction == null)
// 				return false;
			
// 			if (input == "abort" && progressAction.abortable) {
// 				progressAction.abort?.Invoke(this);
// 				progressAction = null;
// 				PushDefaultInput();
// 				return true;
// 			}
			
// 			if (input == "skip") {
// 				stats.RolePlayRating -= progressAction.skipPenalty;
// 				ProgressActionEnd();
// 				return true;
// 			}
			
// 			if (progressAction.restSeconds > 0) {
// 				progressAction.during?.Invoke(this);
// 			} else {
// 				ProgressActionEnd();
// 			}
			
// 			return true;
// 		}
// 		public bool CheckContinue (string input) {
// 			if (string.IsNullOrEmpty(input))
// 				return false;
			
// 			if (input != "c")
// 				return false;
			
// 			if (continues == null)
// 				return false;
			
// 			if (continues.Count == 0)
// 				return false;
			
// 			continues.Dequeue()?.Invoke(this);
			
// 			if (continues.Count == 0 && !diceRoll.rolling)
// 				PushDefaultInput();
			
// 			return true;
// 		}
// 		public void Continue (params Action<GameCharacter>[] then) {
// 			Continue("", then);
// 		}
// 		public void Continue (string text, params Action<GameCharacter>[] then) {
// 			if (continues == null)
// 				continues = new Queue<Action<GameCharacter>>();
			
// 			foreach (var action in then)
// 			{
// 				continues.Enqueue(action);
// 			}
			
// 			PushContinue(text);
// 			// then.Invoke(this);
// 		}
// 		public void SetNewLocation (WorldObject newContainer) {
// 			Console.WriteLine($"new loc for {this} -> {newContainer}");
// 			Container = newContainer;
// 			EnterOrLook();
// 		}
// 		public void ResetToDefaultInput () {
// 			if (!IsPlayerControlled)
// 				return;
			
// 			actionScoping = ActionScoping.GlobalAndDefault;
// 			PushDefaultInput();
// 			scopedActions.Clear();
// 			EnterOrLook();
// 		}
// 		public void SetScopedActions (string intro, params ScopedAction[] options) {
// 			SetScopedActions (intro, null, options);
// 		}
// 		public void SetScopedActions (string intro, GameCharacter target, params ScopedAction[] options) {
// 			scopedTarget = target;
// 			scopedActions = options.ToList();
// 			actionScoping = ActionScoping.ScopedAndGlobal;
// 			PrintOwn(intro);
// 			PrintOwn(string.Join(", ", options.Select(x => x.Description)));
// 		}
// 		#region actions
// 		public virtual string GetDamageText () {
// 			var restHpFraction = stats.HpFractionUnclamped;

// 			if (restHpFraction == 1f)
// 				return RandomString(
// 					$"{this} is fine"
// 				);

// 			if (restHpFraction > .9f)
// 				return RandomString(
// 					$"{this} seems still quite fine"
// 				);

// 			if (restHpFraction > .8f)
// 				return RandomString(
// 					$"{this} has got some scratches"
// 				);

// 			if (restHpFraction > .5f)
// 				return RandomString(
// 					$"{this} is quite hurt"
// 				);

// 			if (restHpFraction > .3f)
// 				return RandomString(
// 					$"{this} choughs up a lot of blood"
// 				);

// 			if (restHpFraction > .1f)
// 				return RandomString(
// 					$"{this} struggles to stay alive",
// 					$"{this} is as good as dead"
// 				);

// 			if (restHpFraction > -.1f)
// 				return RandomString(
// 					$"{this} died",
// 					$"{this} falls to the floor"
// 				);

// 			if (restHpFraction > -.2f)
// 				return RandomString(
// 					$"{this} died instantly"
// 				);

// 			return RandomString(
// 				$"{this} is super dead",
// 				$"{this} splatters into pieces"
// 			);
// 		}
// 		public void DoDamage (GameCharacter other, int amount) {
// 			stats.Hp -= amount;
			
// 			CheckDamageDone(other, amount);
// 		}
// 		private void CheckDamageDone (GameCharacter other, int amount) {
// 			if (amount > 0) {
// 				Print($"{other} deals {amount} damage to {this}.");
// 			} else {
// 				Print($"{other} misses.");
// 			}
			
// 			if (!Alive && IsPlayerControlled) {
// 				var respawn = new ProgressAction();
// 				respawn.text = "respawn";
// 				respawn.abortable = false;
// 				respawn.totalSeconds = 90;
// 				respawn.ready = (x) => {
// 					x.stats.Hp = x.stats.MaxHp;
// 					x.PrintPublic($"{x} revived");
// 				};
// 				SetProgressAction(respawn);
// 			}
			
// 			if (amount > 0 && !Alive)
// 				other.stats.Xp += stats.GearScore * 10;

// 			Print(GetDamageText());

// 			SetImpression(other, 0, -(float)amount / (float)stats.MaxHp * 3f);
// 		}
// 		public override void ActionAttack(GameCharacter other, AttackData data, int diceRoll)
// 		{
// 			var damageMultiplier = (int)Math.Floor((float)diceRoll / stats.Defense);

// 		   	var amount = damageMultiplier * data.GetDamage(other);

// 			other.PrintOwn($"{diceRoll} / {stats.Defense} is {damageMultiplier}. (DMG = {data.GetDamage(other)})");

// 			base.ActionAttack(other, data, amount);
			
// 			DoDamage(other, amount);
// 		}
// 		public void ActionGive(GameCharacter other, WorldObject newObj)
// 		{
// 			GetImpression(other).PersonalOffset += (newObj.value / (stats.Gold == 0 ? .3f : stats.Gold));

// 			newObj.Container = this;

// 			PrintPublic($"{other} gave {newObj} to {this}");
// 		}
// 		public override void ActionLook(GameCharacter localPlayer)
// 		{
// 			base.ActionLook(localPlayer);
// 			PrintNewAlignmentInfo(localPlayer);
// 			localPlayer.PrintOwn(GetDamageText());
// 		}
// 		#endregion
// 		// sockets
// 		public void PushDiceRoll (string jsArray) {
// 			socket?.SendDiceData(jsArray);
// 		}
// 		public void PushContinue (string text) {
// 			if (string.IsNullOrEmpty(text))
// 				text = "Continue";
			
// 			socket?.SendContinue(text);
// 		}
// 		public void PushDefaultInput () {
// 			socket?.SendDefaultInput();
// 		}
// 		private async Task UpdateProgress (ProgressAction action)
// 		{
// 			while (progressAction != null && action.restSeconds > 0) {
			
// 				socket?.SendProgressAction(
// 					action.text,
// 					action.restSeconds,
// 					action.skipPenalty,
// 					action.abortable
// 				);
				
// 				await Task.Delay(750);
// 			}
			
// 			CheckProgressAction("");
// 		}
// 		public void PushProgressAction (ProgressAction action) {
// 			if (action == null)
// 				return;
			
// 			UpdateProgress(action);
// 		}
// 		public void PushStatsAll () {
// 			PushStatsMain();
// 			PushStatsWeapon();
// 			PushStatsAttr();
// 		}
// 		public void PushStatsMain () {
// 			var rp = stats.RolePlayRating.ToString("0.00");
// 			socket?.SendStatsMain($@"
// GOLD {stats.Gold}
// HP {stats.Hp} / {stats.MaxHp}
// XP {stats.Xp} %
// SP {stats.UnusedPoints}
// RP {rp}");
// 		}
// 		public void PushStatsWeapon () {
// 			socket?.SendStatsWeapon($@"
// Equipped: {EquippedWeapon.Name}
// ROLL {EquippedWeapon.GetRollText()}
// DMG: {EquippedWeapon.GetDamageText()}");
// 		}
// 		public void PushStatsAttr () {
// 			socket?.SendStatsAttr(stats.GetUiText());
// 		}
		// public void PushUnlock () {
		// 	socket?.Send("u|-");
		// }
// 		public void PrintPublic(string text)
// 		{
// 			mechanics.terminal.Print(new Output(text, OutputType.Public, this, this.Container));
// 		}
// 		public void PrintOthers(string text)
// 		{
// 			mechanics.terminal.Print(new Output(text, OutputType.OtherOnly, this, this.Container));
// 		}
		public void PrintOwn(string text)
		{
			// mechanics.terminal.Print(new Output(text, OutputType.OwnOnly, this, this.Container));
		}
// 		public void PrintError(string text)
// 		{
// 			PrintOwn(text.AsError());
// 		}
// 		public void PrintPvpLockErrorActive()
// 		{
// 			PrintOwn($"you are locekd for battle, restore or movement for the next {attackerCooldown} seconds, or until you get next attacked.".AsError());
// 		}
// 		public void PrintPvpLockErrorPassive(GameCharacter target)
// 		{
// 			PrintOwn($"{target} can't be hit for the next {target.defenseCooldown} seconds, or until he next attackes.".AsError());
// 		}
// 		public void PrintWarning(string text)
// 		{
// 			PrintOwn(text.AsWarning());
// 		}
// 		public void Print(string text)
// 		{
// 			PrintPublic(text);
// 		}
// 		public void Confirm (string text, Action<GameCharacter> confirmAction, Action<GameCharacter> declineAction = null) {
// 			var options = new List<ScopedAction>();
// 			var agree = ScopedAction.agree;
// 			var decline = ScopedAction.decline;
			
// 			if (declineAction == null) {
// 				declineAction = (actor) => {
// 					actor.actionScoping = ActionScoping.GlobalAndDefault;
// 					actor.scopedActions.Clear();
// 				};
// 			}
			
// 			agree.doAction = confirmAction;
// 			decline.doAction = declineAction;
			
// 			options.Add(agree);
// 			options.Add(decline);
			
// 			PrintOwn($"{text} ({agree.Description}/{decline.Description})");
// 			actionScoping = ActionScoping.ScopedOnly;
// 			scopedActions = options;
// 		}
// 		// AI
// 		public void Act () {
// 			System.Console.WriteLine($"Act for {this}");
// 			if (IsPlayerControlled)
// 				return;
			
// 			foreach (var other in mechanics.world.GetRoommatesOf(this).Where(x => x.Alive))
// 			{
// 				if (GetAccordance(other) > (stats.Chill - CharacterAlignments.AttackThreshold))
// 					continue;

// 				if (GetAccordance(other) < (stats.Confidence - CharacterAlignments.AttackThreshold))
// 					continue;

// 				mechanics.terminal.PrintDelimeter();

// 				mechanics.terminal.PrintGlobal($"{this} tries to attack {other}");
				
// 				other.ActionAttack(this, EquippedWeapon, other.diceRoll.InstaRoll(stats.Get(EquippedWeapon.rollType)));
				
// 				EndTurn(progress: 1f, impression: -.15f);
// 			}
			
// 			EndTurn();
// 		}
	}
}
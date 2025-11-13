using cx3web.WorldObjects;

public class Output {
    public string text;
    public GameCharacter actor;
    public OutputType type;
    public WorldObject container;

	public Output(string text, OutputType type, GameCharacter actor = null, WorldObject container = null)
	{
		this.text = text;
		this.type = type;
		this.actor = actor;
		this.container = container;
	}
}

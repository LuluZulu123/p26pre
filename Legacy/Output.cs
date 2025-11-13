using cx3web.WorldObjects;

public class Output {
    public string text;
    public SocketHandler actor;
    public OutputType type;

	public Output(string text, OutputType type, SocketHandler actor)
	{
		this.text = text;
		this.type = type;
		this.actor = actor;
	}
}

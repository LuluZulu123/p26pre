using Microsoft.AspNetCore;

public class Program
{
	public static readonly bool IsDev = false;
	public static void Main(string[] args)
	{
		if (IsDev) {
			var fake = new Handlers.VA6();
			fake.FakeInput("u").GetAwaiter().GetResult();
			fake.FakeInput("p|1").GetAwaiter().GetResult();
			fake.FakeInput("u").GetAwaiter().GetResult();
			fake.FakeInput("p|2").GetAwaiter().GetResult();
			fake.FakeInput("u").GetAwaiter().GetResult();
		} else
		{
			BuildWebHost(args).Run();
		}
	}

	public static IWebHost BuildWebHost(string[] args) =>
		WebHost.CreateDefaultBuilder(args)
			.UseStartup<Startup>()
			.ConfigureServices(services =>
			{
				services.AddSession();
				services.AddDistributedMemoryCache();
			})
			.Build();
}
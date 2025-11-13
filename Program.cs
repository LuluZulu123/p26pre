using Microsoft.AspNetCore;

public class Program
{
	public static void Main(string[] args)
	{
		BuildWebHost(args).Run();
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
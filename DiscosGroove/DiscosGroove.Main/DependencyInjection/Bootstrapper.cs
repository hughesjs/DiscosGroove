using System;
using System.Collections.Generic;
using DiscosGroove.Core.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiscosGroove.Main.DependencyInjection;

public static class Bootstrapper
{
	public static ServiceProvider Up()
	{
		// TODO - Work out how I'm going to do this properly - using env vars is a bit shit... First time setup maybe?
		IConfigurationBuilder builder = new ConfigurationBuilder()
			.AddInMemoryCollection(new Dictionary<string, string>
			{
				{ "DiscosOptions:DiscosApiKey", Environment.GetEnvironmentVariable("DiscosApiKey") ?? throw new Exception("Need to set DiscosApiUrl env var") },
				{ "DiscosOptions:DiscosApiUrl", Environment.GetEnvironmentVariable("DiscosApiUrl") ?? "https://discosweb.esoc.esa.int/api/"}
			});

		IConfigurationRoot? config = builder.Build();
			

		IServiceCollection services = new ServiceCollection()
			.AddServices(config)
			.AddViewModels()
			.AddViews();

		return services.BuildServiceProvider();
	}
}
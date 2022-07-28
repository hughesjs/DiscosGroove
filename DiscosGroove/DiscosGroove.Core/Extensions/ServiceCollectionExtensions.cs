using DiscosGroove.Core.Services;
using DiscosWebSdk.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiscosGroove.Core.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
	{
		services.AddDiscosServices(config);
		services.AddTransient<IDiscosService, DiscosService>();

		return services;
	}
}
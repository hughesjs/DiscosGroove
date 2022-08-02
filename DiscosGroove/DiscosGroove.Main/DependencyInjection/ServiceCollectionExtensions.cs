using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using DiscosGroove.Main.ViewModels;
using DiscosWebSdk.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiscosGroove.Main.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddViews(this IServiceCollection services)
	{
		IEnumerable<Type> windows = typeof(App).Assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(Window)));

		foreach (Type window in windows)
		{
			services.AddTransient(window);
		}

		return services;
	}

	public static IServiceCollection AddViewModels(this IServiceCollection services)
	{
		IEnumerable<Type> windows = typeof(App).Assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ViewModelBase)));

		foreach (Type window in windows)
		{
			services.AddTransient(window);
		}

		return services;
	}

	public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
	{
		services.AddDiscosServices(config, true);

		return services;
	}
}

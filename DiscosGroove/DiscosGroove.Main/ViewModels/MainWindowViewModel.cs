using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reactive;
using System.Text.Json;
using System.Threading.Tasks;
using System.Timers;
using DiscosGroove.Main.Wrappers;
using DiscosWebSdk.Interfaces.BulkFetching;
using DiscosWebSdk.Models.ResponseModels.DiscosObjects;
using ReactiveUI;

namespace DiscosGroove.Main.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
	private readonly IBulkFetchService<DiscosObject> _bulkFetchService;

	private Task? _loadTask;

	private const string CachePath = "/home/james/tmp/discos.cache"; //TODO - Put this in a setting

	public ReactiveCommand<Unit, Unit> LoadFromDiscosCommand  { get; }
	public ReactiveCommand<Unit, Unit> LoadFromCacheCommand   { get; }
	public ReactiveCommand<Unit, Unit> SaveObjectGraphCommand { get; }
	public ReactiveCommand<Unit, Unit> PlotHistogramCommand   { get; }

	public ObservableSetWrapper<IReadOnlyCollection<DiscosObject>> DiscosObjects { get; } = new();

	public ObservableSetWrapper<string> Status { get; } = new();
	public ObservableSetWrapper<int>    Total  { get; } = new();
	public ObservableSetWrapper<int>    Loaded { get; } = new();

	public MainWindowViewModel(IBulkFetchService<DiscosObject> bulkFetchService)
	{
		Status.Value           = "Idle";
		Total.Value            = 0;
		Loaded.Value           = 0;
		_bulkFetchService      = bulkFetchService;
		
		LoadFromDiscosCommand  = ReactiveCommand.CreateFromTask(LoadFromDiscos);
		LoadFromCacheCommand   = ReactiveCommand.CreateFromTask(LoadFromCache);
		SaveObjectGraphCommand = ReactiveCommand.CreateFromTask(SaveObjectGraph);
		PlotHistogramCommand   = ReactiveCommand.CreateFromTask(PlotHistogram);

		bulkFetchService.DownloadStatusChanged += (_, ds) =>
												  {
													  Loaded.Value = ds.Downloaded;
													  Total.Value  = ds.Total;
													  Console.WriteLine($"Downloading: {ds.Downloaded}/{ds.Total}");
												  };

		Timer watchDog = new();
		watchDog.Interval  =  1000;
		watchDog.AutoReset =  true;
		watchDog.Elapsed   += (_, _) => Console.WriteLine($"LoadTaskStatus: {_loadTask?.Status.ToString() ?? "null"}");
		watchDog.Start();
	}

	private async Task LoadFromDiscos()
	{
		Console.WriteLine("Load Discos");
		_loadTask = Task.Run(DoLoad);// Handle this better
		_loadTask.Start();
	}

	private async Task DoLoad()
	{
		Status.Value        = "Loading...";
		DiscosObjects.Value = await _bulkFetchService.GetAll();
		Status.Value        = "Idle...";
	}
	
	//TODO - These hardcoded paths are shit
	private async Task LoadFromCache()
	{
		Console.WriteLine("Load Cache");
		Status.Value = "Loading...";
		if (File.Exists(CachePath))
		{
			await using FileStream fStream = new(CachePath, FileMode.Open);
			DiscosObjects.Value = await JsonSerializer.DeserializeAsync<IReadOnlyList<DiscosObject>>(fStream);
		}

		Loaded.Value = DiscosObjects.Value?.Count ?? 0;
		Total.Value        = Loaded.Value;
		Status.Value = "Idle...";
	}
	
	private async Task SaveObjectGraph()
	{
		Console.WriteLine("Save Graph");
		Status.Value = "Saving...";
		await using FileStream fStream = new(CachePath, FileMode.Create);
		await JsonSerializer.SerializeAsync(fStream, DiscosObjects.Value);
		Status.Value = "Idle...";
	}
	
	private async Task PlotHistogram()
	{
		Console.WriteLine("Plot Histo");
	}
	
	
}

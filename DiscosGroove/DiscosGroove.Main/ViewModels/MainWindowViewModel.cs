using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using DiscosGroove.Core.Services;
using DiscosGroove.Main.Wrappers;
using DiscosWebSdk.Models.ResponseModels.DiscosObjects;
using ReactiveUI;

namespace DiscosGroove.Main.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IDiscosService _discosService;
    public ReactiveCommand<Unit, Unit> LoadObjectsCommand { get; }

    public ObservableSetWrapper<IReadOnlyCollection<DiscosObject>> DiscosObjects { get; } = new();

    public MainWindowViewModel(IDiscosService discosService)
    {
        _discosService = discosService;
        LoadObjectsCommand = ReactiveCommand.CreateFromTask(LoadObjects);
    }

    private async Task LoadObjects()
    {
        DiscosObjects.Value = await _discosService.GetDiscosObjects();
    }
}
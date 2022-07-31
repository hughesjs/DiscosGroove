using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using DiscosGroove.Main.Wrappers;
using DiscosWebSdk.Models.ResponseModels.DiscosObjects;
using ReactiveUI;

namespace DiscosGroove.Main.ViewModels;

public class MainWindowViewModel : ViewModelBase
{

    public ReactiveCommand<Unit, Unit> LoadObjectsCommand { get; }

    public ObservableSetWrapper<IReadOnlyCollection<DiscosObject>> DiscosObjects { get; } = new();

    public MainWindowViewModel()
    {
        LoadObjectsCommand = ReactiveCommand.CreateFromTask(LoadObjects);
    }

    private async Task LoadObjects()
    {
       
    }
}

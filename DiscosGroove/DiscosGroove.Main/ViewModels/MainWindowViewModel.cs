using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using DiscosGroove.Core.Services;
using DiscosWebSdk.Models.ResponseModels.DiscosObjects;
using ReactiveUI;

namespace DiscosGroove.Main.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IDiscosService _discosService;
        public ReactiveCommand<Unit, Unit> LoadObjectsCommand { get; }

        private IReadOnlyCollection<DiscosObject> _discosObjects = new ReadOnlyCollection<DiscosObject>(new List<DiscosObject>());

        public IReadOnlyCollection<DiscosObject> DiscosObjects
        {
            get => _discosObjects;
            private set => this.RaiseAndSetIfChanged(ref _discosObjects, value);
        }

        public MainWindowViewModel(IDiscosService discosService)
        {
            _discosService = discosService;
            LoadObjectsCommand = ReactiveCommand.CreateFromTask(LoadObjects);
        }

        private async Task LoadObjects()
        {
            DiscosObjects = await _discosService.GetDiscosObjects();
        }
    }
} 
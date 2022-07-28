using ReactiveUI;

namespace DiscosGroove.Main.Wrappers;

public class ObservableSetWrapper<T>: ReactiveObject
{
	private T? _value;

	public ObservableSetWrapper(T value)
	{
		Value = value;
	}
        
	public ObservableSetWrapper()
	{
		_value = default;
	}

	public T? Value
	{
		get => _value;
		set => this.RaiseAndSetIfChanged(ref _value, value);
	}
}
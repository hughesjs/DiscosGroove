using DiscosWebSdk.Models.ResponseModels.DiscosObjects;

namespace DiscosGroove.Core.Services;

public interface IDiscosService
{
	public Task<IReadOnlyCollection<DiscosObject>> GetDiscosObjects();

}
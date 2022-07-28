using DiscosWebSdk.Clients;
using DiscosWebSdk.Models.ResponseModels.DiscosObjects;

namespace DiscosGroove.Core.Services;

public class DiscosService: IDiscosService
{
	private readonly IDiscosClient _client;

	public DiscosService(IDiscosClient client)
	{
		_client = client;
	}

	public async Task<IReadOnlyCollection<DiscosObject>> GetDiscosObjects()
	{
		return await _client.GetMultiple<DiscosObject>();
	}
}
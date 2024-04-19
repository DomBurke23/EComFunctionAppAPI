using EComFunctionAppAPI.Client.Constants;
using EComFunctionAppAPI.Client.Options;
using EComFunctionAppAPI.Client.Requests;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace EComFunctionAppAPI.Client;

public class EComClient : IEComClient
{
    public Uri BaseUri { get; set; }
    private readonly HttpClient _httpClient;
    private readonly EComOptions _eComOptions;

    public EComClient(HttpClient httpClient,
        IOptions<EComOptions> eComOptions)
    {
        _httpClient = httpClient;
        BaseUri = new Uri(eComOptions.Value.BaseUrl);
        _eComOptions = eComOptions.Value;
    }

    public async Task<HttpResponseMessage> SaveOrderAsync(SaveOrderRequest saveOrderRequest)
    {
        var requestPath = "/SaveOrderTrigger";
        var uri = new Uri(BaseUri, requestPath);
        var dataJson = JsonSerializer.Serialize(saveOrderRequest);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = uri,
            Content = new StringContent(dataJson, Encoding.UTF8, MediaTypeNames.Application.Json)
        };
        request.Headers.Accept.Clear();
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        request.Headers.Add(HttpRequestHeaderConstants.XApiKey, _eComOptions.XApiKey);
        var httpResponseMessage = await _httpClient.SendAsync(request, CancellationToken.None);
        return httpResponseMessage;
    }
}

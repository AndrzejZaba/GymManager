using GymManager.Application.Common.Interfaces;
using GymManager.Application.Common.Models.Payments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace GymManager.Infrastructure.Payments;

public class Przelewy24 : IPrzelewy24
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<Przelewy24> _logger;
    private string _crc;
    private string _userName;
    private string _userSecret;
    private string _baseUrl;
    private JsonSerializerSettings _jsonSettings;

    public Przelewy24(
        HttpClient httpClient,
        IConfiguration configuration,
        ILogger<Przelewy24> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;

        GetConfiguration();
        InitHttpClient();
        InitJsonSettings();

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
    }

    private void InitJsonSettings()
    {
        _jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            },
            Formatting = Formatting.Indented,
        };
    }

    private void InitHttpClient()
    {
        _httpClient.BaseAddress = new Uri(_baseUrl);
        _httpClient.Timeout = new TimeSpan(0, 0, 30);
        _httpClient.DefaultRequestHeaders.Clear();

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_userName}:{_userSecret}")));
    }

    private void GetConfiguration()
    {
        _crc = _configuration.GetValue<string>("Przelewy24:Crc");
        _userName = _configuration.GetValue<string>("Przelewy24:UserName");
        _userSecret = _configuration.GetValue<string>("Przelewy24:UserSecret");
        _baseUrl = _configuration.GetValue<string>("Przelewy24:BaseUrl");
}

    public async Task<P24TransactionResponse> NewTransactionAsync(P24TransactionRequest data)
    {
        data.MerchantId = int.Parse(_userName);
        data.PosId = int.Parse(_userName);

        var signString = $"{{\"sessionId\":\"{data.SessionId}\",\"merchantId\":{data.MerchantId},\"amount\":{data.Amount},\"currency\":\"{data.Currency}\",\"crc\":\"{_crc}\"}}";

        data.Sign = GenerateSign(signString);

        var jsonContent = JsonConvert.SerializeObject(data, _jsonSettings);

        var stringContent = new StringContent(jsonContent, UnicodeEncoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/api/v1/transaction/register", stringContent);

        if (!response.IsSuccessStatusCode)
            _logger.LogError(response.RequestMessage.ToString(), null);

        return JsonConvert.DeserializeObject<P24TransactionResponse>(await response.Content.ReadAsStringAsync());

    }

    private string GenerateSign(string signString)
    {
        using (SHA384 sha384Hash = SHA384.Create())
        {
            byte[] sourceBytes = Encoding.UTF8.GetBytes(signString);
            byte[] hashBytes = sha384Hash.ComputeHash(sourceBytes);
            string hash = BitConverter.ToString(hashBytes).Replace("-", "");

            return hash.ToLower();
        }
    }

    public async Task<P24TestAccessResponse> TestConnectionAsync()
    {
        var response = await _httpClient.GetAsync("/api/v1/testAccess");

        if (!response.IsSuccessStatusCode)
            _logger.LogError(response.RequestMessage.ToString(), null);

        return JsonConvert.DeserializeObject<P24TestAccessResponse>(await response.Content.ReadAsStringAsync());

        
    }
}

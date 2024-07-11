using GymManager.Application.Common.Interfaces;
using GymManager.Application.Common.Models.Invoices;
using GymManager.Application.Invoices.Commands.AddInvoice;
using GymManager.Domain.Entities;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace GymManager.Infrastructure.Invoices;

public class GymInvoices : IGymInvoices
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<GymInvoices> _logger;
    private readonly IHttpContext _httpContext;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IApplicationDbContext _context;
    private string _baseUrl;

    public GymInvoices(
        HttpClient httpClient,
        IConfiguration configuration,
        ILogger<GymInvoices> logger,
        IHttpContext httpContext,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUserService,
        IApplicationDbContext context)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
        _httpContext = httpContext;
        _dateTimeService = dateTimeService;
        _currentUserService = currentUserService;
        _context = context;
        _baseUrl = _configuration.GetValue<string>("GymInvoices:BaseUrl");

        _httpClient.BaseAddress = new Uri(_baseUrl);
        _httpClient.Timeout = new TimeSpan(0, 0, 30);

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

    }

    private async Task SetHeader()
    {
        _httpClient.DefaultRequestHeaders.Clear();

        var token = _httpContext.Session.GetString("JWToken");

        if(string.IsNullOrWhiteSpace(token) || TokenHasExpired(token) ) 
        {
            var userId = _currentUserService.UserId;

            if (string.IsNullOrWhiteSpace(userId))
                throw new Exception("unauthorized");

            var user = await _context
                .Users
                .Select(x => new { Id = x.Id, UserName = x.UserName, Password = x.PasswordHash })
                .FirstOrDefaultAsync(x => x.Id == userId);

            token = (await GenerateToken(new AuthenticateRequest 
            { 
                UserName = user.UserName, 
                Password = user.Password 
            })).Token;

            _httpContext.Session.SetString("JWToken", token);

        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    }

    private async Task<AuthenticateResponse> GenerateToken(AuthenticateRequest authenticateRequest)
    {
        
        var jsonContent = JsonConvert.SerializeObject(authenticateRequest);

        _logger.LogInformation(jsonContent);

        var stringContent = new StringContent(jsonContent, UnicodeEncoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/api/v1/tokens", stringContent);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError(response.RequestMessage.ToString(), null);
            throw new Exception(response.RequestMessage.ToString());
        }

        return JsonConvert.DeserializeObject<AuthenticateResponse>(await response.Content.ReadAsStringAsync());

    }


    private bool TokenHasExpired(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return true;

        var jwtHandler = new JwtSecurityTokenHandler();
        var jwtToken = jwtHandler.ReadToken(token);
        var expDate = jwtToken.ValidTo;

        if (expDate < _dateTimeService.Now.AddMinutes(1))
            return true;

        return false;


    }

    public async Task AddInvoice(string ticketId)
    {
        await SetHeader();

        var jsonContent = JsonConvert.SerializeObject(new AddInvoiceCommand
        {
            TicketId = ticketId
        });

        var stringContent = new StringContent(jsonContent, UnicodeEncoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/api/v1/invoices", stringContent);

        if(!response.IsSuccessStatusCode)
        {
            _logger.LogError(response.RequestMessage.ToString(), null);
            throw new Exception(response.RequestMessage.ToString());
        }

    }

}

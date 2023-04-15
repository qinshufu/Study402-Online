using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Study402Online.OrderService.Api.Application.Commands;

namespace Study402Online.OrderService.Api.Application.Clients;

/// <summary>
/// 构建微信支付接口的签名
/// </summary>
public class WechatPayRequestSigntureHandler : DelegatingHandler
{
    private readonly IOptions<WechatPayOptions> _options;

    public WechatPayRequestSigntureHandler(IOptions<WechatPayOptions> options)
    {
        _options = options;
    }

    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken) =>
        SendAsync(request, cancellationToken).Result;

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // 如果是对微信支付的请求
        if (request.RequestUri?.ToString().StartsWith("https://api.mch.weixin.qq.com") ?? false)
            request.Headers.Add("Authorization", await BuildAuthorizationHeader(request, cancellationToken));

        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<string> BuildAuthorizationHeader(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var method = request.Method.Method.ToUpper(CultureInfo.InvariantCulture);
        var url = request.RequestUri?.PathAndQuery ?? "";
        var timespan = (int)(DateTime.Now - DateTime.Parse("1970-1-1")).TotalSeconds;
        var randomString = Random.Shared.NextInt64(10000, 99999).ToString();
        var body = await request.Content!.ReadAsStringAsync(cancellationToken);

        var signtureBuilder = new StringBuilder();
        signtureBuilder.Append(method + "\n");
        signtureBuilder.Append(url + "\n");
        signtureBuilder.Append(timespan + "\n");
        signtureBuilder.Append(randomString + "\n");
        signtureBuilder.Append(body + "\n");

        var signtureData = Encoding.UTF8.GetBytes(signtureBuilder.ToString());
        var privateKeyBytes = Convert.FromBase64String(_options.Value.API_V3_Key);

        using var rsa = RSA.Create();
        rsa.ImportPkcs8PrivateKey(privateKeyBytes, out _);

        var signture = rsa.SignData(signtureData, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        var signtureBase64 = Base64UrlEncoder.Encode(signture);

        var authorizationHeaderValue = new StringBuilder();
        authorizationHeaderValue.Append("Authorization: WECHATPAY2-SHA256-RSA2048" + " ");
        authorizationHeaderValue.Append($"mchid=\"{_options.Value.MerchantId}\",");
        authorizationHeaderValue.Append($"serial_no=\"{_options.Value.CertificateSerialNumber}\",");
        authorizationHeaderValue.Append($"nonce_str=\"{randomString}\",");
        authorizationHeaderValue.Append($"timestamp=\"{timespan}\",");
        authorizationHeaderValue.Append($"signature=\"{signtureBase64}\"");

        return authorizationHeaderValue.ToString();
    }
}
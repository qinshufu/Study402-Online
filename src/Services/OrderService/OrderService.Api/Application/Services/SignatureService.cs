namespace Study402Online.OrderService.Api.Application.Services;

/// <summary>
/// 签名服务
/// </summary>
public class SignatureService : ISignatureService
{
    public Task<bool> VerifyWechatSignatureAsync(Span<byte> bytes)
    {
        throw new NotImplementedException();
    }

    public Task<string> DecryptWechatDataAsync(string ciphertext, string associatedData, string nonce)
    {
        throw new NotImplementedException();
    }
}
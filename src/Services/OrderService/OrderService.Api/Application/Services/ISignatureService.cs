using System.Collections.Specialized;

namespace Study402Online.OrderService.Api.Application.Services;

/// <summary>
/// 签名服务
/// </summary>
public interface ISignatureService
{
    /// <summary>
    /// 验证微信签名
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    Task<bool> VerifyWechatSignatureAsync(Span<byte> bytes);

    /// <summary>
    /// 解密微信加密后的数据
    /// </summary>
    /// <param name="ciphertext"></param>
    /// <param name="associatedData"></param>
    /// <param name="nonce"></param>
    /// <returns></returns>
    Task<string> DecryptWechatDataAsync(string ciphertext, string associatedData, string nonce);
}
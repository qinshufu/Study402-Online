using CaptchaService.Api.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Study402Online.Common.Model;
using StackExchange.Redis;
using Hei.Captcha;
using CaptchaService.Api.Services;
using MediatR;
using CaptchaService.Api.Applications.Commands;

namespace CaptchaService.Api.Controllers
{
    [Route("/api/captcha")]
    [ApiController]
    public class PictureCaptchaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PictureCaptchaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 生成图片验证码
        /// </summary>
        /// <param name="ip">申请验证码的客户（这里的客户指的是人）机器 ID</param>
        /// <returns></returns>
        [HttpPost]
        public Task<Result<PictureCaptcha>> GeneratePictureCaptcha() => _mediator.Send(new PictureCaptchaGenerateCommand());

        /// <summary>
        /// 验证验证码是否有效
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<Result<bool>> VerifyCaptcha(CaptchaVerifyCommand command) => _mediator.Send(command);
    }
}

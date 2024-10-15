using FreshCode.Interfaces;
using FreshCode.MiddleWare;
using FreshCode.UseCases;

namespace FreshCode.Fabrics
{
    public class MiddlewareFabric
    {
        public static IMiddleWare Create(string? platform, UserUseCase userUseCase)
        {
            return platform switch
            {
                "vk" => new VKMiddleWare(userUseCase),
                //"tg" => new TgMiddleWare(),
                _ => throw new ArgumentException("Invalid platform")
            };
        }
    }
}

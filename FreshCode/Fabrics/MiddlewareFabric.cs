using FreshCode.Interfaces;
using FreshCode.MiddleWare;

namespace FreshCode.Fabrics
{
    public class MiddlewareFabric
    {
        public static IMiddleWare Create(string? platform)
        {
            return platform switch
            {
                "vk" => new VKMiddleWare(),
                "tg" => new TgMiddleWare(),
                _ => throw new ArgumentException("Invalid platform")
            };
        }
    }
}

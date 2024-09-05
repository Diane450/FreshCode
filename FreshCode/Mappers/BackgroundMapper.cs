using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Mappers
{
    public static class BackgroundMapper
    {
        public static BackgroundDTO ToDTO(Background background)
        {
            return new BackgroundDTO
            {
                BackgroundId = background.Id,
                X = background.X,
                Y = background.Y,
                Price = background.Price,
            };
        }
    }
}

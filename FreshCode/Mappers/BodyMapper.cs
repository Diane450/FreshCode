using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Mappers
{
    public static class BodyMapper
    {
        public static BodyDTO ToDTO(Body body)
        {
            return new BodyDTO
            {
                Id = body.Id,
                X = body.X,
                Y = body.Y,
            };
        }
    }
}

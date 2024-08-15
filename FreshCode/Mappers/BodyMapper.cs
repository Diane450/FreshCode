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

        public static Body ToEntity(BodyDTO bodyDTO)
        {
            return new Body
            {
                Id = bodyDTO.Id,
                X = bodyDTO.X,
                Y = bodyDTO.Y,
            };

        }
    }
}

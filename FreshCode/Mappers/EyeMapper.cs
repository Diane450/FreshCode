using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Mappers
{
    public static class EyeMapper
    {
        public static EyeDTO ToDTO(Eye eye)
        {
            return new EyeDTO
            {
                Id = eye.Id,
                X = eye.X,
                Y = eye.Y,
            };
        }
    }
}

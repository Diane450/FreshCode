using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Mappers
{
    public static class BonusMapper
    {
        public static BonusDTO ToDTO(Bonu bonus)
        {
            return new BonusDTO
            {
                Id = bonus.Id,
                Value = bonus.Value,
                Characteristic = bonus.Characteristic.Characteristic1,
                Type = bonus.Type.Type
            };
        }
    }
}

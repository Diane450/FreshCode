using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using BonusType = FreshCode.Enums.BonusType;

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
                Type = bonus.Type.Type == "flat" ? BonusType.Flat : BonusType.Percentage
            };
        }
    }
}

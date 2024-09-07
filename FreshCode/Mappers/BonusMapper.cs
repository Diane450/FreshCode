using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using BonusType = FreshCode.ModelsDTO.BonusType;

namespace FreshCode.Mappers
{
    public static class BonusMapper
    {
        public static BonusDTO ToDTO(Bonu bonus)
        {
            return new BonusDTO
            {
                BonusId = bonus.Id,
                Value = bonus.Value,
                Characteristic = bonus.Characteristic.Characteristic1,
                BonusType = bonus.Type.Type == "flat" ? BonusType.flat : BonusType.percent
            };
        }
    }
}

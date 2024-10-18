using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Requests
{
    /// <summary>
    /// Запрос на создание питомца
    /// </summary>
    public class CreatePetRequest
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// BodyDTO тело
        /// </summary>
        public BodyDTO Body { get; set; } = null!;
        /// <summary>
        /// EyeDTO Глаза
        /// </summary>
        public EyeDTO Eyes { get; set; } = null!;
    }
}

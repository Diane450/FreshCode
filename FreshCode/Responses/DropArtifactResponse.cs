namespace FreshCode.Responses
{
    /// <summary>
    /// Дроп с баннера
    /// </summary>
    public class DropArtifactResponse
    {
        /// <summary>
        /// Список полученных артефактов
        /// </summary>
        public List<ArtifactResponse> artifacts { get; set; } = null!;
        /// <summary>
        /// Полученные деньги с повторяющихся артефактов
        /// </summary>
        public int TotalMoney { get; set; }
        /// <summary>
        /// Оставшееся кол-во круток
        /// </summary>
        public int TotalFates { get; set; }
    }
}

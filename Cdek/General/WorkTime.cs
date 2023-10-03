namespace XyloCode.ThirdPartyServices.Cdek.General
{
    /// <summary>
    /// График работы на неделю.
    /// </summary>
    public class WorkTime
    {
        /// <summary>
        /// Порядковый номер дня начиная с единицы. Понедельник = 1, воскресенье = 7.
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// Период работы в эти дни. Если в этот день не работают, то не отображается.
        /// </summary>
        public string Time { get; set; }
    }
}

namespace MISA.HUST._21H._2022.API.Entities.DTO
{
    /// <summary>
    /// Dữ liêu trả về sau khi api lọc, phân trang
    /// </summary>
    /// <typeparam name="L">Kiểu dữ liệu mảng trả về</typeparam>
    public class PagingData<L>
    {
        /// <summary>
        /// Thoả mãn điều kiện lọc, phân trang
        /// </summary>
        public List<L> Data { get; set; } = new List<L>();

        /// <summary>
        /// Tổng số bản ghi thỏa mãn điều kiện
        /// </summary>
        public long TotalCount { get; set; }

    }
}

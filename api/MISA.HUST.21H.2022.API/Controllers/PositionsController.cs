using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.HUST._21H._2022.API.Entities;
using MySqlConnector;

namespace MISA.HUST._21H._2022.API.Controllers
{
    [Route("api/[controller]")] // route hiểu như con đường, route cha
    [ApiController]
    public class PositionsController : ControllerBase
    {
        string connectionOfHuy = "Server=localhost;Port=3306;Database=hust.21h.2022.huytq;Uid=root;Pwd=Huyta@01478;";

        /// <summary>
        /// API lấy ds tất cả vị trí
        /// </summary>
        /// <returns>Danh sách tất cả vị trí</returns>
        /// created by: HUYTQ (8/9/2022)
        [HttpGet]// Phương thức get lấy dữ liệu
        [Route("")]
        // Hàm IActionResult
        public IActionResult GetAllPositions()
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var mySqlConnection = new MySqlConnection(connectionOfHuy);

                // Chuẩn bị câu lệnh truy vấn
                string getAllEmployeeCommand = "SELECT * FROM  positions;";

                // Thực hiện gọi vào DB để chạy câu lệnh SQL với tham số đầu vào ở trên
                var positons = mySqlConnection.Query(getAllEmployeeCommand); //

                // Xử lý kết quả trả về từ DB
                if (positons != null)
                {
                    // Trả về dữ liệu cho client nếu thành công
                    return StatusCode(StatusCodes.Status200OK, positons);
                }
                else
                {
                    // Trả về lỗi nếu thất bại
                    return StatusCode(StatusCodes.Status400BadRequest, "e002");
                }
                // Try catch để bắt exception
            }
            catch (Exception exception) // Bắt exception
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status400BadRequest, "e1001");
            }
        }

        [HttpGet]
        [Route("{positionName}")] //Truyền tham số vào và route con
        public IActionResult GetPositionByName([FromRoute] string positionName)
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var connection = new MySqlConnection(connectionOfHuy);

                // Thực hiện gọi vào DB để chạy câu lệnh SQL với tham số đầu vào ở trên
                positionName = positionName.Replace("'", "''");

                // Chuẩn bị câu lệnh truy vấn
                string getPositionByNameCommand = "SELECT * FROM  positions WHERE PositionName = '" + positionName + "'";

                // Thực hiện gọi vào DB để chạy câu lệnh SQL với tham số đầu vào ở trên
                var positionByName = connection.Query(getPositionByNameCommand); //

                // Xử lý kết quả trả về từ DB
                if (positionByName != null)
                {
                    // Trả về dữ liệu cho client nếu thành công
                    return StatusCode(StatusCodes.Status200OK, positionByName); // Trả về thông tin vị trí theo tên vị trí
                }
                else
                {
                    // Trả về lỗi nếu thất bại
                    return StatusCode(StatusCodes.Status400BadRequest, "e002");
                }
                // Try catch để bắt exception
            }
            catch (Exception exception) // Bắt exception
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status400BadRequest, "Lỗi xoá dữ liệu");
            }
        }

    }
}

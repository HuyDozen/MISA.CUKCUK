using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.HUST._21H._2022.API.Entities;
using MySqlConnector;

namespace MISA.HUST._21H._2022.API.Controllers
{
        [Route("api/[controller]")] // route hiểu như con đường, route cha
        [ApiController]
        public class DepartmentsController : ControllerBase
        {
            string connectionOfHuy = "Server=localhost;Port=3306;Database=hust.21h.2022.huytq;Uid=root;Pwd=Huyta@01478;";
            
            /// <summary>
            /// API lấy ds tất cả phòng ban
            /// </summary>
            /// <returns>Danh sách tất cả phòng ban</returns>
            /// created by: HUYTQ (8/9/2022)
            [HttpGet]// Phương thức get lấy dữ liệu
            [Route("")]
            public IActionResult GetAllDepartments()
            {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var mySqlConnection = new MySqlConnection(connectionOfHuy);

                // Chuẩn bị câu lệnh truy vấn
                string getAllEmployeeCommand = "SELECT * FROM  department;";

                // Thực hiện gọi vào DB để chạy câu lệnh SQL với tham số đầu vào ở trên
                var departments = mySqlConnection.Query(getAllEmployeeCommand); //

                // Xử lý kết quả trả về từ DB
                if (departments != null)
                {
                    // Trả về dữ liệu cho client nếu thành công
                    return StatusCode(StatusCodes.Status200OK, departments);
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
        [Route("{departmentName}")] //Truyền tham số vào và route con
        public IActionResult GetDepartmentByName([FromRoute] string departmentName)
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var connection = new MySqlConnection(connectionOfHuy);

                // Thực hiện gọi vào DB để chạy câu lệnh SQL với tham số đầu vào ở trên
                departmentName = departmentName.Replace("'", "''");

                // Chuẩn bị câu lệnh truy vấn
                string getDepartmentByNameCommand = "SELECT * FROM  department WHERE DepartmentName = '" + departmentName + "'";

                // Thực hiện gọi vào DB để chạy câu lệnh SQL với tham số đầu vào ở trên
                var departmentByName = connection.Query(getDepartmentByNameCommand); //

                // Xử lý kết quả trả về từ DB
                if (departmentByName != null)
                {
                    // Trả về dữ liệu cho client nếu thành công
                    return StatusCode(StatusCodes.Status200OK, departmentByName); // Trả về thông tin phòng ban theo tên phòng ban
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

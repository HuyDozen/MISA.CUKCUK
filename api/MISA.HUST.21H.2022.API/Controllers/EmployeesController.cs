using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.HUST._21H._2022.API.Entities;
using MISA.HUST._21H._2022.API.Entities.DTO;
using MySqlConnector;

namespace MISA.HUST._21H._2022.API.Controllers
{
    [Route("api/[controller]")] // route hiểu như con đường, route cha
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        string connectionOfHuy = "Server=localhost;Port=3306;Database=hust.21h.2022.huytq;Uid=root;Pwd=Huyta@01478;";


        /// <summary>
        /// API lấy ds tất cả nhân viên
        /// </summary>
        /// <returns>Danh sách tất cả nhân viên</returns>
        /// created by: HUYTQ (8/9/2022)
        [HttpGet]// Phương thức get lấy dữ liệu
        // Hàm IActionResult
        public IActionResult GetAllEmployees()
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var connection = new MySqlConnection(connectionOfHuy);

                // Chuẩn bị câu lệnh truy vấn
                string getEmployeesCommand = "SELECT * FROM  employee;";


                // Thực hiện gọi vào DB để chạy câu lệnh SQL với tham số đầu vào ở trên
                var employees = connection.Query(getEmployeesCommand); //

                // Xử lý kết quả trả về từ DB
                if (employees != null)
                {
                    // Trả về dữ liệu cho client nếu thành công
                    return StatusCode(StatusCodes.Status200OK, employees);
                }
                else
                {
                    // Trả về lỗi nếu thất bại
                    return StatusCode(StatusCodes.Status400BadRequest, "Không kết nối được với dữ liệu");
                }
                // Try catch để bắt exception
            }
            catch (Exception exception) // Bắt exception
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
        }

        /// <summary>
        /// API lấy thông tin chi tiết một nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">ID nhân viên</param>
        /// <returns>Thông tin chi tiết một nhân viên</returns>
        /// Created by: HUYTQ (8/9/2022)

        [HttpGet]
        [Route("{employeeID}")] //Truyền tham số vào và route con
        public IActionResult GetEmployeeByID([FromRoute] string employeeID)
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var connection = new MySqlConnection(connectionOfHuy);

                // Thực hiện gọi vào DB để chạy câu lệnh SQL với tham số đầu vào ở trên
                employeeID = employeeID.Replace("'", "''");

                // Chuẩn bị câu lệnh truy vấn
                string getEmployeeByIdCommand = "SELECT * FROM  employee WHERE EmployeeID = '" + employeeID + "'";

                // Thực hiện gọi vào DB để chạy câu lệnh SQL với tham số đầu vào ở trên
                var employeeById = connection.Query(getEmployeeByIdCommand); //

                // Xử lý kết quả trả về từ DB
                if (employeeById != null)
                {
                    // Trả về dữ liệu cho client nếu thành công
                    return StatusCode(StatusCodes.Status200OK, employeeById); // Trả về thông tin nhân viên theo id
                }
                else
                {
                    // Trả về lỗi nếu thất bại
                    return StatusCode(StatusCodes.Status400BadRequest, "Lỗi xoá dữ liệu");
                }
                // Try catch để bắt exception
            }
            catch (Exception exception) // Bắt exception
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
        }

        /// <summary>
        /// API thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên cần thêm mới</param>
        /// <returns>ID của nhân viên vừa thêm mới</returns>
        /// Created by: HUYTQ (8/9/2022)
        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            try
            {

                // Khởi tạo kết nối tới DB MySQL
                var connection = new MySqlConnection(connectionOfHuy);

                // Chuẩn bị câu lệnh INSERT INTO
                string insertEmployeeCommand = "INSERT  INTO employee (EmployeeID, EmployeeCode, EmployeeName, DateOfBirth, Gender, IdentityNumber, PositionID, PositionName, DeparmentID, DepartmenName, WorkStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, IdentityIssuedPlace, Email, PhoneNumber, IdentittyIssuedDate, TaxCode, Salary, JoiningDate) " +
                    "VALUES (@EmployeeID, @EmployeeCode, @EmployeeName, @DateOfBirth, @Gender, @IdentityNumber, @PositionID, @PositionName, @DeparmentID, @DepartmenName, @WorkStatus, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @IdentityIssuedPlace, @Email, @PhoneNumber, @IdentittyIssuedDate, @TaxCode, @Salary, @JoiningDate);";

                // Chuẩn bị tham số đầu vào cho câu lệnh INSERT INTOs
                var employeeID = Guid.NewGuid(); // Guid.NewGuid() --> Tạo ra một chuỗi 36 ký tự và mỗi lần tạo ra là khác nhau
                // Tương ứng vs từng tham số đầu vào thêm vào giá trị của nó (dòng 240->261)
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeID", employeeID);
                parameters.Add("@EmployeeCode", employee.EmployeeCode);
                parameters.Add("@EmployeeName", employee.EmployeeName);
                parameters.Add("@DateOfBirth", employee.DateOfBirth);
                parameters.Add("@Gender", employee.Gender);
                parameters.Add("@IdentityNumber", employee.IdentityNumber);
                parameters.Add("@PositionID", employee.PositionID);
                parameters.Add("@PositionName", employee.PositionName);
                parameters.Add("@DeparmentID", employee.DepartmentID);
                parameters.Add("@DepartmenName", employee.DepartmentName);
                parameters.Add("@WorkStatus", employee.WorkStatus);
                parameters.Add("@CreatedBy", employee.CreatedBy);
                parameters.Add("@CreatedDate", employee.CreatedDate);
                parameters.Add("@ModifiedBy", employee.ModifiedBy);
                parameters.Add("@ModifiedDate", employee.ModifiedDate);
                parameters.Add("@IdentityIssuedPlace", employee.IdentityIssuedPlace);
                parameters.Add("@Email", employee.Email);
                parameters.Add("@PhoneNumber", employee.PhoneNumber);
                parameters.Add("@IdentittyIssuedDate", employee.IdentityIssuedDate);
                parameters.Add("@TaxCode", employee.TaxCode);
                parameters.Add("@Salary", employee.Salary);
                parameters.Add("@JoiningDate", employee.JoiningDate);

                // Thực hiện gọi vào DB để chạy câu lệnh SQL với tham số đầu vào ở trên     
                int numberOfAffectedRows = connection.Execute(insertEmployeeCommand, parameters); // Dùng excute để chạy vào DataBase, return the number of row effected

                // Xử lý kết quả trả về từ DB
                if (numberOfAffectedRows > 0)
                {
                    // Trả về dữ liệu cho client nếu thành công
                    return StatusCode(StatusCodes.Status201Created, employeeID);
                }
                else
                {
                    // Trả về lỗi nếu thất bại
                    return StatusCode(StatusCodes.Status400BadRequest, "e002"); // Trả về lỗi thêm mới thất bại
                }
            }

            // Try catch để bắt exception
            catch (MySqlException mySqlException)
            {
                if (mySqlException.ErrorCode == MySqlErrorCode.DuplicateKeyEntry) //trùng mã
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e003");
                }
                return StatusCode(StatusCodes.Status400BadRequest, "e004");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
        }
        /// <summary>
        /// API sửa 1 nhân viên
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên cần sửa</param>
        /// <param name="employeeID">ID của nhân viên cần sửa</param> //ctrl d = duplicate
        /// <returns>ID của nhân viên vừa thêm mới</returns>
        /// Created by: HUYTQ (8/9/2022)
        [HttpPut]
        [Route("{employeeID}")]
        public IActionResult UpdateEmployee([FromBody] Employee employee, [FromRoute] Guid employeeID)
        {
            try
            {
                //Khởi tạo kêt nối đến DB MySQL
                var connection = new MySqlConnection(connectionOfHuy);

                //Chuẩn bị câu lệnh UPDATE
                string updateEmployeeCommand = "UPDATE employee " +
                    "SET EmployeeCode = @EmployeeCode, " +
                    "EmployeeName = @EmployeeName, " +
                    "DateOfBirth = @DateOfBirth, " +
                    "Gender = @Gender, " +
                    "IdentityNumber = @IdentityNumber, " +
                    "IdentittyIssuedDate = @IdentittyIssuedDate, " +
                    "IdentityIssuedPlace = @IdentityIssuedPlace, " +
                    "Email = @Email, " +
                    "PhoneNumber = @PhoneNumber, " +
                    "PositionName = @PositionName, " +
                    "DepartmentID = @DepartmentID, " +
                    "DepartmentName = @DepartmentName, " +
                    "TaxCode = @TaxCode, " +
                    "Salary = @Salary, " +
                    "JoiningDate = @JoiningDate, " +
                    "WorkStatus = @WorkStatus, " +
                    "CreatedBy = @CreatedBy, " +
                    "CreatedDate = @CreatedDate, " +
                    "ModifiedBy = @ModifiedBy, " +
                    "ModifiedDate = @ModifiedDate, " +
                    "WHERE EmployeeID = @EmployeeID, ";
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeID", employeeID);
                parameters.Add("@EmployeeCode", employee.EmployeeCode);
                parameters.Add("@EmployeeName", employee.EmployeeName);
                parameters.Add("@DateOfBirth", employee.DateOfBirth);
                parameters.Add("@Gender", employee.Gender);
                parameters.Add("@IdentityNumber", employee.IdentityNumber);
                parameters.Add("@PositionID", employee.PositionID);
                parameters.Add("@PositionName", employee.PositionName);
                parameters.Add("@DeparmentID", employee.DepartmentID);
                parameters.Add("@DepartmenName", employee.DepartmentName);
                parameters.Add("@WorkStatus", employee.WorkStatus);
                parameters.Add("@CreatedBy", employee.CreatedBy);
                parameters.Add("@CreatedDate", employee.CreatedDate);
                parameters.Add("@ModifiedBy", employee.ModifiedBy);
                parameters.Add("@ModifiedDate", employee.ModifiedDate);
                parameters.Add("@IdentityIssuedPlace", employee.IdentityIssuedPlace);
                parameters.Add("@Email", employee.Email);
                parameters.Add("@PhoneNumber", employee.PhoneNumber);
                parameters.Add("@IdentittyIssuedDate", employee.IdentityIssuedDate);
                parameters.Add("@TaxCode", employee.TaxCode);
                parameters.Add("@Salary", employee.Salary);
                parameters.Add("@JoiningDate", employee.JoiningDate);

                //Thực hiện gọi vào DB để chạy câu lệnh UPDATE vs tham số ở đầu vào trên
                int numberOfAffectedRows = connection.Execute(updateEmployeeCommand, parameters);

                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, employeeID);
                }    
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e002");
                }    
            }
            // Try catch để bắt exception
            catch (MySqlException mySqlException)
            {
                if (mySqlException.ErrorCode == MySqlErrorCode.DuplicateKeyEntry) //trùng mã
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e003");
                }
                //Lỗi còn lại trả về 001
                return StatusCode(StatusCodes.Status400BadRequest, "e004");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
        }

        /// <summary>
        /// API xoá 1 nhân viên
        /// </summary>
        /// <param name="employeeID">ID của nhân viên cần sửa</param> //ctrl d = duplicate
        /// <returns>ID của nhân viên vừa vừa xoá</returns>
        /// Created by: HUYTQ (8/9/2022)
        [HttpDelete]
        [Route("{employeeID}")] //truyền cái route id của nhân viên

        // Ko cần truyền employee 
        public IActionResult DeleteEmployee([FromRoute] string employeeID)
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var connection = new MySqlConnection(connectionOfHuy);

                // Thực hiện gọi vào DB để chạy câu lệnh SQL với tham số đầu vào ở trên
                employeeID = employeeID.Replace("'", "''");

                // Chuẩn bị câu lệnh truy vấn
                string deleteteEmployeeByIdCommand = "DELETE FROM employee WHERE EmployeeID = '" + employeeID + "'";

                // Thực hiện gọi vào DB để chạy câu lệnh SQL với tham số đầu vào ở trên
                var deleteteEmployeeById = connection.Query(deleteteEmployeeByIdCommand);

                // Xử lý kết quả trả về từ DB
                if (deleteteEmployeeById != null)
                {
                    // Trả về dữ liệu cho client nếu thành công
                    return StatusCode(StatusCodes.Status200OK, "Xoá thành công!"); // 
                }
                else
                {
                    // Trả về lỗi nếu thất bại
                    return StatusCode(StatusCodes.Status400BadRequest, "Lỗi không xoá được nhân viên theo id");
                }
                // Try catch để bắt exception
            }
            catch (Exception exception) // Bắt exception
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
        }

        /// <summary>
        /// API Lấy mã nhân viên mới tự động tăng
        /// </summary>
        /// <returns>Mã nhân viên mới tự động tăng</returns>
        /// Created by: TMSANG (09/06/2022)
        [HttpGet("newCode-employeeID")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var connection = new MySqlConnection(connectionOfHuy);

                // Chuẩn bị tên stored procedure
                string procedureInDb = "Proc_Employee_GetMaxCode";

                // Thực hiện gọi vào DB để chạy stored procedure ở trên
                string maxEmployeeCode = connection.QueryFirstOrDefault<string>(procedureInDb, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý sinh mã nhân viên mới tự động tăng
                // Cắt chuỗi mã nhân viên lớn nhất trong hệ thống để lấy phần số
                // Mã nhân viên mới = "NV" + Giá trị cắt chuỗi ở  trên + 1
                string newEmployeeCode = "NV" + (Int64.Parse(maxEmployeeCode.Substring(2)) + 1).ToString();

                // Trả về dữ liệu cho client
                return StatusCode(StatusCodes.Status200OK, newEmployeeCode);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
        }

    }
}

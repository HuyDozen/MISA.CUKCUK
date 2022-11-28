$(document).ready(function(){

    //Gan su kien cho cac element
    initEvents();

    //Load dữ liệu:
    loadData();

    //
    
})

var employeeId = null;
var formMod = "add";
/**
 * 
 *Thực hiện load dữ liệu lên table
 *Author: TQHUY    
 */

function loadData(){
    
    //Gọi API lấy dữ liệu
    
    $.ajax({
        type: "GET",
        async: null,
        url: "http://localhost:58692/api/Employees",
        success: function (res) {
            $("table#tbEmployee tbody").empty();
            //Xử lý dữ liệu đối tượng

            var sort = 1;
            let ths = $("table#tbEmployee thead th");

            for(const emp of res) {
                //Duyệt từng cột trong tiêu đồ
                var trElement = $(`<tr></tr>`);
                for (const th of ths) {
                    
                    //Lấy ra propValue tương ứng vs các cột
                    const propValue = $(th).attr("propValue");
                    const format = $(th).attr("format");
                    
                    //Lấy gtri tương ứng vs tên của propValue trong đối tượng
                    let  value = null; 
                    
                    if(propValue == "Sort")
                        value = sort;
                    else
                        value = emp[propValue];
                    let classAlign = "";

                    switch (format) {
                        case "workStatus":
                            switch (value) {
                                case 0:
                                    value = "Nghỉ việc";
                                    break;
                                case 1:
                                    value = "Đang làm việc"
                                    break;
                                case 2:
                                    value = "Thử việc"
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "gender":
                            switch (value) {
                                case 0:
                                    value = "Khác";
                                    break;
                                case 1:
                                    value = "Nam"
                                    break;
                                case 2:
                                    value = "Nữ"
                                    break;
                                case 3:
                                    value = null;
                                    break;
                                default:
                                    break;
                                }
                                break;
                        case "stt":
                            classAlign = "content-text__center";
                            break;
                        case "date":
                            value = formatDate(value);
                            classAlign = "content-text__center";
                            break;
                        case "money":
                            value = formatMoney(value);
                            classAlign = "content-text__right";
                            break;
                        default:
                            break;
                    }
                    //Tạo thHTML:
                    let thHTML = `<td class='${classAlign}'>${value ||""}</td>`;
                    //Đẩy vào trElement:
                    trElement.append(thHTML);
                }
                sort++;
                
                //js binding
                $(trElement).data("id", emp.EmployeeId);
                $(trElement).data("entity", emp);
                $("table#tbEmployee tbody").append(trElement);
            }
        },
        error: function() {
            console.log(res);
        }
    });
}

/**
 * 
 * @param {Date} date 
 * @returns 
 */

function formatDate(date) {
    try {
        if(date) {
            date = new Date(date);
            //Ngay
            dateValue = String(date.getDate());       
            //Thang
            let month = String(date.getMonth() + 1);//Thang lay tu 0
            //Nam
            let year = date.getFullYear();
            return `${month}/${dateValue}/${year}`; 
        }else{
            return "";
        }
        return `tháng/ngày/năm`;
    } catch (error) {
        console.log(error)
    }
}

function formatMoney(money){
    try{
        money =  new Intl.NumberFormat('vn-VI', {
            currency: 'VND',
            style: 'currency',
    }).format(money);
    return money;
    } catch(error) {
        console.log(error);
}
}

/**
 * Tạo các sự kiện
 * Author: HUYTQ 
 */
function initEvents() {

    $("#btnSave").click(saveData);

    $(document).on('dblclick','table#tbEmployee tbody tr',function() {
        //Hiển thị form
        $("#dlgEmployeeDetail").show();

        //Focus vào ô đầu tiên
        $('#txtEmployeeCode')[0].focus();
        //Xoá trạng thái được chọn
        $(this).siblings().removeClass("row-selected");

        //In dam dong duoc chon
        this.classList.add("row-selected");

        //Binding dữ liệu tương ứng
        let data = $(this).data('entity');
        employeeId = $(this).data('id');
        

        //Duyệt tất cả input
        let inputs = $("#dlgEmployeeDetail input, #dlgEmployeeDetail select, #dlgEmployeeDetail textarea");
        for(const input of inputs) {
            //Đọc thông tin propValue:
            const propValue = $(input).attr("propValue");
            let value = data[propValue];
            input.value =  value;

            

                
        }
    });

    $("#btnAdd").click(function(){

        formMod = "add";
        //Hiển thị form
        $("#dlgEmployeeDetail").show();

        //Xoá toàn bộ thông tin trong ô nhập
        $('input').val(null);
        $('textarea').val(null);
        
        //Focus ô nhập liệu
        $('#txtEmployeeCode')[0].focus();

        // Lấy ra mã nhân viên mới nhất
        $.ajax({
            type: "GET",
            url: "http://localhost:58692/api/Employees/newCode-employeeID",
            success: function (NewEmployeeCode) {
                $("#txtEmployeeCode").val(NewEmployeeCode);
                $("#txtEmployeeCode").focus();
            }
        });

    });

    $(".dialog__button--close").click(function(){ 
        $("#dlgEmployeeDetail").hide();
    });

    $("#btnCancel").click(function(){
        $("#dlgEmployeeDetail").hide();
    });
    
    $('input[huy]').blur(function () { 
        
        var value = this.value;
        if(!value) {

            //dat trang thai tuong ung 
            //neu value rong hoac null thi hien thi trang thai loi
            $(this).addClass("input__error");
            $(this).attr('title',"Thông tin này không được để trống");
        } else {
            //
            $(this).removeClass("input__error");
            $(this).removeAttr('title');
        }
    });

    $('input[type=email]').blur(function() {
        //Kiem tra email
        var email = this.value;
        var isEmail = checkEmailFormat(email);
        if(!isEmail){
            console.log("Emal không đúng định dạng");
            $(this).addClass("input__error");   
            $(this).attr('title',"Emal không đúng định dạng.");
            

        }else{
            console.log("Emal đúng định dạng");
            $(this).removeClass("input__error");
            $(this).removeAttr('title',"Emal không đúng định dạng.");
            
        }
    });

    $('input[type=phoneNumber]').blur(function() {
        //Kiem tra số
        var phoneNumber = this.value;
        var isNumberPhone = validateNumber(phoneNumber);
        if(!isNumberPhone){
            console.log("Chỉ được nhập số.");
            $(this).addClass("input__error");   
            $(this).attr('title',"Chỉ được nhập số.");
            

        }else{
            console.log("Nhập đúng số");
            $(this).removeClass("input__error");
            $(this).removeAttr('title',"Chỉ được nhập số.");
            
        }
    })
}

function searchInfor() {
    // var input, filter, tbody, tr, a, i, txtValue;
    // input = document.getElementById("btnSearch");
    // filter = input.value.toUpperCase();
    // tbody = document.getElementById("myTbody");
    // tr = tbody.getElementsByTagName("tr");
    // for (i = 0; i < tr.length; i++) {
    //     a = tr[i].getElementsByTagName("a")[0];
    //     txtValue = a.textContent || a.innerText;
    //     if (txtValue.toUpperCase().indexOf(filter) > -1) {
    //         tr[i].style.display = "";
    //     } else {
    //         tr[i].style.display = "none";
    //     }
    // }

    //Lấy giá trị thêm thông tin để truyền tham số

    //Gọi API
    
}

function deleteEmployeeById() {

}

function saveData() {
    //Thu thập
    let inputs = $("#dlgEmployeeDetail input, #dlgEmployeeDetail select, #dlgEmployeeDetail textarea");
    var employee = {};   

    //Build object
        for(const input of inputs) {
            //Đọc thông tin propValue:
            const propValue = $(input).attr("propValue");
            
            // Lấy ra value
            if(propValue) {
                let value = input.value;
                employee[propValue] = value;
            }
            
        }
    
    //Gọi api cất dữ liệu
    $.ajax({
        type: "PUT",
        url: "http://localhost:58692/api/Employees" + employeeId,
        data: JSON.stringify(employee), //convert JSON
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            alert("success")
            //load lại dữ liệu
            loadData();
            //Ẩn form chi tiết
            $("#dlgEmployeeDetail").hide();
        }
    });
  }
/**
 * Hàm check mail
 * @param {any} email 
 * @returns 
 */
function checkEmailFormat(email){
    const re =
    /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
    return email.match(re);
}

function validateNumber(phoneNumber) {
    const pattern = /^[0-9]+$/;
    return phoneNumber.match(pattern )
}
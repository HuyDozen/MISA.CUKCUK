// Khai báo hàm và thay đổi hàm
function usingArray(){
    const cars = ["Truck","Car","Motorcycle"];
    cars[0] = "Van" // Xe tải nhỏ
    cars.push("Tram") // Thêm element (phần tử) cho mảng 
}

// Sử dụng cú pháp có điều kiện với ()?
function checkAgeToVote() {
    let age=document.getElementById("age").value;
    let vote = (age < 18) ? "Quá trẻ":"Quá già";
    document.getElementById("demo3").innerHTML = vote + " để vote."
}

// Con số mở rộng theo cấp số nhân
let i = 123456e3 // i = 123456000
let z = 123456e-3 // 123,456

// Stings có thể đc miêu tả đối tượng vs keyword "new"
// KO nên sư dụng vì nó gây chậm khi thực thi và tạo ra kq ko mong muốn
let x = new String("HuyTa")

// Khai báo đối tượng và thay đổi đối tượng là car
const car = {
    type: "Truck",
    color: "Gray",
    model: "Charverlot",
    // 1 Phương thức có thể có trong 1 thuộc tính của đối tượng
    fullName: function() {
        return this.type + "" + this.model; // Con trỏ this gọi đến (called) đối tượng được định nghĩa trc
    }
};


// Su dung trong typeScript
// class User {
//     name: String;
//     message: string;
  
//     constructor() {
//       this.name = "";
//       this.message = "Hello";
//     }
//   }

car.color = "Red"; // Thay đổi thuộc tính trong constant object
// car["color"] = "Red"
car.owner = "Hùng"; // Thêm mới thuộc tính trong constant object

// Hàm viết hoa
function upper(s) {
    return s.toUpperCase();
}
var who = "reader";
// Sử dụng nội suy (interpolation)
var text = 
`A very ${upper("warm")} welcome
to all of you ${upper('${who}s')}!`;
console.log(text);
// A very WARM welcome
// to all of you READER!

function examplePrototype(){
    // prototype
    var becycle = {
        name: 'WWave'
    }

    var motorbike = {
        name: "WWave",
        type: function() {
            console.log('Chay nhu xe may');
        }
    }
    Object.setPrototypeOf(becycle, motorbike);
    becycle.type(); // Chay nhu xe may 
}


// Sử dụng for trong mảng
function usingLoopOnArray() { 
    const fruits = ["Banana", "Orange", "Apple", "Mango"];
    let fLen = fruits.length;

    let text = "<ul>";
        for (let i = 0; i < fLen; i++) {
    text += "<li>" + fruits[i] + "</li>";
    }
    text += "</ul>";

    document.getElementById("demo5").innerHTML = text;
 }

function usingTemplteHTML() { 
    let header = "Templates Literals";
    let tags = ["template literals", "javascript", "es6"];

    let html = `<h2>${header}</h2><ul>`;

    for (const x of tags) {
        html += `<li>${x}</li>`;
    }

    html += `</ul>`;
    document.getElementById("demo4").innerHTML = html;
}

function input(){
    
    document.getElementById("demo").innerHTML = "Hello JS";
}

function myFunction(){
    // Ẩn dòng text trong thẻ
    document.getElementById("demo2").style.display = "none";
    var x = "Ẩn thành công";
    alert(x)
}

function myFunction1(){
    // Hiện dòng text trong thẻ
    document.getElementById("demo2").style.display = "block";
}

function usingDocumentWrite(){
    document.write("OKAY BN ƠI");
}

// Dùng window.print để in trang web thành file
function usingPrint(){
    print();
}


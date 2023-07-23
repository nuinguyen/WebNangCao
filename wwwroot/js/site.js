// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// fetch('/Controller/Action', {
//     method: 'POST',
//     headers: {
//         'Content-Type': 'application/json'
//     },
//     body: JSON.stringify({ param1: 'value1', param2: 'value2' })
// }).then(response => {
//     if (response.ok) {
//         // Xử lý kết quả trả về từ endpoint
//         response.json().then(data => {
//             console.log(data);
//         });
//     } else {
//         // Xử lý lỗi nếu có
//         console.error('Error:', response.statusText);
//     }
// }).catch(error => {
//     console.error('Error:', error);
// });
// //////////////////////////////////////////////////////////////////////////////////////////
// var xhr = new XMLHttpRequest();
// xhr.open('POST', '/Controller/Action');
// xhr.setRequestHeader('Content-Type', 'application/json');
// xhr.onload = function() {
//     if (xhr.status === 200) {
//         // Xử lý kết quả trả về từ endpoint
//         console.log(xhr.responseText);
//     } else {
//         // Xử lý lỗi nếu có
//         console.error('Error:', xhr.statusText);
//     }
// };
// xhr.onerror = function() {
//     console.error('Error:', xhr.statusText);
// };
// xhr.send(JSON.stringify({ param1: 'value1', param2: 'value2' }));
// [HttpPost]
// public IActionResult Action([FromBody] MyModel model)
// {
//     // Xử lý dữ liệu và trả về kết quả
//     return Ok(new { result = "success" });
// }
var inputField = document.getElementById('myInputField');
inputField.addEventListener('keydown', function(event) {
  var key = event.key;
  if (!/^[a-zA-Z0-9 ]$/.test(key)) {
    event.preventDefault();
  }
});
$('#myInputField').on('keydown', function(event) {
    var key = event.key;
    if (!/^[a-zA-Z0-9 ]$/.test(key)) {
      event.preventDefault();
    }
  });
  $('#myInputField').on('keydown', function(event) {
    if (event.key !== '.' && !/^[0-9]$/.test(event.key)) {
      event.preventDefault();
    }
  });
  $('#myInputField').on('keydown', function(event) {
    if (!/^[a-zA-Z]$/.test(event.key)) {
      event.preventDefault();
    }
  });
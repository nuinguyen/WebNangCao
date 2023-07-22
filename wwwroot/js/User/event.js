$(document).ready(function () {
  //Motal sattus
  $(".update-motel-status").click(function () {
    // console.log({ id: $(this).data("id") });
    // console.log({ status: $(this).data("status") });
    $.ajax({
      url: "UpdateStatus",
      type: "POST",
      data: { 
        id: $(this).data("id") ,
        status: $(this).data("status") 
      }
    }).done(function (response) {
      if (response == "success") {
        alert("Thành công");
      } else {
        alert("Thất bại");

      }
    });
  })





  //Post
  //City
  $('#city').change(function () {
    $.ajax({
      url: 'Home/District',
      type: "POST",
      data: {
        CityId: $(this).val()
      },
      success: function (data) {
        $('#district').html(`
      <option value="">-- Chọn Quận, huyện --</option>  
      ${data}   
     `);
        generateAddress();

      }
    });
  });
  //District
  $('#district').change(function () {
    $.ajax({
      url: 'Home/Village',
      type: "POST",
      data: {
        districtId: $(this).val()
      },
      success: function (data) {
        $('#village').html(`
        <option value="">-- Chọn Thị trấn, xã --</option>  
        ${data}   
       `);
        generateAddress();

      }
    });
  });
  $('#village').change(function () {
    generateAddress();
  });

  function generateAddress() {
    var city = $('#city option:selected').text();
    var district = $('#district option:selected').text();
    var village = $('#village option:selected').text();
    var address = village + ', ' + district + ', ' + city;
    $('#Address_post').text(address);
  }






  //Thêm/ xóa yêu thích
  $(".motel-favourite").click(function () {
    if ($(this).hasClass("active")) {
      $(this).removeClass("active");
      var status = 0;
    } else {
      $(this).addClass("active");
      var status = 1;
    }
    $.ajax({
      url: "Home/Favourite",
      type: "POST",
      data: {
        id: $(this).data("id"),
        status: status
      }
    }).done(function (response) {
      if (response.status == "success") {
        $('.badge-favourite').text(response.count);
      } else {
        alert("Thất bại");

      }
    });
  })
  ///Check dữ liệu
  $(function () {
    $('form.form-signup').submit(function () {
      var name = $('input[name="Name"]').val();
      var email = $('input[name="Email"]').val();
      var phone = $('input[name="Phone"]').val();
      var password = $('input[name="Password"]').val();
      var password_again = $('input[name="Password_again"]').val();

      // Kiểm tra tên không được để trống
      if (name == '') {
        $('form.form-signup .form-signup-alert').removeClass('hide');
        $('form.form-signup .form-signup-alert').text('Vui lòng nhập tên');
        return false;
      }

      // Kiểm tra email không được để trống và phải đúng định dạng
      if (email == '') {
        $('form.form-signup .form-signup-alert').removeClass('hide');
        $('form.form-signup .form-signup-alert').text('Vui lòng nhập email');
        return false;
      } else if (!email.match(/^([\w.%+-]+)@([\w-]+\.)+([\w]{2,})$/i)) {
        // if (Regex.IsMatch(email, @"^[^@\s]+@gmail\.com$"))
        $('form.form-signup .form-signup-alert').removeClass('hide');
        $('form.form-signup .form-signup-alert').text('Email không hợp lệ, vui lòng nhập lại');
        return false;
      }

      // Kiểm tra số điện thoại không được để trống và phải đúng định dạng
      if (phone == '') {
        $('form.form-signup .form-signup-alert').removeClass('hide');
        $('form.form-signup .form-signup-alert').text('Vui lòng nhập số điện thoại');
        return false;
      } else if (!phone.match(/^0([9]|[1-8]\d{1,10})$/)) {
        $('form.form-signup .form-signup-alert').removeClass('hide');
        $('form.form-signup .form-signup-alert').text('Số điện thoại không hợp lệ, vui lòng nhập lại');
        return false;
      }

      // Kiểm tra mật khẩu không được để trống và phải trùng nhau
      if (password == '') {
        $('form.form-signup .form-signup-alert').removeClass('hide');
        $('form.form-signup .form-signup-alert').text('Vui lòng nhập mật khẩu');
        return false;
      } else if (password_again == '') {
        $('form.form-signup .form-signup-alert').removeClass('hide');
        $('form.form-signup .form-signup-alert').text('Vui lòng nhập lại mật khẩu');
        return false;
      } else if (password != password_again) {
        $('form.form-signup .form-signup-alert').removeClass('hide');
        $('form.form-signup .form-signup-alert').text('Mật khẩu không trùng khớp, vui lòng nhập lại');
        return false;
      }
    });
  });
})
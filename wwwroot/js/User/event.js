$(document).ready(function () {
    //DELETE NHAN VIEN
    $(".motel-favourite").click(function () {
        $(this).children('i').css({"color": "red"});
        console.log({ id: $(this).data("id") });
        let idTb = $(this).data("id");
        $.ajax({
            url: "Home/Favourite",
            type: "POST",
            data: { id: $(this).data("id") }
        }).done(function (response) {
            if (response.status == "success") {
                $('.badge-favourite').text(response.count);
            } else {
                alert("Thất bại");

            }
        });
    })
})
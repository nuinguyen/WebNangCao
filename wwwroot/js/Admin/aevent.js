$(document).ready(function(){
    // DELETE NHAN VIEN
    $("._delete_staff").click(function(){
        console.log( {id: $(this).data("id")});
        let isDelete = confirm("Bạn có chắc chắn muốn xóa nhân viên ?");
        let idTb = $(this).data("id");
        if(isDelete) {
            $.ajax({
                url: "Delete",
                type: "POST",
                data: {id: $(this).data("id")}
            }).done(function(response) {
                if(response=="success") {
                    alert("Thành công");
                    $('#staff'+idTb).remove();
                } else {
                    alert("Thất bại");
                }
            });
        }
    })

    // DELETE Motel
    $("._delete_motel").click(function(){
        console.log( {id: $(this).data("id")});
        let isDelete = confirm("Bạn có chắc chắn muốn xóa địa điểm này ?");
        let idTb = $(this).data("id");
        if(isDelete) {
            $.ajax({
                url: "Delete",
                type: "POST",
                data: {id: $(this).data("id")}
            }).done(function(response) {
                if(response=="success") {
                    alert("Thành công");
                    $('#motel'+idTb).remove();
                } else {
                    alert("Thất bại");
                }
            });
        }
    })

    //  DELETE KHU VỰC
     $("._delete_location").click(function(){
        console.log( {id: $(this).data("id")});
        let isDelete = confirm("Are you sure delete motel");
        let idTb = $(this).data("id");
        if(isDelete) {
            $.ajax({
                url: "Delete",
                type: "POST",
                data: {id: $(this).data("id")}
            }).done(function(response) {
                if(response=="success") {
                    alert("Thành công");
                    $('#location'+idTb).remove();
                } else {
                    alert("Thất bại");

                }
            });
        }
    })
})
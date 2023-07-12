$(document).ready(function(){
    //DELETE NHAN VIEN
    $("._delete_staff").click(function(){
        console.log( {id: $(this).data("id")});
        let isDelete = confirm("Are you sure you want to delete");
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
     //DELETE KHU VỰC
     $("._delete_location").click(function(){
        console.log( {id: $(this).data("id")});
        let isDelete = confirm("Are you sure you want to delete");
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
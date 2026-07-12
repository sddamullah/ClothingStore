

$(document).ready(function () {

    LoadCategoryGrid();

});

function LoadCategoryGrid() {

    $("#CategoryGrid").load("/Category/CategoryGrid");

}
function OpenCategoryPopup() {

    $("#_Create").load("/Category/Create");

    $("#categoryPopup").css("display", "flex");
}

function CloseCategoryPopup() {

    $("#categoryPopup").hide();
} 
function AddCategory() {

    var data = {
        varName: $("#varName").val(),
        varDescription: $("#varDescription").val(),
        IsActive: $("#chkIsActive").is(":checked") ? 1 : 0
    };

    $.ajax({

        url: "/Category/AddCategory",
        type: "POST",
        data: data,

        success: function (response) {

            if (response.success) {

                $("#categoryModal").hide();
                LoadCategoryGrid();
                alert(response.message);

            } else {

                alert(response.message);

            }

        },

        error: function () {

            alert("Something went wrong.");

        }

    });

}
    $("#chkIsActive").on("change", function () {
        $("#IsActive").val(this.checked ? 1 : 0);
    });

});
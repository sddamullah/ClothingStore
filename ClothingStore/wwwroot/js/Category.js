$(document).ready(function () {
    LoadCategoryGrid();
});

function LoadCategoryGrid() {
    $("#CategoryGrid").load("/Category/CategoryGrid");
}

function OpenCategoryPopup() {

    $("#_Create").load("/Category/Create", function () {
        $("#categoryPopup").css("display", "flex");
    });
}

function CloseCategoryPopup() {
    $("#categoryPopup").hide();
}

function AddCategory() {

    var data = {
        varName: $("#varName").val().trim(),
        varDescription: $("#varDescription").val().trim(),
        IsActive: $("#chkIsActive").is(":checked")
    };

    $.ajax({
        url: "/Category/AddCategory",
        type: "POST",
        data: data,
        success: function (response) {

            if (response.success) {

                alert(response.message);

                $("#categoryPopup").hide();

                $("#varName").val("");
                $("#varDescription").val("");
                $("#chkIsActive").prop("checked", true);

                LoadCategoryGrid();

            } else {

                alert(response.message);

            }
        },
        error: function (xhr) {

            console.log(xhr.responseText);
            alert("Something went wrong.");

        }
    });
}
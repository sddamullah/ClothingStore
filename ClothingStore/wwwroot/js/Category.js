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
               
                Empty();
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
function Empty() {
 
    $("#varName").val("");
    $("#varDescription").val("");
    $("#chkIsActive").prop("checked", true);
}  
function OpenEditCategoryPopup(id) {
    $("#_Edit").load("/Category/GetCategoryById?id=" + id, function () {
        $("#editCategoryPopup").css("display", "flex");
    });
}

function CloseEditCategoryPopup() {
    $("#editCategoryPopup").hide();
}

function UpdateCategory() {
    var data = {
        intSeqId: $("#editIntSeqId").val(),
        varName: $("#editVarName").val().trim(),
        varDescription: $("#editVarDescription").val().trim(),
        IsActive: $("#editChkIsActive").is(":checked")
    };

    $.ajax({
        url: "/Category/UpdateCategory",
        type: "POST",
        data: data,
        success: function (response) {
            if (response.success) {
                alert(response.message);
                $("#editCategoryPopup").hide();
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
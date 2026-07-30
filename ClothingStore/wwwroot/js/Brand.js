// ==========================================
// BRAND CRUD - JavaScript
// ==========================================

let selectedCreateFile = null;
let selectedEditFile = null;

$(document).ready(function () {
    LoadBrandGrid();
});


// ------------------------------------------
// GRID
// ------------------------------------------

function LoadBrandGrid() {
    $.ajax({
        url: '/Brand/GetBrandGrid',
        type: 'GET',
        success: function (data) {
            $('#brandGridContainer').html(data);
        }
    });
}


// ------------------------------------------
// CREATE POPUP
// ------------------------------------------

function OpenCreateBrandPopup() {
    $.ajax({
        url: '/Brand/Create',
        type: 'GET',
        success: function (data) {

            selectedCreateFile = null;

            $('#brandPopupContainer').html(data);
            $("#Brandpopup").show();
            // Removed unobtrusive validation

            InitLogoDropZone('logoDropZone', 'logoFile', 'logoPreview', false);
        }
    });
}


// ------------------------------------------
// EDIT POPUP
// ------------------------------------------

function OpenEditBrandPopup(id) {
    $.ajax({
        url: '/Brand/Edit',
        type: 'GET',
        data: { id: id },
        success: function (data) {

            selectedEditFile = null;

            $('#brandPopupContainer').html(data);
            $("#Brandpopup").show();
            // Removed unobtrusive validation

            InitLogoDropZone('editLogoDropZone', 'editLogoFile', 'editLogoPreview', true);
        }
    });
}


// ------------------------------------------
// LOGO DROP ZONE
// ------------------------------------------

function InitLogoDropZone(zoneId, inputId, previewId, isEdit) {

    var dropZone = document.getElementById(zoneId);
    var fileInput = document.getElementById(inputId);

    if (!dropZone || !fileInput)
        return;

    fileInput.addEventListener('change', function (e) {

        var file = e.target.files[0];

        if (file) {
            SetLogoFile(file, previewId, isEdit);
        }

    });

    dropZone.addEventListener('dragover', function (e) {
        e.preventDefault();
    });

    dropZone.addEventListener('drop', function (e) {

        e.preventDefault();

        var file = e.dataTransfer.files[0];

        if (file) {

            fileInput.files = e.dataTransfer.files;

            SetLogoFile(file, previewId, isEdit);

        }

    });

}


function SetLogoFile(file, previewId, isEdit) {

    if (isEdit)
        selectedEditFile = file;
    else
        selectedCreateFile = file;

    var reader = new FileReader();

    reader.onload = function (e) {

        var preview = document.getElementById(previewId);

        if (preview) {

            preview.src = e.target.result;
            preview.style.display = "inline-block";

        }

    };

    reader.readAsDataURL(file);

}


// ------------------------------------------
// SAVE
// ------------------------------------------

function AddBrand() {

    // Manual Validation

    if ($("#varBrandName").val().trim() == "") {

        alert("Brand Name is required.");

        $("#varBrandName").focus();

        return;

    }

    if (!selectedCreateFile) {

        $("#logoValidationMsg").text("Logo is required");

        return;

    }
    else {

        $("#logoValidationMsg").text("");

    }


    var formData = new FormData();

    formData.append("varBrandName", $("#varBrandName").val());

    formData.append("isActive", $("#chkIsActive").is(":checked"));

    formData.append("logoFile", selectedCreateFile);


    $.ajax({

        url: "/Brand/Create",

        type: "POST",

        data: formData,

        contentType: false,

        processData: false,

        success: function (response) {

            if (response.success) {

                selectedCreateFile = null;

                $("#brandPopupContainer").html("");

                LoadBrandGrid();
                $("#Brandpopup").hide();
            }
            else {

                alert(response.message);

            }

        }

    });

}



// ------------------------------------------
// UPDATE
// ------------------------------------------

function UpdateBrand() {

    // Manual Validation

    if ($("#editVarBrandName").val().trim() == "") {

        alert("Brand Name is required.");

        $("#editVarBrandName").focus();

        return;

    }

    var formData = new FormData();

    formData.append("intSeqId", $("#editIntSeqId").val());

    formData.append("varBrandName", $("#editVarBrandName").val());

    formData.append("isActive", $("#editChkIsActive").is(":checked"));

    if (selectedEditFile) {

        formData.append("logoFile", selectedEditFile);

    }


    $.ajax({

        url: "/Brand/Edit",

        type: "POST",

        data: formData,

        contentType: false,

        processData: false,

        success: function (response) {

            if (response.success) {

                selectedEditFile = null;
                $("#Brandpopup").hide();
                $("#brandPopupContainer").html("");

                LoadBrandGrid();

            }
            else {

                alert(response.message);

            }

        }

    });

}



// ------------------------------------------
// DELETE
// ------------------------------------------

function DeleteBrand(id) {

    if (!confirm("Are you sure you want to delete this brand?"))
        return;

    $.ajax({

        url: "/Brand/Delete",

        type: "POST",

        data: { id: id },

        success: function (response) {

            if (response.success) {

                LoadBrandGrid();

            }
            else {

                alert(response.message);

            }

        }

    });

} function CloseBrandpopup() {
    $("#Brandpopup").hide();
    $("#brandPopupContainer").html("");
}
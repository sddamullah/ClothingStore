// ==========================================
// GENDER CRUD - JavaScript
// Uses manual validation (no jQuery Unobtrusive Validation)
// ==========================================

$(document).ready(function () {
    LoadGenderGrid();
});


// ------------------------------------------
// GRID
// ------------------------------------------

function LoadGenderGrid() {
    $.ajax({
        url: '/Gender/GetGenderGrid',
        type: 'GET',
        success: function (data) {
            $('#genderGridContainer').html(data);
        }
    });
}


// ------------------------------------------
// CREATE POPUP
// ------------------------------------------

function OpenCreateGenderPopup() {
    $.ajax({
        url: '/Gender/Create',
        type: 'GET',
        success: function (data) {
            $('#genderPopupContainer').html(data);
        }
    });
}


// ------------------------------------------
// EDIT POPUP
// ------------------------------------------

function OpenEditGenderPopup(id) {
    $.ajax({
        url: '/Gender/Edit',
        type: 'GET',
        data: { id: id },
        success: function (data) {
            $('#genderPopupContainer').html(data);
        }
    });
}


// ------------------------------------------
// MANUAL VALIDATION
// ------------------------------------------

function ValidateCreateGenderForm() {
    var isValid = true;

    var genderName = $('#genderName').val();
    if (!genderName || genderName.trim() === '') {
        $('#genderNameError').text('Gender Name is required');
        isValid = false;
    } else {
        $('#genderNameError').text('');
    }

    var displayOrder = parseInt($('#displayOrder').val());
    if (!displayOrder || displayOrder <= 0) {
        $('#displayOrderError').text('Display Order is required and must be greater than 0');
        isValid = false;
    } else {
        $('#displayOrderError').text('');
    }

    return isValid;
}

function ValidateEditGenderForm() {
    var isValid = true;

    var genderName = $('#editGenderName').val();
    if (!genderName || genderName.trim() === '') {
        $('#editGenderNameError').text('Gender Name is required');
        isValid = false;
    } else {
        $('#editGenderNameError').text('');
    }

    var displayOrder = parseInt($('#editDisplayOrder').val());
    if (!displayOrder || displayOrder <= 0) {
        $('#editDisplayOrderError').text('Display Order is required and must be greater than 0');
        isValid = false;
    } else {
        $('#editDisplayOrderError').text('');
    }

    return isValid;
}


// ------------------------------------------
// SAVE (CREATE)
// ------------------------------------------

function AddGender() {
    if (!ValidateCreateGenderForm()) {
        return;
    }

    var data = {
        GenderName: $('#genderName').val(),
        DisplayOrder: $('#displayOrder').val(),
        IsActive: $('#chkIsActive').is(':checked')
    };

    $.ajax({
        url: '/Gender/Create',
        type: 'POST',
        data: data,
        success: function (response) {
            if (response.success) {
                $('#genderPopupContainer').html('');
                LoadGenderGrid();
            } else {
                alert(response.message);
            }
        }
    });
}


// ------------------------------------------
// UPDATE (EDIT)
// ------------------------------------------

function UpdateGender() {
    if (!ValidateEditGenderForm()) {
        return;
    }

    var data = {
        GenderId: $('#editGenderId').val(),
        GenderName: $('#editGenderName').val(),
        DisplayOrder: $('#editDisplayOrder').val(),
        IsActive: $('#editChkIsActive').is(':checked')
    };

    $.ajax({
        url: '/Gender/Edit',
        type: 'POST',
        data: data,
        success: function (response) {
            if (response.success) {
                $('#genderPopupContainer').html('');
                LoadGenderGrid();
            } else {
                alert(response.message);
            }
        }
    });
}


// ------------------------------------------
// DELETE
// ------------------------------------------

function DeleteGender(id) {
    if (!confirm('Are you sure you want to delete this gender?')) {
        return;
    }

    $.ajax({
        url: '/Gender/Delete',
        type: 'POST',
        data: { id: id },
        success: function (response) {
            if (response.success) {
                LoadGenderGrid();
            } else {
                alert(response.message);
            }
        }
    });
}

// ==========================================
// PRODUCT IMAGE CRUD - JavaScript
// Uses manual validation (no jQuery Unobtrusive Validation)
// ==========================================

let selectedCreateImageFile = null;
let selectedEditImageFile = null;

$(document).ready(function () {
    LoadProductImagesGrid();
});


// ------------------------------------------
// GRID
// ------------------------------------------

function LoadProductImagesGrid() {
    $.ajax({
        url: '/ProductImage/GetProductImagesGrid',
        type: 'GET',
        success: function (data) {
            $('#productImageGridContainer').html(data);
        }
    });
}


// ------------------------------------------
// CREATE POPUP
// ------------------------------------------

function OpenCreateProductImagePopup() {
    $.ajax({
        url: '/ProductImage/Create',
        type: 'GET',
        success: function (data) {
            selectedCreateImageFile = null;
            $('#productImagePopupContainer').html(data);
            $('#ProductImagePopup').show();
            InitImageDropZone('imageDropZone', 'imageFile', 'imagePreview', false);

            // Auto-fetch the next Display Order for this product
            // as soon as the user enters a Product Id (still editable after)
            $('#productId').on('blur', function () {
                var productId = parseInt($(this).val());
                if (productId > 0) {
                    FetchNextDisplayOrder(productId, '#displayOrder');
                }
            });
        }
    });
}


// ------------------------------------------
// EDIT POPUP
// ------------------------------------------

function OpenEditProductImagePopup(id) {
    $.ajax({
        url: '/ProductImage/Edit',
        type: 'GET',
        data: { id: id },
        success: function (data) {
            selectedEditImageFile = null;
            $('#productImagePopupContainer').html(data);
            $('#ProductImagePopup').show();
            InitImageDropZone('editImageDropZone', 'editImageFile', 'editImagePreview', true);
        }
    });
}


// Calls the server to get the next Display Order number for a given Product Id
function FetchNextDisplayOrder(productId, targetInputSelector) {
    $.ajax({
        url: '/ProductImage/GetNextDisplayOrder',
        type: 'GET',
        data: { productId: productId },
        success: function (response) {
            $(targetInputSelector).val(response.nextOrder);
        }
    });
}


// ------------------------------------------
// IMAGE DROP ZONE (shared by Create + Edit)
// ------------------------------------------

function InitImageDropZone(zoneId, inputId, previewId, isEdit) {
    var dropZone = document.getElementById(zoneId);
    var fileInput = document.getElementById(inputId);

    if (!dropZone || !fileInput) {
        return;
    }

    fileInput.addEventListener('change', function (e) {
        var file = e.target.files[0];
        if (file) {
            SetImageFile(file, previewId, isEdit);
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
            SetImageFile(file, previewId, isEdit);
        }
    });
}

function SetImageFile(file, previewId, isEdit) {
    if (isEdit) {
        selectedEditImageFile = file;
    } else {
        selectedCreateImageFile = file;
    }

    var reader = new FileReader();
    reader.onload = function (e) {
        var preview = document.getElementById(previewId);
        if (preview) {
            preview.src = e.target.result;
            preview.style.display = 'inline-block';
        }
    };
    reader.readAsDataURL(file);
}


// ------------------------------------------
// MANUAL VALIDATION
// (only Product Id and Display Order are validated - Image is optional)
// ------------------------------------------

function ValidateCreateProductImageForm() {
    var isValid = true;

    var productId = parseInt($('#productId').val());
    if (!productId || productId <= 0) {
        $('#productIdError').text('Product Id is required and must be greater than 0');
        isValid = false;
    } else {
        $('#productIdError').text('');
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

function ValidateEditProductImageForm() {
    var isValid = true;

    var productId = parseInt($('#editProductId').val());
    if (!productId || productId <= 0) {
        $('#editProductIdError').text('Product Id is required and must be greater than 0');
        isValid = false;
    } else {
        $('#editProductIdError').text('');
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

function AddProductImage() {
    if (!ValidateCreateProductImageForm()) {
        return;
    }

    var formData = new FormData();
    formData.append('ProductId', $('#productId').val());
    formData.append('DisplayOrder', $('#displayOrder').val());
    formData.append('IsMain', $('#chkIsMain').is(':checked'));

    // Image is optional - only attach if the user picked/dropped one
    if (selectedCreateImageFile) {
        formData.append('imageFile', selectedCreateImageFile);
    }

    $.ajax({
        url: '/ProductImage/Create',
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response.success) {
                selectedCreateImageFile = null;
                $('#productImagePopupContainer').html('')
                $('#ProductImagePopup').hide()
                LoadProductImagesGrid();
            } else {
                alert(response.message);
            }
        }
    });
}


// ------------------------------------------
// UPDATE (EDIT)
// ------------------------------------------

function UpdateProductImage() {
    if (!ValidateEditProductImageForm()) {
        return;
    }

    var formData = new FormData();
    formData.append('ImageId', $('#editImageId').val());
    formData.append('ProductId', $('#editProductId').val());
    formData.append('DisplayOrder', $('#editDisplayOrder').val());
    formData.append('IsMain', $('#editChkIsMain').is(':checked'));

    if (selectedEditImageFile) {
        formData.append('imageFile', selectedEditImageFile);
    }

    $.ajax({
        url: '/ProductImage/Edit',
        type: 'POST',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            if (response.success) {
                selectedEditImageFile = null;
                $('#productImagePopupContainer').html('');
                $('#ProductImagePopup').hide();
                LoadProductImagesGrid();
            } else {
                alert(response.message);
            }
        }
    });
}


// ------------------------------------------
// DELETE
// ------------------------------------------

function DeleteProductImage(id) {
    if (!confirm('Are you sure you want to delete this image?')) {
        return;
    }

    $.ajax({
        url: '/ProductImage/Delete',
        type: 'POST',
        data: { id: id },
        success: function (response) {
            if (response.success) {
                LoadProductImagesGrid();
            } else {
                alert(response.message);
            }
        }
    });
}
function CloseProductImagePopup() {
    $('#ProductImagePopup').hide()
}
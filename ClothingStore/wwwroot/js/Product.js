$(document).ready(function () {
    LoadProductGrid();
});

function LoadProductGrid() {
    $("#ProductGrid").load("/Product/ProductGrid");
}


function OpenProductPopup() {

    $("#_CreateProduct").load("/Product/Create", function () {

        $("#productPopup").css("display", "flex");

    });

}
function DeleteProduct(id) {

    $.ajax({

        url: "/Product/DeleteProduct?id=" + id,

        type: "DELETE",

        success: function (response) {

            if (response.success) {

                alert(response.message);

                LoadProductGrid();

            }
            else {

                alert(response.message);

            }

        },

        error: function (xhr) {

            console.log(xhr.responseText);
            alert("Something went wrong.");

        }

    });

}
function CloseProductPopup() {

    $("#productPopup").hide();

}
function AddProduct() {

    var formData = new FormData();

    formData.append("intCategoryId",
        $("#intCategoryId").val()
    );

    formData.append("varName",
        $("#varName").val()
    );

    formData.append("varProductCode",
        $("#varProductCode").val()
    );

    formData.append("varDescription",
        $("#varDescription").val()
    );

    formData.append("flPrice",
        $("#flPrice").val()
    );

    formData.append("flDiscountPrice",
        $("#flDiscountPrice").val()
    );

    formData.append("intQuantity",
        $("#intQuantity").val()
    );

    formData.append("varBrand",
        $("#varBrand").val()
    );

    formData.append("varSize",
        $("#varSize").val()
    );

    formData.append("varColor",
        $("#varColor").val()
    );


    var file = $("#ImageFile")[0].files[0];

    if (file) {
        formData.append("ImageFile", file);
    }


    formData.append("isFeatured",
        $("#isFeatured").is(":checked")
    );

    formData.append("isActive",
        $("#isActive").is(":checked")
    );


    $.ajax({

        url: "/Product/AddProduct",

        type: "POST",

        data: formData,

        contentType: false,

        processData: false,


        success: function (response) {

            alert(response.message);

            LoadProductGrid();

        },


        error: function (xhr) {

            console.log(xhr.responseText);

        }

    });

}


function OpenEditProductPopup(id) {

   
    $("#_EditProduct").load("/Product/Edit/" + id, function () {

        $("#productEditPopup").css("display", "flex");

    });

}



function CloseEditProductPopup() {

    $("#productEditPopup").hide();

}
function UpdateProduct() {


    var formData = new FormData();


    formData.append("intSeqId",
        $("#intSeqId").val());


    formData.append("intCategoryId",
        $("#intCategoryId").val());


    formData.append("varName",
        $("#varName").val());


    formData.append("flPrice",
        $("#flPrice").val());


    formData.append("intQuantity",
        $("#intQuantity").val());



    var file = $("#ImageFile")[0].files[0];


    if (file) {
        formData.append("ImageFile", file);
    }



    $.ajax({

        url: "/Product/UpdateProduct",

        type: "POST",

        data: formData,

        contentType: false,

        processData: false,


        success: function (response) {

            alert(response.message);

            $("#productEditPopup").hide();

            LoadProductGrid();

        }

    });


}
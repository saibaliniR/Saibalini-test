$(document).ready(function () {
    $("#tblProducts tbody tr").remove();
   // var rawdata = { 'Category': 'Electronics' };
    $.ajax({
        type: "GET",
        url: "/Product/Product",
        //data: rawdata,
        dataType: 'json',
        success: function (data) {
            var items = '';
            $.each(data, function (i, item) {
                var rows = "<tr>"
                    + "<td class='prtoductData' data-productId='"+item.Id+"'>" + item.Id + "</td>"
                    + "<td class='prtoductData' data-productName='" + item.Name + "'>" + item.Name + "</td>"
                    + "<td class='prtoductData' data-productPrice='" + item.Price + "'>" + item.Price + "</td>"
                    + "<td class='prtoductData' data-productDescription='" + item.Description + "'>" + item.Description + "</td>"
                    + "<td> <a class = 'editIcon' href='javascript: void (0);'  data-productId='" + item.Id + "'><i class='fa fa-edit'></i></a> &nbsp; &nbsp; <a class ='deleteIcon' href='javascript: void (0);'  data-productId='" + item.Id +"'><i class='fa fa-trash'></i></a></td>"
                    + "</tr>";
                $('#tblProducts tbody').append(rows);
            });
        },
        error: function (errorData) {
            //respone will be returned here
            alert(errorData);
            var ErrorResponse = jQuery.parseJSON(errorData.responseText);
            alert("Message: " + ErrorResponse.Message);
            alert("StackTrace: " + ErrorResponse.StackTrace);
            alert("ExceptionType: " + ErrorResponse.ExceptionType);
        }
    });

    $(".add-product").click ( function () {
        $(".addNewProduct").show();
        $(".productDetail").hide();
    })
    $('#productList').click(function () {
        $(".addNewProduct").hide();
        $(".productDetail").show();
    });
    $('#add-product-form').submit(function (event) {
        event.preventDefault();
        var formData = {
            name: $("#productName").val(),
            price: $("#productPrice").val(),
            description: $("#productDescription").val(),
        };

        $.ajax({
            type: 'POST',
            url: '/Product/Product',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            data: JSON.stringify(formData),
            success: function (response) {
                alert("Product Added Successfully!")
                window.location.reload();
            }
        });

    });
    $('#tblProducts').on('click', '.editIcon', function () {
        var $tr = $(this).closest('tr');
        var productId = $tr.find('td[data-productId]').data('productid');
        var productName = $tr.find('td[data-productName]').data('productname');
        var productPrice = $tr.find('td[data-productPrice]').data('productprice');
        var productDescription = $tr.find('td[data-productDescription]').data('productdescription');
        $(".productDetail").hide();
        $(".addNewProduct").show();
        $("#productName").html(productName);
        $("#productPrice").html(productPrice);
        $("#productDescription").html(productDescription);
        $("#submit-product").hide();
        $("#edit-product").show();
    });
});
$(document).ready(function () {
    $('#Category').val(0);
    $('#dpProduct').val(0);
    $('#Category').change(function () {
        var CtId = $("#Category").val();
        GetProduct(CtId);
    });
    $("#btnAddToList").click(function () {
        AddToCart();
    });
    $("#btnConfirmOrder").click(function () {
        SaveOrder();
    });
});
function SaveOrder() {
    var objOrderModel = {};
    var ListOfOrderDetailsModel = new Array();
    objOrderModel.Location = $("#txtLocation").val();
    objOrderModel.CustomerName = $("#txtCustName").val();
    objOrderModel.OrderDate = $("#txtDeliveryDate").val();

    $("#tblOrderListCart").find("tr:gt(0)").each(function () {
        var OrderDetailsModel = {};
        OrderDetailsModel.ItemId = parseFloat($(this).find("td:eq(0)").text());
        OrderDetailsModel.Quantity = parseFloat($(this).find("td:eq(2)").text());
        ListOfOrderDetailsModel.push(OrderDetailsModel);
    });
    objOrderModel.ListOfOrderDetailsModel = ListOfOrderDetailsModel;
    $.ajax({
        async: true,
        type: 'POST',
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(objOrderModel),
        url: '/Billing/SaveWholeSaleOrder',
        success: function (data) {
            alert(data);
        },
        error: function () {
            alert("There is some problem to Register your Order");
        }
    });

}
function GetProduct(CtId) {
    $.ajax({
        async: true,
        type: 'GET',
        datatype: 'JSON',
        contentType: 'application/json; charset=utf-8',
        data: { CategoryId: CtId },
        url: '/Billing/GetProducts',
        success: function (data) {
            $('#dpProduct').empty();
                console.log(data.length);
            for (var i = 0; i < data.length; i++) {
                var objProd = new Option(data[i].ProductName, data[i].ProductId);
                $('#dpProduct').append(objProd);
                $('#dpProduct').val(0);
            }
        }
    });
}
function AddToCart() {
    var tblCartList = $("#tblOrderListCart");
    var Quantity = $("#txtQuantity").val();
    var PrtId = $("#dpProduct").val();
    var PrtName = $("#dpProduct option:selected").text();
    var ProductList = "<tr><td hidden>" +
        PrtId +
        "</td><td>" +
        PrtName +
        "</td><td>" +
        Quantity +
        "</td><td><input type='button' value='Remove' name='remove' class='btn btn-sm btn-danger' onclick='RemoveProduct(this)'/> </tr></tr>";
    tblCartList.append(ProductList);
    Refresh();
}
function RemoveProduct(PtrId) {
    $(PtrId).closest('tr').remove();
}
function Refresh() {
    $('#PrtId').val(0);
    $("#Category").val(0);
    $("#dpProduct").val(0);
    $("#txtQuantity").val(0);
    $("#txtDiscount").val(0);
}
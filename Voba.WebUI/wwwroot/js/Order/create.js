var orderList = [];
$(document).ready(function () {

    //$("#orderForm").on("submit", function (e) {
    //    e.preventDefault(); // Formun normal submit olmasını engelle

    //    // Formdaki verileri toplayacağız (Kullanıcı, Firma, Adres vb.)
    //    // Burada ürün listesini de toplamalısın, seninki şu an orderList ise onu kullanabiliriz

    //    // Öncelikle dropdown ve diğer inputlardan değerleri al
    //    var orderTypeId = parseInt($("#orderTypeId").val());
    //    var userId = parseInt($("#userId").val());
    //    var companyName = $("#companyName").val();
    //    var shippingAddress = $("#shippingAddress").val();

    //    // Eğer ürün listesini manuel yönetiyorsan, onu kullan
    //    // Yoksa tablodan toplamak için şu kodu kullanabilirsin:

    //    var orderProducts = [];
    //    $("#orderRows tr.order-row").each(function (index, row) {
    //        orderProducts.push({
    //            ModelName: $(row).find('input[name^="orderProductsmodelName"]').val(),
    //            Color: $(row).find('input[name^="orderProductscolor"]').val(),
    //            Quantity: parseInt($(row).find('input[name^="orderProductsquantity"]').val()) || 0,
    //            Length: parseFloat($(row).find('input[name^="orderProductslength"]').val()) || 0,
    //            Width: parseFloat($(row).find('input[name^="orderProductswidth"]').val()) || 0,
    //            SquareMeter: parseFloat($(row).find('input[name^="orderProductssquareMeter"]').val()) || 0,
    //            SquareMeterPrice: parseFloat($(row).find('input[name^="orderProductssquareMeterPrice"]').val()) || 0,
    //            Price: parseFloat($(row).find('input[name^="orderProductsprice"]').val()) || 0,
    //            Description: $(row).find('input[name^="orderProductsdescription"]').val(),
    //        });
    //    });

    //    // Tüm veriyi tek nesnede topla
    //    var orderData = {
    //        UserId: userId,
    //        CompanyName: companyName,
    //        ShippingAddress: shippingAddress,
    //        OrderTypeId: orderTypeId,
    //        OrderProducts: orderProducts
    //    };

    //    // AJAX ile backend'e POST et
    //    $.ajax({
    //        url: '/Admin/Orders/CreatePost',
    //        method: 'POST',
    //        contentType: 'application/json',
    //        data: JSON.stringify(orderData),
    //        success: function (response) {
    //            alert("Sipariş başarıyla kaydedildi.");
    //            window.location.href = '/Admin/Orders'; // Başarılıysa liste sayfasına dön
    //        },
    //        error: function (err) {
    //            alert("Bir hata oluştu.");
    //            console.error(err);
    //        }
    //    });
    //});

});

function addProductList() {
    var orderProduct = {
        quantity: $('#quantity').val(),
        length: $('#length').val(),
        width: $('#width').val(),
        color: $('#color').val(),
        /*productId: $('#productCode').val(),*/
    };
    

    orderList.push(orderProduct); // Bu artık temiz bir JS nesnesi
    /*$('#orderModal').modal('hide');*/
}
//function saveOrders() {
//    console.log(orderList); // orderList'i konsola yazdır)
//    var order = {

//        shippingAddress: $('#shippingAddress').val(),
//        companyName: $('#companyName').val(),
//        userId: $('#userId').val(),
//        orderProducts: orderList,
//    };
//    $.ajax({
//        url: '/admin/Orders/CreatePost', // Controller yolun burası olmalı
//        method: 'POST',
//        contentType: 'application/json; charset=UTF-8' ,
//        data: JSON.stringify(order), // JavaScript nesnesini JSON'a çevir
//        success: function (response) {
//            alert('Sipariş başarıyla gönderildi!');
//        },
//        error: function (xhr, status, error) {
//            alert('Bir hata oluştu: ' + xhr.responseText);
//        }
//    });

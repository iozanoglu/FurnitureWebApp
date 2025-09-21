$(document).ready(function () {
    // Sadece admin için select2
    if (isAdmin) {
        $('#userId').select2({ width: '100%' });
    }

    calculateTotals();

    // Satır silme
    $('#orderTableContainer').on('click', '.remove-row', function () {
        $(this).closest('tr').remove();
        updateRowNumbers();
        calculateTotals();
    });

    // Otomatik hesaplama
    $('#orderTableContainer').on('input', '.boy, .en, .adet, .fiyat', function () {
        const row = $(this).closest('tr');
        const boy = parseFloat(row.find('.boy').val()) || 0;
        const en = parseFloat(row.find('.en').val()) || 0;
        const adet = parseFloat(row.find('.adet').val()) || 0;
        const fiyat = parseFloat(row.find('.fiyat').val()) || 0;

        const metrekare = (boy * en * adet) / 10000;
        const toplam = metrekare * fiyat;

        row.find('.metrekare').val(metrekare.toFixed(4));
        row.find('.toplam').val(toplam.toFixed(2));
        calculateTotals();
    });

    // Form submit
    $('#orderForm').submit(function (e) {
        e.preventDefault();
        const formData = new FormData(this);
        const jsonData = {};
        const tableData = [];

        for (let [key, value] of formData.entries()) {
            if (!value || value.trim() === '') continue;

            if (key.startsWith('orderProducts')) {
                const match = key.match(/^orderProducts([A-Za-z0-9]+)(\d+)$/i);
                if (match) {
                    const colName = match[1];
                    const rowIndex = parseInt(match[2]);
                    if (!tableData[rowIndex]) tableData[rowIndex] = {};
                    tableData[rowIndex][colName] = value;
                }
            } else {
                jsonData[key] = value;
            }
        }

        jsonData.orderProducts = tableData.filter(item => item && Object.keys(item).length > 0);
        if (isAdmin) {
        postMethod('/admin/Orders/CreatePost', JSON.stringify(jsonData), '/admin/Orders/Index');
        }
        else
        {
            jsonData.userId = $('#userId').val();
            postMethod('/UserOrders/CreatePost', JSON.stringify(jsonData), '/UserOrders/Index');
        }
    });
});

function updateRowNumbers() {
    const rows = document.querySelectorAll("#orderRows tr");
    rows.forEach((row, index) => {
        const numberInput = row.querySelector("input[id^='orderProductsnumber']");
        if (numberInput) numberInput.value = index + 1;
    });
}

function calculateTotals() {
    let totalM2 = 0;
    let totalPrice = 0;

    const rows = document.querySelectorAll("#orderRows tr");
    rows.forEach(row => {
        const m2Input = row.querySelector(".metrekare");
        const m2 = parseFloat((m2Input.value || "0").replace(",", ".")) || 0;
        totalM2 += m2;

        if (isAdmin) {
            const priceInput = row.querySelector(".toplam");
            const price = parseFloat((priceInput.value || "0").replace(",", ".")) || 0;
            totalPrice += price;
        }
    });

    document.getElementById("totalSquareMeter").value = totalM2.toFixed(4);
    if (isAdmin) document.getElementById("totalPrice").value = totalPrice.toFixed(2);
}

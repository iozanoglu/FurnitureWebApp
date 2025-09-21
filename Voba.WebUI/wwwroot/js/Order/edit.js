$(document).ready(function () {

    $(".datepickerForOrderEdit").each(function () {
        var $input = $(this);
        $input.datepicker({
            dateFormat: "dd/mm/yy"
        });

        // Eğer inputta value varsa, Datepicker’a ata
        var currentVal = $input.val();
        if (currentVal) {
            // dd-MM-yyyy -> Date objesi
            var parts = currentVal.split(".");
            if (parts.length === 3) {
                var day = parseInt(parts[0], 10);
                var month = parseInt(parts[1], 10) - 1; // JS ay 0-11
                var year = parseInt(parts[2], 10);
                $input.datepicker("setDate", new Date(year, month, day));
            }
        }
    });


    calculateTotals();
})
function toNumber(v) {
    return parseFloat((v || "0").toString().replace(',', '.')) || 0;
}

function computeRow(tr) {
    const boy = toNumber(tr.querySelector('.boy').value);
    const en = toNumber(tr.querySelector('.en').value);
    const adet = toNumber(tr.querySelector('.adet').value);
    const fiyat = toNumber(tr.querySelector('.fiyat').value);

    const m2 = (boy * en * adet) / 10000;
    const toplam = m2 * fiyat;

    tr.querySelector('.metrekare').value = m2.toFixed(4);
    tr.querySelector('.toplam').value = toplam.toFixed(2);
    calculateTotals();
}

function calculateTotals() {
    let totalM2 = 0;
    let totalPrice = 0;

    document.querySelectorAll('#orderRows tr').forEach(tr => {
        const m2 = toNumber(tr.querySelector('.metrekare').value);
        const price = toNumber(tr.querySelector('.toplam').value);

        totalM2 += m2;
        totalPrice += price;
    });

    document.getElementById('totalSquareMeter').value = totalM2.toFixed(4);
    document.getElementById('grandTotalPrice').value = totalPrice.toFixed(2);
}

function wireRow(tr) {
    tr.addEventListener('input', function (e) {
        if (e.target.classList.contains('boy') || e.target.classList.contains('en') || e.target.classList.contains('adet') || e.target.classList.contains('fiyat')) {
            computeRow(tr);
            calculateTotals();
        }
    });

    const removeBtn = tr.querySelector('.remove-row');
    if (removeBtn) {
        removeBtn.addEventListener('click', function () {
            tr.remove();
            calculateTotals();
            updateRowNumbers();
        });
    }
}

function updateRowNumbers() {
    document.querySelectorAll('#orderRows tr').forEach((row, i) => {
        row.querySelector("td:first-child").textContent = i + 1;
    });
}

document.querySelectorAll('#orderRows tr.order-row').forEach(tr => wireRow(tr));

document.getElementById('addRow').addEventListener('click', function () {
    const tbody = document.getElementById('orderRows');
    const i = tbody.children.length;

    const tr = document.createElement('tr');
    tr.className = 'order-row';
    tr.innerHTML = `
                            <td class="text-center">${i + 1}</td>
                            <td><input name="OrderProducts[${i}].ModelName" class="form-control" /></td>
                            <td><input name="OrderProducts[${i}].Color" class="form-control" /></td>
                            <td><input name="OrderProducts[${i}].Length" class="form-control boy" /></td>
                            <td><input name="OrderProducts[${i}].Width" class="form-control en"/></td>
                            <td><input name="OrderProducts[${i}].Quantity" class="form-control adet"/></td>
                            <td><input name="OrderProducts[${i}].SquareMeter" class="form-control metrekare" readonly /></td>
                            <td><input name="OrderProducts[${i}].SquareMeterPrice" class="form-control fiyat"/></td>
                            <td><input name="OrderProducts[${i}].Price" class="form-control toplam" readonly /></td>
                            <td><input name="OrderProducts[${i}].Description" class="form-control" /></td>
                            <td><button type="button" class="btn btn-danger btn-sm remove-row">&times;</button></td>
                        `;
    tbody.appendChild(tr);
    wireRow(tr);
});

$('#orderEditForm').submit(function (e) {
    e.preventDefault();
    const formData = new FormData(this);
    const jsonData = {};
    const tableData = [];

    for (let [key, value] of formData.entries()) {
        if (!value || value.trim() === '') continue;

        if (key.startsWith('OrderProducts')) {
            const match = key.match(/^OrderProducts\[(\d+)\]\.(.+)$/);
            if (match) {
                const rowIndex = parseInt(match[1]);
                const colName = match[2];

                if (!tableData[rowIndex]) tableData[rowIndex] = {};
                if (['length', 'width', 'quantity', 'squareMeter', 'squareMeterPrice', 'price'].includes(colName)) {
                    tableData[rowIndex][colName] = parseFloat(value.toString().replace(',', '.')) || 0;
                } else if (['orderId', 'id'].includes(colName)) { tableData[rowIndex][colName] = parseInt(value); }
                else {
                    tableData[rowIndex][colName] = value;
                }
            }
        } else {
            if (['userId', 'orderTypeId', 'id','status'].includes(key)) { jsonData[key] = parseInt(value); }
            else {
                jsonData[key] = value;
            }
            
        }
    }
    jsonData.orderDate = parseDateToISO(formData.get('orderDate'));
    jsonData.shipmentDate = parseDateToISO(formData.get('shipmentDate'));
    jsonData.orderProducts = tableData.filter(item => item && Object.keys(item).length > 0);
    jsonData.totalSquareMeter = parseFloat(document.getElementById('totalSquareMeter').value.replace(',', '.'));
    jsonData.totalPrice = parseFloat(document.getElementById('grandTotalPrice').value.replace(',', '.'));


    postMethod("/Admin/Orders/Edit", JSON.stringify(jsonData), "/Admin/Orders/Index");
});

function parseDateToISO(dateStr) {
    if (!dateStr) return null;
    const parts = dateStr.split("/"); // dd/MM/yyyy
    if (parts.length !== 3) return null;
    const day = parts[0].padStart(2, "0");
    const month = parts[1].padStart(2, "0");
    const year = parts[2];
    return `${year}-${month}-${day}`; // yyyy-MM-dd
}

function formatDateToDDMMYYYY(dateStr) {
    if (!dateStr) return "";
    const parts = dateStr.split("-");
    if (parts.length !== 3) return dateStr;
    return `${parts[2]}/${parts[1]}/${parts[0]}`;
}

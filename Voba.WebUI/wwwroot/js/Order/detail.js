function printTable(tableId) {
    // Tabloyu ve üst bilgiyi klonla
    const table = document.getElementById(tableId).cloneNode(true);
    const orderSummary = document.querySelector('.order-summary').cloneNode(true);

    // İşlem kolonunu kaldır (son sütun)
    table.querySelectorAll('tr').forEach(tr => {
        const lastCell = tr.lastElementChild;
        if (lastCell) lastCell.remove();
    });

    // Tüm CSS ve style taglerini al
    const styles = Array.from(document.querySelectorAll('link[rel="stylesheet"], style'))
        .map(node => node.outerHTML)
        .join('\n');

    // Özel yazdırma CSS’i
    const printCSS = `
        <style>
            @media print {
                body { margin: 0.5cm; font-size: 10pt; }
                table { width: 100% !important; border-collapse: collapse !important; page-break-inside: avoid; }
                table, th, td { border: 1px solid #000 !important; }
                th, td { padding: 0.3rem !important; }
                .order-summary { display: flex !important; flex-wrap: wrap !important; gap: 0.5rem; margin-bottom: 1rem; }
                .order-summary > div { flex: 0 0 auto; padding: 0.3rem !important; font-size: 10pt !important; background: #fff !important; }
                tfoot tr { background-color: #f8f9fa !important; font-weight: bold; }
                .btn { display: none !important; }
            }
        </style>
    `;

    // Yeni pencere aç ve yazdır
    const printWindow = window.open('', '_blank');
    printWindow.document.write(`
        <html>
        <head>
            <title>&nbsp;</title>
            ${styles}
            ${printCSS}
        </head>
        <body>
            ${orderSummary.outerHTML}
            ${table.outerHTML}
        </body>
        </html>
    `);

    printWindow.document.close();
    printWindow.onload = function () {
        printWindow.focus();
        printWindow.print();
        printWindow.close();
    };
}

// Excel butonu
document.getElementById('exportExcel').addEventListener('click', function () {
    var table = document.getElementById('ordersTable');
    var summary = document.querySelector('.order-summary');

    // Özet bilgileri ayrı satırlar halinde hazırla
    var summaryRows = [];
    var musteriAdi = "";
    var siparisKodu = "";

    summary.querySelectorAll('div.p-2').forEach(div => {
        var label = div.querySelector('strong').innerText;
        var value = div.innerText.replace(label, '').trim();
        summaryRows.push([label, value]);

        // Dinamik dosya adı için değerleri al
        if (label.includes("Kişi")) musteriAdi = value.replace(/\s+/g, "_"); // boşlukları _ yap
        if (label.includes("Sipariş Kodu")) siparisKodu = value;
    });

    // Tabloyu klonla ve İşlem kolonunu kaldır
    var clonedTable = table.cloneNode(true);
    clonedTable.querySelectorAll('tr').forEach(tr => {
        const lastCell = tr.lastElementChild;
        if (lastCell) lastCell.remove();
    });

    // Workbook oluştur
    var wb = XLSX.utils.book_new();
    var ws = XLSX.utils.aoa_to_sheet(summaryRows); // Özet başlangıçta

    // Tabloyu ekle
    var tableJson = XLSX.utils.sheet_to_json(XLSX.utils.table_to_sheet(clonedTable, { raw: true }), { header: 1 });
    XLSX.utils.sheet_add_aoa(ws, tableJson, { origin: "A" + (summaryRows.length + 2) });

    // Sheet’i ekle
    XLSX.utils.book_append_sheet(wb, ws, "Sipariş Detayları");

    // Dinamik dosya adı
    var dosyaAdi = `${musteriAdi} ${siparisKodu}.xlsx`;

    // Excel dosyasını indir
    XLSX.writeFile(wb, dosyaAdi);
});





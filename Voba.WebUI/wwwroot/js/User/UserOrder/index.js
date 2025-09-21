$(document).ready(function () {
    $('#ordersTable').DataTable({
        paging: true,
        searching: true,
        ordering: true,
        responsive: true,
        language: {
            url: "//cdn.datatables.net/plug-ins/1.13.6/i18n/tr.json"
        },
        pageLength: 10,
        lengthMenu: [10, 25, 50, 100],
        order: [[1, "desc"]] // 1 => Sipariş Kodu sütunu, "desc" => azalan sıra (en yeni üstte)
    });
});

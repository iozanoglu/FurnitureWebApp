$(document).ready(function () {
    var tables = [];

    $('.datatable').each(function () {
        var table = $(this).DataTable({
            "pageLength": 10,
            "lengthChange": false,
            "searching": true,
            "ordering": true,
            "info": false,
            "order": [[1, "desc"]],
            "columnDefs": [
                { "width": "15%", "targets": 0 },
                { "width": "15%", "targets": 1 },
                { "width": "25%", "targets": 2 },
                { "width": "15%", "targets": 3 },
                { "width": "15%", "targets": 4 },
                { "width": "15%", "targets": 5 }
            ],
            "autoWidth": false,
            language: {
                search: "Ara:",
                paginate: {
                    first: "İlk",
                    last: "Son",
                    next: "Sonraki",
                    previous: "Önceki"
                },
                info: "_TOTAL_ kayıt içerisinden _START_ - _END_ arası gösteriliyor",
                zeroRecords: "Kayıt bulunamadı"
            },
            dom: 'lrtip' // Tablo arama kutusunu gizliyoruz
        });

        tables.push(table);
    });

    // Global arama inputu
    $('#globalSearch').on('keyup', function () {
        var val = this.value;
        tables.forEach(function (t) {
            t.search(val).draw();
        });
    });
});

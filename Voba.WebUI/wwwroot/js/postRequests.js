function postMethod(url, data, doneUrl = '', contentType = 'application/json')
{
    $.ajax({
        url: url,
        type: 'POST',
        contentType: contentType,
        processData: false,
        data: data,
        success: function (response) {
            showSuccessPopup('İşlem başarılı!', doneUrl);
            return response;
        },
        error: function (xhr, status, error) {
            showErrorPopup('İşlem başarısız! ');
        }
    });
}
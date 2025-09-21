function showSuccessPopup(message, redirectUrl='') {
    Swal.fire({
        icon: 'success',
        title: 'İşlem başarılı!',
        text: message,
        confirmButtonText: 'Tamam'
    }).then((result) => {
        if (result.isConfirmed && redirectUrl) {
            window.location.href = redirectUrl;
        }
    });

}

function showErrorPopup(message) {
    Swal.fire({
        icon: 'error',
        title: 'İşlem başarısız!',
        text: message,
        confirmButtonText: 'Tamam'
    });

}
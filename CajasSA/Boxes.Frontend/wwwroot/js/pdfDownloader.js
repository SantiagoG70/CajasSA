window.downloadPdf = (base64Data, fileName) => {
    const link = document.createElement('a');
    link.href = "data:application/pdf;base64," + base64Data;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);

    // Mostrar mensaje de éxito
    if (window.Swal) {
        Swal.fire({
            icon: 'success',
            title: 'Factura descargada',
            text: 'La factura se ha generado y descargado correctamente.',
            timer: 3000,
            showConfirmButton: false
        });
    } else {
        alert("Factura descargada correctamente.");
    }
};
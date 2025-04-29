let table;
$(document).ready(function () {
    initiateTable();
});

function initiateTable(){
    table = $('#itemTable').DataTable({
        ajax: {
            url: '/Item/GetAll',
            type: 'GET',
            dataSrc: ''
        },
        columns: [
            {data: "name", title: "Name"},
            {data: "description", title: "Description"},
            {data: "quantity", title: "Quantity"},
            {data: "price", title: "Price"},
        ],
        dom: 'Bfrtip', 
        buttons: [
            {
                extend: 'excelHtml5',
                title: 'Item Data',
                className: 'd-none', 
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'pdfHtml5',
                title: 'Item Data',
                className: 'd-none', 
                exportOptions: {
                    columns: ':visible'
                }
            }
        ]
    });
}


$('#downloadTemplate').on('click', function() {
    window.location.href = "/Item/GetItemTemplate";
})

$('#exportExcel').on('click', function () {
    table.button('.buttons-excel').trigger();
});

$('#exportPdf').on('click', function () {
    table.button('.buttons-pdf').trigger();
});

$('#fileInput').on('change', function(event) {
    const fileInput = event.target;
    const file = fileInput.files[0];

    if (!file) {
        Swal.fire('Please select a file to upload.');
        return;
    }

    if (file.size === 0) {
        Swal.fire('Selected file is empty.');
        return;
    }

    const formData = new FormData();
    formData.append('fileBase', file);

    $.ajax({
        url: '/Item/BulkInsert',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function(response) {
            Swal.fire({
                icon: 'success',
                title: 'Upload Successful',
                text: 'Item successfully uploaded.'
            });

            $('#itemTable').DataTable().ajax.reload(null, false);
        },
        error: function(xhr, status, error) {
            console.error('Error uploading file:', error);
            Swal.fire({
                icon: 'error',
                title: 'Upload Failed',
                text: 'There was an error uploading the file.'
            });
        }
    });
});
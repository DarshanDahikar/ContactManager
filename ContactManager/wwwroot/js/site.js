$(document).ready(function () {
    $('.btn-edit').click(function () {
        var id = $(this).data('id');
        $.get('/Contacts/Edit/' + id, function (data) {
            $('#modal-body').html(data);
            $('#editModal').modal('show');
        });
    });

    $('#btn-save').click(function () {
        var form = $('#editForm');
        $.ajax({
            type: "POST",
            url: form.attr('action'),
            data: form.serialize(),
            success: function () {
                $('#editModal').modal('hide');
                location.reload();
            },
            error: function (xhr, status, error) {
                alert('Error: ' + error);
            }
        });
    });
});

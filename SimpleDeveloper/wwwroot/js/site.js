
$('#languages').select2({
    theme: "bootstrap-5",
    width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-100') ? '100%' : 'style',
    placeholder: 'Select languages',
    allowClear: true,
    closeOnSelect: false,
});
function previewImage(event) {
    var input = event.target;
    var reader = new FileReader();

    reader.onload = function () {
        var dataURL = reader.result;
        var output = document.getElementById('photoPreview');
        output.src = dataURL;
        document.getElementById('photoPreviewContainer').classList.remove('hidden');
    };

    reader.readAsDataURL(input.files[0]);
}

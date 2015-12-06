$('a[type="data-modal"]').click(function(event) {
    $(this).modal({
        escapeClose: true,
        clickClose: true,
        showClose: false,
        fadeDuration: 250
    });
  return false;
});

$(".hasDatepicker").datetimepicker({
    format: "DD/MM/YYYY"
});
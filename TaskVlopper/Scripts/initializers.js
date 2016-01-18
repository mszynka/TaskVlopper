$('a[type="data-modal"]').click(function (event) {
    $(this).modal({
        escapeClose: true,
        clickClose: true,
        showClose: false,
        fadeDuration: 250
    });
    return false;
});

Pace.on("done",
        function () {
            $(".hasDatepicker").datetimepicker({
                format: "MM/DD/YYYY",
            });
        });
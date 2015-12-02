$('a[type="data-modal"]').click(function(event) {
    $(this).modal({
        escapeClose: true,
        clickClose: true,
        showClose: false,
        fadeDuration: 250,
        fadeDelay: 0.80
    });
  return false;
});
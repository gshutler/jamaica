$(function() {
    $(".formElement.watermark").each(function() {
        var formElement = $(this);
        var input = formElement.find("input");
        var label = formElement.find("label");
        input.focus(function() {
            label.hide();
        });
        input.blur(function() {
            if (!input.val()) {
                label.show();
            }
        });
    });
});
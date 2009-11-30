$(function() {
    $(".formElement.watermark").each(function() {
        var formElement = $(this);
        var input = formElement.find("input");
        var label = formElement.find("label");
        input.focus(function() {
            label.hide();
        });
        var hideWarkmarkIfValueEntered = function() {
            if (!input.val()) {
                label.show();
            }
        }
        input.blur(hideWarkmarkIfValueEntered);
        hideWarkmarkIfValueEntered();
    });
});
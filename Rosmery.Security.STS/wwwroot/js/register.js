var SnippetRegister = function() {

    var login = $('#m_login');

    var displayValidationElements = function () {
        $(".form-control-feedback").each(function () {
            if ($(this).html() !== "")
                $(this).show();
            else
                $(this).hide();
        });
    };

    return {

        init: function() {
            displayValidationElements();
        }
    };
}();

jQuery(document).ready(function() {
    SnippetRegister.init();
});
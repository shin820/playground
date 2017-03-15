(function ($) {
    $(function () {

        var _$taskStateCombobox = $('#TaskStateCombobox');

        _$taskStateCombobox.change(function () {
            location.href = '/Tasks?status=' + _$taskStateCombobox.val();
        });

    });
})(jQuery);
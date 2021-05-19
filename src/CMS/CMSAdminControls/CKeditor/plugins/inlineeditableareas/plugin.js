CKEDITOR.plugins.add('inlineeditableareas', {
    icons: 'inlineeditableareas',
    init: function (editor) {
        editor.addCommand('insertEditableArea', {
            exec: function (editor) {
                var editableAreaId = Math.random().toString(36).substring(2);
                var html = '<editable-area id="' + editableAreaId + '"></editable-area>';
                var editableArea = CKEDITOR.dom.element.createFromHtml(html);

                editor.insertElement(editableArea);
            }
        });
        editor.ui.addButton('inlineeditableareas', {
            label: 'Insert widget area',
            command: 'insertEditableArea',
            toolbar: 'insert'
        });
    }
});
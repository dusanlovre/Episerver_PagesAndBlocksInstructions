define(
    [
        "dojo/_base/declare",
        "dijit/_Widget",
        "dijit/_TemplatedMixin"
    ],
    function (
        declare,
        _Widget,
        _TemplatedMixin,
    ) {
        return declare([_Widget, _TemplatedMixin],
            {
                postCreate: function () {
                    console.log(this)
                    if (this.instructionsExist) {
                        this.instructionslink.href = this.linkUrl;
                        this.instructionslink.innerHTML = `<strong>${this.linkCaption}</strong>`;
                    }
                },
                templateString: `<div> \
                     <a href="" data-dojo-attach-point="instructionslink" target="_blank"></div>`,
            })
    }
);
$(function () {
    $("#toolbox_tabs").tabs();
});

var design_view_model = new LabelMakerViewModel();
var currentAfterAddElement = design_view_model.afterAddElement;
design_view_model.afterAddElement = function (element, index, data) {
    if (currentAfterAddElement) {
        currentAfterAddElement(element, index, data);
    }
    if (element.nodeType === 1) {
        $("label.id_label").last().each(function () {
            this.parentElement.id = this.innerText;
        });

        $("label.top").last().each(function() {
            $(this.parentElement).css("top", this.innerText.toString() + "px");
        });

        $("label.left").last().each(function() {
            $(this.parentElement).css("left", this.innerText.toString() + "px");
        });

        $("label.width").last().each(function() {
            $(this.parentElement).css("width", this.innerText.toString() + "px");
        });

        $("label.height").last().each(function() {
            $(this.parentElement).css("height", this.innerText.toString() + "px");
        });

        $(element).filter("div").last().resizable();
        $(element).filter("div").last().draggable({
            containment: '#glassbox',
            scroll: false
        })
        
        .click(function() {
            //var id = this.childNodes[1].innerText;
            //for (var i = 0; i < self.receipt_lines().length; i++) {
            //    if (self.receipt_lines()[i].id == id) {
            //        this.selected_element(self.receipt_lines()[i]);
            //        $("input[name='X']").first().text = this.selected_element.X;
            //        $("input[name='Y']").first().text = this.selected_element.Y;
            //        $("input[name='width']").first().text = this.selected_element.Width;
            //        $("input[name='height']").first().text = this.selected_element.Height;

            //    }
            //}
        })
        
        .mousemove(function() {
            var coord = $(this).position();
            $("p:last").text("left: " + coord.left + ", top: " + coord.top);
        })
        
        .mouseup(function() {
            var id = this.childNodes[1].innerText;
            for (var i = 0; i < design_view_model.receipt_lines().length; i++) {
                if (design_view_model.receipt_lines()[i].id == id) {
                    design_view_model.property_fields.removeAll();
                    design_view_model.property_fields.push({ name: "X", value: design_view_model.receipt_lines()[i].left });
                    design_view_model.property_fields.push({ name: "Y", value: design_view_model.receipt_lines()[i].top });
                    design_view_model.property_fields.push({ name: "Width", value: design_view_model.receipt_lines()[i].width });
                    design_view_model.property_fields.push({ name: "Length", value: design_view_model.receipt_lines()[i].height });
                    design_view_model.property_fields.push({ name: "Text", value: design_view_model.receipt_lines()[i].text });
                    design_view_model.property_fields.push({ name: "Id", value: design_view_model.receipt_lines()[i].id });
                }
            }
        });
    }
};

ko.applyBindings(design_view_model);

$(document).ready(function () {

    $("#glassbox label.id_label").each(function () {
        this.parentElement.id = this.innerText;
    });

    $("#glassbox label.top").each(function () {
        $(this.parentElement).css("top", this.innerText.toString() + "px");
    });

    $("#glassbox label.left").each(function () {
        $(this.parentElement).css("left", this.innerText.toString() + "px");
    });

    $("#glassbox/label.width").each(function () {
        $(this.parentElement).css("width", this.innerText.toString() + "px");
    });

    $("#glassbox/label.height").each(function () {
        $(this.parentElement).css("height", this.innerText.toString() + "px");
    });

    $("#glassbox .ui-widget-content").resizable();
    $(".ui-widget-content").draggable({
        containment: '#glassbox',
        scroll: false
    })
    .mousemove(function () {
        var coord = $(this).position();
        $("p:last").text("left: " + coord.left + ", top: " + coord.top);
    })
    .mouseup(function () {
        var id = this.childNodes[1].innerText;
        for (var i = 0; i < design_view_model.receipt_lines().length; i++) {
            if (design_view_model.receipt_lines()[i].element_id == id) {
                design_view_model.receipt_lines()[i].left = self.left;
                design_view_model.receipt_lines()[i].top = self.top;
                design_view_model.receipt_lines()[i].width = self.width;
                design_view_model.receipt_lines()[i].height = self.height;
                break;
            }
        }
    });
});


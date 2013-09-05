function ReceiptHeader(id, name) {
    var self = this;
    self.id = id;
    self.receiptName = name;
}

function ReceiptElement(id, type, text, left, top, width, height) {
    var self = this;
    self.id = id;
    self.receiptHeaderId = {};
    self.type = type;
    self.text = text;
    self.left = left;
    self.top = top;
    self.width = width;
    self.height = height;
}

function DesignLabelViewModel() {
    var self = this;

    self.receiptName = ko.observable("Default Thermal");
    self.receiptId = ko.observable("");
    self.selectedReceiptId = ko.observable();
    self.selectedObject = ko.observable();

    self.property_fields = [
        { name: "X", value: "0" },
        { name: "Y", value: "0" },
        { name: "Width", value: "line_item" },
        { name: "Length", value: "discount" },
        { name: "Text", value: "sub_total" },
        { name: "Id", value: "Id" },
    ];

    self.reciept_fields = [
        { name: "Invoice Number", value: "invoice_number" },
        { name: "User", value: "user" },
        { name: "Line Item", value: "line_item" },
        { name: "Discount", value: "discount" },
        { name: "Sub total", value: "sub_total" },
        { name: "Tax", value: "tax" },
        { name: "To Pay", value: "to_pay" },
        { name: "Total", value: "total" }
    ];

    self.product_fields = [
        { name: "sku", value: "sku" },
        { name: "handle", value: "handle" },
        { name: "Product Name", value: "name" },
        { name: "Product Type", value: "sku" }
    ];

    self.receipt_lines = ko.observableArray();

    self.receipt_lines.push(new ReceiptElement("00000000-0000-0000-0000-000000000000", "free_text", "Tax Invoice:", 0, 0, 150, 32));
    self.receipt_lines.push(new ReceiptElement("00000000-0000-0000-0000-000000000000", "free_text", ".....{Invoice #}", 162, 0, 150, 32));

    self.existingReceipts = ko.observableArray();
    self.freeTextValue = ko.observable("Enter Value");

    $.getJSON("/VendHook/api/Receipt", function(data) {
        for (var i = 0; i < data.length; i++) {
            self.existingReceipts.push(new ReceiptHeader(data[i].Id, data[i].ReceiptName));
        }
        
    });

    self.elementCount = 2;
    self.selected_element = null;

    self.AddFieldElement = function () {
        var top = self.NextOffset();
        var newElement = new ReceiptElement("00000000-0000-0000-0000-000000000000",
            "field", this.value, 0, top, 150, 24);
        
        self.receipt_lines.push(newElement);

        var newPrice = new ReceiptElement("00000000-0000-0000-0000-000000000000",
            "value", ".......{" + this.value + "}", 162, top, 150, 24);

        self.receipt_lines.push(newPrice);
    };

    self.AddFreeText = function() {
        var top = self.NextOffset();
        var newElement = new ReceiptElement("00000000-0000-0000-0000-000000000000",
            "field", self.freeTextValue(), 0, top, 240, 32);
        self.receipt_lines.push(newElement);
    };

    self.AddRepeater = function() {
        var top = self.NextOffset();
        var newElement = new ReceiptElement("00000000-0000-0000-0000-000000000000",
            "repstart", "===Items Rep Start===", 0, top, 300, 16);
        self.receipt_lines.push(newElement);

        var newElement = new ReceiptElement("00000000-0000-0000-0000-000000000000",
            "repend", "===Items Rep End===", 0, top + 60, 300, 16);
        self.receipt_lines.push(newElement);
    };

    self.DeleteElement = function () {
        if (self.selectedObject != null) {
            self.receipt_lines.remove(selectedObject);
        }
    };

    self.LoadReceipt = function () {
        var selectedValue = self.selectedReceiptId();
        if (selectedValue) {
            $.getJSON("/VendHook/api/Receipt/" + selectedValue.id, function(data) {
                self.receiptId = data.Id;
                self.receiptName = data.ReceiptName;
                $.getJSON("/VendHook/api/ReceiptLine?$filter=ReceiptHeaderId eq guid\'" + data.Id.toString() + "\'", function (data) {
                    self.receipt_lines.removeAll();
                    for (var i = 0; i < data.length; i++) {
                        var receiptElement = new ReceiptElement(data[i].Id, data[i].Type, data[i].Text, data[i].Left, data[i].Top, data[i].Width, data[i].Height);
                        receiptElement.receiptHeaderId = data.ReceiptHeaderId;
                        self.receipt_lines.push(receiptElement);
                    }
                });
            });
        }
    };

    self.NextOffset = function() {
        var top = self.elementCount * 41;
        self.elementCount++;
        if (top > 300) {
            top = 300;
        }
        return top;
    };

    self.afterAddElement = function (element, index, data) {
        $("label.id_label").last().each(function () {
            this.parentElement.id = this.innerText;
        });

        $("label.top").last().each(function () {
            $(this.parentElement).css("top", this.innerText.toString() + "px");
        });

        $("label.left").last().each(function () {
            $(this.parentElement).css("left", this.innerText.toString() + "px");
        });

        $("label.width").last().each(function () {
            $(this.parentElement).css("width", this.innerText.toString() + "px");
        });

        $("label.height").last().each(function () {
            $(this.parentElement).css("height", this.innerText.toString() + "px");
        });

        $(element).filter("div").last().resizable();
        $(element).filter("div").last().draggable({
            containment: '#glassbox',
            scroll: false
        })
        .click(function() {
            var id = this.childNodes[1].innerText;
            for (var i = 0; i < self.receipt_lines().length; i++) {
                if (self.receipt_lines()[i].element_id == id) {
                    this.selectedObject = self.receipt_lines()[i];
                    // populate properties table
                }
            }
        })
        .mousemove(function() {
            var coord = $(this).position();
            $("p:last").text("left: " + coord.left + ", top: " + coord.top);
        })
        .mouseup(function() {
            var id = this.childNodes[1].innerText;
            for (var i = 0; i < self.receipt_lines().length; i++) {
                if (self.receipt_lines()[i].element_id == id) {
                    self.receipt_lines()[i].left = self.left;
                    self.receipt_lines()[i].top = self.top;
                    self.receipt_lines()[i].width = self.width;
                    self.receipt_lines()[i].height = self.height;
                }
            }
        });
        
    };

    self.SaveReceipt = function () {
        var receiptId = self.receiptId();
        var receiptName = self.receiptName;
        
        if (receiptId.length == 0) {
            $.post("/VendHook/api/Receipt", {
                    Id: "00000000-0000-0000-0000-000000000000",
                    receiptName: receiptName
            })
            .success(function(data) {
            })
            .done(function (data) {
                self.receiptId(data.Id);
                var receiptLines = [];
                $(".ui-widget-content").each(function () {
                    var top = $(this).css("top") || "0px";
                    var left = $(this).css("left") || "0px";
                    var width = $(this).css("width") || "0px";
                    var height = $(this).css("height") || "0px";
                    var receiptLine = new ReceiptElement(this.childNodes[1].innerText,
                    this.childNodes[3].innerText, // receiptLine.type
                    this.childNodes[13].innerText, // receiptLine.text
                    parseInt(left.substring(0, left.length - 2)), // receiptLine.left
                    parseInt(top.substring(0, top.length - 2)),
                    parseInt(width.substring(0, width.length - 2)), // receiptLine.width
                    parseInt(height.substring(0, height.length - 2))); // receiptLine.height
                    receiptLine.receiptHeaderId = self.receiptId();
                    receiptLine.field = this.childNodes[3].innerText;
                    
                    if (receiptLine.id.length == 0 || receiptLine.id.indexOf("element") >= 0) {
                        receiptLine.id = "00000000-0000-0000-0000-000000000000";
                    }
                    $.post("/VendHook/api/ReceiptLine", receiptLine)
                        .done(function(lineData) {
                            receiptLine.id = lineData.Id;
                        });
                    receiptLines.push(receiptLine);
                });
                self.receipt_lines.removeAll();
                for (var i = 0; i < receiptLines.length; i++) {
                    self.receipt_lines.push(receiptLines[i]);
                }
            });
        } else {
            var data = {
                Id: receiptId,
                receiptName: receiptName
            };
            $.ajax({
                type: "PUT",
                url: "/VendHook/api/Receipt/" + receiptId,
                data: data,
                success: function(data) {
                    self.receiptId(data.Id);
                    var receiptLines = [];
                    $(".ui-widget-content").each(function () {
                        var top = $(this).css("top") || "0px";
                        var left = $(this).css("left") || "0px";
                        var width = $(this).css("width") || "0px";
                        var height = $(this).css("height") || "0px";
                        var receiptLine = new ReceiptElement(this.childNodes[1].innerText, //receiptLine.id
                        this.childNodes[3].innerText,             //  receiptLine.type = 
                        this.childNodes[13].innerText,            //  receiptLine.text = 
                        parseInt(left.substring(0, left.length - 2)), // receiptLine.left
                        parseInt(top.substring(0, top.length - 2)),
                        parseInt(width.substring(0, width.length - 2)), // receiptLine.width
                        parseInt(height.substring(0, height.length - 2))); // receiptLine.height
                        receiptLine.field = this.childNodes[3].innerText;
                        receiptLine.receiptHeaderId = self.receiptId();
                        receiptLines.push(receiptLine);
                        if (receiptLine.id.length == 0 || receiptLine.id.indexOf("element") >= 0) {
                            receiptLine.id = "00000000-0000-0000-0000-000000000000";
                            $.post("/VendHook/api/ReceiptLine", receiptLine);
                        } else {
                            $.ajax({ type: "PUT", url: "/VendHook/api/ReceiptLine/" + receiptLine.id, data: receiptLine });
                        }
                    });
                    self.receipt_lines.removeAll();
                    for (var i = 0; i < receiptLines.length; i++) {
                        self.receipt_lines.push(receiptLines[i]);
                    }
                }
            });
        }
    };
}

$(function () {
    $("#toolbox_tabs").tabs();
});

var design_view_model = new DesignLabelViewModel();

ko.applyBindings(design_view_model);

$(document).ready(function() {

    $("label.id_label").each(function () {
        this.parentElement.id = this.innerText;
    });

    $("label.top").each(function() {
        $(this.parentElement).css("top", this.innerText.toString() + "px");
    });

    $("label.left").each(function () {
        $(this.parentElement).css("left", this.innerText.toString() + "px");
    });

    $("label.width").each(function () {
        $(this.parentElement).css("width", this.innerText.toString() + "px");
    });

    $("label.height").each(function () {
        $(this.parentElement).css("height", this.innerText.toString() + "px");
    });

    $(".ui-widget-content").resizable();
    $(".ui-widget-content").draggable({
        containment: '#glassbox',
        scroll: false
    })
    .mousemove(function() {
        var coord = $(this).position();
        $("p:last").text("left: " + coord.left + ", top: " + coord.top);
    })
    .mouseup(function() {
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


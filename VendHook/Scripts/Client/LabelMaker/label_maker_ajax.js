function LabelMakerAjax(label) {
    
    self.LoadReceipt = function () {
        var selectedValue = label.selectedReceiptId();
        if (selectedValue) {
            $.getJSON("/VendHook/api/Receipt/" + selectedValue.id, function (data) {
                label.receiptId = data.Id;
                label.receiptName = data.ReceiptName;
                $.getJSON("/VendHook/api/ReceiptLine?$filter=ReceiptHeaderId eq guid\'" + data.Id.toString() + "\'", function (data) {
                    label.receipt_lines.removeAll();
                    for (var i = 0; i < data.length; i++) {
                        var receiptElement = new ReceiptElement(data[i].Id, data[i].Type, data[i].Text, data[i].Left, data[i].Top, data[i].Width, data[i].Height);
                        receiptElement.receiptHeaderId = data.ReceiptHeaderId;
                        label.receipt_lines.push(receiptElement);
                    }
                });
            });
        }
    };

    self.SaveReceipt = function (label) {
        var receiptId = label.receiptId();
        var receiptName = label.receiptName;

        if (receiptId.length == 0) {
            $.post("/VendHook/api/Receipt", {
                Id: "00000000-0000-0000-0000-000000000000",
                receiptName: receiptName
            })
            .success(function (data) {
            })
            .done(function (data) {
                label.receiptId(data.Id);
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
                    receiptLine.receiptHeaderId = label.receiptId();
                    receiptLine.field = this.childNodes[3].innerText;

                    if (receiptLine.id.length == 0 || receiptLine.id.indexOf("element") >= 0) {
                        receiptLine.id = "00000000-0000-0000-0000-000000000000";
                    }
                    $.post("/VendHook/api/ReceiptLine", receiptLine)
                        .done(function (lineData) {
                            receiptLine.id = lineData.Id;
                        });
                    receiptLines.push(receiptLine);
                });
                label.receipt_lines.removeAll();
                for (var i = 0; i < receiptLines.length; i++) {
                    label.receipt_lines.push(receiptLines[i]);
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
                success: function (data) {
                    label.receiptId(data.Id);
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
                        receiptLine.receiptHeaderId = label.receiptId();
                        receiptLines.push(receiptLine);
                        if (receiptLine.id.length == 0 || receiptLine.id.indexOf("element") >= 0) {
                            receiptLine.id = "00000000-0000-0000-0000-000000000000";
                            $.post("/VendHook/api/ReceiptLine", receiptLine);
                        } else {
                            $.ajax({ type: "PUT", url: "/VendHook/api/ReceiptLine/" + receiptLine.id, data: receiptLine });
                        }
                    });
                    label.receipt_lines.removeAll();
                    for (var i = 0; i < receiptLines.length; i++) {
                        label.receipt_lines.push(receiptLines[i]);
                    }
                }
            });
        }
    };
}
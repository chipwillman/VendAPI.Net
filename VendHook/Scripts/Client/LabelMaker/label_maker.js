function LabelMakerViewModel() {
    var self = this;

    self.receiptName = ko.observable("Default Thermal");
    self.receiptId = ko.observable("");
    self.selectedReceiptId = ko.observable();
    self.selectedObject = ko.observable();

    self.property_fields = ko.observableArray();
    //[
    //    { name: "X", value: "0" },
    //    { name: "Y", value: "0" },
    //    { name: "Width", value: "160" },
    //    { name: "Length", value: "160" },
    //    { name: "Text", value: "text" },
    //    { name: "Id", value: "Id" },
    //];

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

    //self.receipt_lines.push(new ReceiptElement(Guid.create().toString(), "free_text", "Tax Invoice:", 0, 0, 150, 32));
    //self.receipt_lines.push(new ReceiptElement(Guid.create().toString(), "free_text", ".....{Invoice #}", 162, 0, 150, 32));

    self.existingReceipts = ko.observableArray();
    self.freeTextValue = ko.observable("Enter Value");

    $.getJSON("/VendHook/api/Receipt", function (data) {
        for (var i = 0; i < data.length; i++) {
            self.existingReceipts().push(new ReceiptHeader(data[i].Id, data[i].ReceiptName));
        }

    });

    // self.elementCount = 2;
    self.selected_element = ko.observable();

    self.AddFieldElement_ByName = function(name) {
        var top = self.NextOffset();
        var id = Guid.create();
        var newElement = new ReceiptElement(id.toString(),
            "field", name, 0, top, 150, 24);

        self.receipt_lines.push(newElement);

        id = Guid.create();
        var newPrice = new ReceiptElement(id.toString(),
            "value", ".......{" + name + "}", 162, top, 150, 24);

        self.receipt_lines.push(newPrice);
    };

    self.AddFieldElement = function () {
        var name = this.value;
        self.AddFieldElement_ByName(name);
    };

    self.AddFreeText = function () {
        var top = self.NextOffset();
        var newElement = new ReceiptElement(Guid.create().toString(),
            "field", self.freeTextValue(), 0, top, 240, 32);
        self.receipt_lines.push(newElement);
    };

    self.AddRepeater = function () {
        var top = self.NextOffset();
        var newElement = new ReceiptElement(Guid.create().toString(),
            "repstart", "===Items Rep Start===", 0, top, 300, 16);
        self.receipt_lines.push(newElement);

        newElement = new ReceiptElement(Guid.create().toString(),
            "repend", "===Items Rep End===", 0, top + 60, 300, 16);
        self.receipt_lines.push(newElement);
    };

    self.DeleteElement = function () {
        if (self.selectedObject() != null) {
            var removedItems = self.receipt_lines.remove(self.selectedObject());
            if (removedItems && removedItems.length > 0) {
                
            }
        }
    };

    self.NextOffset = function () {
        var top = self.elementCount * 41;
        self.elementCount++;
        if (top > 300) {
            top = 300;
        }
        return top;
    };

    self.afterAddElement = function (element, index, data) {

    };
}


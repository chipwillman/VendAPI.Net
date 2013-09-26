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



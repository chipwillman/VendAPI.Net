/// <reference path="../../../../VendHook/Scripts/guid.js" />
/// <reference path="../../../../VendHook/Scripts/jquery-1.7.1.js" />
/// <reference path="../../../../VendHook/Scripts/jquery-ui-1.8.20.js" />
/// <reference path="../../../../VendHook/Scripts/knockout-2.1.0.js" />
/// <reference path="../../../../VendHook/Scripts/Client/LabelMaker/label.js" />
/// <reference path="../../../../VendHook/Scripts/Client/LabelMaker/label_maker.js" />

describe("Label Maker", function() {
    var labelMaker;
    beforeEach(function () {
        labelMaker = new LabelMakerViewModel();
    });
    describe("after initialization", function () {
        it("should have a receipt line list", function () {
            expect(labelMaker.receipt_lines).toBeDefined("No receipt Lines present");
        });
        
        it("should contain no receipt elements", function () {
            expect(labelMaker.receipt_lines().length).toBe(0);
        });
        
        it("should have a method", function () {
            expect(labelMaker.AddFieldElement).toBeDefined("AddFieldElement");
            expect(labelMaker.AddFieldElement).toBeDefined("AddRepeater");
        });
    });

    describe("when adding receipt Element", function () {
        it("the receipt lines", function () {
            var receiptLine = new ReceiptElement(Guid.create().toString(), "field", "invoice_no", 0, 0, 150, 24);
            labelMaker.receipt_lines().push(receiptLine);
            expect(labelMaker.receipt_lines().length).toBe(1, "count should increase to 1");
        });
    });

    describe("knockout binding requirements", function() {
        beforeEach(function () {
            labelMaker = new LabelMakerViewModel();
        });
        
        it("has bound fields", function () {
            expect(labelMaker.freeTextValue).toBeDefined("freeTextValue");
        });

        it("allows the addition of fields elements", function () {
            labelMaker.AddFieldElement_ByName("invoice_number");
            expect(labelMaker.receipt_lines().length).toBe(2);
            var element = labelMaker.receipt_lines()[0];
            expect(element.text).toBe("invoice_number");
            var field = labelMaker.receipt_lines()[1];
            expect(field.text).toContain("invoice_number");
        });

        it("allows the addition of repeater elements", function() {
            labelMaker.AddRepeater();
            expect(labelMaker.receipt_lines().length).toBe(2);
            var repeater = labelMaker.receipt_lines()[0];
            expect(repeater.type).toBe("repstart");
            repeater = labelMaker.receipt_lines()[1];
            expect(repeater.type).toBe("repend");
        });

        it("allows the addition of free text elements", function () {
            var testFreeText = "Free text to be added to the receipt lines";
            labelMaker.freeTextValue(testFreeText);
            labelMaker.AddFreeText();
            expect(labelMaker.receipt_lines().length).toBe(1);
            var freeText = labelMaker.receipt_lines()[0];
            expect(freeText.text).toBe(testFreeText);
        });
    });

    describe("Element List Removal", function() {
        it("allows the removal of an added item", function() {
            labelMaker.AddFieldElement_ByName("invoice_number");
            expect(labelMaker.receipt_lines().length).toBe(2);
            var addedElement = labelMaker.receipt_lines()[1];
            expect(addedElement.text).toContain("invoice_number");
            labelMaker.selectedObject(addedElement);
            labelMaker.DeleteElement();
            expect(labelMaker.receipt_lines().length).toBe(1);
        });
    });
});
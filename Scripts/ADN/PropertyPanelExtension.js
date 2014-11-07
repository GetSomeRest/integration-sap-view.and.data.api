///////////////////////////////////////////////////////////////////////////////
// PropertyPanel viewer Extension
// by Philippe Leefsma, October 2014
//
///////////////////////////////////////////////////////////////////////////////
AutodeskNamespace("Autodesk.ADN");

Autodesk.ADN.PropertyPanel = function (viewer) {

    _self = this;

    this.viewer = viewer;

    Autodesk.Viewing.Extensions.ViewerPropertyPanel.call(
        this,
        this.viewer);

    _self.setNodeProperties = function (nodeId) {

        Autodesk.Viewing.Extensions.ViewerPropertyPanel.prototype.setNodeProperties.call(
            _self, nodeId);

        _self.selectedNodeId = nodeId;
    };

    _self.setProperties = function (properties) {

        Autodesk.Viewing.Extensions.ViewerPropertyPanel.prototype.setProperties.call(
            _self, properties);

        this.viewer.getPropertyValue(_self.selectedNodeId, "SAPProductId",
               function (productId) {

                   if (productId !== 'undefined') {

                       $.getJSON(window.location + '/api/sap?productId=' + productId,
                           function (product) {

                               // custom SAP properties

                               //_self.addProperty("Description", product.Description, "SAP");

                               _self.addProperty("Product Name", product.Name, "SAP Data");
                               _self.addProperty("Supplier Name", product.SupplierName, "SAP Data");
                               _self.addProperty("Price", product.Price, "SAP Data");
                               _self.addProperty("Currency", getCurrency(product.Currency), "SAP Data");
                           });
                   }
               });
    };
};

Autodesk.ADN.PropertyPanel.prototype =
    Object.create(Autodesk.Viewing.Extensions.ViewerPropertyPanel.prototype);

Autodesk.ADN.PropertyPanel.prototype.constructor =
    Autodesk.ADN.PropertyPanel;


Autodesk.ADN.PropertyPanelExtension = function (viewer, options) {

    // base constructor
    Autodesk.Viewing.Extension.call(this, viewer, options);

    var _viewer = viewer;

    var _self = this;

    ///////////////////////////////////////////////////////////////////////////
    // load callback
    //
    ///////////////////////////////////////////////////////////////////////////
    _self.load = function () {

        var panel = new Autodesk.ADN.PropertyPanel(_viewer);

        _viewer.setPropertyPanel(panel);

        return true;
    };

    ///////////////////////////////////////////////////////////////////////////
    // unload callback
    //
    ///////////////////////////////////////////////////////////////////////////
    _self.unload = function () {

        var panel = new Autodesk.Viewing.Extensions.ViewerPropertyPanel(
           _viewer);

        _viewer.setPropertyPanel(panel);

        return true;
    };
};

Autodesk.ADN.PropertyPanelExtension.prototype =
    Object.create(Autodesk.Viewing.Extension.prototype);

Autodesk.ADN.PropertyPanelExtension.prototype.constructor =
    Autodesk.ADN.PropertyPanelExtension;

Autodesk.Viewing.theExtensionManager.registerExtension(
    'SAP Property Panel',
    Autodesk.ADN.PropertyPanelExtension);
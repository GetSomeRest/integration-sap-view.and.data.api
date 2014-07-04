var _auth;
var _viewer;

///////////////////////////////////////////////////////////////////////////
//
//
///////////////////////////////////////////////////////////////////////////
function getPropertyValue(viewer, dbId, name, callback) {

    function propsCallback(result) {

        if (result.properties) {

            for (var i = 0; i < result.properties.length; i++) {

                var prop = result.properties[i];

                if (prop.displayName == name) {

                    callback(prop.displayValue);
                }
            }

            callback('');
        }
    }

    viewer.getProperties(dbId, propsCallback);
}

///////////////////////////////////////////////////////////////////////////
//
//
///////////////////////////////////////////////////////////////////////////
function addTableRow(table, values, cssStyle) {

    var rowsCount = table.getElementsByTagName('tr').length;

    var row = table.insertRow(rowsCount);

    if (typeof cssStyle !== 'undefined') {
        row.style.cssText = cssStyle;
    }

    for (i = 0; i < values.length; i++) {
        var cell = row.insertCell(i);
        cell.innerHTML = values[i];
    }
}

function clearTable(table) {
    while (table.hasChildNodes()) {
        table.removeChild(table.lastChild);
    }
}

function clearCurrentModel() {

    $('#propsDialog').dialog('close');

    var controller = angular.element($('#tableCtrlId')).scope();

    controller.setData([]);
    controller.$apply();

    $('#jstree').empty();
    $("#jstree").jstree("destroy");

    var viewerElement = document.getElementById('viewer3d');
    viewerElement.parentNode.removeChild(viewerElement);
}

///////////////////////////////////////////////////////////////////////////
//
//
///////////////////////////////////////////////////////////////////////////
function loadDocument(auth, viewer, documentId) {

    if (documentId.indexOf('urn:') !== 0)
        documentId = 'urn:' + documentId;

    Autodesk.Viewing.Document.load(
        documentId,
        auth,
        function (viewerDoc) {

            //viewer.setBackgroundColor(0.8, 0.8, 0.8, 0.8, 0.8, 0.8);

            var items = Autodesk.Viewing.Document.getSubItemsWithProperties(
                viewerDoc.getRootItem(),
                //{ 'mime': 'application/autodesk-svf' },
                { 'type': 'geometry', 'role': '3d' },
                true);

            if (items.length > 0) {

                var item3d = viewerDoc.getViewablePath(items[0]);

                viewer.load(item3d);

                //viewer.load(items[0].urn);

                viewer.addEventListener(
                    Autodesk.Viewing.GEOMETRY_LOADED_EVENT,
                    onGeometryLoaded);
            }
        },
        function (msg) {

            AlertBox.displayError(
                document.getElementById('viewer3d'),
                "Load Error: " + msg);
        }
    );
}

///////////////////////////////////////////////////////////////////////////
//
//
///////////////////////////////////////////////////////////////////////////
function getAllLeafComponents(viewer, callback) {

    function getLeafComponentsRec(parent) {

        var components = [];

        if (typeof parent.children !== "undefined") {

            var children = parent.children;

            for (var i = 0; i < children.length; i++) {

                var child = children[i];

                if (typeof child.children !== "undefined") {

                    var subComps = getLeafComponentsRec(child);

                    components.push.apply(components, subComps);
                }
                else {
                    components.push(child);
                }
            }
        }

        return components;
    }

    viewer.getObjectTree(function (result) {

        var allLeafComponents = getLeafComponentsRec(result);

        callback(allLeafComponents);
    });
}

///////////////////////////////////////////////////////////////////////////
//
//
///////////////////////////////////////////////////////////////////////////
function populateTree(viewer) {

    $('#jstree').jstree({

        'core': {
            check_callback: true
        },

        /*'types' : {
            'default' : {
                'icon': {
                    "image": "~/Images/assembly.png"
                }
            },
            'parent' : {
                'icon': {
                    "image": "~/Images/assembly.png"
                }
              }
            },
        'plugins' : ['types']*/
    });

    $('#jstree').on("ready.jstree",
        function (e, data) {

            var treeRef = $('#jstree').jstree(true);

            _viewer.getObjectTree(function (rootComponent) {

                var rootNode = createNode(
                    treeRef,
                    '#',
                    rootComponent);

                buildTreeRec(treeRef, rootNode, rootComponent);

                $('#jstree').jstree("open_node", rootNode);
            });
        });

    $("#jstree").on("select_node.jstree",
        function (event, data) {

            var node = data.node;


        });

    $("#jstree").on("dblclick.jstree",
        function (event) {

            var ids = $('#jstree').jstree('get_selected');

            var selectedId = parseInt(ids[0]);

            displayComponentInfo(selectedId);

            _viewer.isolateById(selectedId);
            _viewer.docstructure.handleAction(["focus"], selectedId);
        });

    function createNode(tree, parentNode, component) {

        var icon = (component.children ?
            '/sapdemo/Images/parent.png' :
            '/sapdemo/Images/child.png');

        var nodeData = {
            'text': component.name,
            'id': component.dbId,
            'icon': icon
        };

        var node = tree.create_node(
            parentNode,
            nodeData,
            'last',
            false,
            false);

        return node;
    }

    function buildTreeRec(tree, parentNode, component) {

        if (component.children) {

            var children = component.children;

            for (var i = 0; i < children.length; i++) {

                var childComponent = children[i];

                var childNode = createNode(
                    tree,
                    parentNode,
                    childComponent);

                if (childComponent.children) {

                    buildTreeRec(tree, childNode, childComponent);
                }
            }
        }
    }
}

///////////////////////////////////////////////////////////////////////////
//
//
///////////////////////////////////////////////////////////////////////////
function populateProductsTable(productIdListStr) {

    $.getJSON(window.location + '/api/sap?productIdList=' + productIdListStr,
        function (products) {

            var controller = angular.element($('#tableCtrlId')).scope();

            controller.setData(products);
            controller.$apply();
        }
    );
}

///////////////////////////////////////////////////////////////////////////
//
//
///////////////////////////////////////////////////////////////////////////
function createViewer() {

    var viewerContainer = document.getElementById('viewerContainer');

    var viewerElement = document.createElement("div");

    viewerElement.id = 'viewer3d';
    viewerElement.style.height = "100%";

    viewerContainer.appendChild(viewerElement);

    var viewer = new Autodesk.Viewing.Private.GuiViewer3D(viewerElement, {});

    viewer.initialize();

    viewer.addEventListener('selection', onViewerItemSelected);

    // disable scrolling on DOM document 
    // while mouse pointer is over viewer area
    $('#viewer3d').hover(
        function () {
            var scrollX = window.scrollX;
            var scrollY = window.scrollY;
            window.onscroll = function () {
                window.scrollTo(scrollX, scrollY);
            };
        },
        function () {
            window.onscroll = null;
        }
    );

    // disable default context menu on viewer div 
    $('#viewer3d').on('contextmenu', function (e) {
        e.preventDefault();
    });

    return viewer;
}

///////////////////////////////////////////////////////////////////////////
//
//
///////////////////////////////////////////////////////////////////////////
function onGeometryLoaded(event) {

    _viewer.removeEventListener(
        Autodesk.Viewing.GEOMETRY_LOADED_EVENT,
        onGeometryLoaded);

    var productIdList = [];

    populateTree();

    getAllLeafComponents(_viewer, function (components) {

        async.each(components,
            function (component, callback) {

                getPropertyValue(
                    _viewer,
                    component.dbId,
                    "SAPProductId",
                    function (value) {

                        if (productIdList.indexOf(value) < 0)
                            productIdList.push(value);

                        callback();
                    });
            },
            function (err) {

                if (productIdList.length > 0) {

                    var productIdListStr = '';

                    for (var i = 0; i < productIdList.length; i++)
                        productIdListStr += productIdList[i] + ';';

                    populateProductsTable(productIdListStr);
                }
            });
    });
}

///////////////////////////////////////////////////////////////////////////
//
//
///////////////////////////////////////////////////////////////////////////
function displayComponentInfo(dbId) {

    getPropertyValue(_viewer, dbId, "SAPProductId",
        function (productId) {

            if (productId === '') {
                $('#propsDialog').dialog('close');
                return;
            }

            $.getJSON(window.location + '/api/sap?productId=' + productId,
                function (product) {

                    var table = document.getElementById("propsTable");

                    clearTable(table);

                    addTableRow(table, ["ProductId", product.ProductId]);
                    addTableRow(table, ["Name", product.Name]);
                    addTableRow(table, ["Supplier Id", product.SupplierId]);
                    addTableRow(table, ["Supplier Name", product.SupplierName]);
                    addTableRow(table, ["Price", product.Price]);
                    addTableRow(table, ["Currency", getCurrency(product.Currency)]);

                    var container = $('#viewerContainer');

                    var dlg = $("#propsDialog");

                    dlg.dialog('open');
                });
        });
}

///////////////////////////////////////////////////////////////////////////
//
//
///////////////////////////////////////////////////////////////////////////
function addToCombo(modelName, modelURN) {

    var combo = document.getElementById("combo");
    var option = document.createElement("option");

    option.text = modelName;
    option.modelURN = modelURN;

    try {
        combo.add(option, null);
    }
    catch (error) {
        combo.add(option); // IE only
    }
}

function onModelSelected() {

    var combo = document.getElementById("combo");

    var modelURN = combo.options[combo.selectedIndex].modelURN;

    clearCurrentModel();

    _viewer = createViewer();

    loadDocument(_auth, _viewer, modelURN);
}

///////////////////////////////////////////////////////////////////////////
//
//
///////////////////////////////////////////////////////////////////////////
function onViewerItemSelected(event) {

    $('#propsDialog').dialog('close');

    var dbIdArray = event.dbIdArray;

    for (var i = 0; i < dbIdArray.length; i++) {

        var dbId = dbIdArray[i];

        displayComponentInfo(dbId);
    }
}

///////////////////////////////////////////////////////////////////////////
//
//
///////////////////////////////////////////////////////////////////////////
function authenticate(accessToken) {

    var options = {};

    options.env = "AutodeskProduction";

    options.accessToken = accessToken;

    initializeEnvironmentVariable(options);
    initializeServiceEndPoints();

    return initializeAuth(null, options);
}

///////////////////////////////////////////////////////////////////////////
//
//
///////////////////////////////////////////////////////////////////////////
function initialize() {

    $('#layoutContainer').layout({

        north__paneSelector: "#paneNorth",
        center__paneSelector: "#paneCenter",
        west__paneSelector: "#paneWest",
        south__paneSelector: "#paneSouth",

        north__resizable: false,

        center__size: 650,
        south__size: 500,
        west__size: 400,

        center__onresize: function () {
            _viewer.resize();
        }
    });

    $.getJSON(window.location + '/api/credentials', function (accessToken) {

        console.log("Access Token:" + accessToken);

        _auth = authenticate(accessToken);

        _viewer = createViewer();

        var documentId = 'dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6c2FwMS9TZWF0LmR3Zg==';

        loadDocument(_auth, _viewer, documentId);
    })
    .done(function () { })
    .fail(function (jqXHR, textStatus, errorThrown) {
        //alert('getJSON request failed! ' + textStatus);
    })
    .always();

    var container = $('#viewerContainer');

    var dlg = $("#propsDialog").dialog({
        width: 'auto',
        autoResize: true,
        modal: false,
        autoOpen: false,
        closeOnEscape: true,
        resizable: false,
        //position: ['left', 20],
        open: function () {
            $('.ui-dialog').addClass('custom');
        },
        close: function () {
            $('.ui-dialog').removeClass('custom');
        }
    });

    dlg.parent().draggable({
        containment: '#viewerContainer'
    });

    $('#propsDialog').hover(
        function () {
            $('.ui-dialog.custom').addClass('hovered');
        },
        function () {
            $('.ui-dialog.custom').removeClass('hovered');
        }
    );

    $('#urn').keydown(function (e) {
        if (e.keyCode === 13 && this.value !== '') {
            clearCurrentModel();

            _viewer = createViewer();

            loadDocument(_auth, _viewer, this.value);
        }
    });

    $(document).keyup(function (e) {

        // esc
        if (e.keyCode == 27) {

            $('#propsDialog').dialog('close');

            _viewer.getObjectTree(function (rootComponent) {

                _viewer.docstructure.handleAction(
                    ["focus"],
                    rootComponent.dbId);
            });
        }
    });
}

var app = angular.module('Angular', ['ngGrid']);

app.controller('TableCtrl', function ($scope, $timeout, currencyCodes) {

    $scope.hashToArray = function (hash) {
        var array = [];
        for (var key in hash) {
            array.push({ id: parseInt(key, 10), value: hash[key] });
        }
        return array;
    };

    $scope.statuses = $scope.hashToArray(currencyCodes);

    $scope.cellInputEditableTemplate =
        '<input ng-class="\'colt\' + col.index" ' +
        'ng-input="COL_FIELD" ' +
        'ng-model="COL_FIELD" ' +
        'ng-blur="updateEntity(row)" />';

    $scope.cellSelectEditableTemplate =
        '<select ng-class="\'colt\' + col.index" ' +
        'ng-input="COL_FIELD" ng-model="COL_FIELD" ' +
        'ng-options="item.id as item.value for item in statuses" ' +
        'ng-blur="updateEntity(row)" />';

    $scope.data = [
      //{ ProductId: 'ProductId', Name: 'Name', SupplierId: 'SupplierId', SupplierName: 'SupplierName', Currency: 1, Price: 'Price' }
    ];

    $scope.setData = function (data) {
        $scope.data = data;
    };

    $scope.gridOptions = {
        data: 'data',
        enableRowSelection: false,
        enableCellEditOnFocus: true,
        multiSelect: false,
        columnDefs: [
        {
            field: 'ProductId',
            displayName: 'Product Id',
            enableCellEdit: false
        },
        {
            field: 'Name',
            displayName: 'Name',
            enableCellEdit: false
        },
        {
            field: 'SupplierId',
            displayName: 'Supplier Id',
            enableCellEdit: false
        },
        {
            field: 'SupplierName',
            displayName: 'Supplier Name',
            enableCellEdit: false
        },
        {
            field: 'Currency',
            displayName: 'Currency',
            enableCellEdit: true,
            //enableCellEditOnFocus: true,
            editableCellTemplate: $scope.cellSelectEditableTemplate,
            cellFilter: 'mapStatus'
        },
        {
            field: 'Price',
            displayName: 'Price',
            enableCellEdit: true,
            //enableCellEditOnFocus: true,
            editableCellTemplate: $scope.cellInputEditableTemplate
        }]
    };

    $scope.updateEntity = function (row) {

        if (!$scope.save) {
            $scope.save = {
                promise: null,
                pending: false,
                row: null
            };
        }

        $scope.save.row = row.rowIndex;

        if (!$scope.save.pending) {

            $scope.save.pending = true;
            $scope.save.promise = $timeout(function () {

                // $scope.list[$scope.save.row].$update();

                //console.log("Here you'd save your record to the server, we're updating row: "
                //            + $scope.save.row + " to be: "
                //            + $scope.list[$scope.save.row].name + ","
                //            + $scope.list[$scope.save.row].age + ","
                //            + $scope.list[$scope.save.row].status);

                $scope.save.pending = false;

            }, 500);
        }
    };

    $scope.$on('ngGridEventStartCellEdit', function () {
        console.log('ngGridEventStartCellEdit');
    });

    $scope.$on('ngGridEventEndCellEdit', function (data) {
        if (!data.targetScope.row) {
            data.targetScope.row = [];
        }

        var product = data.targetScope.row.entity;

        console.log(product);

        $.ajax({
            type: "POST",
            data: JSON.stringify(product),
            url: window.location + '/api/sap',
            contentType: "application/json",
            dataType: "json"
        });
    });
})

.directive('ngBlur', function () {
    return function (scope, elem, attrs) {
        elem.bind('blur', function () {
            scope.$apply(attrs.ngBlur);
        });
    };
})

.filter('mapStatus', function (currencyCodes) {
    return function (input) {
        if (currencyCodes[input]) {
            return currencyCodes[input];
        } else {
            return 'Unknown';
        }
    };
})

.factory('currencyCodes', function () {
    return {
        0: 'EUR',
        1: 'USD',
        2: 'JPY',
        3: 'MXN',
        4: 'ARS',
        5: 'GBP',
        6: 'CAD',
        7: 'BRL',
        8: 'CHF',
        9: 'ZAR',
        10: 'INR',
        11: 'PLN',
        12: 'CNY',
        13: 'DKK',
        14: 'RUB'
    };
});

function getCurrency(code) {

    switch (code) {
        case 0: return 'EUR';
        case 1: return 'USD';
        case 2: return 'JPY';
        case 3: return 'MXN';
        case 4: return 'ARS';
        case 5: return 'GBP';
        case 6: return 'CAD';
        case 7: return 'BRL';
        case 8: return 'CHF';
        case 9: return 'ZAR';
        case 10: return 'INR';
        case 11: return 'PLN';
        case 12: return 'CNY';
        case 13: return 'DKK';
        case 14: return 'RUB'
        default: return 'Unknown';
    }
};

$(document).ready(function () {

    initialize();

    addToCombo('Seat', 'dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6c2FwMS9TZWF0LmR3Zg==');
    addToCombo('Chassis', 'dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6c2FwMS9DaGFzc2lzLmR3Zg==');
    addToCombo('Suspension', 'dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6c2FwMS9TdXNwZW5zaW9uLmR3Zg==');
    addToCombo('Trailer', 'dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6c2FwMS9UcmFpbGVyLmR3Zg==');
});

/*
you want ng-grid to initialize after you have data.

The following solution requires using angular-ui.

<div ui-if="dataForGrid.length>0" ng-grid="gridOptions" ng-style="getTableStyle()"  />

$scope.getTableStyle= function() {
   var rowHeight=30;
   var headerHeight=45;
   return {
      height: ($scope.dataForGrid.length * rowHeight + headerHeight) + "px"
   };
};*/

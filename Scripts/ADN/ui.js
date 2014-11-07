///////////////////////////////////////////////////////////////////////////////
// Copyright (c) Autodesk, Inc. All rights reserved 
// Written by Philippe Leefsma 2014 - ADN/Developer Technical Services
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted, 
// provided that the above copyright notice appears in all copies and 
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting 
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS. 
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC. 
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
///////////////////////////////////////////////////////////////////////////////

var adnViewerMng;

///////////////////////////////////////////////////////////////////////////
//
//
///////////////////////////////////////////////////////////////////////////
function getToken() {

    var url = window.location + '/api/credentials?json=true';

    var xhr = new XMLHttpRequest();

    xhr.open("GET", url, false);

    xhr.send(null);

    var token = xhr.responseText.substring(
        1, xhr.responseText.length - 1);

    return token;
}

///////////////////////////////////////////////////////////////////////////
//
//
///////////////////////////////////////////////////////////////////////////
$(document).ready(function () {

    initializeUI();

    addToCombo('Seat', 'dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6YWRuLTE3LjA3LjIwMTQtMTAuNTYuMTYvU2VhdDA4MTAuZHdm');
    addToCombo('Chassis', 'dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6c2FwMS9DaGFzc2lzLmR3Zg==');
    addToCombo('Suspension', 'dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6c2FwMS9TdXNwZW5zaW9uLmR3Zg==');
    addToCombo('Trailer', 'dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6c2FwMS9UcmFpbGVyLmR3Zg==');

    adnViewerMng = new Autodesk.ADN.Toolkit.Viewer.AdnViewerManager(
       getToken,
       document.getElementById('viewerContainer'));

    loadDocument('dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6YWRuLTE3LjA3LjIwMTQtMTAuNTYuMTYvU2VhdDA4MTAuZHdm');
});

///////////////////////////////////////////////////////////////////////////
//
//
///////////////////////////////////////////////////////////////////////////
function loadDocument(urn) {

    adnViewerMng.loadDocument(urn,

        function (viewer) {

            viewer.loadExtension('SAP Property Panel');

            viewer.addEventListener(

                Autodesk.Viewing.GEOMETRY_LOADED_EVENT,

                function (event) {

                    initializeTree(viewer);

                    initializeTable(viewer);

                    viewer.addEventListener(
                        Autodesk.Viewing.SELECTION_CHANGED_EVENT,
                        onItemSelected);
                });
        });
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
}

///////////////////////////////////////////////////////////////////////////
// Populate Tree with components
//
///////////////////////////////////////////////////////////////////////////
function initializeTree(viewer) {

    $('#jstree').jstree({

        'core': {
            check_callback: true
        }
    });

    $('#jstree').on("ready.jstree",
        function (e, data) {

            var treeRef = $('#jstree').jstree(true);

            viewer.getObjectTree(function (rootComponent) {

                var rootNode = createNode(
                    treeRef,
                    '#',
                    rootComponent);

                buildTreeRec(treeRef, rootNode, rootComponent);

                $('#jstree').jstree("open_node", rootNode);
            });
        });

    $("#jstree").on('before.jstree',
        function (e, data) {
            //console.log('b4');
        });

    $("#jstree").on('changed.jstree',
        function (e, data) {
            //data.selected.length
            //console.log(data.instance.get_node(data.selected[0]).text);
        });

    $("#jstree").on("select_node.jstree",
        function (event, data) {

            var node = data.node;

            //console.log(node);
        });

    $("#jstree").on("dblclick.jstree",
       function (event) {

           var ids = $('#jstree').jstree('get_selected');

           var dbId = parseInt(ids[0]);

           viewer.isolateById(dbId);

           viewer.fitToView(dbId);
       });

    function createNode(tree, parentNode, component) {

        var icon = (component.children ?
            '/sapdemo/Images/parent.png':
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
function initializeTable(viewer) {

    var productIdList = [];

    viewer.getAllLeafComponents(

        function (components) {

            async.each(components,
                function (component, callback) {

                    viewer.getPropertyValue(
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

                        $.getJSON(window.location + '/api/sap?productIdList=' + productIdListStr,

                            function (products) {

                                var controller = angular.element($('#tableCtrlId')).scope();

                                controller.setData(products);
                                controller.$apply();
                            }
                        );
                    }
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

///////////////////////////////////////////////////////////////////////////
//
//
///////////////////////////////////////////////////////////////////////////
function onModelSelected() {

    clearCurrentModel();

    var combo = document.getElementById("combo");

    var urn = combo.options[combo.selectedIndex].modelURN;

    loadDocument(urn);
}

///////////////////////////////////////////////////////////////////////////
//
//
///////////////////////////////////////////////////////////////////////////
function onItemSelected(event) {

    $('#propsDialog').dialog('close');

    var dbIdArray = event.dbIdArray;

    for (var i = 0; i < dbIdArray.length; i++) {

        var dbId = dbIdArray[i];

    }
}

///////////////////////////////////////////////////////////////////////////
//
//
///////////////////////////////////////////////////////////////////////////
function displayComponentInfo(dbId) {

    adnViewerMng.getPropertyValue(dbId, "SAPProductId",
        function (productId) {

            if (productId === 'undefined') {
                $('#propsDialog').dialog('close');
                return;
            }

            $.getJSON(window.location + '/api/sap?productId=' + productId,
                function (product) {

                    var properties = [
                        { displayName: 'Product Name', displayValue: product.Name},
                        { displayName: 'Description', displayValue: product.Description },
                        { displayName: 'Supplier Name', displayValue: product.SupplierName },
                        { displayName: 'Price', displayValue: product.Price },
                        { displayName: 'Currency', displayValue: getCurrency(product.Currency) },
                    ];

  
                    var table = document.getElementById("propsTable");

                    clearTable(table);

                    addTableRow(table, ["ProductId", product.ProductId]);
                    addTableRow(table, ["Name", product.Name]);
                    addTableRow(table, ["Description", product.Description]);
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
function initializeUI() {

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
            adnViewerMng.getViewer().resize();
        }
    });

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

            adnViewerMng.loadDocument(
              this.value,
              function (viewer) {
                  viewer.impl.setLightPreset(8);
              });
        }
    });

    $(document).keyup(function (e) {

        // esc
        if (e.keyCode == 27) {

            $('#propsDialog').dialog('close');
        }
    });
}

///////////////////////////////////////////////////////////////////////////
// AngularJS code
//
///////////////////////////////////////////////////////////////////////////
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

    $scope.data = [];

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
            field: 'Description',
            displayName: 'Description',
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
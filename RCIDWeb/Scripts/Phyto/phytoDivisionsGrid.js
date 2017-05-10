$(function () {
   // debugger;
    $("#grid").jqGrid
        ({
            url: "/Phyto/GetDivisionsGrid",
            datatype: 'json',
            mtype: 'Get',
            //table header name   
            colNames: ['DivisionID', 'Division Name', 'Common Name','Is Active'],
            //prmNames is needed to send the id to the controller
            prmNames: { id: "DivisionID" },
            //colModel takes the data from controller and binds to grid 
            sortname: 'DivisionName',
            colModel: [
                {
                    key: true,
                    hidden: true,
                    name: 'DivisionID',
                    index: 'DivisionID', 
                    defaultValue: 0
                }, {
                    key: false,
                    name: 'DivisionName',
                    index: 'DivisionName',
                    editable: true
                },{
                    key: false,
                    name: 'DivisionCommonName',
                    index: 'DivisionCommonName',
                    editable: true
                }, {
                    key: false,
                    name: 'DivisionActive',
                    index: 'DivisionActive',
                    editable: true,                    
                    edittype: 'checkbox',
                    editoptions: { value: "true:false", defaultValue:"true" }
                }],

            pager: jQuery('#pager'),
            rowNum: 10,
            rowList: [10, 20, 30, 40],
            height: '100%',
            viewrecords: true,
            caption: 'Phyto Division',
            emptyrecords: 'No records to display',
            jsonReader:
            {
                root: "rows",
                page: "page",
                total: "total",
                records: "records",
                repeatitems: false,
                Id: "0"
            },
            autowidth: true,
            multiselect: false
            //pager-you have to choose here what icons should appear at the bottom  
            //like edit,create,delete icons  
        }).navGrid('#pager',
        {
            edit: true,
            add: true,
            //del: true,
            search: false,
            refresh: true
        }, {
            // edit options  
            zIndex: 100,
            url: '/Phyto/EditDivision',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,               
            afterComplete: function (response) {
                DisplayResult(response);
            }
        }, {
            // add options  
            zIndex: 100,
            url: "/Phyto/CreateDivision",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                DisplayResult(response);
            }
        }, {
            // delete options  
            zIndex: 100,
            url: "/Phyto/DeleteDivision",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,           
            afterComplete: function (response) {
                DisplayResult(response);
            }
        });
});  
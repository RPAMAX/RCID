$(function () {
   // debugger;
    $("#grid").jqGrid
        ({
            url: "/Lake/GetParametersGrid",
            datatype: 'json',
            mtype: 'Get',
            //table header name   
            colNames: ['ID','Name', 'Full Name', 'Test Method','Unit', 'Is Active'],
            //prmNames is needed to send the id to the controller
            prmNames: { id: "ParameterID" },
            //colModel takes the data from controller and binds to grid 
            sortname: 'ParameterName',
            colModel: [
                {
                    key: true,
                    hidden: true,
                    name: 'ParameterID',
                    index: 'ParameterID', 
                    defaultValue: 0
                }, {
                    key: false,
                    name: 'ParameterName',                    
                    editable: true
                }, {
                    key: false,
                    name: 'ParameterFullName',
                    editable: true
                },{
                    key: false,
                    name: 'ParameterTestMethod',                    
                    editable: true
                },{
                    key: false,
                    name: 'ParameterUnit',
                    editable: true
                },{
                    key: false,
                    name: 'ParameterActive',                    
                    editable: true,                    
                    edittype: 'checkbox',
                    editoptions: { value: "true:false", defaultValue: "true", defaultValue:"true" }
                }],

            pager: jQuery('#pager'),
            rowNum: 10,
            rowList: [10, 20, 30, 40],
            height: '100%',
            viewrecords: true,
            caption: 'Lake Parameter',
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
            url: '/Lake/EditParameter',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,               
            afterComplete: function (response) {
                DisplayResult(response);
            }
        }, {
            // add options  
            zIndex: 100,
            url: "/Lake/CreateParameter",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                DisplayResult(response);
            }
        }, {
            // delete options  
            zIndex: 100,
            url: "/Lake/DeleteParameter",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,           
            afterComplete: function (response) {
                DisplayResult(response);
            }
        });
});  
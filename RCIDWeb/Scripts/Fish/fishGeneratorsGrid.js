$(function () {
   // debugger;
    $("#grid").jqGrid
        ({
            url: "/Fish/GetGeneratorsGrid",
            datatype: 'json',
            mtype: 'Get',
            //table header name   
            colNames: ['GeneratorID', 'Generator Name', 'Is Active'],
            //prmNames is needed to send the id to the controller
            prmNames: { id: "GeneratorID" },
            //colModel takes the data from controller and binds to grid 
            sortname: 'GeneratorName',
            colModel: [
                {
                    key: true,
                    hidden: true,
                    name: 'GeneratorID',
                    index: 'GeneratorID', 
                    defaultValue: 0
                }, {
                    key: false,
                    name: 'GeneratorName',
                    index: 'GeneratorName',
                    editable: true
                }, {
                    key: false,
                    name: 'GeneratorActive',
                    index: 'GeneratorActive',
                    editable: true,                    
                    edittype: 'checkbox',
                    editoptions: { value: "true:false", defaultValue: "true", defaultValue: "true" }
                }],

            pager: jQuery('#pager'),
            rowNum: 10,
            rowList: [10, 20, 30, 40],
            height: '100%',
            viewrecords: true,
            caption: 'Fish Generator',
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
            url: '/Fish/EditGenerator',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,               
            afterComplete: function (response) {
                DisplayResult(response);
            }
        }, {
            // add options  
            zIndex: 100,
            url: "/Fish/CreateGenerator",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                DisplayResult(response);
            }
        }, {
            // delete options  
            zIndex: 100,
            url: "/Fish/DeleteGenerator",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,           
            afterComplete: function (response) {
                DisplayResult(response);
            }
        });
});  
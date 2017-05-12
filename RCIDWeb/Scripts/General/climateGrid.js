$(function () {
   // debugger;
    $("#grid").jqGrid
        ({
            url: "/General/GetClimatesGrid",
            datatype: 'json',
            mtype: 'Get',
            //table header name   
            colNames: ['ClimateID', 'Climate Name', 'Is Active'],
            //prmNames is needed to send the id to the controller
            prmNames: { id: "ClimateID" },
            //colModel takes the data from controller and binds to grid 
            sortname: 'ClimateName',
            colModel: [
                {
                    key: true,
                    hidden: true,
                    name: 'ClimateID',
                    index: 'ClimateID', 
                    defaultValue: 0
                }, {
                    key: false,
                    name: 'ClimateName',
                    index: 'ClimateName',
                    editable: true
                }, {
                    key: false,
                    name: 'ClimateActive',
                    index: 'ClimateActive',
                    editable: true,                    
                    edittype: 'checkbox',
                    editoptions: { value: "true:false", defaultValue: "true" }
                }],

            pager: jQuery('#pager'),
            rowNum: 10,
            rowList: [10, 20, 30, 40],
            height: '100%',
            viewrecords: true,
            caption: 'General Climate',
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
            url: '/General/EditClimate',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,               
            afterComplete: function (response) {
                DisplayResult(response);
            }
        }, {
            // add options  
            zIndex: 100,
            url: "/General/CreateClimate",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                DisplayResult(response);
            }
        }, {
            // delete options  
            zIndex: 100,
            url: "/General/DeleteClimate",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,           
            afterComplete: function (response) {
                DisplayResult(response);
            }
        });
});  
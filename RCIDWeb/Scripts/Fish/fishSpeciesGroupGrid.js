$(function () {
   // debugger;
    $("#grid").jqGrid
        ({
            url: "/Fish/GetSpeciesGroup",
            datatype: 'json',
            mtype: 'Get',
            //table header name   
            colNames: ['SpeciesGroupID', 'Species Group Name', 'Is Active'],
            //prmNames is needed to send the id to the controller
            prmNames: { id: "SpeciesGroupID" },
            //colModel takes the data from controller and binds to grid 
            sortname: 'SpeciesGroupName',
            colModel: [
                {
                    key: true,
                    hidden: true,
                    name: 'SpeciesGroupID',
                    index: 'SpeciesGroupID', 
                    defaultValue: 0
                }, {
                    key: false,
                    name: 'SpeciesGroupName',
                    index: 'SpeciesGroupName',
                    editable: true
                }, {
                    key: false,
                    name: 'SpeciesGroupActive',
                    index: 'SpeciesGroupActive',
                    editable: true,                    
                    edittype: 'checkbox',
                    editoptions: { value: "true:false" }
                }],

            pager: jQuery('#pager'),
            rowNum: 10,
            rowList: [10, 20, 30, 40],
            height: '100%',
            viewrecords: true,
            caption: 'Fish Species Group',
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
            url: '/Fish/EditSpeciesGroup',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,               
            afterComplete: function (response) {
                DisplayResult(response);
            }
        }, {
            // add options  
            zIndex: 100,
            url: "/Fish/CreateSpeciesGroup",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                DisplayResult(response);
            }
        }, {
            // delete options  
            zIndex: 100,
            url: "/Fish/DeleteSpeciesGroup",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,           
            afterComplete: function (response) {
                DisplayResult(response);
            }
        });
});  
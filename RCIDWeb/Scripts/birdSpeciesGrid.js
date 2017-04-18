$(function () {
   // debugger;
    $("#grid").jqGrid
        ({
            url: "/Birds/GetSpecies",
            datatype: 'json',
            mtype: 'Get',
            //table header name   
            colNames: ['SpeciesID', 'Species Name'],
            //prmNames is needed to send the id to the controller
            prmNames: { id: "SpeciesID" },
            //colModel takes the data from controller and binds to grid 
            sortname: 'SpeciesName',
            colModel: [
                {
                    key: true,
                    hidden: true,
                    name: 'SpeciesID',
                    index: 'SpeciesID', 
                    defaultValue: 0
                }, {
                    key: false,
                    name: 'SpeciesName',
                    index: 'SpeciesName',
                    editable: true
                }],

            pager: jQuery('#pager'),
            rowNum: 10,
            rowList: [10, 20, 30, 40],
            height: '100%',
            viewrecords: true,
            caption: 'Bird species',
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
            url: '/Birds/EditSpecies',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,               
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                    $("#successMsgDiv").text(response.responseText);
                    $("#successMsgDiv").show();
                }
            }
        }, {
            // add options  
            zIndex: 100,
            url: "/Birds/CreateSpecies",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                    $("#successMsgDiv").text(response.responseText);
                    $("#successMsgDiv").show();
                }
            }
        /*}, {
            // delete options  
            zIndex: 100,
            url: "/Birds/DeleteSpecies",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure you want to delete this item?",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }*/
        });
});  
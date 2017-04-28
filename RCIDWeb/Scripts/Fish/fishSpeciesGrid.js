$(function () {
   // debugger;
    $("#grid").jqGrid
        ({
            url: "/Fish/GetSpecies",
            datatype: 'json',
            mtype: 'Get',
            //table header name   
            colNames: ['SpeciesID', 'Species Name', 'Is Active'],
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
                    editable: true,
                    editoptions: {size: '20', maxlength: '50'}
                }, {
                    key: false,
                    name: 'SpeciesActive',
                    index: 'SpeciesActive',
                    editable: true,                    
                    edittype: 'checkbox',
                    editoptions: { value: "true:false" }
                }],

            pager: jQuery('#pager'),
            rowNum: 10,
            rowList: [10, 20, 30, 40],
            height: '100%',
            viewrecords: true,
            caption: 'Fish Species',
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
            width: 400,
            url: '/Fish/EditSpecies',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,               
            afterComplete: function (response) {
                if (response.responseText) {   
                    if (response.responseText.indexOf("error") == -1) {
                        $("#successMsgDiv").text(response.responseText);
                        $("#successMsgDiv").show();
                    } else
                    {                       
                        $("#errorMsgDiv").text(response.responseText);
                        $("#errorMsgDiv").show();
                    }
                }
            }
        }, {
            // add options  
            zIndex: 100,
            width: 400,
            url: "/Fish/CreateSpecies",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {                   
                    $("#successMsgDiv").text(response.responseText);
                    $("#successMsgDiv").show();
                }
            }
        }, {
            // delete options  
            zIndex: 100,
            url: "/Fish/DeleteSpecies",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,           
            afterComplete: function (response) {
                if (response.responseText) {
                    $("#successMsgDiv").text(response.responseText);
                    $("#successMsgDiv").show();
                }
            }
        });
});  
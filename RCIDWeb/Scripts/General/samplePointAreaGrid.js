$(function () {
   // debugger;
    $("#grid").jqGrid
        ({
            url: "/General/GetSamplePointAreasGrid",
            datatype: 'json',
            mtype: 'Get',
            //table header name   
            colNames: ['SamplePointAreaID', 'SourceID', 'Sample Point Area Name', 'Is Active'],
            //prmNames is needed to send the id to the controller
            prmNames: { id: "SamplePointAreaID" },
            //colModel takes the data from controller and binds to grid 
            sortname: 'SamplePointAreaName',
            colModel: [
                {
                    key: true,
                    hidden: true,
                    name: 'SamplePointAreaID',
                    index: 'SamplePointAreaID', 
                    defaultValue: 0
                }, {
                    key: false,
                    hidden: true,
                    name: 'SourceID',
                    index: 'SourceID',
                    editable: true
                },{
                    key: false,
                    name: 'SamplePointAreaName',
                    index: 'SamplePointAreaName',
                    editable: true
                }, {
                    key: false,
                    name: 'SamplePointAreaActive',
                    index: 'SamplePointAreaActive',
                    editable: true,                    
                    edittype: 'checkbox',
                    editoptions: { value: "true:false" }
                }],

            pager: jQuery('#pager'),
            rowNum: 10,
            rowList: [10, 20, 30, 40],
            height: '100%',
            viewrecords: true,
            caption: 'Sample Point Areas',
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
            url: '/General/EditSamplePointArea',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,               
            afterComplete: function (response) {
                if (response.responseText) {                   
                    $("#successMsgDiv").text(response.responseText);
                    $("#successMsgDiv").show();
                }
            }
        }, {
            // add options  
            zIndex: 100,
            url: "/General/CreateSamplePointArea",
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
            url: "/General/DeleteSamplePointArea",
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
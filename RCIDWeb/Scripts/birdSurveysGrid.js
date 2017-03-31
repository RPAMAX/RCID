$(function () {
    debugger;
    $("#grid").jqGrid
        ({
            url: "/Birds/GetSurveys",
            datatype: 'json',
            mtype: 'Get',
            //table header name   
            colNames: ['SurveyId', 'Survey Date', 'Climate Name', 'Sample Point Area'],
            //colModel takes the data from controller and binds to grid   
            colModel: [
                {
                    key: true,
                    hidden: true,
                    name: 'SurveyId',
                    index: 'SurveyId',
                    editable: true
                }, {
                    key: false,
                    label: 'SurveyDate',
                    name: 'SurveyDate',
                    //index: 'SurveyorName',
                    editable: true,
                    formatter: 'date'
                }, {
                    key: false,
                    label: 'ClimateName',
                    name: 'ClimateName',
                    index: 'ClimateName',
                    editable: true,
                    edittype: 'select',
                    //   editoptions: { value:"1:Warm Months (Mar-Nov);2:Cold Months (Dec-Feb)"}
                    editoptions: {
                        dataUrl: "/General/GetClimates",
                        buildSelect: function (data) {
                            var response = jQuery.parseJSON(data);
                            var s = '<select>';
                            jQuery.each(response, function (i, item) {
                                s += '<option value="' + response[i].ClimateID + '">' + response[i].ClimateName + '</option>';
                            });
                            return s + "</select>";

                        }
                    }
                }, {
                    key: false,
                    label: 'SamplePointAreaName',
                    name: 'SamplePointAreaName',
                    index: 'SamplePointAreaName',
                    editable: true,
                    edittype: 'select',                 
                    editoptions: {
                        dataUrl: "/General/GetSamplePointAreas",
                        buildSelect: function (data) {
                            var response = jQuery.parseJSON(data);
                            var s = '<select>';
                            jQuery.each(response, function (i, item) {
                                s += '<option value="' + response[i].SamplePointAreaID + '">' + response[i].SamplePointAreaName + '</option>';
                            });
                            return s + "</select>";

                        }
                    }
                }],

            pager: jQuery('#pager'),
            rowNum: 10,
            rowList: [10, 20, 30, 40],
            height: '100%',
            viewrecords: true,
            caption: 'Bird surveyor',
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
            del: true,
            search: false,
            refresh: true
        }, {
            // edit options  
            zIndex: 100,
            url: '/Birds/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        }, {
            // add options  
            zIndex: 100,
            url: "/Jqgrid/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        }, {
            // delete options  
            zIndex: 100,
            url: "/Jqgrid/Delete",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure you want to delete this task?",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        });
});  


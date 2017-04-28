$(function () {
  //  debugger;
    $("#masterGrid").jqGrid
        ({
            url: "/Fish/GetSurveys",
            datatype: 'json',
            mtype: 'Get',
            //table header name   
            colNames: ['SurveyID', 'Survey Year', 'SamplePointAreaName', 'Sample Point Area', 'Comments'],
            prmNames: { id: "SurveyID" },
            //colModel takes the data from controller and binds to grid   
            colModel: [
                {
                    key: true,
                    hidden: true,
                    name: 'SurveyID',
                    index: 'SurveyID',
                    editable: true
                }, {
                    key: false,
                    label: 'SurveyYear',
                    name: 'SurveyYear',
                    editable: true,
                    editrules:
                    {
                        number: true,
                        minValue: 1950,
                        maxValue: new Date().getFullYear()
                    },
                }, {
                    key: false,
                    label: 'SamplePointAreaName',
                    name: 'SamplePointAreaName',
                    index: 'SamplePointAreaName',
                    editable: false

                }, {
                    key: false,
                    label: 'SamplePointAreaID',
                    name: 'SamplePointAreaID',
                    index: 'SamplePointAreaID',
                    editrules: { edithidden: true }, hidedlg: true,
                    hidden: true,
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
                }, {
                    key: false,                    
                    name: 'SurveyComments',
                    editable: true,
                    edittype: 'textarea',
                    editoptions:{ rows: '5', cols: 50 }
                }],

            pager: jQuery('#pager'),
            rowNum: 10,
            rowList: [10, 20, 30, 40],
            height: '100%',            
            viewrecords: true,
            caption: 'Fish Surveys',
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
            multiselect: false,
            onSelectRow: function (id) {                
                jQuery("#locationGrid")
                    .setGridParam({ postData: { id: id } })                   
                    .trigger('reloadGrid');                
            }, 
            //pager-you have to choose here what icons should appear at the bottom  
            //like edit,create,delete icons  
        }).navGrid('#pager',
        {
            edit: true,
            add: true,
            del: false,
            search: false,
            refresh: true
        }, {
            // edit options  
            zIndex: 100,
            width: 500,
            url: '/Fish/EditSurvey',
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
            width: 500,
            url: '/Fish/CreateSurvey',
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {                    
                    $("#successMsgDiv").text(response.responseText);
                    $("#successMsgDiv").show();
                }
            }
        //}, {
        //    //// delete options  
        //    //zIndex: 100,
        //    //url: "/Jqgrid/Delete",
        //    //closeOnEscape: true,
        //    //closeAfterDelete: true,
        //    //recreateForm: true,
        //    //msg: "Are you sure you want to delete this task?",
        //    //afterComplete: function (response) {
        //    //    if (response.responseText) {
        //    //        alert(response.responseText);
        //    //    }
        //    //}
        });
    
    $("#locationGrid").jqGrid
        ({
            url: '/Fish/GetSurveyLocations',
            datatype: 'json',
            mtype: 'Get',
            colNames: ['SurveyID', 'Number', 'Location Details', 'Date', 'Duration(seconds)', 'Generator', 'GeneratorName', 'Comments'],
            colModel: [
                {
                    key: false,
                    hidden: true,
                    name: 'SurveyID',
                    index: 'SurveyID',
                }, {
                    key: true,
                    name: 'SurveyNumber'

                }, {
                    key: false,
                    name: 'LocationDetails',
                    editable: true
                }, {
                    key: false,
                    label: 'SurveyDate',
                    name: 'SurveyDate',
                    editable: true,
                    formatter: 'date',
                    editoptions: {
                        size: 20,
                        maxlengh: 10,
                        dataInit: function (element) {
                            $(element).datepicker({
                                constrainInput: false,
                                showOn: 'button',
                                buttonText: '...'
                            });
                        }
                    },
                }, {
                    key: false,
                    name: 'SurveyDurationSeconds',
                    editable: true,
                    editrules:
                    {
                        number: true,
                        minValue: 0
                    },
                }, {
                    key: false,
                    name: 'GeneratorID',
                    hidden: true,
                    editable: true,
                    editrules: { edithidden: true }, hidedlg: true,
                    hidden: true,
                    editable: true,
                    edittype: 'select',
                    editoptions: {
                        dataUrl: "/Fish/GetGenerators",
                        buildSelect: function (data) {
                            var response = jQuery.parseJSON(data);
                            var s = '<select>';
                            jQuery.each(response, function (i, item) {
                                s += '<option value="' + response[i].GeneratorID + '">' + response[i].GeneratorName + '</option>';
                            });
                            return s + "</select>";

                        }
                    }
                }, {
                    key: false,
                    name: 'GeneratorName',
                    editable: false
                }, {
                    key: false,
                    name: 'Comments',
                    editable: true,
                    edittype: 'textarea',
                    editoptions: { rows: '5', cols: 50 }
                }
            ],
            pager: jQuery('#pagerL'),
            rowNum: 10,
            rowList: [10, 20, 30, 40],
            height: '100%',
            viewrecords: true,
            autowidth: true,
            multiselect: false,
            caption: 'Location grid',
            emptyrecords: 'No records to display',
            jsonReader:
            {
                root: "rows",
                page: "page",
                total: "total",
                records: "records",
                repeatitems: false,
                Id: "0"
            }

        }).navGrid('#pagerL',
                {
                    edit: true,
                    add: true,
                    del: false,
                    search: false,
                    refresh: true
        }, {
            // edit options  
            zIndex: 100,
            width: 500,
            url: '/Fish/UpdateSurveyLocation',
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    $("#successMsgDiv").text(response.responseText);
                    $("#successMsgDiv").show();
                }
            }        
        }, {
            // add options  
            zIndex: 100,
            width: 500,
            url: '/Fish/CreateSurveyLocation',
            closeOnEscape: true,
            closeAfterAdd: true,            
            onclickSubmit: function (response, postdata) {                
                var selectedRowId = $("#masterGrid").jqGrid('getGridParam', 'selrow');
                response.url = '/Fish/CreateSurveyLocation' + "?SurveyID=" + selectedRowId;
                alert(response.url);           
                
            },
            afterComplete: function (response) {
                if (response.responseText) {
                    $("#successMsgDiv").text(response.responseText);
                    $("#successMsgDiv").show();
                }
            }
        }
            );



});  


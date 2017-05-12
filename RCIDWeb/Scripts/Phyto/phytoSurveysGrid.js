$(function () {
  //  debugger;
    $("#masterGrid").jqGrid
        ({
            url: "/Phyto/GetSurveys",
            datatype: 'json',
            mtype: 'Get',
            //table header name   
            colNames: ['SurveyID', 'Survey Date', 'SamplePointArea', 'Sample Point Area Name','Location Details', 'Is Active'],
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
                                buttonText: '...',
                                maxDate: new Date()
                            });
                        }
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
                    shrinktofit: true,
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
                    name: 'LocationDetails',
                    shrinktofit: true,
                    editable: true,
                    edittype: 'textarea',
                    editoptions:{ rows: '5', cols: 50 }
                }, {
                    key: false,
                    name: 'SurveyActive',
                    width: 40,
                    shrinktofit: true,
                    editable: true,
                    edittype: 'checkbox',
                    editoptions: { value: "true:false", defaultValue: "true" }
                }],

            pager: jQuery('#pager'),
            rowNum: 10,
            rowList: [10, 20, 30, 40],
            height: '100%',            
            viewrecords: true,
            caption: 'Phyto Surveys',
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
                jQuery("#detailGrid")
                    .setGridParam({ postData: { id: id } })                   
                    .trigger('reloadGrid');                
            }, 
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
            width: 500,
            url: '/Phyto/EditSurvey',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function (response) {
                DisplayResult(response);
            }           
        }, {
            // add options  
            zIndex: 100,
            width: 500,
            url: '/Phyto/CreateSurvey',
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                DisplayResult(response);
            }
        }, {
            // delete options  
            zIndex: 100,
            url: "/Phyto/DeleteSurvey",
            closeOnEscape: true,
            closeAfterDelete: true,                        
            afterComplete: function (response) {
                DisplayResult(response);
            }
        });
    
    $("#detailGrid").jqGrid
        ({
            url: '/Phyto/GetSurveyDetails',
            datatype: 'json',
            mtype: 'Get',
            colNames: ['SurveyID', 'Species', 'Species Name', 'Count', 'Is Active'],
            prmNames: { id: "SpeciesID" },
            colModel: [
                {
                    key: false,
                    hidden: true,
                    name: 'SurveyID',
                    index: 'SurveyID',
                }, {
                    key: true,
                    name: 'SpeciesID',
                    hidden: true,
                    editable: true,
                    editrules: { edithidden: true }, hidedlg: true,
                    hidden: true,
                    editable: true,
                    edittype: 'select',
                    editoptions: {
                        dataUrl: "/Phyto/GetSpeciesList",
                        buildSelect: function (data) {
                            var response = jQuery.parseJSON(data);
                            var s = '<select>';
                            jQuery.each(response, function (i, item) {
                                s += '<option value="' + response[i].SpeciesID + '">' + response[i].SpeciesName + '</option>';
                            });
                            return s + "</select>";

                        }
                    }
                }, {
                    key: false,
                    name: 'SpeciesName',
                    editable: false
                },{
                    key: false,
                    name: 'SurveyCount',
                    width: 60,
                    editable: true,
                    editrules:
                    {
                        number: true,
                        minValue: 0
                    },
                },{
                    key: false,
                    name: 'SurveyDetailActive',
                    width: 40,
                    editable: true,
                    edittype: 'checkbox',
                    editoptions: { value: "true:false", defaultValue: "true" }
                }
            ],
            pager: jQuery('#pagerD'),
            rowNum: 10,
            rowList: [10, 20, 30, 40],
            height: '100%',
            viewrecords: true,
            autowidth: true,
            multiselect: false,             
            caption: 'Survey Details',
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

        }).navGrid('#pagerD',
                {
                    edit: true,
                    add: true,
                    del: true,
                    search: false,
                    refresh: true
        }, {
            // edit options  
            zIndex: 100,
            width: 500,
            url: '/Phyto/EditSurveyDetail',
            closeOnEscape: true,
            closeAfterEdit: true,
            onclickSubmit: function (response, postdata) {
                var selectedRowId = $("#masterGrid").jqGrid('getGridParam', 'selrow');
                response.url = '/Phyto/EditSurveyDetail' + "?SurveyID=" + selectedRowId;
            },
            afterComplete: function (response) {
                DisplayResult(response);
            }        
        }, {
            // add options  
            zIndex: 100,
            width: 500,
            url: '/Phyto/CreateSurveyDetail',
            closeOnEscape: true,
            closeAfterAdd: true,            
            onclickSubmit: function (response, postdata) {                
                var selectedRowId = $("#masterGrid").jqGrid('getGridParam', 'selrow');
                response.url = '/Phyto/CreateSurveyDetail' + "?SurveyID=" + selectedRowId;
            },
            afterComplete: function (response) {
                DisplayResult(response);
            }
        }, {
            // delete options  
            zIndex: 100,            
            url: '/Phyto/DeleteSurveyDetail',
            closeOnEscape: true,
            closeAfterDelete: true,
            onclickSubmit: function (response, postdata) {
                var selectedRowId = $("#masterGrid").jqGrid('getGridParam', 'selrow');
                response.url = '/Phyto/DeleteSurveyDetail' + "?SurveyID=" + selectedRowId;
            },
            afterComplete: function (response) {
                DisplayResult(response);
            }
        }
        );

});  


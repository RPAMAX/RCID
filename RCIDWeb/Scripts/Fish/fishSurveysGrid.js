$(function () {
  //  debugger;
    $("#masterGrid").jqGrid
        ({
            url: "/Fish/GetSurveys",
            datatype: 'json',
            mtype: 'Get',
            //table header name   
            colNames: ['SurveyID', 'Survey Year', 'Sample Point Area Name', 'Sample Point Area', 'Comments', 'Is Active'],
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
                    width: 40,
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
                    name: 'SurveyComments',
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
            del: true,
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
                DisplayResult(response);
            }           
        }, {
            // add options  
            zIndex: 100,
            width: 500,
            url: '/Fish/CreateSurvey',
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                DisplayResult(response);
            }
        }, {
            // delete options  
            zIndex: 100,
            url: "/Fish/DeleteSurvey",
            closeOnEscape: true,
            closeAfterDelete: true,                        
            afterComplete: function (response) {
                DisplayResult(response);
            }
        });
    
    $("#locationGrid").jqGrid
        ({
            url: '/Fish/GetSurveyLocations',
            datatype: 'json',
            mtype: 'Get',
            colNames: ['SurveyID', 'Number', 'Location Details', 'Date', 'Duration(seconds)', 'Generator', 'Generator Name', 'Comments', 'Is Active'],
            prmNames: { id: "SurveyNumber" },
            colModel: [
                {
                    key: false,
                    hidden: true,
                    name: 'SurveyID',
                    index: 'SurveyID',
                }, {
                    key: true,
                    hidden: true,
                    name: 'SurveyNumber',
                    index: 'SurveyNumber',
                    editoptions: { defaultValue: 1},                    
                    editable: true
                }, {
                    key: false,
                    name: 'LocationDetails',                    
                    editable: true
                }, {
                    key: false,
                    label: 'SurveyDate',
                    name: 'SurveyDate',
                    width: 40,
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
                    name: 'SurveyDurationSeconds',
                    width: 60,
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
                    width: 50,
                    editable: false
                }, {
                    key: false,
                    name: 'SurveyLocationComments',
                    editable: true,
                    edittype: 'textarea',
                    editoptions: { rows: '5', cols: 50}
                }, {
                    key: false,
                    name: 'SurveyLocationActive',
                    width: 40,
                    editable: true,
                    edittype: 'checkbox',
                    editoptions: { value: "true:false", defaultValue: "true" }
                }
            ],
            pager: jQuery('#pagerL'),
            rowNum: 10,
            rowList: [10, 20, 30, 40],
            height: '100%',
            viewrecords: true,
            autowidth: true,
            multiselect: false,
            onSelectRow: function (id) {
                var masterId = $("#masterGrid").jqGrid('getGridParam', 'selrow');
                jQuery("#detailGrid")
                    .setGridParam({ postData: { id: id, surveyID: masterId } })
                    .trigger('reloadGrid');
            }, 
            caption: 'Survey Locations',
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
                    del: true,
                    search: false,
                    refresh: true
        }, {
            // edit options  
            zIndex: 100,
            width: 500,
            url: '/Fish/EditSurveyLocation',
            closeOnEscape: true,
            closeAfterEdit: true,
            onclickSubmit: function (response, postdata) {
                var selectedRowId = $("#masterGrid").jqGrid('getGridParam', 'selrow');
                response.url = '/Fish/EditSurveyLocation' + "?SurveyID=" + selectedRowId;
            },
            afterComplete: function (response) {
                DisplayResult(response);
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
            },
            afterComplete: function (response) {
                DisplayResult(response);
            }
        }, {
            // delete options  
            zIndex: 100,            
            url: '/Fish/DeleteSurveyLocation',
            closeOnEscape: true,
            closeAfterDelete: true,
            onclickSubmit: function (response, postdata) {
                var selectedRowId = $("#masterGrid").jqGrid('getGridParam', 'selrow');
                response.url = '/Fish/DeleteSurveyLocation' + "?SurveyID=" + selectedRowId;
            },
            afterComplete: function (response) {
                DisplayResult(response);
            }
        }
        );


        $("#detailGrid").jqGrid
            ({
                url: '/Fish/GetSurveyDetails',
                datatype: 'json',
                mtype: 'Get',
                colNames: ['SurveyID', 'SurveyNumber', 'Detail #', 'Species Name', 'Species Name', 'Size mm', 'Size Inches',
                    'Size Inch Group', 'Weight(pounds)', 'Weight(ounces)', 'Total Weight(Lbs)', 'Is Active'],
                prmNames: { id: "SurveyDetailID" },
                colModel: [
                    {
                        key: false,
                        hidden: true,
                        name: 'SurveyID'                        
                    }, {
                        key: false,
                        hidden: true,
                        name: 'SurveyNumber'                        
                    }, {
                        key: true,
                        name: 'SurveyDetailID',
                        hidden: true,
                        editable: true,
                        editoptions: { defaultValue: 1 },   
                        width: 40
                    }, {
                        key: false,
                        name: 'SpeciesID',
                        hidden: true,
                        editable: true,
                        editrules: { edithidden: true }, hidedlg: true,
                        hidden: true,
                        editable: true,
                        edittype: 'select',
                        editoptions: {
                            dataUrl: "/Fish/GetSpeciesList",
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
                        name: 'SpeciesSizeMillimeters',
                        width: 50,
                        editable: true,
                        editrules:
                        {
                            number: true,
                            minValue: 0
                        },
                    },{
                        key: false,
                        name: 'SpeciesSizeInchesStr',
                        width: 40,
                        editable: false
                    }, {
                        key: false,
                        name: 'SpeciesSizeInchGroup',
                        width: 50,                                                
                        editable: false
                    }, {
                        key: false,
                        name: 'SpeciesWeightPounds',
                        width: 50,
                        editable: true,
                        editrules:
                        {
                            number: true,
                            minValue: 0
                        },
                    }, {
                        key: false,
                        name: 'SpeciesWeightOunces',
                        width: 50,
                        editable: true,
                        editrules:
                        {
                            number: true,
                            minValue: 0
                        },
                    }, {
                        key: false,
                        name: 'SpeciesWeightLbs',
                        width: 60,
                        editable: false
                    }, {
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
                url: '/Fish/EditSurveyDetail',
                closeOnEscape: true,
                closeAfterEdit: true,
                onclickSubmit: function (response, postdata) {
                    var surveyRowId = $("#masterGrid").jqGrid('getGridParam', 'selrow');
                    var surveyNumberId = $("#locationGrid").jqGrid('getGridParam', 'selrow');                    
                    response.url = '/Fish/EditSurveyDetail' + "?SurveyID=" + surveyRowId + "&SurveyNumber=" + surveyNumberId;
                },
                afterComplete: function (response) {
                    DisplayResult(response);
                }
            }, {
                // add options  
                zIndex: 100,
                width: 500,
                url: '/Fish/CreateSurveyDetail',
                closeOnEscape: true,
                closeAfterAdd: true,
                onclickSubmit: function (response, postdata) {
                    var selectedRowId = $("#masterGrid").jqGrid('getGridParam', 'selrow');
                    var surveyNumberId = $("#locationGrid").jqGrid('getGridParam', 'selrow');  
                    response.url = '/Fish/CreateSurveyDetail' + "?SurveyID=" + selectedRowId + "&SurveyNumber=" + surveyNumberId;
                },
                afterComplete: function (response) {
                    DisplayResult(response);
                }
            }, {
                // delete options  
                zIndex: 100,
                url: '/Fish/DeleteSurveyDetail',
                closeOnEscape: true,
                closeAfterDelete: true,
                onclickSubmit: function (response, postdata) {
                    var selectedRowId = $("#masterGrid").jqGrid('getGridParam', 'selrow');
                    var surveyNumberId = $("#locationGrid").jqGrid('getGridParam', 'selrow');  
                    response.url = '/Fish/DeleteSurveyDetail' + "?SurveyID=" + selectedRowId + "&SurveyNumber=" + surveyNumberId;
                },
                afterComplete: function (response) {
                    DisplayResult(response);
                }
            }
            );


});  


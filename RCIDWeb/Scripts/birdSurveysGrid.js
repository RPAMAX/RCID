$(function () {
  //  debugger;
    $("#masterGrid").jqGrid
        ({
            url: "/Birds/GetSurveys",
            datatype: 'json',
            mtype: 'Get',
            //table header name   
            colNames: ['SurveyID', 'Survey Date', 'Climate Name', 'Climate', 'Surveyor Name', 'Surveyor', 'Sample Point Area'],
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
                    //index: 'SurveyorName',
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
                    label: 'ClimateName',
                    name: 'ClimateName',
                    index: 'ClimateName',
                    editable: false                   
                                 
                }, {
                    key: false,
                    label: 'ClimateID',
                    name: 'ClimateID',
                    index: 'ClimateID',
                    editrules: { edithidden: true }, hidedlg: true,
                    hidden: true,
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
                    label: 'SurveyorName',
                    name: 'SurveyorName',
                    index: 'SurveyorName',
                    editable: false

                }, {
                    key: false,
                    label: 'SurveyorID',
                    name: 'SurveyorID',
                    index: 'SurveyorID',
                    editrules: { edithidden: true }, hidedlg: true,
                    hidden: true,
                    editable: true,
                    edittype: 'select',
                    //   editoptions: { value:"1:Warm Months (Mar-Nov);2:Cold Months (Dec-Feb)"}
                    editoptions: {
                        dataUrl: "/Birds/GetSurveyorsList",
                        buildSelect: function (data) {
                            var response = jQuery.parseJSON(data);
                            var s = '<select>';
                            jQuery.each(response, function (i, item) {
                                s += '<option value="' + response[i].SurveyorID + '">' + response[i].SurveyorName + '</option>';
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
           // del: true,
            search: false,
            refresh: true
        }, {
            // edit options  
            zIndex: 100,
            url: '/Birds/EditSurvey',
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
            url: '/Birds/CreateSurvey',
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
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
    
    $("#detailGrid").jqGrid
        ({
            url: '/Birds/GetSurveyDetails',
            datatype: 'json',
            mtype: 'Get',
            colNames: ['SurveyID', 'SpeciesID', 'SpeciesName', 'SurveyDetailCount'],
            colModel: [
                {
                    key: true,
                    hidden: true,
                    name: 'SpeciesID',
                    index: 'SpeciesID',
                }, {
                    key: false,
                    name: 'SpeciesName',
                    width: 100
                },
                {
                    key: false,
                    name: 'SurveyDetailCount',
                    width: 100
                },
                {
                    key: false,
                    name: 'SurveyID',
                    width: 100
                },
            ],
            pager: jQuery('#pagerD'),
            rowNum: 10,
            rowList: [10, 20, 30, 40],
            height: '100%',
            viewrecords: true,                       
            caption: 'Detail grid',
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
            
        }).navGrid('#pagerD', { add: false, edit: false, del: false, search: false, refresh: false });
});  


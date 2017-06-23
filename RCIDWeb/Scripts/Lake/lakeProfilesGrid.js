$(function () {
   // debugger;
    $("#masterGrid").jqGrid
        ({
            url: "/Lake/GetProfilesGrid",
            datatype: 'json',
            mtype: 'Get',
            //table header name   
            colNames: ['ID','Date', 'Sample Point Reference ID', 'Sample Point Name', 'Sample Point'],
            //prmNames is needed to send the id to the controller
            prmNames: { id: "ProfileID" },
            //colModel takes the data from controller and binds to grid 
            sortname: 'ProfileDate',
            colModel: [
                {            

                    key: true,
                    hidden: true,
                    name: 'ProfileID',
                    index: 'ProfileID', 
                    defaultValue: 0
                }, {
                    key: false,
                    name: 'ProfileDate',                                        
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
                    }                       
                }, {
                    key: false,
                    name: 'SamplePointRefID',
                    width: 60,
                    editable: false
                }, {
                    key: false,
                    name: 'SamplePointName',
                    editable: false
                }, {
                    key: false,
                    label: 'SamplePointID',
                    name: 'SamplePointID',
                    index: 'SamplePointID',
                    shrinktofit: true,
                    editrules: { edithidden: true }, hidedlg: true,
                    hidden: true,
                    editable: true,
                    edittype: 'select',
                    editoptions: {
                        dataUrl: "/General/GetActiveSamplePoints",
                        buildSelect: function (data) {
                            var response = jQuery.parseJSON(data);
                            var s = '<select>';
                            jQuery.each(response, function (i, item) {
                                s += '<option value="' + response[i].SamplePointID + '">' + response[i].SamplePointRefID + '</option>';
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
            caption: 'Lake Profiles',
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
            //del: true,
            search: false,
            refresh: true
        }, {
            // edit options  
            zIndex: 100,            
            url: '/Lake/EditProfile',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,               
            afterComplete: function (response) {
                DisplayResult(response);
            }
        }, {
            // add options  
            zIndex: 100,           
            url: "/Lake/CreateProfile",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                DisplayResult(response);
            }
        }, {
            // delete options  
            zIndex: 100,
            url: "/Lake/DeleteProfile",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,           
            afterComplete: function (response) {
                DisplayResult(response);
            }
        });
});  

var lastSel = null;

$("#detailGrid").jqGrid
    ({
        url: '/Lake/GetProfileDetailsGrid',
        datatype: 'json',
        mtype: 'Get',     
        prmNames: { id: "PrimaryKeyForView" },
        colNames: ['PK','ProfileID','Depth Feet', 'Parameter Name', 'Parameter', 'Value', 'Notes'],
        colModel: [
            {
                key: true,
                hidden: true,
                name: 'PrimaryKeyForView',
                index: 'PrimaryKeyForView',
            }, {
                key: false,
                name: 'ProfileID',
                index: 'ProfileID',
                editable: true,
                hidden: true                
            },{
                key: false,               
                name: 'DepthFeet',
                index: 'DepthFeet',
                editable: true,
                editoptions: { disabled: true},
                width: 30,
            }, {
                key: false,
                name: 'ParameterName',
                editable: false,
                width: 60,
            },{
                key: false,
                label: 'ParameterID',
                name: 'ParameterID',
                index: 'ParameterID',
                shrinktofit: true,
                editrules: { edithidden: true }, hidedlg: true,
                hidden: true,
                editable: true,
                edittype: 'select',
                editoptions: {
                    readonly: "readonly",
                    dataUrl: "/Lake/GetParameterList",
                    buildSelect: function (data) {
                        var response = jQuery.parseJSON(data);
                        var s = '<select>';
                        jQuery.each(response, function (i, item) {
                            s += '<option value="' + response[i].ParameterID + '">' + response[i].ParameterFullName + '</option>';
                        });
                        return s + "</select>";

                    }
                }
            },{
                key: false,
                name: 'ParameterValue',
                editable: true,
                width: 40
            }, {
                key: false,                
                name: 'ProfileDetailNotes',
                editable: true,
                shrinktofit: true,
                editable: true,
                edittype: 'textarea',
                editoptions: { rows: '5', cols: 50 },
                width: 60
            }
        ],
        pager: jQuery('#pagerD'),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        height: '100%',
        viewrecords: true,
        caption: 'Lake profile details',
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
        saveAfterSelect:true,
        onSelectRow: function (id) {           
            if (id && id != lastSel) {
                //save changes in row
                
                $('#detailGrid').saveRow(lastSel,
                    {
                        successfunc: function (response) { DisplayResult(response);  }
                    });
                lastSel = id;
            }
            //trigger inline edit for row
         
           
            $('#detailGrid').editRow(id, 
                {
                   
                        keys: true,
                        url: '/Lake/EditProfileDetail'
                 }
            );
            //cm.editable = true;
        }
       

    }).navGrid('#pagerD',
        {
            add: true,
            edit: false,
            del: true,
            search: false,
            refresh: false
        },{
            // edit options  
            zIndex: 100,
            width: 400,
            url: '/Lake/EditProfileDetail',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            onclickSubmit: function (response, postdata) {
                var profileRowId = $("#masterGrid").jqGrid('getGridParam', 'selrow');                
                response.url = '/Lake/EditProfileDetail' + "?ProfileID=" + profileRowId;
            },
            afterComplete: function (response) {
                DisplayResult(response);
            },
            recreateForm: true,
            beforeShowForm: function (form) {
                $('#tr_ParameterID', form).show();
                $('#tr_ParameterValue', form).show();
                $('#tr_ProfileDetailNotes', form).show();    
                form.find(".FormElement[readonly]")
                    .prop("disabled", true)
                    .addClass("ui-state-disabled")
                    .closest(".DataTD")
                    .prev(".CaptionTD")
                    .prop("disabled", true)
                    .addClass("ui-state-disabled");  
                form.find("select")
                    .prop("disabled", true)
                    .addClass("ui-state-disabled")
                    .closest(".DataTD")
                    .prev(".CaptionTD")
                    .prop("disabled", true)
                    .addClass("ui-state-disabled");                  
            }
        }, {
            // add options  
            zIndex: 100,
            width: 400,
            url: "/Lake/CreateProfileDetail",
            closeOnEscape: true,
            closeAfterAdd: true,            
            onclickSubmit: function (response, postdata) {
                var profileRowId = $("#masterGrid").jqGrid('getGridParam', 'selrow');
                response.url = '/Lake/CreateProfileDetail' + "?ProfileID=" + profileRowId;
            },
            afterComplete: function (response) {
                DisplayResult(response);
            },
            recreateForm: true,
            beforeShowForm: function (form) {
                //var cm = jQuery("#detailGrid").jqGrid('getColProp', 'DepthFeet');            
                //cm.editable = true;
                //cm.disabled = false;

                $('#tr_DepthFeet', form).show();
                $('#tr_ParameterID', form).hide();
                $('#tr_ParameterValue', form).hide();
                $('#tr_ProfileDetailNotes', form).hide();                
                form.find(".FormElement[disabled]")
                    .prop("disabled", false)
                    .prop("readonly", false)
                    .addClass("ui-state-enabled")
                    .closest(".DataTD")
                    .prev(".CaptionTD")
                    .prop("disabled", false)
                    .prop("readonly", false)
                    .addClass("ui-state-enabled");
                form.find("select")
                    .prop("disabled", false)
                    .addClass("ui-state-enabled")
                    .closest(".DataTD")
                    .prev(".CaptionTD")                    
                    .prop("disabled", false)
                    .addClass("ui-state-enabled");   
            }
        }, {
            // delete options  
            zIndex: 100,            
            url: "/Lake/DeleteProfileDetail",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            onclickSubmit: function (response, postdata) {
                var profileRowId = $("#masterGrid").jqGrid('getGridParam', 'selrow');
                var profileDetailRowId = $("#detailGrid").jqGrid('getGridParam', 'selrow');
                var value = $("#detailGrid").jqGrid('getCell', profileDetailRowId, 'ParameterID');
                response.url = '/Lake/DeleteProfileDetail' + "?ProfileID=" + profileRowId + "&ParameterID=" + value;
            },
            afterComplete: function (response) {
                DisplayResult(response);
            }
        });
 
$("#detailGrid").jqGrid('inlineNav', '#pagerD',
    {
        edit: true,
        editicon: "ui-icon-pencil",
        add: false,
        editParams: {
            keys: true,
            url: '/Lake/EditProfileDetail'
        }

    });
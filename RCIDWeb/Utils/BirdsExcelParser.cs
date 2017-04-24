using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using RCIDRepository.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RCIDWeb.Utils
{
    public static class BirdsExcelParser
    {
        public static List<BirdSurvey> ParseFile(string filepath)
        {
            List<BirdSurvey> surveyList = new List<BirdSurvey>();
            try
            {
                using (XLWorkbook workBook = new XLWorkbook(filepath))
                {
                    IXLWorksheet workSheet = workBook.Worksheet(1);

                    foreach (IXLRow row in workSheet.Rows())
                    {
                        switch (row.RowNumber())
                        {
                            //first three rows are survey headers
                            case 1:
                                foreach (IXLCell cell in row.Cells())
                                {
                                    string value = cell.Value.ToString();
                                    //starting a survey
                                    if (!value.Equals("Observer"))
                                    {
                                        BirdSurvey item = new BirdSurvey();
                                        item.SurveyorName = value;

                                        //TODO remove this line! there should be a way to get the climate id for this item
                                        item.ClimateID = 1;

                                        surveyList.Add(item);
                                    }
                                }
                                break;
                            case 2:
                                foreach (IXLCell cell in row.Cells())
                                {
                                    string value = cell.Value.ToString();
                                    //add date to survey
                                    if (!value.Equals("Date"))
                                    {
                                        DateTime date = cell.GetDateTime();

                                        BirdSurvey item = surveyList[cell.Address.ColumnNumber - 2];
                                        item.SurveyDate = date;
                                    }
                                }
                                break;
                            case 3:
                                foreach (IXLCell cell in row.Cells())
                                {
                                    string value = cell.Value.ToString();
                                    //add date to survey
                                    if (!value.Equals("Location"))
                                    {
                                        BirdSurvey item = surveyList[cell.Address.ColumnNumber - 2];
                                        item.SamplePointAreaName = value;

                                    }
                                }
                                break;

                            // after three rows, survey details start
                            default:
                                string speciesName = "";
                                foreach (IXLCell cell in row.Cells())
                                {
                                    //all rows except the last one which is a total row
                                    
                                        if (cell.Address.ColumnNumber == 1)
                                        {                                           
                                            speciesName = cell.Value.ToString();
                                            if (speciesName.Equals("TOTAL SPECIES")) { break; } //this is the last row
                                        }
                                        else
                                        {
                                            if (!cell.IsEmpty())
                                            {
                                                BirdSurvey parent = surveyList[cell.Address.ColumnNumber - 2];
                                            
                                                BirdSurveyDetails detail = new BirdSurveyDetails();
                                                detail.SpeciesName = speciesName;
                                                detail.SurveyDetailCount = cell.Value.CastTo<int>();
                                            
                                                if (parent.Details == null) { parent.Details = new List<BirdSurveyDetails>(); }

                                                parent.Details.Add(detail);
                                            }
                                        }
                                    
                                }

                                break;
                        }
                    }
                }

                return surveyList;
            }
            catch (Exception e) {
                throw e;
            }
        }
    }
}
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RCIDWeb.Utils
{
    public static class BirdsExcelParser
    {
        public static void ParseFile(string filepath)
        {
            //open the excel using openxml sdk  
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(filepath, false))
            {

                //create the object for workbook part  
                WorkbookPart wbPart = doc.WorkbookPart;

                //statement to get the count of the worksheet  
                int worksheetcount = doc.WorkbookPart.Workbook.Sheets.Count();

                //statement to get the sheet object  
                Sheet mysheet = (Sheet)doc.WorkbookPart.Workbook.Sheets.ChildElements.GetItem(0);

                //statement to get the worksheet object by using the sheet id  
                Worksheet Worksheet = ((WorksheetPart)wbPart.GetPartById(mysheet.Id)).Worksheet;

                //Note: worksheet has 8 children and the first child[1] = sheetviewdimension,....child[4]=sheetdata  
                int wkschildno = 4;


                //statement to get the sheetdata which contains the rows and cell in table  
                SheetData Rows = (SheetData)Worksheet.ChildElements.GetItem(wkschildno);


                //getting the row as per the specified index of getitem method  
                Row currentrow = (Row)Rows.ChildElements.GetItem(0);

                //getting the cell as per the specified index of getitem method  
                Cell currentcell = (Cell)currentrow.ChildElements.GetItem(0);


            }
        }
    }
}
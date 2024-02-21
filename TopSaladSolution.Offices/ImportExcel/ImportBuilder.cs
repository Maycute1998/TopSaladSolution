using System.Data;
using System.IO;
using System.Linq;

namespace TopSaladSolution.Offices.ImportExcel
{
    public static class ImportBuilder
    {
        public static DataTable ImportExcel(Stream file, string SheetName = "")
        {
            using (var pck = new OfficeOpenXml.ExcelPackage(file))
            {
                /*
                    Get sheet name by name
                    if sheet name nulll or empty then get sheet name default
                */
                var ws = string.IsNullOrEmpty(SheetName) ? pck.Workbook.Worksheets.FirstOrDefault()
                   : pck.Workbook.Worksheets[SheetName];

                var tbl = new DataTable();
                var hasHeader = true;
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                }
                var startRow = hasHeader ? 2 : 1;
                for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    var row = tbl.NewRow();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                    tbl.Rows.Add(row);
                }
                return tbl;
            }
        }
    }
}

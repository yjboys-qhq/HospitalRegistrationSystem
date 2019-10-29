using System.Data;
using Aspose.Cells;


namespace Tool
{
    class toExcel
    {
        /// <summary>
        /// DataTable转Excel文件
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static void ExportExcelWithAspose(DataTable dt, string path)
        {
            Workbook book = new Workbook();
            Worksheet sheet = book.Worksheets[0];
            Cells cells = sheet.Cells;
            int Colnum = dt.Columns.Count;
            int Rownum = dt.Rows.Count;

            for (int i = 0; i < Colnum; i++)
            {
                cells[0, i].PutValue(dt.Columns[i].ColumnName);
            }

            for (int i = 0; i < Rownum; i++)
                for (int j = 0; j < Colnum; j++)
                {
                    cells[i + 1, j].PutValue(dt.Rows[i][j].ToString());
                }
            book.Save(path , Aspose.Cells.SaveFormat.Excel97To2003);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using Excel;

public class DoExcel {
    public static DataSet ReadExcel(string path)
    {
        FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        DataSet result = excelReader.AsDataSet();
        excelReader.Close();
        return result;

    }
    public static List<DepenceTableData> Load(string path)
    {
        List<DepenceTableData> _data = new List<DepenceTableData>();
        DataSet resultds = ReadExcel(path);
        int column = resultds.Tables[0].Columns.Count;
        int row = resultds.Tables[0].Rows.Count;
        Debug.LogWarning(column + "  " + row);
        for(int i=1;i<row;i++)  
        { 
            DepenceTableData temp_data;
            temp_data.vs = new string[column];

            for (int j = 0; j < column; j++)
            {
                temp_data.vs[j] = resultds.Tables[0].Rows[i][j].ToString();
                
                if (j==column-1)
                {
                    _data.Add(temp_data);
                    Debug.Log(temp_data.vs.Length);
                }
               
                //Debug.Log(_data.Count);
            }

        }
        return _data;
    }

}

public struct DepenceTableData
{
    public  string[] vs;
}


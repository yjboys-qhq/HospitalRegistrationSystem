using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Windows;

namespace WindowsFormsApp1
{
    class DBHelperAccess
    {
        #region 变量声明处  
        public OleDbConnection Conn;
        public string ConnString;//连接字符串     
        #endregion

        #region 构造函数与连接关闭数据库  
                
        public DBHelperAccess(string Path)
        {
            //ConnString = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=";//03-07数据库用  

            //2010数据库以上版本  
            ConnString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+Path;

            //获取程序当前路径  


            if (Conn == null)
            {
                Conn = new OleDbConnection(ConnString);
            }
            if (Conn.State != ConnectionState.Open)
            {
                Conn.Open();
            }

        }



        /// </summary>      
        /// 打开数据源链接      
        /// </summary>      
        /// <returns></returns>      
        public OleDbConnection DbConn()
        {
            Conn.Open();
            return Conn;
        }

        /// </summary>      
        /// 请在数据传递完毕后调用该函数，关闭数据链接。      
        /// </summary>      
        public void Close()
        {
            Conn.Close();
        }
        #endregion

        #region 数据库基本操作  

        /// <summary>      
        /// 根据SQL命令返回数据DataTable数据表,      
        /// 可直接作为dataGridView的数据源      
        /// </summary>      
        /// <param name="SQL"></param>      
        /// <returns></returns>      
        public DataTable SelectToDataTable(string SQL)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            OleDbCommand command = new OleDbCommand(SQL, Conn);
            adapter.SelectCommand = command;
            DataTable Dt = new DataTable();
            adapter.Fill(Dt);
            return Dt;
        }


        /// <summary>      
        /// 根据SQL命令返回数据DataSet数据集     
        /// </summary>      
        /// <param name="SQL"></param>      
        /// <param name="subtableName">在返回的数据集中所添加的表的名称</param>      
        /// <returns></returns>      
        public DataSet SelectToDataSet(string SQL, string subtableName)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            OleDbCommand command = new OleDbCommand(SQL, Conn);
            adapter.SelectCommand = command;
            DataSet Ds = new DataSet();
            Ds.Tables.Add(subtableName);
            adapter.Fill(Ds, subtableName);
            return Ds;
        }


        /// <summary>      
        /// 在指定的数据集中添加带有指定名称的表，由于存在覆盖已有名称表的危险，返回操作之前的数据集。      
        /// </summary>      
        /// <param name="SQL"></param>      
        /// <param name="subtableName">添加的表名</param>      
        /// <param name="DataSetName">被添加的数据集名</param>      
        /// <returns></returns>      
        public DataSet SelectToDataSet(string SQL, string subtableName, DataSet DataSetName)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            OleDbCommand command = new OleDbCommand(SQL, Conn);
            adapter.SelectCommand = command;
            DataTable Dt = new DataTable();
            DataSet Ds = new DataSet();
            Ds = DataSetName;
            adapter.Fill(DataSetName, subtableName);
            return Ds;
        }


        /// <summary>      
        /// 根据SQL命令返回OleDbDataAdapter，      
        /// 使用前请在主程序中添加命名空间System.Data.OleDb      
        /// </summary>      
        /// <param name="SQL"></param>      
        /// <returns></returns>      
        public OleDbDataAdapter SelectToOleDbDataAdapter(string SQL)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            OleDbCommand command = new OleDbCommand(SQL, Conn);
            adapter.SelectCommand = command;
            return adapter;
        }


        /// <summary>      
        /// 执行SQL命令，不需要返回数据的修改，删除可以使用本函数      
        /// </summary>      
        /// <param name="SQL"></param>      
        /// <returns></returns>      
        public bool ExecuteSQLNonquery(string SQL)
        {
            OleDbCommand cmd = new OleDbCommand(SQL, Conn);
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}


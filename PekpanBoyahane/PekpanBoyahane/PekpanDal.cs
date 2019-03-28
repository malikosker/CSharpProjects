using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

namespace PekpanBoyahane
{

    //public delegate void dlg_errorLog(string grp, string dtl);

    public class PekpanDal
    {

        internal delegate void dShowLog(bool addLog, string group, string detail, int iconId);
        internal dShowLog cbShowLog;


        private string Server = String.Empty;
        private string Username = String.Empty;
        private string Password = String.Empty;
        private string Database = String.Empty;

        //private dlg_errorLog errorLog;

        public string connectionString = string.Empty;
        public SqlConnection SQLConnection = null;

        private bool SqlInAction = false;

        public PekpanDal()
        {
            //errorLog = null;
            cbShowLog = null;
        }

        private bool connected;
        public bool Connected
        {
            get
            {
                bool r = false;
                if (connected)
                    if (SQLConnection != null)
                        r = (SQLConnection.State != ConnectionState.Closed) | (SQLConnection.State != ConnectionState.Broken);
                return r;
            }
            set
            {
                if (value)
                {
                    if (!connected)
                        if (ConnectToDB(Server, Username, Password, Database))
                            connected = value;
                }
                else
                {
                    if (connected)
                        DisconnectDB();
                    connected = value;
                }
            }
        }

        public String DatabaseName
        {
            get { return Database; }
        }

        public void DisconnectDB()
        {
            if (SQLConnection != null)
            {
                if (SQLConnection.State == System.Data.ConnectionState.Open)
                    SQLConnection.Close();
                SQLConnection.Dispose();
                connected = false;
            }
            SQLConnection = null;
        }

        public bool ConnectToDB(string server, string username, string pass, string database)
        {
            Server = server;
            Username = username;
            Password = pass;
            Database = database;


            if (!Server.Equals(String.Empty))
            {
                connectionString = "Data Source=" + Server + ";";
                connectionString += "User ID=" + Username + ";";
                connectionString += "Password=" + Password + ";";
                connectionString += "Initial Catalog=" + Database;

                //connectionString = "Data Source = " + Server + "; Initial Catalog = " + Database + "; Integrated Security = SSPI;";

                SQLConnection = new SqlConnection();

                try
                {
                    SQLConnection.ConnectionString = connectionString;
                    SQLConnection.Open();

                    bool r = (SQLConnection.State == System.Data.ConnectionState.Open) | (SQLConnection.State == System.Data.ConnectionState.Connecting) | (SQLConnection.State == System.Data.ConnectionState.Executing) | (SQLConnection.State == System.Data.ConnectionState.Fetching);
                    connected = r;
                    return r;
                    // You can get the server version 
                    // SQLConnection.ServerVersion
                }
                catch (Exception Ex)
                {
                    if (SQLConnection != null)
                        SQLConnection.Dispose();

                    string ErrorMessage = "SQL Server veritabanına bağlanırken hata ile karşılaşıldı.";
                    ErrorMessage += Environment.NewLine;
                    ErrorMessage += Environment.NewLine;
                    ErrorMessage += Ex.Message;

                    MessageBox.Show(ErrorMessage, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
            }
            else
            {
                MessageBox.Show("Veritabanı İsmi Belirtilmemiş!", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void GiveError(string msg)
        {
            if (cbShowLog != null)
                cbShowLog(true, "Veritabanı", msg, 8);
            string eFile = Application.StartupPath + "\\Err.sql";
            using (StreamWriter sw = new StreamWriter(eFile))
            {
                sw.WriteLine(msg);
            }
        }

        public bool ConnectToDBTrusted(string server, string database)
        {
            Server = server;
            Database = database;

            if (!Server.Equals(String.Empty))
            {
                connectionString = "Data Source = " + Server + "; Initial Catalog = " + Database + "; Integrated Security = SSPI;";

                SQLConnection = new SqlConnection();

                try
                {
                    SQLConnection.ConnectionString = connectionString;
                    SQLConnection.Open();

                    bool r = (SQLConnection.State == System.Data.ConnectionState.Open) | (SQLConnection.State == System.Data.ConnectionState.Connecting) | (SQLConnection.State == System.Data.ConnectionState.Executing) | (SQLConnection.State == System.Data.ConnectionState.Fetching);
                    connected = r;
                    return r;
                    // You can get the server version 
                    // SQLConnection.ServerVersion
                }
                catch (Exception Ex)
                {
                    if (SQLConnection != null)
                        SQLConnection.Dispose();

                    string ErrorMessage = "SQL Server veritabanına bağlanırken hata ile karşılaşıldı.";
                    ErrorMessage += Environment.NewLine;
                    ErrorMessage += Environment.NewLine;
                    ErrorMessage += Ex.Message;

                    MessageBox.Show(ErrorMessage, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
            }
            else
            {
                MessageBox.Show("Veritabanı İsmi Belirtilmemiş!", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool ConnectToDBCS(string ConnStr)
        {

            if (!ConnStr.Equals(String.Empty))
            {

                SQLConnection = new SqlConnection();

                try
                {
                    connectionString = ConnStr;
                    SQLConnection.ConnectionString = ConnStr;
                    SQLConnection.Open();

                    bool r = (SQLConnection.State == System.Data.ConnectionState.Open) | (SQLConnection.State == System.Data.ConnectionState.Connecting) | (SQLConnection.State == System.Data.ConnectionState.Executing) | (SQLConnection.State == System.Data.ConnectionState.Fetching);
                    connected = r;
                    return r;
                    // You can get the server version 
                    // SQLConnection.ServerVersion
                }
                catch (Exception Ex)
                {
                    if (SQLConnection != null)
                        SQLConnection.Dispose();

                    string ErrorMessage = "SQL Server veritabanına bağlanırken hata ile karşılaşıldı.";
                    ErrorMessage += Environment.NewLine;
                    ErrorMessage += Environment.NewLine;
                    ErrorMessage += Ex.Message;

                    MessageBox.Show(ErrorMessage, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
            }
            else
            {
                MessageBox.Show("Veritabanı İsmi Belirtilmemiş!", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool ExecuteNonQuerySilent(String Sql)
        {
            bool retVal = false;
            if (SQLConnection != null)
            {
                if (SQLConnection.State == System.Data.ConnectionState.Open)
                {
                    try
                    {
                        using (SqlCommand sqlCmd = new SqlCommand(Sql, SQLConnection))
                        {
                            sqlCmd.ExecuteNonQuery();
                            retVal = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        retVal = false;
                        GiveError(ex.Message + " [" + Sql + "]");
                    }
                }
            }
            return retVal;
        }

        public int ExecuteNonQuery(String Sql, out string msg)
        {
            int recsAffected = -1;
            msg = string.Empty;
            if (SQLConnection != null)
            {
                if (SQLConnection.State == System.Data.ConnectionState.Open)
                {
                    try
                    {
                        if (SqlInAction)
                        {
                            System.Threading.Thread.Sleep(10);
                            SqlInAction = false;
                        }
                        SqlInAction = true;
                        using (SqlCommand sqlCmd = new SqlCommand(Sql, SQLConnection))
                        {
                            recsAffected = sqlCmd.ExecuteNonQuery();
                            SqlInAction = false;
                        }
                    }
                    catch (SqlException ex)
                    {
                        msg = "SQL Hatası : " + ex.Message + " (Sorgu:" + Sql + ")";
                        SqlInAction = false;
                        GiveError(msg);
                        MessageBox.Show(ex.Message + "\n\nSorgu:" + Sql, "SQL Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                    catch (Exception ex)
                    {
                        msg = "Hata : " + ex.Message + " (Sorgu:" + Sql + ")";
                        GiveError(msg);
                        SqlInAction = false;
                        MessageBox.Show(ex.Message + "\n\nSorgu:" + Sql, "Genel Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                }
            }
            return recsAffected;
        }

        //returns only affected row count
        public bool ExecuteNonQuery(String Sql, out int recsAffected, out string msg)
        {
            bool retVal = false;
            recsAffected = -1;
            msg = string.Empty;
            if (SQLConnection != null)
            {
                if (SQLConnection.State == System.Data.ConnectionState.Open)
                {
                    try
                    {
                        using (SqlCommand sqlCmd = new SqlCommand(Sql, SQLConnection))
                        {
                            recsAffected = sqlCmd.ExecuteNonQuery();
                            retVal = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        retVal = false;
                        GiveError(ex.Message + " Sql:" + Sql);
                    }
                }
            }
            return retVal;
        }

        //Returns only one field
        public object ExecuteScalar(String Sql)
        {
            if (SQLConnection != null)
            {
                if (SQLConnection.State == System.Data.ConnectionState.Open)
                {
                    try
                    {
                        using (SqlCommand sqlCmd = new SqlCommand(Sql, SQLConnection))
                        {
                            return sqlCmd.ExecuteScalar();
                        }
                    }
                    catch (SqlException ex)
                    {
                        GiveError(ex.Message + " Sql:" + Sql);
                        MessageBox.Show(ex.Message + "\n\nSorgu:" + Sql, "Sorgu Yürütme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        GiveError(ex.Message + " Sql:" + Sql);
                        MessageBox.Show(ex.Message + "\n\nSorgu:" + Sql, "Sorgu Yürütme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                else return null;
            }
            else return null;
        }

        public string ExecuteScalarStr(String Sql)
        {
            string retVal = string.Empty;
            if (SQLConnection != null)
            {
                if (SQLConnection.State == System.Data.ConnectionState.Open)
                {
                    try
                    {
                        using (SqlCommand sqlCmd = new SqlCommand(Sql, SQLConnection))
                        {
                            object o = sqlCmd.ExecuteScalar();
                            if ((o != null) && (!(o is DBNull)))
                                retVal = o.ToString();
                        }
                    }
                    catch (SqlException ex)
                    {
                        GiveError(ex.Message + " Sql:" + Sql);
                        MessageBox.Show(ex.Message + "\n\nSorgu:" + Sql, "Sorgu Yürütme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        GiveError(ex.Message + " Sql:" + Sql);
                        MessageBox.Show(ex.Message + "\n\nSorgu:" + Sql, "Sorgu Yürütme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return retVal;
        }

        public int CreateCommand(string queryString, out string msg)
        {
            msg = string.Empty;
            int rows = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Connection.Open();
                    rows = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return rows;
        }

        public bool ExecuteScalarCon(String Sql, out string fields, out string errMsg)
        {
            errMsg = string.Empty;
            fields = string.Empty;
            bool retBool = false;
            object retObj = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(Sql, connection))
                    {
                        command.Connection.Open();
                        retObj = command.ExecuteScalar();
                        retBool = true;
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }


            if (retBool)
            {
                if (retObj != null)
                {
                    bool dbNull = retObj is DBNull;
                    if (!dbNull)
                    {
                        string retStr = retObj.ToString();
                        fields = retStr;
                    }
                    else fields = string.Empty;
                }
                else fields = string.Empty;
            }

            return retBool;
        }

        public bool ExecuteScalar(String Sql, out string fields, out string errMsg)
        {
            errMsg = String.Empty;
            fields = string.Empty;
            bool retBool = false;
            object retObj = null;
            if (SQLConnection != null)
            {
                if (SQLConnection.State == System.Data.ConnectionState.Open)
                {
                    try
                    {
                        using (SqlCommand sqlCmd = new SqlCommand(Sql, SQLConnection))
                        {
                            retObj = sqlCmd.ExecuteScalar();
                            retBool = true;
                        }
                    }
                    catch (SqlException ex)
                    {
                        errMsg = "SqlException:" + ex.Message + " " + Sql;
                        GiveError(errMsg);
                    }
                    catch (Exception ex)
                    {
                        errMsg = "Exception:" + ex.Message + " " + Sql;
                        GiveError(errMsg);
                    }
                }
            }

            if (retObj != null)
            {
                bool dbNull = retObj is DBNull;
                if (!dbNull)
                {
                    string retStr = retObj.ToString();
                    fields = retStr;
                }
                else fields = string.Empty;
            }
            else fields = string.Empty;

            return retBool;
        }

        public object ExecuteScalarSilent(String Sql)
        {
            if (SQLConnection != null)
            {
                if (SQLConnection.State == System.Data.ConnectionState.Open)
                {
                    try
                    {
                        using (SqlCommand sqlCmd = new SqlCommand(Sql, SQLConnection))
                        {
                            return sqlCmd.ExecuteScalar();
                        }
                    }
                    catch (Exception ex)
                    {
                        GiveError(ex.Message + " Sql:" + Sql);
                        return null;
                    }
                }
                else return null;
            }
            else return null;
        }

        //returns only filled 1 datatable
        public DataTable GetDataTable(String Sql)
        {
            if (SQLConnection != null)
            {
                if (SQLConnection.State == System.Data.ConnectionState.Open)
                {
                    try
                    {
                        SqlDataAdapter SQLDataAdapter = new SqlDataAdapter(Sql, SQLConnection);
                        DataTable dtResult = new DataTable();
                        SQLDataAdapter.Fill(dtResult);

                        SQLDataAdapter.Dispose();
                        return dtResult;
                    }
                    catch (SqlException ex)
                    {
                        GiveError(ex.Message + " DataTable Sql:" + Sql);
                        MessageBox.Show(ex.Message, "DataTable Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        GiveError(ex.Message + " DataTable Sql:" + Sql);
                        MessageBox.Show(ex.Message, "DataTable Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                else return null;
            }
            else return null;
        }

        public DataSet GetDataSet(String Sql)
        {
            if (SQLConnection != null)
            {
                if (SQLConnection.State == System.Data.ConnectionState.Open)
                {
                    try
                    {
                        SqlDataAdapter SQLDataAdapter = new SqlDataAdapter(Sql, SQLConnection);
                        DataSet dsResult = new DataSet();
                        SQLDataAdapter.Fill(dsResult);

                        SQLDataAdapter.Dispose();
                        return dsResult;
                    }
                    catch (SqlException ex)
                    {
                        GiveError(ex.Message + " DataSet Sql:" + Sql);
                        MessageBox.Show("Sorgu:" + Sql + "\r\n\n" + ex.Message, "Dataset Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        GiveError(ex.Message + " DataSet Sql:" + Sql);
                        MessageBox.Show("Sorgu:" + Sql + "\r\n\n" + ex.Message, "Dataset Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                else return null;
            }
            else return null;
        }

        public SqlDataReader GetDataReader(String Sql)
        {
            if (SQLConnection != null)
            {
                if (SQLConnection.State == System.Data.ConnectionState.Open)
                {
                    try
                    {
                        using (SqlCommand sqlCmd = new SqlCommand(Sql, SQLConnection))
                        {
                            return sqlCmd.ExecuteReader();
                        }
                    }
                    catch (SqlException ex)
                    {
                        GiveError(ex.Message + " Reader Sql:" + Sql);

                        string eFile = Application.StartupPath + "\\Err.sql";
                        using (StreamWriter sw = new StreamWriter(eFile))
                        {
                            sw.WriteLine(ex.Message + "\t" + Sql);
                        }

                        MessageBox.Show(ex.Message, "Veri Okuyucu (Data Reader) Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        GiveError(ex.Message + " Reader Sql:" + Sql);
                        MessageBox.Show(ex.Message, "Veri Okuyucu (Data Reader) Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                else return null;
            }
            else return null;
        }

        public List<string> GetDataReaderList(String Sql)
        {
            if (SQLConnection != null)
            {
                if (SQLConnection.State == System.Data.ConnectionState.Open)
                {
                    try
                    {
                        using (SqlCommand sqlCmd = new SqlCommand(Sql, SQLConnection))
                        {
                            List<string> lst = new List<string>();
                            using (SqlDataReader rdr = sqlCmd.ExecuteReader())
                            {
                                while (rdr.Read())
                                {
                                    string fldFirst = rdr.GetString(0);
                                    lst.Add(fldFirst);
                                }
                            }
                            return lst;
                        }
                    }
                    catch (SqlException ex)
                    {
                        GiveError(ex.Message + " Reader Sql:" + Sql);
                        MessageBox.Show(ex.Message, "Veri Okuyucu (Data Reader) Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        GiveError(ex.Message + " Reader Sql:" + Sql);
                        MessageBox.Show(ex.Message, "Veri Okuyucu (Data Reader) Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                else return null;
            }
            else return null;
        }

        public List<string> GetDataReaderListColumn(String Sql, int columnNumber)
        {
            if (SQLConnection != null)
            {
                if (SQLConnection.State == System.Data.ConnectionState.Open)
                {
                    try
                    {
                        using (SqlCommand sqlCmd = new SqlCommand(Sql, SQLConnection))
                        {
                            List<string> lst = new List<string>();
                            using (SqlDataReader rdr = sqlCmd.ExecuteReader())
                            {
                                while (rdr.Read())
                                {
                                    for (int i = 0; i < columnNumber; i++)
                                    {
                                        if (i > 0)
                                        {
                                            string fldFirst = rdr.GetInt32(i).ToString();
                                            lst.Add(fldFirst);
                                        }
                                        else
                                        {
                                            string fldFirst = rdr.GetString(i);
                                            lst.Add(fldFirst);
                                        }
                                        
                                    }
                                }
                            }
                            return lst;
                        }
                    }
                    catch (SqlException ex)
                    {
                        GiveError(ex.Message + " Reader Sql:" + Sql);
                        MessageBox.Show(ex.Message, "Veri Okuyucu (Data Reader) Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        GiveError(ex.Message + " Reader Sql:" + Sql);
                        MessageBox.Show(ex.Message, "Veri Okuyucu (Data Reader) Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                else return null;
            }
            else return null;
        }

        public List<string> GetDataReaderListColumn2(String Sql, int columnNumber)
        {
            if (SQLConnection != null)
            {
                if (SQLConnection.State == System.Data.ConnectionState.Open)
                {
                    try
                    {
                        using (SqlCommand sqlCmd = new SqlCommand(Sql, SQLConnection))
                        {
                            List<string> lst = new List<string>();
                            using (SqlDataReader rdr = sqlCmd.ExecuteReader())
                            {
                                while (rdr.Read())
                                {
                                    for (int i = 0; i < columnNumber; i++)
                                    {
                                        string fldFirst = rdr.GetInt32(i).ToString();
                                        lst.Add(fldFirst);
                                    }
                                }
                            }
                            return lst;
                        }
                    }
                    catch (SqlException ex)
                    {
                        GiveError(ex.Message + " Reader Sql:" + Sql);
                        MessageBox.Show(ex.Message, "Veri Okuyucu (Data Reader) Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        GiveError(ex.Message + " Reader Sql:" + Sql);
                        MessageBox.Show(ex.Message, "Veri Okuyucu (Data Reader) Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                else return null;
            }
            else return null;
        }



        internal void SetLogger(dShowLog logger)
        {
            cbShowLog = logger;
        }

    }
}

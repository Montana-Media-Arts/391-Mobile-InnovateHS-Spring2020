using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

//Creates a connection to the sql server, which is opened and closed as an intermediary between specific table methods and the database.
public class DatabaseConnection
{
    SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

    //Used for Insert, Update, and Delete commands
    public void uploadCommand(string query, SqlParameter[] parameters = null)
    {
        //Open Connection
        sqlConnection.Open();

        //Create Command
        SqlCommand cmd = new SqlCommand(query);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = sqlConnection;
        if (parameters != null) cmd.Parameters.AddRange(parameters);

        //Execute command on database
        cmd.ExecuteNonQuery();

        //Close Connection
        sqlConnection.Close();
    }

    //Used for Insert, Update, and Delete commands that return the id of the item modified
    public int uploadAndReturnCommand(string query, SqlParameter outPutVal, SqlParameter[] parameters = null)
    {
        //Open Connection
        sqlConnection.Open();

        int outputID = -1971;

        //Create Command
        SqlCommand cmd = new SqlCommand(query);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = sqlConnection;
        if (parameters != null) cmd.Parameters.AddRange(parameters);
        cmd.Parameters.Add(outPutVal);

        //Execute command on database
        cmd.ExecuteNonQuery();

        //Close Connection
        sqlConnection.Close();

        if (outPutVal.Value != DBNull.Value) outputID = Convert.ToInt32(outPutVal.Value);
        return outputID;
    }

    //Used for SELECT commands
    public DataSet downloadCommand(string query, SqlParameter[] parameters = null)
    {
        //Open Connection
        sqlConnection.Open();

        //Create Command
        SqlCommand cmd = new SqlCommand(query);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = sqlConnection;
        if (parameters != null) cmd.Parameters.AddRange(parameters);

        //Recieve DataSet
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet data = new DataSet();
        adapter.Fill(data);

        //Close Connection and return dataSet
        sqlConnection.Close();
        return data;
    }
}

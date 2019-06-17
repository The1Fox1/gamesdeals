using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Data
{
    internal sealed class SqlDataProvider : Sabio.Data.Providers.IDataProvider
    {
        #region - Private Members -
        private static SqlDataProvider _instance = null;
        private const string LOG_CAT = "DAO";
        #endregion

        #region - Ctro's -
        private SqlDataProvider() { }

        static SqlDataProvider()
        {
            _instance = new SqlDataProvider();
        }

        #endregion

        #region - Instance -

        public static SqlDataProvider Instance
        {
            get
            {
                return _instance;
            }
        }

        #endregion

        #region - IDataProvider Memebers -

        public void ExecuteCmd(Func<SqlConnection> connectionStringSource, 
            string storedProc,
            Action<SqlParameterCollection> inputParamMapper,
            Action<IDataReader, short> singleRecordMapper,
            Action<SqlParameterCollection> returnParameters = null,
            Action<SqlCommand> cmdModifier = null)
        {
            if (singleRecordMapper == null)
                throw new NullReferenceException("ObjectMapper is required.");

            SqlDataReader reader = null;
            SqlCommand cmd = null;
            SqlConnection conn = null;
            short result = 0;
            try
            {

                using (conn = connectionStringSource())
                {
                    if (conn != null)
                    {

                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd = GetCommand(conn, storedProc, inputParamMapper);
                        if (cmd != null)
                        {
                            if (cmdModifier != null)
                                cmdModifier(cmd);

                            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                            while (true)
                            {

                                while (reader.Read())
                                {
                                    if (singleRecordMapper != null)
                                        singleRecordMapper(reader, result);
                                }

                                result += 1;

                                if (reader.IsClosed || !reader.NextResult())
                                    break;

                                if (result > 10)
                                {
                                    throw new Exception("Too many result sets returned");
                                }
                            }

                            reader.Close();

                            if (returnParameters != null)
                                returnParameters(cmd.Parameters);

                            if (conn.State != ConnectionState.Closed)
                                conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                if (conn != null && conn.State != ConnectionState.Closed)
                    conn.Close();
            }


        }


        public int ExecuteNonQuery(Func<SqlConnection> dataSouce, string storedProc,
            Action<SqlParameterCollection> paramMapper, Action<SqlParameterCollection> returnParameters = null)
        {
            SqlCommand cmd = null;
            SqlConnection conn = null;
            try
            {

                using (conn = dataSouce())
                {
                    if (conn != null)
                    {
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd = GetCommand(conn, storedProc, paramMapper);

                        if (cmd != null)
                        {
                            int returnValue = cmd.ExecuteNonQuery();

                            if (conn.State != ConnectionState.Closed)
                                conn.Close();

                            if (returnParameters != null)
                                returnParameters(cmd.Parameters);

                            return returnValue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                    conn.Close();
            }

            return -1;

        }


        #endregion



        #region - Private Methods (Execute, GetCommand) -

        private SqlCommand GetCommand(SqlConnection conn, string cmdText = null, Action<SqlParameterCollection> paramMapper = null)
        {
            SqlCommand cmd = null;

            if (conn != null)
                cmd = conn.CreateCommand();

            if (cmd != null)
            {
                if (!String.IsNullOrEmpty(cmdText))
                {
                    cmd.CommandText = cmdText;
                    cmd.CommandType = CommandType.StoredProcedure;
                }

                if (paramMapper != null)
                    paramMapper(cmd.Parameters);
            }

            return cmd;

        }

        private IDbCommand GetCommand(IDbConnection conn, string cmdText = null, Action<IDataParameterCollection> paramMapper = null)
        {
            IDbCommand cmd = null;

            if (conn != null)
                cmd = conn.CreateCommand();

            if (cmd != null)
            {
                if (!String.IsNullOrEmpty(cmdText))
                {
                    cmd.CommandText = cmdText;
                    cmd.CommandType = CommandType.StoredProcedure;
                }

                if (paramMapper != null)
                    paramMapper(cmd.Parameters);
            }

            return cmd;

        }

        private void HandleException(Exception ex)
        {
            throw ex;
        }

        #endregion
    }
}

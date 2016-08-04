// <copyright file="SqlHelper.cs" company="Objectivity Bespoke Software Specialists">
// Copyright (c) Objectivity Bespoke Software Specialists. All rights reserved.
// </copyright>
// <license>
//     The MIT License (MIT)
//     Permission is hereby granted, free of charge, to any person obtaining a copy
//     of this software and associated documentation files (the "Software"), to deal
//     in the Software without restriction, including without limitation the rights
//     to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//     copies of the Software, and to permit persons to whom the Software is
//     furnished to do so, subject to the following conditions:
//     The above copyright notice and this permission notice shall be included in all
//     copies or substantial portions of the Software.
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//     IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//     FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//     AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//     LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//     OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//     SOFTWARE.
// </license>

namespace Objectivity.Test.Automation.Common.Helpers
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    using NLog;

    /// <summary>
    /// Class is used for execution SQL queries and reading data from database.
    /// </summary>
    public static class SqlHelper
    {
        /// <summary>
        /// NLog logger handle
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Method is used for execution SQL query (select) and reading each row from column.
        /// </summary>
        /// <param name="command">SQL query</param>
        /// <param name="connectionString">Server, user, pass</param>
        /// <param name="column">Name of column</param>
        /// <returns>
        /// Collection of each row existed in column.
        /// </returns>
        /// <example>How to use it: <code>
        /// var connectionString = "User ID=sqluser;Password=sqluserpassword;server=servername;";
        /// const string ColumnName = "AccountNumber";
        /// const string SqlQuery = "SELECT  AccountNumber as " + ColumnName + " FROM [AdventureWorks].[Sales].[Customer] where [CustomerID] in (1, 2)";
        /// var result = SqlHelper.ExecuteSqlCommand(SqlQuery, connectionString, ColumnName);
        /// </code></example>
        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "SQL injection is in this case expected.")]
        public static ICollection<string> ExecuteSqlCommand(string command, string connectionString, string column)
        {
            Logger.Trace(CultureInfo.CurrentCulture, "Execute sql query '{0}'", command);

            var resultList = new List<string>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var sqlCommand = new SqlCommand(command, connection))
                {
                    using (var sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (!sqlDataReader.HasRows)
                        {
                            Logger.Error(CultureInfo.CurrentCulture, "No result for: {0} \n {1} \n {2}", command, connectionString, column);
                            return resultList;
                        }

                        while (sqlDataReader.Read())
                        {
                            resultList.Add(sqlDataReader[column].ToString());
                        }
                    }
                }
            }

            if (resultList.Count == 0)
            {
                Logger.Error(CultureInfo.CurrentCulture, "No result for: {0} \n {1} \n {2}", command, connectionString, column);
            }
            else
            {
                Logger.Trace(CultureInfo.CurrentCulture, "Sql results: {0}", resultList);
            }

            return resultList;
        }

        /// <summary>
        /// Method is used for execution SQL query (select) and reading each column from row.
        /// </summary>
        /// <param name="command">SQL query</param>
        /// <param name="connectionString">Server, user, pass</param>
        /// <param name="columns">Name of columns</param>
        /// <returns>
        /// Dictionary of each column existed in raw.
        /// </returns>
        /// <example>How to use it: <code>
        /// var connectionString = "User ID=sqluser;Password=sqluserpassword;server=servername;";
        /// ICollection&lt;string&gt; column = new List&lt;string&gt;();
        /// column.Add("NationalIDNumber");
        /// column.Add("ContactID");
        /// const string SqlQuery = "SELECT [NationalIDNumber] as " + column.ElementAt(0) + " , [ContactID] as " + column.ElementAt(1) + " from [AdventureWorks].[HumanResources].[Employee] where EmployeeID=1";
        /// Dictionary&lt;string, string&gt; results = SqlHelper.ExecuteSqlCommand(command, GetConnectionString(server), column);
        /// </code></example>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Exception when there is not given column in results from SQL query</exception>
        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "SQL injection is in this case expected.")]
        public static Dictionary<string, string> ExecuteSqlCommand(string command, string connectionString, IEnumerable<string> columns)
        {
            Logger.Trace(CultureInfo.CurrentCulture, "Execute sql query '{0}'", command);

            var resultList = new Dictionary<string, string>();
            var resultTemp = new Dictionary<string, string>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var sqlCommand = new SqlCommand(command, connection))
                {
                    using (var sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (!sqlDataReader.HasRows)
                        {
                            Logger.Error(CultureInfo.CurrentCulture, "No result for: {0} \n {1}", command, connectionString);
                            return resultList;
                        }

                        while (sqlDataReader.Read())
                        {
                            for (int i = 0; i < sqlDataReader.FieldCount; i++)
                            {
                                resultTemp[sqlDataReader.GetName(i)] = sqlDataReader.GetSqlValue(i).ToString();
                            }
                        }
                    }
                }
            }

            foreach (string column in columns)
            {
                string keyValue;

                if (resultTemp.TryGetValue(column, out keyValue))
                {
                    resultList[column] = keyValue;
                }
                else
                {
                    throw new KeyNotFoundException(string.Format(CultureInfo.CurrentCulture, " Exception while trying to get results from sql query, lack of column '{0}'", column));
                }
            }

            foreach (KeyValuePair<string, string> entry in resultList)
            {
                Logger.Trace(CultureInfo.CurrentCulture, "Sql results: {0} = {1}", entry.Key, entry.Value);
            }

            return resultList;
        }
    }
}

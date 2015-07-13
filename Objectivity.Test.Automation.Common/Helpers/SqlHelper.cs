/*
The MIT License (MIT)

Copyright (c) 2015 Objectivity Bespoke Software Specialists

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace Objectivity.Test.Automation.Common.Helpers
{
    using System.Collections.Generic;
    using System.Data.SqlClient;

    /// <summary>
    /// Class is used for execution SQL queries and reading data from database.
    /// </summary>
    public static class SqlHelper
    {
        /// <summary>
        /// Method is used for execution SQL query (select) and reading each row from column.
        /// </summary>
        /// <param name="command">SQL query</param>
        /// <param name="connectionString">Server, user, pass</param>
        /// <param name="column">Name of column </param>
        /// <returns> Collection of each row existed in column.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "SQL injection is in this case expected.")]
        public static ICollection<string> ExecuteSqlCommand(string command, string connectionString, string column)
        {
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
                            return resultList;
                        }

                        while (sqlDataReader.Read())
                        {
                            resultList.Add(sqlDataReader[column].ToString());
                        }
                    }
                }
            }

            return resultList;
        }
    }
}

// <copyright file="MdxHelper.cs" company="Objectivity Bespoke Software Specialists">
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
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    using Microsoft.AnalysisServices.AdomdClient;
    using NLog;

    /// <summary>
    /// Class is used for execution MDX queries and reading data from Analysis Services.
    /// </summary>
    public static class MdxHelper
    {
        /// <summary>
        /// NLog logger handle
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Method is used for execution MDX query and reading each row from column.
        /// </summary>
        /// <param name="command">MDX query string.</param>
        /// <param name="connectionString">The Analysis Services connection string.</param>
        /// <param name="index">The index of column.</param>
        /// <returns>Collection of MDX query results</returns>
        /// <example>How to use it: <code>
        /// var connectionString = "Provider=MSOLAP.5;Password=password;Persist Security Info=True;User ID=username;Initial Catalog=AdventureWorks;Data Source=servername;MDX Compatibility=1;Safety Options=2;MDX Missing Member Mode=Error";
        /// const string SqlQuery = "Select [Measures].[Internet Average Sales Amount] on Columns, [Product].[Category].members on Rows From [AdventureWorks];";
        /// ICollection&lt;string&gt; result = MdxHelper.ExecuteMdxCommand(mdxQuery, connectionString, 1);
        /// </code></example>
        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "Mdx injection is in this case expected.")]
        public static ICollection<string> ExecuteMdxCommand(string command, string connectionString, int index)
        {
            Logger.Debug(CultureInfo.CurrentCulture, "Send mdx query.");
            Logger.Debug(CultureInfo.CurrentCulture, "Query: {0}", command);
            Logger.Debug(CultureInfo.CurrentCulture, "AS connection string: {0}", connectionString);
            Logger.Debug(CultureInfo.CurrentCulture, "Index: {0}", index);

            var resultList = new List<string>();
            using (var connection = new AdomdConnection(connectionString))
            {
                connection.Open();

                using (var mdxCommand = new AdomdCommand(command, connection))
                {
                    using (var mdxReader = mdxCommand.ExecuteReader())
                    {
                        while (mdxReader.Read())
                        {
                            ////getName - column name
                            resultList.Add(mdxReader[index].ToString());
                        }
                    }
                }

                return resultList;
            }
        }
    }
}

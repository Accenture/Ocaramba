﻿// <copyright file="ProjectTestBase.cs" company="Objectivity Bespoke Software Specialists">
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

namespace Ocaramba.Tests.Xunit
{
    using System;
    using global::Xunit;
    using Ocaramba;
    using NLog;

    /// <summary>
    /// The base class for all tests <see href="https://github.com/ObjectivityLtd/Ocaramba/wiki/ProjectTestBase-class">More details on wiki</see>
    /// </summary>
    public class ProjectTestBase : IDisposable, IClassFixture<TestFixture>
    {
        private bool disposed;

#if net47
        private static readonly NLog.Logger Logger = LogManager.GetCurrentClassLogger();
#endif
#if netcoreapp2_2
        private static readonly NLog.Logger Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectTestBase"/> class.
        /// </summary>
        protected ProjectTestBase()
        {
            this.DriverContext.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Logger.Info("new test");
            this.DriverContext.Start();
        }

        public bool Disposed
        {
            get
            {
                if (!this.disposed)
                {
                    return this.disposed;
                }

                throw new ObjectDisposedException("CanBeDisposed");
            }
        }

        protected DriverContext DriverContext { get; } = new DriverContext();

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.DriverContext.Stop();
                }

                this.disposed = true;
            }
        }
    }
}

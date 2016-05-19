// <copyright file="FileType.cs" company="Objectivity Bespoke Software Specialists">
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
    /// <summary>
    /// Files type
    /// </summary>
    public enum FileType
    {
        /// <summary>
        /// File type not implemented
        /// </summary>
        None = 0,

        /// <summary>
        /// Portable document format files
        /// </summary>
        Pdf = 1,

        /// <summary>
        ///  Microsoft Excel worksheet sheet (97–2003)
        /// </summary>
        Xls = 2,

        /// <summary>
        /// Microsoft Word document
        /// </summary>
        Doc = 3,

        /// <summary>
        /// Comma-separated values files
        /// </summary>
        Csv = 4,

        /// <summary>
        /// Text files
        /// </summary>
        Txt = 5,

        /// <summary>
        /// Office open XML worksheet sheet
        /// </summary>
        Xlsx = 6,

        /// <summary>
        /// Office Open XML document
        /// </summary>
        Docx = 7,

        /// <summary>
        /// Graphics Interchange Format
        /// </summary>
        Gif = 8,

        /// <summary>
        /// Joint Photographic Experts Group
        /// </summary>
        Jpg = 9,

        /// <summary>
        /// Microsoft Windows Bitmap formatted image
        /// </summary>
        Bmp = 10,

        /// <summary>
        /// Portable Network Graphic
        /// </summary>
        Png = 11,

        /// <summary>
        /// Open data file format
        /// </summary>
        Xml = 12,

        /// <summary>
        /// Hyper text markup language
        /// </summary>
        Html = 13,

        /// <summary>
        /// Microsoft PowerPoint Presentation
        /// </summary>
        Ppt = 14,

        /// <summary>
        /// Office Open XML Presentation
        /// </summary>
        Pptx = 15
    }
}
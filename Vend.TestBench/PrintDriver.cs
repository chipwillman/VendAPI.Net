// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrintDriver.cs" company="">
//   
// </copyright>
// <summary>
//   The print through driver.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PrintDriver
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The print through driver.
    /// </summary>
    public class PrintThroughDriver
    {
        #region Public Methods and Operators

        /// <summary>
        /// The send string to printer.
        /// </summary>
        /// <param name="szPrinterName">
        /// The sz printer name.
        /// </param>
        /// <param name="szString">
        /// The sz string.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool SendStringToPrinter(string szPrinterName, string szString)
        {
            IntPtr pBytes;
            int dwCount;

            // How many characters are in the string?
            dwCount = szString.Length;

            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);

            // Send the converted ANSI string to the printer.
            SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The close printer.
        /// </summary>
        /// <param name="hPrinter">
        /// The h printer.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, 
            CallingConvention = CallingConvention.StdCall)]
        internal static extern bool ClosePrinter(IntPtr hPrinter);

        /// <summary>
        /// The end doc printer.
        /// </summary>
        /// <param name="hPrinter">
        /// The h printer.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, 
            CallingConvention = CallingConvention.StdCall)]
        internal static extern bool EndDocPrinter(IntPtr hPrinter);

        /// <summary>
        /// The end page printer.
        /// </summary>
        /// <param name="hPrinter">
        /// The h printer.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, 
            CallingConvention = CallingConvention.StdCall)]
        internal static extern bool EndPagePrinter(IntPtr hPrinter);

        /// <summary>
        /// The get last error.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("kernel32.dll", EntryPoint = "GetLastError", SetLastError = false, ExactSpelling = true, 
            CallingConvention = CallingConvention.StdCall)]
        internal static extern int GetLastError();

        /// <summary>
        /// The open printer.
        /// </summary>
        /// <param name="szPrinter">
        /// The sz printer.
        /// </param>
        /// <param name="hPrinter">
        /// The h printer.
        /// </param>
        /// <param name="pd">
        /// The pd.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, 
            ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool OpenPrinter(
            [MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, long pd);

        /// <summary>
        /// The start doc printer.
        /// </summary>
        /// <param name="hPrinter">
        /// The h printer.
        /// </param>
        /// <param name="level">
        /// The level.
        /// </param>
        /// <param name="di">
        /// The di.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, 
            ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool StartDocPrinter(
            IntPtr hPrinter, int level, [In] [MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        /// <summary>
        /// The start page printer.
        /// </summary>
        /// <param name="hPrinter">
        /// The h printer.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, 
            CallingConvention = CallingConvention.StdCall)]
        internal static extern bool StartPagePrinter(IntPtr hPrinter);

        /// <summary>
        /// The write printer.
        /// </summary>
        /// <param name="hPrinter">
        /// The h printer.
        /// </param>
        /// <param name="pBytes">
        /// The p bytes.
        /// </param>
        /// <param name="dwCount">
        /// The dw count.
        /// </param>
        /// <param name="dwWritten">
        /// The dw written.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, 
            CallingConvention = CallingConvention.StdCall)]
        internal static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the print queue.
        // Returns true on success, false on failure.

        /// <summary>
        /// The send bytes to printer.
        /// </summary>
        /// <param name="szPrinterName">
        /// The sz printer name.
        /// </param>
        /// <param name="pBytes">
        /// The p bytes.
        /// </param>
        /// <param name="dwCount">
        /// The dw count.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, int dwCount)
        {
            int dwError = 0, dwWritten = 0;
            var hPrinter = new IntPtr(0);
            var di = new DOCINFOA();
            bool bSuccess = false; // Assume failure unless you specifically succeed.
            di.pDocName = "C# Debug Print RAW Document";
            di.pDataType = "RAW";

            // Open the printer.
            if (OpenPrinter(szPrinterName, out hPrinter, 0))
            {
                // Start a document.
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // Start a page.
                    if (StartPagePrinter(hPrinter))
                    {
                        // Write your bytes.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);

                        EndPagePrinter(hPrinter);
                    }

                    EndDocPrinter(hPrinter);
                }

                ClosePrinter(hPrinter);
            }

            // If you did not succeed, GetLastError may give more information

            // about why not.
            if (bSuccess == false)
            {
                dwError = GetLastError();
            }

            return bSuccess;
        }

        #endregion

        /// <summary>
        /// The docinfoa.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        internal class DOCINFOA
        {
            /// <summary>
            /// The p doc name.
            /// </summary>
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;

            /// <summary>
            /// The p output file.
            /// </summary>
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;

            /// <summary>
            /// The p data type.
            /// </summary>
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
    }

    /// <summary>
    /// The docinfo.
    /// </summary>
    internal struct DOCINFO
    {
        #region Fields

        /// <summary>
        /// The p data type.
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pDataType;

        /// <summary>
        /// The p doc name.
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pDocName;

        /// <summary>
        /// The p output file.
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pOutputFile;

        #endregion
    }
}
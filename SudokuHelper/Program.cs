using Common;
using System;
using System.Threading;
using System.Windows.Forms;
using SudokuHelper.FormDir;

/**
* @author AppTimDev 
*
* @date 12-2-2023
* 
* @description Provide tools to help solving sudoku
* 
* @source https://github.com/AppTimDev
*/

namespace SudokuHelper
{
    static class Program
    {
        // Mutex can be made static so that GC doesn't recycle
        // same effect with GC.KeepAlive(mutex) at the end of main
        //static bool bCreatedNew;
        //static Mutex mutex = new Mutex(false, "Unique-Application-Concurrent", out bCreatedNew);
        static Semaphore _semaphore = new Semaphore(2, 2, "Max-2-Application-Concurrent");

        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Logger.InitConfig();
            //set timeout 1 ms
            if (_semaphore.WaitOne(1))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
               
                Application.Run(new SudokuForm());
                _semaphore.Release();
            }
            else
            {
                //exceed max num of Application
                return;
            }
        }
    }
}

/**
 *  RuDiCon Soft (c) 2017
 * 
 *  Program Entry point.
 *  
 *  @created 2017-09-19 Artem Nikolaev
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace DataDepositer
{
   
    static class Program
    {
        //public static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            Logger.Log.Info("  Start DataDepositor.");
            bool IsExit = false;
            uint ErrorCounter = 0; // 20 for prevent deadloop

            while (!IsExit && ErrorCounter < 20)
            {
                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    string configname;
                    if (args.Length > 0)
                    {
                        configname = args[0];
                    }
                    else
                    {
                        configname = "config.ini";
                    }

                    MainForm main = new MainForm(configname);
                    Application.Run(main);
                    IsExit = true;
                }
                catch(NotImplementedException e)
                {
                    MessageBox.Show("NotImplementedException Exception detected!!!!");
                    throw;
                }
                catch (Exception e)
                {
                    Logger.Log.Error(e.Message);
                    ErrorCounter++;
                }
            }

            if (!IsExit)
            {
                MessageBox.Show("20 errors after run detected...!!!!","ERROR AFTER START", MessageBoxButtons.OK);
            }
        }
    }
}

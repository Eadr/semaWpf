﻿using Sema.Mediator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Sema.FsLayer
{
    static class ManagerFs
    {
        #region GetTableNameFromCtl
        public static string GetTableNameFromCtl()
        {
            string tableName = "Не найден файл контрола";
            try
            {                
                DirectoryInfo currentDir = GetCurrentDir();
                FileInfo[] pathArr = currentDir.GetFiles("*.ctl");
                if (pathArr.Length > 0)
                {
                    string pathToCtl = pathArr[0].FullName;
                    tableName = GetTableName(pathToCtl);
                }                
            }
            catch (Exception)
            {
                throw;
            }
            return tableName;
        }



        private static string GetTableName(string path)
        {
            string str = File.ReadAllText(path);
            try
            {
                string startText = "INTO TABLE";
                string endText = "FIELDS TERMINATED BY";
                int indexStart = str.IndexOf(startText);
                str = str.Substring(indexStart + startText.Length);
                int indexEnd = str.IndexOf(endText);
                str = str.Substring(0, indexEnd);
                str = str.Replace("\"", "").Trim();
            }
            catch (Exception)
            {
                throw;
            }            
            return str;
        }
        #endregion


        public static DirectoryInfo GetCurrentDir()
        {            
            try
            {
                return new DirectoryInfo(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location));                
            }
            catch (Exception)
            {
                throw;
            }         
        }


        public static DateTime GetCurrentFileDate()
        {
            try
            {
                return File.GetLastWriteTime(System.Reflection.Assembly.GetEntryAssembly().Location);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static DateTime GetActualFileDate()
        {
            try
            {
                return File.GetLastWriteTime(@"x:\utils\Semaphore_new\0_Sema.exe");    //return GetExeFile(@"x:\utils\Semaphore_new").LastWriteTimeUtc;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Update()
        {
            try
            {
                string path = System.Reflection.Assembly.GetEntryAssembly().Location;
                Process proc = new Process();
                string pathAsArg = path;
                if (path.Contains(" "))
                {
                    pathAsArg = "\"" + pathAsArg + "\"";
                }
                proc.StartInfo.Arguments = pathAsArg;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.FileName = @"x:\utils\Semaphore_updater\SemaUpd.exe";
                proc.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
        }

        internal static string GetCtlFromBat(string currentBat)
        {
            string ctlName;
            try
            {
                if (!currentBat.Contains(":")) currentBat = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), currentBat);
                string str = File.ReadAllText(currentBat);
                ctlName = str.Substring(str.LastIndexOf("=") + 1).Trim();                
            }
            catch (Exception)
            {
                throw;
            }
            return ctlName;
        }

        internal static string GetLogName(string ctlName)
        {
            string logName;
            try
            {
                string ext = Path.GetExtension(ctlName);
                logName = ctlName.Replace(ext, ".log");
            }
            catch (Exception)
            {
                throw;
            }            
            return logName;
        }
    }
}

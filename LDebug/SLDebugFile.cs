﻿using System;
using System.Collections.Generic;
using System.IO;
using LGame.LCommon;
using UnityEngine;

namespace LGame.LDebug
{

    /*****
     * 
     * 
     * 将日志输出到指定的文件中
     * 
     * 
     */

    public static class SLDebugFile
    {

        /// <summary>
        /// 文件日志列表
        /// </summary>
        private static List<string> _fileLogs = new List<string>();

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="msg"></param>
        private static void WriteToFile(string msg)
        {
            if (!SLConfig.IsDebugMode || !SLConfig.IsWriteLogToFile) return;
            if (string.IsNullOrEmpty(msg)) return;
            if (_fileLogs.Count >= SLConfig.DebugCacheCount) SaveToFile();
            _fileLogs.Add(msg);
        }

        /// <summary>
        /// 将日志保存到日志文件中
        /// </summary>
        public static void SaveToFile()
        {
            if (!SLConfig.IsDebugMode || !SLConfig.IsWriteLogToFile || _fileLogs == null) return;
            string savePath = SLPathHelper.UnityLogFilePath();
            FileStream stream = File.Open(savePath, FileMode.OpenOrCreate);
            if (stream.Length > SLConfig.KbSize)
            {
                stream.Close();
                File.WriteAllText(savePath, "");
            }
            stream.Close();
            stream = null;

            StreamWriter write = File.AppendText(savePath);
            for (int i = 0, len = _fileLogs.Count; i < len; i++)
            {
                write.WriteLine(_fileLogs[i]);
            }
            _fileLogs.Clear();
            write.Flush();
            write.Close();
            write = null;
        }

        /// <summary>
        /// 写日志 log 类型的
        /// </summary>
        /// <param name="msg">输出日志</param>
        public static void Write(object msg)
        {
            WriteToFile(string.Format("[File log]:{0}", msg));
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void Write(string msg, params object[] args)
        {
            WriteToFile(string.Format("[File log]:" + msg, args));
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="args"></param>
        public static void Write(params object[] args)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < args.Length; ++i)
            {
                sb.Append(args[i]);
                sb.Append(", ");
            }
            WriteToFile(string.Format("[File log]:{0}", sb));
        }

        /// <summary>
        /// 输出错误日志
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteError(object msg)
        {
            WriteToFile(string.Format("[File Error]:{0}", msg));
        }

        /// <summary>
        /// 输出错误日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void WriteError(string msg, params object[] args)
        {
            WriteToFile(string.Format("[File Error]:" + msg, args));
        }

        /// <summary>
        /// 输出错误日志
        /// </summary>
        /// <param name="args"></param>
        public static void WriteError(params object[] args)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < args.Length; ++i)
            {
                sb.Append(args[i]);
                sb.Append(", ");
            }
            WriteToFile(string.Format("[File Error]:{0}", sb));
        }

        /// <summary>
        /// 输出警告日志
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteWarning(object msg)
        {
            WriteToFile(string.Format("[File Warning]:{0}", msg));
        }

        /// <summary>
        /// 输出警告日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void WriteWarning(string msg, params object[] args)
        {
            WriteToFile(string.Format("[File Warning]:" + msg, args));
        }

        /// <summary>
        /// 输出警告日志
        /// </summary>
        /// <param name="args"></param>
        public static void WriteWarning(params object[] args)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < args.Length; ++i)
            {
                sb.Append(args[i]);
                sb.Append(", ");
            }
            WriteToFile(string.Format("[File Warning]:{0}", sb));
        }

        /// <summary>
        /// 清理数据
        /// </summary>
        public static void Clear()
        {
            SaveToFile();
            _fileLogs.Clear();
        }

    }

}


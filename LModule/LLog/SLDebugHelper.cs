using System;
using System.Collections.Generic;
using LGame.LCommon;

namespace LGame.LDebug
{

    /*
     * 
     * 日志输出方式
     * 
     */

    public sealed class SLDebugHelper
    {

        private SLDebugHelper() { }

        /// <summary>
        /// 写日志 log 类型的
        /// </summary>
        /// <param name="msg">输出日志</param>
        public static void Write(object msg)
        {
            if (!LCSConfig.IsDebugMode) return;
            LCSConsole.Write(msg);
            if (LCSConfig.IsWriteLogToFile) LCSLogFile.Write(msg);
            if (LCSConfig.IsWriteLogToGui) LCSLogGUI.Write(msg);
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void Write(string msg, params object[] args)
        {
            if (!LCSConfig.IsDebugMode) return;
            LCSConsole.Write(msg, args);
            if (LCSConfig.IsWriteLogToFile) LCSLogFile.Write(msg, args);
            if (LCSConfig.IsWriteLogToGui) LCSLogGUI.Write(msg, args);
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="args"></param>
        public static void Write(params object[] args)
        {
            if (!LCSConfig.IsDebugMode) return;
            LCSConsole.Write(args);
            if (LCSConfig.IsWriteLogToFile) LCSLogFile.Write(args);
            if (LCSConfig.IsWriteLogToGui) LCSLogGUI.Write(args);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteError(object msg)
        {
            if (!LCSConfig.IsDebugMode) return;
            LCSConsole.WriteError(msg);
            if (LCSConfig.IsWriteLogToFile) LCSLogFile.WriteError(msg);
            if (LCSConfig.IsWriteLogToGui) LCSLogGUI.WriteError(msg);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void WriteError(string msg, params object[] args)
        {
            if (!LCSConfig.IsDebugMode) return;
            LCSConsole.WriteError(msg, args);
            if (LCSConfig.IsWriteLogToFile) LCSLogFile.WriteError(msg, args);
            if (LCSConfig.IsWriteLogToGui) LCSLogGUI.WriteError(msg, args);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="args"></param>
        public static void WriteError(params object[] args)
        {
            if (!LCSConfig.IsDebugMode) return;
            LCSConsole.WriteError(args);
            if (LCSConfig.IsWriteLogToFile) LCSLogFile.WriteError(args);
            if (LCSConfig.IsWriteLogToGui) LCSLogGUI.WriteError(args);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteWarning(object msg)
        {
            if (!LCSConfig.IsDebugMode) return;
            LCSConsole.WriteWarning(msg);
            if (LCSConfig.IsWriteLogToFile) LCSLogFile.WriteWarning(msg);
            if (LCSConfig.IsWriteLogToGui) LCSLogGUI.WriteWarning(msg);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void WriteWarning(string msg, params object[] args)
        {
            if (!LCSConfig.IsDebugMode) return;
            LCSConsole.WriteWarning(msg, args);
            if (LCSConfig.IsWriteLogToFile) LCSLogFile.WriteWarning(msg, args);
            if (LCSConfig.IsWriteLogToGui) LCSLogGUI.WriteWarning(msg, args);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="args"></param>
        public static void WriteWarning(params object[] args)
        {
            if (!LCSConfig.IsDebugMode) return;
            LCSConsole.WriteWarning(args);
            if (LCSConfig.IsWriteLogToFile) LCSLogFile.WriteWarning(args);
            if (LCSConfig.IsWriteLogToGui) LCSLogGUI.WriteWarning(args);
        }

    }

}




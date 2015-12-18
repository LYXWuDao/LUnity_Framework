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
            SLConsole.Write(msg);
            if (LCSConfig.IsWriteLogToFile) SLLogFile.Write(msg);
            if (LCSConfig.IsWriteLogToGui) SLLogGUI.Write(msg);
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void Write(string msg, params object[] args)
        {
            if (!LCSConfig.IsDebugMode) return;
            SLConsole.Write(msg, args);
            if (LCSConfig.IsWriteLogToFile) SLLogFile.Write(msg, args);
            if (LCSConfig.IsWriteLogToGui) SLLogGUI.Write(msg, args);
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="args"></param>
        public static void Write(params object[] args)
        {
            if (!LCSConfig.IsDebugMode) return;
            SLConsole.Write(args);
            if (LCSConfig.IsWriteLogToFile) SLLogFile.Write(args);
            if (LCSConfig.IsWriteLogToGui) SLLogGUI.Write(args);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteError(object msg)
        {
            if (!LCSConfig.IsDebugMode) return;
            SLConsole.WriteError(msg);
            if (LCSConfig.IsWriteLogToFile) SLLogFile.WriteError(msg);
            if (LCSConfig.IsWriteLogToGui) SLLogGUI.WriteError(msg);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void WriteError(string msg, params object[] args)
        {
            if (!LCSConfig.IsDebugMode) return;
            SLConsole.WriteError(msg, args);
            if (LCSConfig.IsWriteLogToFile) SLLogFile.WriteError(msg, args);
            if (LCSConfig.IsWriteLogToGui) SLLogGUI.WriteError(msg, args);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="args"></param>
        public static void WriteError(params object[] args)
        {
            if (!LCSConfig.IsDebugMode) return;
            SLConsole.WriteError(args);
            if (LCSConfig.IsWriteLogToFile) SLLogFile.WriteError(args);
            if (LCSConfig.IsWriteLogToGui) SLLogGUI.WriteError(args);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteWarning(object msg)
        {
            if (!LCSConfig.IsDebugMode) return;
            SLConsole.WriteWarning(msg);
            if (LCSConfig.IsWriteLogToFile) SLLogFile.WriteWarning(msg);
            if (LCSConfig.IsWriteLogToGui) SLLogGUI.WriteWarning(msg);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void WriteWarning(string msg, params object[] args)
        {
            if (!LCSConfig.IsDebugMode) return;
            SLConsole.WriteWarning(msg, args);
            if (LCSConfig.IsWriteLogToFile) SLLogFile.WriteWarning(msg, args);
            if (LCSConfig.IsWriteLogToGui) SLLogGUI.WriteWarning(msg, args);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="args"></param>
        public static void WriteWarning(params object[] args)
        {
            if (!LCSConfig.IsDebugMode) return;
            SLConsole.WriteWarning(args);
            if (LCSConfig.IsWriteLogToFile) SLLogFile.WriteWarning(args);
            if (LCSConfig.IsWriteLogToGui) SLLogGUI.WriteWarning(args);
        }

    }

}




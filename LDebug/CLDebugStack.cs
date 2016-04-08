using LGame.LBehaviour;
using LGame.LCommon;
using UnityEngine;

namespace LGame.LDebug
{

    /*****
     * 
     * 
     *  获得日志堆栈
     * 
     */

    public class CLDebugStack : ALBehaviour
    {

        private static CLDebugStack _instance = null;

        private static CLDebugGUI _debugGui = null;

        /// <summary>
        /// 日志堆栈回调函数
        /// 
        /// 不能在输出 log 日志
        /// </summary>
        /// <param name="logString">输入的日志</param>
        /// <param name="stackTrace">堆栈数据</param>
        /// <param name="type">日志类型</param>
        private void LogCallback(string logString, string stackTrace, LogType type)
        {
            switch (type)
            {
                case LogType.Log:
                    SLDebugFile.Write(logString);
                    SLDebugFile.Write(stackTrace);
                    if (_debugGui != null)
                    {
                        _debugGui.Write(logString);
                        _debugGui.Write(stackTrace);
                    }
                    break;
                case LogType.Warning:
                    SLDebugFile.WriteWarning(logString);
                    SLDebugFile.WriteWarning(stackTrace);
                    if (_debugGui != null)
                    {
                        _debugGui.WriteWarning(logString);
                        _debugGui.WriteWarning(stackTrace);
                    }
                    break;
                case LogType.Error:
                    SLDebugFile.WriteError(logString);
                    SLDebugFile.WriteError(stackTrace);
                    if (_debugGui != null)
                    {
                        _debugGui.WriteError(logString);
                        _debugGui.WriteError(stackTrace);
                    }
                    break;
            }
        }

        /// <summary>
        /// 开始堆栈
        /// </summary>
        /// <returns></returns>
        public static CLDebugStack Begin()
        {
            // todo: 增加启动 项目开始时设置
            if (_instance != null) return _instance;
            GameObject create = SLToolsHelper.Create("_LOG Stack");
            DontDestroyOnLoad(create);
            _instance = SLToolsHelper.FindComponet<CLDebugStack>(create);
            _debugGui = CLDebugGUI.Instance;
            Application.RegisterLogCallback(_instance.LogCallback);
            return _instance;
        }

        /// <summary>
        /// 清除数据
        /// </summary>
        protected override void OnClear()
        {
            _instance = null;
            Application.RegisterLogCallback(null);
            if (SLConfig.IsWriteLogToFile) SLDebugFile.Clear();
        }

    }

}

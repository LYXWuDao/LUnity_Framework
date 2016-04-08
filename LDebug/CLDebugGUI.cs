using System;
using System.Collections.Generic;
using LGame.LBehaviour;
using LGame.LCommon;
using UnityEngine;

namespace LGame.LDebug
{

    /***
     * 
     * 使用 gui 显示 debug
     * 
     * 最大显示 20 条
     * 
     */

    public class CLDebugGUI : ALBehaviour
    {
        /// <summary>
        /// 输出日志列表
        /// </summary>
        private static List<DebugEntity> _guiLogs = new List<DebugEntity>();

        /// <summary>
        /// 当前类的实例
        /// 
        /// 实现单例模式
        /// </summary>
        private static CLDebugGUI _instance = null;

        /// <summary>
        /// 用于线程锁
        /// </summary>
        private static object _lock = new object();

        /// <summary>
        /// 创建 gui 日志输出实例
        /// </summary>
        public static CLDebugGUI Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        GameObject create = SLToolsHelper.Create("_GUI Debug");
                        _instance = SLToolsHelper.FindComponet<CLDebugGUI>(create);
                        DontDestroyOnLoad(create);
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 增加输出日志
        /// </summary>
        /// <param name="log">输出的日志</param>
        /// <param name="color">日志颜色</param>
        private void Write(string log, Color color)
        {
            if (!SLConfig.IsDebugMode || !SLConfig.IsWriteLogToGui || string.IsNullOrEmpty(log)) return;
            if (_guiLogs.Count >= SLConfig.DebugCacheCount) _guiLogs.RemoveAt(0);
            _guiLogs.Add(new DebugEntity
            {
                Content = log,
                FontColor = color
            });
        }

        /// <summary>
        /// 写日志 log 类型的
        /// </summary>
        /// <param name="msg">输出日志</param>
        public void Write(object msg)
        {
            Write(msg.ToString(), Color.white);
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Write(string msg, params object[] args)
        {
            Write(string.Format(msg, args), Color.white);
        }

        /// <summary>
        /// 输出格式化数据
        /// </summary>
        /// <param name="args"></param>
        public void Write(params object[] args)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < args.Length; ++i)
            {
                sb.Append(args[i]);
                sb.Append(", ");
            }
            Write(sb.ToString(), Color.white);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="msg"></param>
        public void WriteError(object msg)
        {
            Write(msg.ToString(), Color.red);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void WriteError(string msg, params object[] args)
        {
            Write(string.Format(msg, args), Color.red);
        }

        /// <summary>
        /// 输出错误
        /// </summary>
        /// <param name="args"></param>
        public void WriteError(params object[] args)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < args.Length; ++i)
            {
                sb.Append(args[i]);
                sb.Append(", ");
            }
            Write(sb.ToString(), Color.red);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="msg"></param>
        public void WriteWarning(object msg)
        {
            Write(msg.ToString(), Color.yellow);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void WriteWarning(string msg, params object[] args)
        {
            Write(string.Format(msg, args), Color.yellow);
        }

        /// <summary>
        /// 输出警告
        /// </summary>
        /// <param name="args"></param>
        public void WriteWarning(params object[] args)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < args.Length; ++i)
            {
                sb.Append(args[i]);
                sb.Append(", ");
            }
            Write(sb.ToString(), Color.yellow);
        }

        /// <summary>
        /// 清除当前界面的数据
        /// </summary>
        protected override void OnClear()
        {
            _instance = null;
            if (_guiLogs != null) _guiLogs.Clear();
        }

        /// <summary>
        /// 在界面显示 debug 日志
        /// </summary>
        protected override void OnGUI()
        {
            if (!SLConfig.IsDebugMode || !SLConfig.IsWriteLogToGui) return;
            Rect rect = new Rect(5f, 5f, 30000f, 40f);

            // todo: 优化显示

            for (int i = 0, imax = _guiLogs.Count; i < imax; ++i)
            {
                DebugEntity entity = _guiLogs[i];
                GUI.skin.label.fontSize = 20;
                GUI.color = entity.FontColor;
                GUI.Label(rect, entity.Content);
                rect.y += 40f;
                rect.x += 2f;
            }
        }

    }

}


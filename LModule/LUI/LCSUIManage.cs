using System;
using System.Collections.Generic;
using LGame.LBase;
using LGame.LCommon;
using LGame.LDebug;
using LGame.LSource;
using UnityEngine;

namespace LGame.LUI
{
    /****
     * 
     * 
     *   ui 资源管理
     *  
     *   界面操作入口
     *  
     */
    public sealed class LCSUIManage : LATManager<LAUIBehaviour>
    {
        /// <summary>
        /// 私有话 
        /// 
        /// 不允许实例化
        /// </summary>
        private LCSUIManage() { }

        /// <summary>
        /// ui 界面的根节点
        /// </summary>
        private static Transform _uiRoot;

        /// <summary>
        /// ui摄像机
        /// </summary>
        private static UICamera _uiCamera;

        /// <summary>
        ///  创建界面
        /// </summary>
        /// <param name="winPath">加载资源路径</param>
        /// <param name="winName">打开界面的名字</param>
        /// <param name="winScript">界面脚本</param>
        private static LAUIBehaviour CreatePage(string winName, string winPath, string winScript)
        {
            if (string.IsNullOrEmpty(winName))
            {
                SLDebugHelper.WriteError("打开的界面名字为空! pageName = " + winName);
                return null;
            }
            if (string.IsNullOrEmpty(winPath))
            {
                SLDebugHelper.WriteError("加载资源 AssetBundle 文件路径为空! bundlePath = " + winPath);
                return null;
            }
            GameObject ui = LCSManageSource.LoadAssetSource(winName, winPath);
            if (ui == null)
            {
                SLDebugHelper.WriteError("加载的资源不存在!");
                return null;
            }
            GameObject go = GameObject.Instantiate(ui) as GameObject;
            if (go == null) return null;
            LCSCompHelper.InitTransform(go, UIRoot);
            LAUIBehaviour uiSprite = LCSCompHelper.GetComponent<LAUIBehaviour>(go);
            if (uiSprite != null) return uiSprite;

            if (LCSConfig.IsLuaWindow)
            {
                uiSprite = LCSCompHelper.AddComponet<LCLuaPage>(go);
                // todo: 处理lua 初始化的问题
            }
            else
            {
                uiSprite = go.AddComponent(winScript) as LAUIBehaviour;
            }
            return uiSprite;
        }

        /// <summary>
        ///  尝试创建创建界面
        /// </summary>
        /// <param name="winPath">加载资源路径</param>
        /// <param name="winName">打开界面的名字</param>
        /// <param name="winScript">界面脚本</param>
        /// <param name="win">返回的界面</param>
        private static bool TryCreatePage(string winName, string winPath, string winScript, out LAUIBehaviour win)
        {
            win = CreatePage(winName, winPath, winScript);
            return win != null;
        }

        /// <summary>
        /// 异步打开界面回调
        /// </summary>
        private static void AsyncOpenWindowCallback(string winName, GameObject go)
        {
            if (string.IsNullOrEmpty(winName))
            {
                SLDebugHelper.WriteError("打开的界面名字为空! winName = string.Empty");
                return;
            }

            if (go == null)
            {
                SLDebugHelper.WriteError("资源加载失败!");
                return;
            }

            GameObject ui = GameObject.Instantiate(go) as GameObject;
            if (ui == null) return;
            LCSCompHelper.InitTransform(go, UIRoot);
            LAUIBehaviour win = LCSCompHelper.GetComponent<LAUIBehaviour>(ui);

            int depth = 1;
            LAUIBehaviour topWin = TopWindow();
            if (topWin != null) depth = topWin.WinDepth + LCSConfig.DepthSpan;

            // 初始化当前界面
            win.OnOpen(depth, winName);
            Add<LCSUIManage>(winName, win);
        }

        /// <summary>
        /// 得到 2d 主摄像机
        /// </summary>
        public static UICamera UIMainCamera
        {
            get
            {
                if (_uiCamera == null) _uiCamera = GameObject.FindObjectOfType<UICamera>();
                return _uiCamera;
            }
            set
            {
                if (value == null) return;
                _uiCamera = value;
            }
        }

        /// <summary>
        /// 界面根节点
        /// </summary>
        public static Transform UIRoot
        {
            get
            {
                if (_uiRoot == null) _uiRoot = UIMainCamera.gameObject.transform;
                return _uiRoot;
            }
            set
            {
                if (value == null) return;
                _uiRoot = value;
            }
        }

        /// <summary>
        /// 得到当前最高的 ui 界面
        /// </summary>
        /// <returns></returns>
        public static LAUIBehaviour TopWindow()
        {
            List<string> winNames = FindKeys<LCSUIManage>();
            if (winNames == null || winNames.Count <= 0) return null;
            string winName = winNames[winNames.Count - 1];
            return Find<LCSUIManage>(winName);
        }

        /// <summary>
        /// 尝试得到最高的界面
        /// </summary>
        /// <param name="topWin"></param>
        /// <returns></returns>
        public static bool TryTopWindow(out LAUIBehaviour topWin)
        {
            return (topWin = TopWindow()) != null;
        }

        /// <summary>
        /// 同步打开界面
        /// </summary>
        /// <param name="winName">
        /// 
        /// 界面的名字，唯一
        /// 例如： uiLogin
        /// 
        /// </param>
        /// <param name="winPath">
        /// 
        /// 界面加载路径
        /// 
        /// 相对的完整路径，例如：UI/uiLogin.data
        /// 
        /// </param>
        /// <param name="winScript">界面的脚本</param>
        public static void OpenWindow(string winName, string winPath, string winScript)
        {
            if (string.IsNullOrEmpty(winName))
            {
                SLDebugHelper.WriteError("打开的界面名字为空! winName = string.Empty");
                return;
            }

            if (string.IsNullOrEmpty(winPath))
            {
                SLDebugHelper.WriteError("加载资源 AssetBundle 文件路径为空! bundlePath = string.Empty");
                return;
            }

            LAUIBehaviour win = null;
            if (TryFind<LCSUIManage>(winName, out win)) return;

            int depth = 1;
            // 当前最高的界面失去焦点
            LAUIBehaviour topWin = TopWindow();
            if (topWin != null)
            {
                depth = topWin.WinDepth + LCSConfig.DepthSpan;
                topWin.OnLostFocus();
            }

            if (!TryCreatePage(winName, winPath, winScript, out win))
            {
                SLDebugHelper.WriteWarning("创建 ui 界面 LAUIBehaviour 失败!");
                return;
            }

            // 初始化当前界面
            win.OnOpen(depth, winName);
            Add<LCSUIManage>(winName, win);
        }

        /// <summary>
        /// 异步打开界面
        /// </summary>
        /// <param name="winName"></param>
        /// <param name="winPath"></param>
        public static void AsyncOpenWindow(string winName, string winPath)
        {
            LAUIBehaviour win = null;
            if (TryFind<LCSUIManage>(winName, out win)) return;

            // 当前最高的界面失去焦点
            LAUIBehaviour topWin = TopWindow();
            if (topWin != null) topWin.OnLostFocus();

            LCSManageSource.AsyncLoadSource(winName, winPath, AsyncOpenWindowCallback);
        }

        /// <summary>
        /// 关闭最上层的界面
        /// </summary>
        public static void CloseTopWindow()
        {
            LAUIBehaviour win = null;
            if (!TryTopWindow(out win)) return;
            win.Destroy();
            Remove<LCSUIManage>(win.WinName);
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        public static void CloseWindow(string winName)
        {
            LAUIBehaviour win = null;
            if (!TryFind<LCSUIManage>(winName, out win)) return;
            CloseWindow(win);
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="win"></param>
        public static void CloseWindow(LAUIBehaviour win)
        {
            if (win == null) return;
            win.Destroy();
            Remove<LCSUIManage>(win.WinName);
            LAUIBehaviour topWin = null;
            if (!TryTopWindow(out topWin)) return;
            topWin.OnFocus();
        }

        /// <summary>
        /// 关闭所有的界面
        /// </summary>
        public static void CloseAllWindow()
        {
            List<LAUIBehaviour> win = FindValues<LCSUIManage>();
            if (win == null) return;
            for (int i = 0, len = win.Count; i < len; i++) CloseWindow(win[i]);
        }

        /// <summary>
        /// 刷新界面
        /// </summary>
        /// <param name="winName">
        /// 
        /// winName：如果为空刷新所有的界面
        ///          不为空刷新当前的界面
        /// 
        /// </param>
        public static void RefreshWindow(string winName)
        {
            if (string.IsNullOrEmpty(winName))
            {
                List<LAUIBehaviour> win = FindValues<LCSUIManage>();
                if (win == null) return;
                for (int i = 0, len = win.Count; i < len; i++) win[i].OnRefresh();
            }
            else
            {
                LAUIBehaviour win = null;
                if (!TryFind<LCSUIManage>(winName, out win)) return;
                win.OnRefresh();
            }
        }
    }
}

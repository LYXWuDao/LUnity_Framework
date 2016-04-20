using System;
using System.Collections.Generic;
using LGame.LCommon;
using LGame.LDebug;
using LGame.LSource;
using UnityEngine;

namespace LGame.LScenes
{

    /***
     * 
     * 场景管理
     * 
     * 场景只会打开一个，没打开一个就会关闭另一个场景
     * 
     */

    public class SLScenesManage
    {

        private static CLBaseDicData<string, CLSceneBehaviour> _sceneDataManage = new CLBaseDicData<string, CLSceneBehaviour>();

        /// <summary>
        /// 当前场景的名字
        /// </summary>
        private static string CurrentName = string.Empty;

        /// <summary>
        ///  是否在导入场景
        /// </summary>
        private static bool IsLoadScene = false;

        /// <summary>
        /// 场景加载完成时回调
        /// </summary>
        /// <param name="sceneName">加载的场景名字</param>
        /// <param name="finish">场景加载完成后回调</param>
        /// <param name="sceneScript">场景加载的脚本</param>
        private static void LoadSceneFinish(string sceneName, string sceneScript, Action finish = null)
        {
            if (!string.IsNullOrEmpty(CurrentName))
            {
                // 当前已经打开一个场景
                CLSceneBehaviour curr = _sceneDataManage.Find(CurrentName);
                curr.LeaveScene();
                _sceneDataManage.Remove(CurrentName);
            }

            CurrentName = sceneName;

            if (string.IsNullOrEmpty(sceneName))
            {
                SLConsole.WriteError("打开的场景名字为空 sceneName = string.Empty");
                return;
            }

            GameObject root = GameObject.Find("SceneRoot");
            if (root == null)
            {
                SLConsole.WriteError("场景不存在 SceneRoot 根节点!");
                return;
            }

            CLSceneBehaviour scene = SLToolsHelper.GetComponent<CLSceneBehaviour>(root);

            // 为场景加载绑定行为组件
            if (scene == null && !string.IsNullOrEmpty(sceneScript))
            {
                if (SLConfig.IsLuaWindow)
                {
                    scene = SLToolsHelper.AddComponet<CLSceneLuaBehaviour>(root);
                    // todo: 处理lua 初始化的问题
                }
                else
                {
                    scene = root.AddComponent(sceneScript) as CLSceneBehaviour;
                }
            }

            if (scene == null) scene = SLToolsHelper.FindComponet<CLSceneBehaviour>(root);

            scene.SceneName = sceneName;
            scene.EnterScene();
            _sceneDataManage.Add(sceneName, scene);

            // 场景加载完成回调
            if (finish != null)
            {
                finish();
                finish = null;
            }
        }

        /// <summary>
        /// 验证当前场景是否打开
        /// </summary>
        /// <param name="sceneName">场景的名字</param>
        public static bool VerifyOpenScene(string sceneName)
        {
            return !string.IsNullOrEmpty(CurrentName) && CurrentName == sceneName;
        }

        /// <summary>
        /// 得到当前场景
        /// </summary>
        public static CLSceneBehaviour CurrentScene
        {
            get
            {
                if (string.IsNullOrEmpty(CurrentName)) return null;
                return _sceneDataManage.Find(CurrentName);
            }
        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="sceneName">场景的名字</param>
        /// <param name="scenePath">场景的路径</param>
        /// <param name="sceneScript">场景的脚本</param>
        /// <param name="finish">场景加载完成回调</param>
        public static LoadSourceEntity AsyncOpenToScenes(string sceneName, string scenePath, string sceneScript, Action finish = null)
        {
            if (IsLoadScene)
            {
                SLConsole.WriteError("正在打开另外一个场景!");
                return null;
            }

            IsLoadScene = true;

            if (string.IsNullOrEmpty(sceneName))
            {
                SLConsole.WriteError("打开的场景名字为空 sceneName = string.Empty");
                return null;
            }

            if (string.IsNullOrEmpty(scenePath))
            {
                SLConsole.WriteError("场景加载路径为空， scenePath = string.Empty");
                return null;
            }

            if (VerifyOpenScene(sceneName))
            {
                SLConsole.WriteError("场景已经加载 sceneName = " + sceneName);
                return null;
            }

            return SLManageSource.AsyncLoadSceneAssetSource(sceneName, scenePath, delegate (LoadSourceEntity entity)
            {
                IsLoadScene = false;
                LoadSceneFinish(sceneName, sceneScript, finish);
            });
        }

    }

}


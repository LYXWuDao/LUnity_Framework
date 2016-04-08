using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LGame.LBehaviour;
using LGame.LCommon;
using LGame.LDebug;
using UnityEngine;

namespace LGame.LSource
{

    /****
     * 
     * 
     * 异步加载资源 (立即加载外部资源，异步加载到游戏中)
     * 
     * 使用文件流，二进制方式进行加载
     * 
     * 例如： 模型，界面，图片，声音，特效 等.
     * 
     * 使用单例模式
     * 
     */

    public sealed class CLAsyncLoadSource : ALBehaviour
    {

        /// <summary>
        /// 构造函数私有化
        /// 
        /// 单例模式需要
        /// </summary>
        private CLAsyncLoadSource()
        {

        }

        /// <summary>
        /// 当前异步加载的数据
        /// </summary>
        private AsyncOperation AsyncAsset = null;

        /// <summary>
        /// 当前加载的实体
        /// </summary>
        private LoadSourceEntity CurrentEntity = null;

        /// <summary>
        /// 创建异步导入请求
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private AssetBundle CreateAssetBundle(LoadSourceEntity entity)
        {
            if (entity == null)
            {
                SLConsole.WriteError("资源加载实体数据为空，不能加载！");
                Destroy();
                return null;
            }

            if (string.IsNullOrEmpty(entity.ResName))
            {
                SLConsole.WriteError("导入资源名字为空, resName = string.Empty");
                Destroy();
                return null;
            }

            if (string.IsNullOrEmpty(entity.BundlePath))
            {
                SLConsole.WriteError("导入 AssetBundle 路径为空, bundPath = string.Empty");
                Destroy();
                return null;
            }

            string realPath = SLPathHelper.UnityLoadSourcePath() + entity.BundlePath;

            if (string.IsNullOrEmpty(realPath))
            {
                SLConsole.WriteError("导入 AssetBundle 真实路径为空, realPath = string.Empty");
                Destroy();
                return null;
            }

            byte[] bytes = SLManageSource.GetLoadFileBytes(realPath);
            if (bytes == null || bytes.Length <= 0)
            {
                Destroy();
                return null;
            }

            AssetBundle bundle = AssetBundle.CreateFromMemoryImmediate(bytes);
            if (bundle == null)
            {
                SLConsole.WriteError("创建资源 AssetBundle 失败!");
                Destroy();
                return null;
            }

            return bundle;
        }

        /// <summary>
        /// 开始异步加载
        /// 
        /// 普通资源加载
        /// </summary>
        /// <param name="request">异步AssetBundle </param>
        /// <param name="entity">加载资源后实体</param>
        /// <returns></returns>
        private IEnumerator StartAssetLoad(AssetBundleRequest request, LoadSourceEntity entity)
        {
            if (request == null)
            {
                SLConsole.WriteError("异步加载 AssetBundleRequest 不存在!, request = null");
                Destroy();
                yield return 0;
            }
            yield return request;

            entity.LoadObj = request.asset;
            entity.Progress = 1;
            entity.IsDone = true;
            if (entity.Finish != null) entity.Finish(entity);
            Destroy();
        }

        /// <summary>
        /// 开始异步加载
        /// 
        /// 场景加载
        /// </summary>
        /// <param name="request">异步AssetBundle </param>
        /// <param name="entity">加载资源后实体</param>
        /// <returns></returns>
        private IEnumerator StartSceneLoad(AsyncOperation request, LoadSourceEntity entity)
        {
            if (request == null)
            {
                SLConsole.WriteError("异步加载 AsyncOperation 不存在!, request = null");
                Destroy();
                yield return 0;
            }
            yield return request;

            entity.LoadObj = null;
            entity.Progress = 1;
            entity.IsDone = true;
            if (entity.Finish != null) entity.Finish(entity);
            Destroy();
        }

        /// <summary>
        /// 支持多个加载
        /// </summary>
        public static CLAsyncLoadSource Instance
        {
            get
            {
                GameObject create = SLToolsHelper.Create("_async load");
                DontDestroyOnLoad(create);
                return SLToolsHelper.FindComponet<CLAsyncLoadSource>(create); ;
            }
        }

        /// <summary>
        /// 加载 assetbundle 资源文件
        /// </summary>
        /// <param name="entity">加载资源后实体</param>
        /// <returns></returns>
        public LoadSourceEntity AsyncLoadAssetSource(LoadSourceEntity entity)
        {
            AssetBundle bundle = CreateAssetBundle(entity);
            if (bundle == null) return null;
            entity.Bundle = bundle;
            CurrentEntity = entity;

            AssetBundleRequest request = bundle.LoadAsync(entity.ResName, typeof(UnityEngine.Object));

            if (request == null)
            {
                SLConsole.WriteError("创建异步加载资源失败！！");
                Destroy();
                return null;
            }

            AsyncAsset = request;

            // 开始异步加载
            StartCoroutine(StartAssetLoad(request, entity));
            return entity;
        }

        /// <summary>
        /// 导入场景资源
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public LoadSourceEntity AsyncLoadSceneAssetSource(LoadSourceEntity entity)
        {
            AssetBundle bundle = CreateAssetBundle(entity);
            if (bundle == null) return null;
            entity.Bundle = bundle;
            CurrentEntity = entity;
            AsyncOperation request = Application.LoadLevelAsync(entity.ResName);

            if (request == null)
            {
                SLConsole.WriteError("创建异步加载场景失败！！");
                Destroy();
                return null;
            }

            AsyncAsset = request;

            // 开始异步加载
            StartCoroutine(StartSceneLoad(request, entity));
            return entity;
        }

        protected override void OnClear()
        {
            CurrentEntity = null;
            AsyncAsset = null;
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (CurrentEntity == null || AsyncAsset == null) return;

            if (!AsyncAsset.isDone) CurrentEntity.Progress = AsyncAsset.progress;
        }

    }

}
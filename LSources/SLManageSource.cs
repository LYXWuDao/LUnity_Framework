using System;
using System.Collections.Generic;
using LGame.LCommon;
using LGame.LDebug;
using UnityEngine;
using System.IO;
using LGame.LMobile;

namespace LGame.LSource
{

    /***
     * 
     * 
     * 所有资源加载卸载管理类型
     * 
     * 包括图片，界面，模型，声音等
     * 
     * 
     */

    public sealed class SLManageSource : CLTypeDicData<SLManageSource, string, LoadSourceEntity>
    {
        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="resName">资源的名字(名字唯一)</param>
        /// <param name="bundPath">加载 资源 AssetBundle 的路径</param>
        /// <param name="ltype">资源的类型</param>
        /// <param name="way">加载资源的方式</param>
        /// <returns></returns>
        private static LoadSourceEntity LoadSource(string resName, string bundPath, LoadType ltype, LoadWay way, Action<LoadSourceEntity> finish = null)
        {
            LoadSourceEntity entity = null;
            if (TryFind(resName, out entity))
            {
                if (entity.Finish != null) entity.Finish(entity);
                return entity;
            }
            entity = new LoadSourceEntity { ResName = resName, BundlePath = bundPath, Type = ltype, Finish = finish };
            LoadSourceEntity load = null;

            switch (way)
            {
                case LoadWay.SyncAsset:
                    load = SLImmedLoadSource.ImmedLoadAssetSource(entity);
                    break;
                case LoadWay.AsyncAsset:
                    load = CLAsyncLoadSource.Instance.AsyncLoadAssetSource(entity);
                    break;
                case LoadWay.TextSource:
                    load = SLImmedLoadSource.ImmedLoadTextSource(entity);
                    break;
                case LoadWay.Resource:
                    load = SLImmedLoadSource.ImmedLoadResources(entity);
                    break;
                case LoadWay.AsyncSceneSource:
                    load = CLAsyncLoadSource.Instance.AsyncLoadSceneAssetSource(entity);
                    break;
            }

            if (load == null)
            {
                // 资源加载失败，就表示这个资源不需要在加载
                entity.Progress = 1;
                entity.IsDone = true;
            }

            Add(resName, entity);
            return entity;
        }

        /// <summary>
        /// 获取各平台文件的字节流
        /// </summary>
        /// <param name="filePath">文件的路径</param>
        /// <returns></returns>
        public static byte[] GetLoadFileBytes(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return null;
#if !UNITY_EDITOR && UNITY_ANDROID
            return SLAndroidJava.GetStaticBytes("getFileBytes", filePath);
#endif
            if (!File.Exists(filePath))
            {
                SLConsole.WriteError("文件路径不存在,bundPath = " + filePath);
                return null;
            }
            return File.ReadAllBytes(filePath);
        }

        /// <summary>
        /// 同步加载打包的资源
        /// </summary>
        /// <param name="resName">资源的名字</param>
        /// <param name="bundPath">资源的路径</param>
        /// <param name="ltype">资源的类型</param>
        /// <returns></returns>
        public static LoadSourceEntity LoadAssetSource(string resName, string bundPath, LoadType ltype)
        {
            return LoadSource(resName, bundPath, ltype, LoadWay.SyncAsset);
        }

        /// <summary>
        /// 异步加载打包的资源
        /// </summary>
        /// <param name="resName">资源的名字</param>
        /// <param name="bundPath">资源的路径</param>
        /// <param name="ltype">资源的类型</param>
        /// <param name="finish"></param>
        /// <returns></returns>
        public static LoadSourceEntity AsyncLoadAssetSource(string resName, string bundPath, LoadType ltype, Action<LoadSourceEntity> finish)
        {
            return LoadSource(resName, bundPath, ltype, LoadWay.AsyncAsset, finish);
        }

        /// <summary>
        /// 加载没有打包的资源
        /// 
        /// 一般用来加载文本文件
        /// 
        /// </summary>
        /// <param name="resName">资源的名字</param>
        /// <param name="bundPath">资源的路径</param>
        /// <returns></returns>
        public static LoadSourceEntity LoadTextSource(string resName, string bundPath)
        {
            return LoadSource(resName, bundPath, LoadType.TextContent, LoadWay.TextSource);
        }

        /// <summary>
        /// 加载 Resource 下面的文件
        /// </summary>
        /// <param name="resName">资源的名字</param>
        /// <param name="bundPath">资源的路径</param>
        /// <param name="ltype">资源的类型</param>
        /// <returns></returns>
        public static LoadSourceEntity LoadResource(string resName, string bundPath, LoadType ltype)
        {
            return LoadSource(resName, bundPath, LoadType.TextContent, LoadWay.Resource);
        }

        /// <summary>
        /// 异步加载场景资源
        /// </summary>
        /// <param name="resName">资源名字</param>
        /// <param name="bundPath">资源路径</param>
        /// <param name="finish">资源加载完成回调</param>
        /// <returns></returns>
        public static LoadSourceEntity AsyncLoadSceneAssetSource(string resName, string bundPath, Action<LoadSourceEntity> finish)
        {
            return LoadSource(resName, bundPath, LoadType.Object, LoadWay.AsyncSceneSource, finish);
        }

        /// <summary>
        /// 移出单个资源
        /// </summary>
        /// <param name="resName">资源名字</param>
        /// <returns></returns>
        public static bool RemoveSource(string resName)
        {
            if (string.IsNullOrEmpty(resName))
            {
                SLConsole.WriteError("移出的资源名字为空！,resName = " + resName);
                return true;
            }
            LoadSourceEntity entity;
            if (!TryFind(resName, out entity))
            {
                SLConsole.WriteError("移出的资源不存在！,resName = " + resName);
                return true;
            }
            SLUnloadSource.UnLoadSource(entity.Bundle);
            Remove(resName);
            return true;
        }

        /// <summary>
        /// 移出所有资源
        /// </summary>
        /// <returns></returns>
        public static void RemoveAllSource()
        {
            foreach (LoadSourceEntity entity in FindAllValues())
                SLUnloadSource.UnLoadSource(entity.Bundle);
            RemoveAll();
        }
    }

}
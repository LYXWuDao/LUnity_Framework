using System;
using System.Collections;
using System.IO;
using LGame.LCommon;
using LGame.LDebug;
using UnityEngine;

namespace LGame.LSource
{

    /****
     * 
     * 
     *  立即导入资源
     * 
     *  同步加载资源
     *  
     *      同步从外部加载资源，同步加载到游戏中
     * 
     */

    public static class SLImmedLoadSource
    {

        /// <summary>
        /// 验证资源实体类
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static LoadSourceEntity VerifySourceEntity(LoadSourceEntity entity)
        {
            if (entity == null)
            {
                SLConsole.WriteError("加载资源数据为空，不能加载资源!");
                return null;
            }

            if (string.IsNullOrEmpty(entity.ResName))
            {
                SLConsole.WriteError("导入资源名字为空,resName = string.Empty");
                return null;
            }

            if (string.IsNullOrEmpty(entity.BundlePath))
            {
                SLConsole.WriteError("导入 AssetBundle 路径为空, bundPath = string.Empty");
                return null;
            }

            return entity;
        }

        /// <summary>
        /// 同步加载 AssetBundle 类 资源
        /// 
        /// 区分 Android， iphone， untiy 
        /// 
        /// 默认 unity
        /// 
        /// </summary>
        /// <param name="entity">加载资源数据的实体</param>
        /// <returns></returns>
        public static LoadSourceEntity ImmedLoadAssetSource(LoadSourceEntity entity)
        {
            entity = VerifySourceEntity(entity);
            if (entity == null) return null;

            string realPath = SLPathHelper.UnityLoadSourcePath() + entity.BundlePath;

            byte[] bytes = SLManageSource.GetLoadFileBytes(realPath);
            if (bytes == null)
            {
                SLConsole.WriteError("获取文件的字节流数据为空! bytes = null");
                return null;
            }

            AssetBundle bundle = AssetBundle.CreateFromMemoryImmediate(bytes);

            if (bundle == null)
            {
                SLConsole.WriteError("创建资源 AssetBundle 失败!");
                return null;
            }

            UnityEngine.Object retobj = bundle.Load(entity.ResName);
            if (retobj == null)
            {
                SLConsole.WriteError("资源 AssetBundle 中不存在 resName = " + entity.ResName);
                return null;
            }

            entity.Bundle = bundle;
            entity.LoadObj = retobj;
            entity.Progress = 1;
            entity.IsDone = true;
            return entity;
        }

        /// <summary>
        /// 得到资源的二进制数据
        /// </summary>
        /// <param name="entity">资源加载实体类</param>
        public static LoadSourceEntity ImmedLoadTextSource(LoadSourceEntity entity)
        {
            entity = VerifySourceEntity(entity);
            if (entity == null) return null;

            string realPath = SLPathHelper.UnityLoadSourcePath() + entity.BundlePath;

            entity.SourceBytes = SLManageSource.GetLoadFileBytes(realPath);
            if (entity.SourceBytes != null)
                entity.TextContent = System.Text.Encoding.UTF8.GetString(entity.SourceBytes);
            entity.Progress = 1;
            entity.IsDone = true;
            return entity;
        }

        /// <summary>
        /// 加载 Resources 下的资源
        /// </summary>
        /// <param name="entity">资源加载实体类</param>
        /// <returns></returns>
        public static LoadSourceEntity ImmedLoadResources(LoadSourceEntity entity)
        {
            entity = VerifySourceEntity(entity);
            if (entity == null) return null;

            entity.LoadObj = Resources.Load(entity.BundlePath);
            entity.Progress = 1;
            entity.IsDone = true;
            return entity;
        }

    }

}
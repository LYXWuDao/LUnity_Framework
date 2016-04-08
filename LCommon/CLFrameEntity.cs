using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


namespace LGame.LCommon
{

    /****
     *
     * 
     *   框架内公用实体类
     * 
     * 
     */

    /// <summary>
    /// 框架输出日志实体类
    /// </summary>
    public class DebugEntity
    {

        /// <summary>
        /// 日志内容
        /// </summary>
        public string Content = string.Empty;

        /// <summary>
        /// 日志输出的颜色
        /// 
        /// 默认白色
        /// </summary>
        public Color FontColor = Color.white;

    }

    /***
     * 
     * 导入资源实体类
     * 
     */

    public class LoadSourceEntity
    {

        /// <summary>
        /// 资源的id
        /// </summary>
        public string SourceId = string.Empty;

        /// <summary>
        /// 资源的名字
        /// </summary>
        public string ResName = string.Empty;

        /// <summary>
        /// 加载 资源 AssetBundle 的路径
        /// </summary>
        public string BundlePath = string.Empty;

        /// <summary>
        ///  资源 AssetBundle
        /// </summary>
        public AssetBundle Bundle = null;

        /// <summary>
        /// 加载的资源
        /// </summary>
        public UnityEngine.Object LoadObj = null;

        /// <summary>
        /// 资源加载类型
        /// </summary>
        public LoadType Type = LoadType.Object;

        /// <summary>
        /// 资源的二进制数据
        /// </summary>
        public byte[] SourceBytes = null;

        /// <summary>
        /// 直接加载的文本文件内容
        /// </summary>
        public string TextContent = string.Empty;

        /// <summary>
        /// 资源加载完成
        /// </summary>
        public bool IsDone = false;

        /// <summary>
        /// 加载进度
        /// </summary>
        public float Progress = 0;

        /// <summary>
        /// 资源加载完成后回调
        /// </summary>
        public Action<LoadSourceEntity> Finish = null;

        public LoadSourceEntity()
        {
            SourceId = SLGuid.NewUpperGuid();
        }

    }

    /// <summary>
    /// 程序性能/运行时间观察实体类
    /// </summary>
    public class RecordWatchEntity
    {
        /// <summary>
        /// 观察 key
        /// 
        /// 尽可能的使用 Guid
        /// </summary>
        public string WatchKey = string.Empty;

        /// <summary>
        /// 开始时间
        /// </summary>
        public float StartTime = 0;

        /// <summary>
        /// 结束时间
        /// </summary>
        public float EndTime = 0;

        /// <summary>
        /// 时间差
        /// </summary>
        public float DiffTime
        {
            get
            {
                return EndTime - StartTime;
            }
        }

        /// <summary>
        /// 添加性能观察, 使用C#内置
        /// </summary>
        public Stopwatch Watch = null;

        /// <summary>
        /// 是否正在观察
        /// </summary>
        public bool IsWatch
        {
            set;
            get;
        }

        /// <summary>
        /// 开始观察
        /// </summary>
        public void BeginWatch()
        {
            IsWatch = true;
            Watch = new Stopwatch();
            Watch.Start();
        }

        /// <summary>
        /// 结束观察
        /// </summary>
        public void EndWatch()
        {
            IsWatch = false;
            if (Watch == null) return;
            Watch.Stop();
        }

        /// <summary>
        /// 得到观察时间
        /// 
        /// 单位 秒
        /// </summary>
        public double GetWatchTime()
        {
            if (Watch == null || Watch.IsRunning) return -1;
            //  获取当前实例测量得出的总时间
            TimeSpan timespan = Watch.Elapsed;
            // 转换成秒
            return timespan.TotalSeconds;
        }

    }


    /// <summary>
    /// tween 运动实体
    /// </summary>
    public class TweenerEntity
    {

        /// <summary>
        /// 运动类型
        /// </summary>
        public TweenerType TweenType = TweenerType.None;

        /// <summary>
        /// 播放动作的目标对象
        /// </summary>
        public GameObject Target = null;

        /// <summary>
        /// 起始 Vector3
        /// </summary>
        public Vector3 fVector = Vector3.zero;

        /// <summary>
        /// 目标 Vector3
        /// </summary>
        public Vector3 tVector = Vector3.one;

        /// <summary>
        /// 颜色改变值
        /// </summary>
        public Color fColor = Color.white;

        /// <summary>
        /// 颜色改变值
        /// </summary>
        public Color tColor = Color.black;

        /// <summary>
        /// alpha 起始
        /// </summary>
        public float from = 0f;

        /// <summary>
        /// alpha 目标
        /// </summary>
        public float to = 1f;

        /// <summary>
        /// 运动时间
        /// </summary>
        public float Duration = 0f;

        /// <summary>
        /// 是否立即下一个
        /// </summary>
        public bool IsImmediately = false;

    }

}
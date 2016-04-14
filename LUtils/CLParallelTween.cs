using System;
using System.Collections.Generic;
using LGame.LBehaviour;
using LGame.LCommon;
using UnityEngine;

namespace LGame.LUtils
{

    /****
     * 
     * 
     *  执行一串 并行 tweener 事件
     *
     *  多个 tweener 一起运行
     * 
     *  主要对 tweener 事件辅助
     * 
     */

    public class CLParallelTween : CLTweenEvent
    {

        /// <summary>
        /// 禁止外部私有化
        /// </summary>
        private CLParallelTween()
        {

        }

        /// <summary>
        /// 增加一个 透明运动
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginAlpha(GameObject go, float duration, float from, float to)
        {
            BeginAlpha<CLParallelTween>(go, duration, from, to);
        }

        /// <summary>
        /// 增加一个 位置运动
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginPosition(GameObject go, float duration, Vector3 from, Vector3 to)
        {
            BeginPosition<CLParallelTween>(go, duration, from, to);
        }

        /// <summary>
        /// 增加一个 旋转运动
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginRotation(GameObject go, float duration, Vector3 from, Vector3 to)
        {
            BeginRotation<CLParallelTween>(go, duration, from, to);
        }

        /// <summary>
        /// 增加一个 大小
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginScale(GameObject go, float duration, Vector3 from, Vector3 to)
        {
            BeginScale<CLParallelTween>(go, duration, from, to);
        }

        /// <summary>
        /// 颜色改变
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static void BeginColor(GameObject go, float duration, Color from, Color to)
        {
            BeginColor<CLParallelTween>(go, duration, from, to);
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (TweenerList.Count <= 0)
            {
                Destroy();
                return;
            }

            TweenerEntity entity = TweenerList[0];
            UITweener tween = BeginTweener(entity);
            if (entity == null || tween == null) return;

            tween.PlayForward();
            TweenerList.RemoveAt(0);
        }

        /// <summary>
        /// 清理当前数据
        /// </summary>
        protected override void OnClear()
        {
            OnClear<CLParallelTween>();
        }

    }

}


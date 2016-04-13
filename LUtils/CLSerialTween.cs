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
     *   执行一串 串行 tweener 事件
     *
     *  一个 tweener 一个 tweener 的执行
     * 
     *  主要对 tweener 事件辅助
     * 
     */

    public class CLSerialTween : CLTweenEvent
    {

        /// <summary>
        /// 禁止外部私有化
        /// </summary>
        private CLSerialTween()
        {

        }

        /// <summary>
        /// 运动时间
        /// </summary>
        private float mTweenTime = 0;

        /// <summary>
        /// 增加一个 透明运动
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginAlpha(GameObject go, float duration, float from, float to)
        {
            BeginAlpha<CLSerialTween>(go, duration, from, to);
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
            BeginPosition<CLSerialTween>(go, duration, from, to);
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
            BeginRotation<CLSerialTween>(go, duration, from, to);
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
            BeginScale<CLSerialTween>(go, duration, from, to);
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
            BeginColor<CLSerialTween>(go, duration, from, to);
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (TweenerList.Count <= 0 && mTweenTime <= 0)
            {
                Destroy();
                return;
            }

            if (mTweenTime > 0)
            {
                mTweenTime -= deltaTime;
                return;
            }

            UITweener tween = TweenerList[0];
            if (tween == null) return;
            mTweenTime = tween.duration;
            tween.PlayForward();
            TweenerList.RemoveAt(0);
        }

        /// <summary>
        /// 清理当前数据
        /// </summary>
        protected override void OnClear()
        {
            OnClear<CLSerialTween>();
            mTweenTime = 0;
        }
        
    }

}


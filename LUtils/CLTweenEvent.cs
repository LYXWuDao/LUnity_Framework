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

    public class CLTweenEvent : ALBehaviour
    {

        /// <summary>
        /// 保存创建的类型数据
        /// </summary>
        private static CLBaseDicData<Type, CLTweenEvent> _tweenData = new CLBaseDicData<Type, CLTweenEvent>();

        /// <summary>
        /// 当前 Tweener 的数据
        /// </summary>
        protected List<UITweener> TweenerList = new List<UITweener>();

        private static CLTweenEvent CreateTween(Type tweenType)
        {
            CLTweenEvent tween;
            if (_tweenData.TryFind(tweenType, out tween)) return tween;

            tween = SLToolsHelper.Create<CLTweenEvent>("_tween event");
            DontDestroyOnLoad(tween);

            _tweenData.Add(tweenType, tween);

            return tween;
        }

        /// <summary>
        /// 增加一个 透明运动
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginAlpha<T>(GameObject go, float duration, float from, float to)
        {
            if (go == null) return;

            CLTweenEvent tween = CreateTween(typeof(T));
            if (tween == null) return;

            TweenAlpha alpha = TweenAlpha.Begin(go, duration, to);
            alpha.from = from;
            alpha.to = to;
            alpha.method = UITweener.Method.EaseInOut;

            tween.TweenerList.Add(alpha);
        }

        /// <summary>
        /// 增加一个 位置运动
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginPosition<T>(GameObject go, float duration, Vector3 from, Vector3 to)
        {
            if (go == null) return;

            CLTweenEvent tween = CreateTween(typeof(T));
            if (tween == null) return;

            TweenPosition position = TweenPosition.Begin(go, duration, to);
            position.from = from;
            position.to = to;
            position.method = UITweener.Method.EaseInOut;

            tween.TweenerList.Add(position);
        }

        /// <summary>
        /// 增加一个 旋转运动
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginRotation<T>(GameObject go, float duration, Vector3 from, Vector3 to)
        {
            if (go == null) return;

            CLTweenEvent tween = CreateTween(typeof(T));
            if (tween == null) return;

            TweenRotation rotation = TweenRotation.Begin(go, duration, Quaternion.identity);
            rotation.from = from;
            rotation.to = to;
            rotation.method = UITweener.Method.EaseInOut;

            tween.TweenerList.Add(rotation);
        }

        /// <summary>
        /// 增加一个 大小
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginScale<T>(GameObject go, float duration, Vector3 from, Vector3 to)
        {
            if (go == null) return;

            CLTweenEvent tween = CreateTween(typeof(T));
            if (tween == null) return;

            TweenScale scale = TweenScale.Begin(go, duration, to);
            scale.from = from;
            scale.to = to;
            scale.method = UITweener.Method.EaseInOut;

            tween.TweenerList.Add(scale);
        }

        /// <summary>
        /// 颜色改变
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static void BeginColor<T>(GameObject go, float duration, Color from, Color to)
        {
            if (go == null) return;

            CLTweenEvent tween = CreateTween(typeof(T));
            if (tween == null) return;

            TweenColor color = TweenColor.Begin(go, duration, to);
            color.from = from;
            color.to = to;
            color.method = UITweener.Method.EaseInOut;

            tween.TweenerList.Add(color);
        }

        /// <summary>
        /// 清理当前数据
        /// </summary>
        protected void OnClear<T>()
        {
            if (_tweenData != null) _tweenData.Remove(typeof(T));

            if (TweenerList != null) TweenerList.Clear();
            TweenerList = null;
        }

    }

}


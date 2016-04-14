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
        protected List<TweenerEntity> TweenerList = new List<TweenerEntity>();

        private static CLTweenEvent CreateTween<T>() where T : CLTweenEvent
        {
            Type tweenType = typeof(T);
            CLTweenEvent tween;
            if (_tweenData.TryFind(tweenType, out tween)) return tween;

            tween = SLToolsHelper.Create<T>("_tween event");
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
        public static void BeginAlpha<T>(GameObject go, float duration, float from, float to) where T : CLTweenEvent
        {
            if (go == null) return;

            CLTweenEvent tween = CreateTween<T>();
            if (tween == null) return;

            tween.TweenerList.Add(new TweenerEntity()
            {
                TweenType = TweenerType.Alpha,
                Target = go,
                from = from,
                to = to,
                Duration = duration
            });

        }

        /// <summary>
        /// 增加一个 位置运动
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginPosition<T>(GameObject go, float duration, Vector3 from, Vector3 to) where T : CLTweenEvent
        {
            if (go == null) return;

            CLTweenEvent tween = CreateTween<T>();
            if (tween == null) return;

            tween.TweenerList.Add(new TweenerEntity()
            {
                TweenType = TweenerType.Position,
                Target = go,
                fVector = from,
                tVector = to,
                Duration = duration
            });
        }

        /// <summary>
        /// 增加一个 旋转运动
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginRotation<T>(GameObject go, float duration, Vector3 from, Vector3 to) where T : CLTweenEvent
        {
            if (go == null) return;

            CLTweenEvent tween = CreateTween<T>();
            if (tween == null) return;

            tween.TweenerList.Add(new TweenerEntity()
            {
                TweenType = TweenerType.Rotation,
                Target = go,
                fVector = from,
                tVector = to,
                Duration = duration
            });
        }

        /// <summary>
        /// 增加一个 大小
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginScale<T>(GameObject go, float duration, Vector3 from, Vector3 to) where T : CLTweenEvent
        {
            if (go == null) return;

            CLTweenEvent tween = CreateTween<T>();
            if (tween == null) return;

            tween.TweenerList.Add(new TweenerEntity()
            {
                TweenType = TweenerType.Scale,
                Target = go,
                fVector = from,
                tVector = to,
                Duration = duration
            });
        }

        /// <summary>
        /// 颜色改变
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static void BeginColor<T>(GameObject go, float duration, Color from, Color to) where T : CLTweenEvent
        {
            if (go == null) return;

            CLTweenEvent tween = CreateTween<T>();
            if (tween == null) return;

            tween.TweenerList.Add(new TweenerEntity()
            {
                TweenType = TweenerType.Color,
                Target = go,
                fColor = from,
                tColor = to,
                Duration = duration
            });
        }

        public static UITweener BeginTweener(TweenerEntity entity)
        {
            if (entity == null) return null;

            entity.Target.SetActive(true);
            UITweener tween = null;

            switch (entity.TweenType)
            {
                case TweenerType.Scale:
                    TweenScale scale = TweenScale.Begin(entity.Target, entity.Duration, entity.tVector);
                    scale.from = entity.fVector;
                    scale.to = entity.tVector;
                    scale.method = UITweener.Method.EaseInOut;
                    tween = scale;
                    break;
                case TweenerType.Alpha:
                    TweenAlpha alpha = TweenAlpha.Begin(entity.Target, entity.Duration, entity.to);
                    alpha.from = entity.from;
                    alpha.to = entity.to;
                    alpha.method = UITweener.Method.EaseInOut;
                    tween = alpha;
                    break;
                case TweenerType.Position:
                    TweenPosition position = TweenPosition.Begin(entity.Target, entity.Duration, entity.tVector);
                    position.from = entity.fVector;
                    position.to = entity.tVector;
                    position.method = UITweener.Method.EaseInOut;
                    tween = position;
                    break;
                case TweenerType.Rotation:
                    TweenRotation rotation = TweenRotation.Begin(entity.Target, entity.Duration, Quaternion.identity);
                    rotation.from = entity.fVector;
                    rotation.to = entity.tVector;
                    rotation.method = UITweener.Method.EaseInOut;
                    tween = rotation;
                    break;
                case TweenerType.Color:
                    TweenColor color = TweenColor.Begin(entity.Target, entity.Duration, entity.fColor);
                    color.from = entity.fColor;
                    color.to = entity.tColor;
                    color.method = UITweener.Method.EaseInOut;
                    tween = color;
                    break;
            }

            return tween;
        }

        /// <summary>
        /// 清理当前数据
        /// </summary>
        protected void OnClear<T>() where T : CLTweenEvent
        {
            if (_tweenData != null) _tweenData.Remove(typeof(T));

            if (TweenerList != null) TweenerList.Clear();
            TweenerList = null;
        }

    }

}


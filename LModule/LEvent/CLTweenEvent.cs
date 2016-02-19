using System;
using System.Collections.Generic;
using LGame.LBehaviour;
using LGame.LCommon;
using UnityEngine;

namespace LGame.LEvent
{

    /****
     * 
     * 
     * 执行 一串 tweener 事件
     * 
     * 主要对 tweener 事件辅助
     * 
     */

    public class CLTweenEvent : ALBehaviour
    {
        private static CLTweenEvent _tweenEvent = null;

        private static CLTweenEvent Instance
        {
            get
            {
                if (_tweenEvent == null) BeginTweener();
                return _tweenEvent;
            }
        }

        /// <summary>
        /// 运动时间
        /// </summary>
        private float mTweenTime = 0;

        private List<TweenerEntity> TweenerList;

        /// <summary>
        /// 开始一个新的运动队列
        /// 
        /// 和原来以增加的队列的并行
        /// 
        /// 后面增加的运动, 默认添加到新的队列中
        /// </summary>
        public static void BeginTweener()
        {
            _tweenEvent = SLCompHelper.Create<CLTweenEvent>("_tween event");
            _tweenEvent.TweenerList = new List<TweenerEntity>();
            DontDestroyOnLoad(_tweenEvent);
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
            if (go == null) return;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                TweenType = TweenerType.Alpha,
                Target = go,
                from = from,
                to = to,
                Duration = duration,
                IsImmediately = false
            });
        }

        /// <summary>
        /// 增加一个 透明运动，是否立即开始下一个
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginAlphaImmediate(GameObject go, float duration, float from, float to)
        {
            if (go == null) return;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                TweenType = TweenerType.Alpha,
                Target = go,
                from = from,
                to = to,
                Duration = duration,
                IsImmediately = true
            });
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
            if (go == null) return;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                TweenType = TweenerType.Position,
                Target = go,
                fVector = from,
                tVector = to,
                Duration = duration,
                IsImmediately = false
            });
        }

        /// <summary>
        /// 增加一个 位置运动
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginPositionImmediate(GameObject go, float duration, Vector3 from, Vector3 to)
        {
            if (go == null) return;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                TweenType = TweenerType.Position,
                Target = go,
                fVector = from,
                tVector = to,
                Duration = duration,
                IsImmediately = true
            });
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
            if (go == null) return;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                TweenType = TweenerType.Rotation,
                Target = go,
                fVector = from,
                tVector = to,
                Duration = duration,
                IsImmediately = false
            });
        }

        /// <summary>
        /// 增加一个 旋转运动
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginRotationImmediate(GameObject go, float duration, Vector3 from, Vector3 to)
        {
            if (go == null) return;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                TweenType = TweenerType.Rotation,
                Target = go,
                fVector = from,
                tVector = to,
                Duration = duration,
                IsImmediately = true
            });
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
            if (go == null) return;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                TweenType = TweenerType.Scale,
                Target = go,
                fVector = from,
                tVector = to,
                Duration = duration,
                IsImmediately = false
            });
        }

        /// <summary>
        /// 增加一个 大小
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginScaleImmediate(GameObject go, float duration, Vector3 from, Vector3 to)
        {
            if (go == null) return;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                TweenType = TweenerType.Scale,
                Target = go,
                fVector = from,
                tVector = to,
                Duration = duration,
                IsImmediately = true
            });
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
            if (go == null) return;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                TweenType = TweenerType.Color,
                Target = go,
                fColor = from,
                tColor = to,
                Duration = duration,
                IsImmediately = false
            });
        }

        /// <summary>
        /// 颜色改变
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginColorImmediate(GameObject go, float duration, Color from, Color to)
        {
            if (go == null) return;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                TweenType = TweenerType.Color,
                Target = go,
                fColor = from,
                tColor = to,
                Duration = duration,
                IsImmediately = true
            });
        }

        public override void OnUpdate(float deltaTime)
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

            TweenerEntity entity = TweenerList[0];
            if (entity == null || entity.TweenType == TweenerType.None)
            {
                TweenerList.RemoveAt(0);
                return;
            }
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
            mTweenTime = entity.IsImmediately ? 0 : tween.duration;
            tween.PlayForward();
            TweenerList.RemoveAt(0);
        }

        /// <summary>
        /// 清理当前数据
        /// </summary>
        public override void OnClear()
        {
            _tweenEvent = null;
            if (TweenerList != null) TweenerList.Clear();
            TweenerList = null;
            mTweenTime = 0;
        }
    }

}


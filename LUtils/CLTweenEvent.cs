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
     * 执行 一串 tweener 事件
     * 
     * 主要对 tweener 事件辅助
     * 
     */

    public class CLTweenEvent : LABehaviour
    {
        private static CLTweenEvent _tweenEvent = null;

        private static CLTweenEvent Instance
        {
            get
            {
                if (_tweenEvent == null)
                {
                    GameObject create = LCSCompHelper.Create("_tween event");
                    _tweenEvent = LCSCompHelper.FindComponet<CLTweenEvent>(create);
                    DontDestroyOnLoad(create);
                }
                return _tweenEvent;
            }
        }

        /// <summary>
        /// 运动时间
        /// </summary>
        private float mTweenTime = 0;

        /// <summary>
        /// 保存一个列表
        /// </summary>
        private List<TweenerEntity> TweenerList = new List<TweenerEntity>();

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
            TweenAlpha alpha = TweenAlpha.Begin(go, duration, to);
            alpha.from = from;
            alpha.to = to;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                Tweener = alpha,
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
            TweenAlpha alpha = TweenAlpha.Begin(go, duration, to);
            alpha.from = from;
            alpha.to = to;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                Tweener = alpha,
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
            TweenPosition position = TweenPosition.Begin(go, duration, to);
            position.from = from;
            position.to = to;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                Tweener = position,
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
            TweenPosition position = TweenPosition.Begin(go, duration, to);
            position.from = from;
            position.to = to;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                Tweener = position,
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
            TweenRotation rota = TweenRotation.Begin(go, duration, Quaternion.identity);
            rota.from = from;
            rota.to = to;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                Tweener = rota,
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
            TweenRotation rota = TweenRotation.Begin(go, duration, Quaternion.identity);
            rota.from = from;
            rota.to = to;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                Tweener = rota,
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
            TweenScale scale = TweenScale.Begin(go, duration, to);
            scale.from = from;
            scale.to = to;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                Tweener = scale,
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
            TweenScale scale = TweenScale.Begin(go, duration, to);
            scale.from = from;
            scale.to = to;
            Instance.TweenerList.Add(new TweenerEntity()
            {
                Tweener = scale,
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
            if (entity == null) return;
            UITweener tween = entity.Tweener;
            mTweenTime = entity.IsImmediately ? 0 : tween.duration;
            tween.PlayForward();
            TweenerList.RemoveAt(0);
        }
    }

}


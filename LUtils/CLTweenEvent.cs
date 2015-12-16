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
     * 执行 一串 tween 事件
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
        private List<UITweener> TweenList = new List<UITweener>();

        /// <summary>
        /// 增加一个 透明运动
        /// </summary>
        /// <param name="go">运动的对象</param>
        /// <param name="duration">运动的时间</param>
        /// <param name="from">起始值</param>
        /// <param name="to">目标值</param>
        public static void BeginAlpha(GameObject go, float duration, float from, float to)
        {
            TweenAlpha alpha = TweenAlpha.Begin(go, duration, to);
            alpha.from = from;
            alpha.to = to;
            Instance.TweenList.Add(alpha);
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
            TweenPosition position = TweenPosition.Begin(go, duration, to);
            position.from = from;
            position.to = to;
            Instance.TweenList.Add(position);
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
            TweenRotation rota = TweenRotation.Begin(go, duration, Quaternion.identity);
            rota.from = from;
            rota.to = to;
            Instance.TweenList.Add(rota);
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
            TweenScale scale = TweenScale.Begin(go, duration, to);
            scale.from = from;
            scale.to = to;
            Instance.TweenList.Add(scale);
        }

        public override void OnUpdate(float deltaTime)
        {
            if (TweenList.Count <= 0) return;

            if (mTweenTime > 0)
            {
                mTweenTime -= deltaTime;
                return;
            }

            UITweener tween = TweenList[0];

            mTweenTime = tween.duration;
            tween.PlayForward();
            TweenList.RemoveAt(0);
        }
    }

}


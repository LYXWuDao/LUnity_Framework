using System;
using System.Collections.Generic;
using LGame.LBehaviour;
using LGame.LCommon;
using UnityEngine;

namespace LGame.LUtils
{

    /******
     * 
     * 
     * 间隔多少时间
     * 
     */

    public class CLDelayAction : ALBehaviour
    {

        private static CLDelayAction _instance = null;

        private static object _lock = new object();

        private static CLDelayAction Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            GameObject go = SLCompHelper.Create("_delay action");
                            _instance = SLCompHelper.FindComponet<CLDelayAction>(go);
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// 当前回调 action
        /// </summary>
        private Action mActionBack = null;

        /// <summary>
        /// 调用 action 间隔时间
        /// </summary>
        private float mActionTime = 0;

        public override void OnUpdate(float deltaTime)
        {
            if (mActionBack == null) return;
            if (mActionTime > 0)
            {
                mActionTime -= deltaTime;
                return;
            }
            mActionBack();
            mActionBack = null;
            RemoveAction();
        }

        public static CLDelayAction BeginAction(float delayTime, Action action)
        {
            if (action == null) return null;
            if (delayTime <= 0)
            {
                action();
                return null;
            }
            CLDelayAction delact = Instance;
            delact.mActionBack = action;
            delact.mActionTime = delayTime;
            return delact;
        }

        /// <summary>
        /// 移出当前脚本
        /// </summary>
        public void RemoveAction()
        {
            Destroy(this);
        }

        public override void OnClear()
        {
            _instance = null;
        }

    }

}
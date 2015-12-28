using System;
using System.Collections.Generic;
using LGame.LBehaviour;
using LGame.LCommon;
using UnityEngine;

namespace LGame.LScenes
{

    /****
     * 
     * 
     * 场景 响应事件行为组件
     * 
     * 如果是 c# 界面则直接继承它
     * 如果是lua 则需要继承 CLSceneLuaBehaviour
     * 
     * 每个场景都会有一个 SceneRoot  节点，挂载这个组件
     * 
     */

    public class CLSceneBehaviour : ALBehaviour
    {

        /// <summary>
        /// 界面上所有的 Collider
        /// </summary>
        [NonSerialized]
        private Collider[] mBoxColliders;

        /// <summary>
        /// 收集界面上所有的 UIEventTrigger  事件
        /// </summary>
        [NonSerialized]
        private UIEventTrigger[] mEventTrigger;

        /// <summary>
        /// 场景的名字
        /// </summary>
        public string SceneName
        {
            get
            {
                return SLScenesManage.CurrentScene.SceneName;
            }
        }

        /// <summary>
        /// 保留上层的操作
        /// </summary>
        public override void Awake()
        {

            mBoxColliders = SLCompHelper.GetComponents<Collider>(gameObject);
            mEventTrigger = SLCompHelper.GetComponents<UIEventTrigger>(gameObject);

            if (mEventTrigger != null && mEventTrigger.Length > 0)
            {
                for (int i = 0, len = mEventTrigger.Length; i < len; i++)
                {
                    UIEventTrigger trigger = mEventTrigger[i];
                    trigger.onClick.Add(new EventDelegate(OnClick));
                    trigger.onDoubleClick.Add(new EventDelegate(OnDoubleClick));
                    trigger.onPress.Add(new EventDelegate(OnPress));
                    trigger.onRelease.Add(new EventDelegate(OnRelease));
                }
            }

            OnAwake();
        }

        public override void Start()
        {
            OnStart();
        }

        /// <summary>
        /// 单击事件
        /// 
        /// 之类不应该继承它，而应该继承 OnCollider
        /// 
        /// 而且外部不应该调用这个函数
        /// 
        /// </summary>
        private void OnClick()
        {
            Collider click = UICamera.lastHit.collider;
            if (click == null) return;
            OnCollider(click.gameObject);
        }

        /// <summary>
        /// 双击事件
        /// 
        /// 之类不应该继承它，而应该继承 OnDoubleCollider
        /// 
        /// 而且外部不应该调用这个函数
        /// 
        /// </summary>
        private void OnDoubleClick()
        {
            Collider dclick = UICamera.lastHit.collider;
            if (dclick == null) return;
            OnDoubleCollider(dclick.gameObject);
        }

        /// <summary>
        /// 按下一定时间
        /// </summary>
        private void OnPress()
        {
            Collider press = UICamera.lastHit.collider;
            if (press == null) return;
            OnPressCollider(press.gameObject);
        }

        /// <summary>
        /// 鼠标按下一定时间后抬起
        /// </summary>
        private void OnRelease()
        {
            Collider release = UICamera.lastHit.collider;
            if (release == null) return;
            OnReleaseCollider(release.gameObject);
        }

        /// <summary>
        /// 进入场景
        /// 
        /// 用于父类操作保持，不允许重写
        /// </summary>
        public void EnterScene()
        {
            OnFocus();
            OnEnterScene();
        }

        /// <summary>
        /// 离开场景
        /// 
        /// 用于父类操作保持，不允许重写
        /// </summary>
        public void LeaveScene()
        {
            mBoxColliders = null;
            mEventTrigger = null;
            OnLostFocus();
            OnLeaveScene();
        }

        /// <summary>
        ///  场景获得焦点
        /// </summary>
        public void OnFocus()
        {
            if (mBoxColliders == null) return;
            SLCompHelper.CollidersEnabled(mBoxColliders);
        }

        /// <summary>
        /// 场景失去焦点
        /// </summary>
        public void OnLostFocus()
        {
            if (mBoxColliders == null) return;
            SLCompHelper.CollidersEnabled(mBoxColliders, false);
        }

        /// <summary>
        /// 用于子类继承
        /// </summary>
        public virtual void OnAwake()
        {

        }

        /// <summary>
        /// 用于子类继承
        /// </summary>
        public virtual void OnStart()
        {

        }

        /// <summary>
        /// 进入场景
        /// 
        /// 用于子类继承
        /// </summary>
        public virtual void OnEnterScene()
        {

        }

        /// <summary>
        /// 离开场景
        /// 
        /// 用于之类继承
        /// </summary>
        public virtual void OnLeaveScene()
        {

        }

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="btn"></param>
        public virtual void OnCollider(GameObject btn)
        {

        }

        /// <summary>
        /// 子类继承该函数
        /// 
        /// 双击函数
        /// </summary>
        /// <param name="btn">当前双击 Collider</param>
        public virtual void OnDoubleCollider(GameObject btn)
        {

        }

        /// <summary>
        /// 子类继承该函数
        /// 
        /// 按下时候的操作
        /// </summary>
        /// <param name="btn">按下时操作的 Collider</param>
        public virtual void OnPressCollider(GameObject btn)
        {

        }

        /// <summary>
        /// 子类继承该函数
        /// 
        /// 抬起鼠标或者手指的操作
        /// </summary>
        /// <param name="btn">抬起的 Collider </param>
        public virtual void OnReleaseCollider(GameObject btn)
        {

        }

    }

}


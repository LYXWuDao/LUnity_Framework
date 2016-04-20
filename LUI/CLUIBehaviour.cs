using System;
using LGame.LBehaviour;
using LGame.LCommon;
using UnityEngine;
using LGame.LSource;

namespace LGame.LUI
{

    /***
     * 
     * 
     * ui 界面响应事件行为组件
     *  
     * 如果是 c# 界面则直接继承它
     * 如果是lua 界面 则需要继承 CLUILuaBehaviour
     * 
     * 
     */

    public class CLUIBehaviour : ALBehaviour
    {

        /// <summary>
        /// 界面上所以的panel
        /// </summary>
        [NonSerialized]
        private UIPanel[] mPanels;

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
        /// 当前长按的对象
        /// 
        /// 以按下为准
        /// </summary>
        [NonSerialized]
        private Collider mCurrLoopColliders = null;

        /// <summary>
        /// 按下一定时间后,才算长按
        /// </summary>
        private float mLoopTime = 0.3f;

        /// <summary>
        /// 点击间隔时间
        /// </summary>
        private float mClickDelayTime = 0f;

        /// <summary>
        /// 按下的间隔时间
        /// </summary>
        private float mPressDelayTime = 0f;

        /// <summary>
        /// 界面 ui
        /// </summary>
        [NonSerialized]
        private string mWinName = string.Empty;

        /// <summary>
        /// 界面的深度
        /// </summary>
        [NonSerialized]
        private int mWinDepth = 0;

        /// <summary>
        /// 窗口深度
        /// </summary>
        public int WinDepth
        {
            get
            {
                return mWinDepth;
            }
        }

        /// <summary>
        /// 窗口的名字
        /// </summary>
        public string WinName
        {
            get
            {
                return mWinName;
            }
        }

        /// <summary>
        /// 子类不需要继承， 继承 OnAwake
        /// 
        /// 父类保留做操作
        /// 
        /// 如果继承 则需要先调用父类的 Awake 方法
        /// </summary>
        protected override void Awake()
        {
            mCurrLoopColliders = null;
            mLoopTime = 0.3f;
            mClickDelayTime = 0f;
            mPressDelayTime = 0f;

            mPanels = SLToolsHelper.GetComponents<UIPanel>(gameObject);
            mBoxColliders = SLToolsHelper.GetComponents<Collider>(gameObject);
            mEventTrigger = SLToolsHelper.GetComponents<UIEventTrigger>(gameObject);

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

        /// <summary>
        /// 子类不需要继承， 继承 OnStart
        /// 
        /// 父类保留做操作
        /// 
        /// 如果子类继承该函数，要先调用父类的 Start方法
        /// </summary>
        protected override void Start()
        {
            OnStart();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deltaTime"></param>
        protected override void Update()
        {
            float dltTime = Time.deltaTime;

            if (mCurrLoopColliders != null)
            {
                if (mLoopTime <= 0) OnLongPress();
                mLoopTime -= dltTime;
            }

            if (mClickDelayTime > 0) mClickDelayTime -= dltTime;
            if (mPressDelayTime > 0) mPressDelayTime -= dltTime;

            OnUpdate(dltTime);
        }

        /// <summary>
        /// 界面销毁
        /// </summary>
        protected override void OnDestroy()
        {
            OnClear();
            // 卸载资源
            SLManageSource.RemoveSource(WinName);
        }

        /// <summary>
        /// 单击事件
        /// 
        /// 之类不应该继承它，而应该继承 OnCollider
        /// 
        /// 而且外部不应该调用这个函数
        /// 
        /// </summary>
        protected void OnClick()
        {
            if (mClickDelayTime > 0) return;
            mClickDelayTime = 0.2f;
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
        protected void OnDoubleClick()
        {
            Collider dclick = UICamera.lastHit.collider;
            if (dclick == null) return;
            OnDoubleCollider(dclick.gameObject);
        }

        /// <summary>
        /// 按下一定时间
        /// </summary>
        protected void OnPress()
        {
            if (mPressDelayTime > 0) return;
            mPressDelayTime = 0.2f;
            Collider press = UICamera.lastHit.collider;
            if (press == null) return;
            mCurrLoopColliders = press;
            mLoopTime = 0.3f;
            OnPressCollider(press.gameObject);
        }

        /// <summary>
        /// 鼠标按下一定时间后抬起
        /// </summary>
        protected void OnRelease()
        {
            Collider release = UICamera.lastHit.collider;
            if (release == null) return;
            mCurrLoopColliders = null;
            mLoopTime = 0.3f;
            OnReleaseCollider(release.gameObject);
        }

        /// <summary>
        /// 鼠标响应长按
        /// </summary>
        protected void OnLongPress()
        {
            if (mCurrLoopColliders == null) return;
            OnLoopPressCollider(mCurrLoopColliders.gameObject);
        }

        /// <summary>
        /// 打开界面
        /// 
        /// 打开并创建界面
        /// </summary>
        public void OnOpen() { }

        /// <summary>
        /// 打开一个具有深度的窗口
        /// </summary>
        /// <param name="depth"></param>
        public void OnOpen(int depth)
        {
            mWinDepth = depth;
        }

        /// <summary>
        /// 打开界面
        /// </summary>
        /// <param name="depth">界面深度</param>
        /// <param name="winName">界面的名字</param>
        public void OnOpen(int depth, string winName)
        {
            mWinDepth = depth;
            mWinName = winName;
            if (mPanels == null) return;
            for (int i = 0, len = mPanels.Length; i < len; i++)
                mPanels[i].depth = depth + i;
        }

        /// <summary>
        ///  界面获得焦点
        /// </summary>
        public void OnFocus()
        {
            if (mBoxColliders == null) return;
            SLToolsHelper.CollidersEnabled(mBoxColliders);
        }

        /// <summary>
        /// 界面失去焦点
        /// </summary>
        public void OnLostFocus()
        {
            if (mBoxColliders == null) return;
            SLToolsHelper.CollidersEnabled(mBoxColliders, false);
        }

        /// <summary>
        /// 显示
        /// </summary>
        public void OnShow()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public void OnHide()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// 关闭界面
        /// 
        /// 关闭并销毁该界面
        /// 
        /// 如果该类被重写，需要调用base该方法
        /// </summary>
        public void OnClose()
        {
            mPanels = null;
            mBoxColliders = null;
            mEventTrigger = null;
            mCurrLoopColliders = null;
            mLoopTime = 0.3f;
            mClickDelayTime = 0f;
            mPressDelayTime = 0f;
            SLUIManage.CloseWindow(this);
        }

        /// <summary>
        /// 刷新面板
        /// </summary>
        public virtual void OnRefresh()
        {

        }

        /// <summary>
        /// 子类继承
        /// </summary>
        protected virtual void OnAwake()
        {
        }

        /// <summary>
        /// 子类继承
        /// </summary>
        protected virtual void OnStart()
        {
        }
        
        /// <summary>
        /// 子类继承该函数
        ///     
        /// 点击函数
        /// </summary>
        /// <param name="btn">当前点击 Collider </param>
        protected virtual void OnCollider(GameObject btn)
        {

        }

        /// <summary>
        /// 子类继承该函数
        /// 
        /// 双击函数
        /// </summary>
        /// <param name="btn">当前双击 Collider</param>
        protected virtual void OnDoubleCollider(GameObject btn)
        {

        }

        /// <summary>
        /// 子类继承该函数
        /// 
        /// 按下时候的操作
        /// </summary>
        /// <param name="btn">按下时操作的 Collider</param>
        protected virtual void OnPressCollider(GameObject btn)
        {

        }

        /// <summary>
        /// 子类继承该函数
        /// 
        /// 抬起鼠标或者手指的操作
        /// </summary>
        /// <param name="btn">抬起的 Collider </param>
        protected virtual void OnReleaseCollider(GameObject btn)
        {

        }

        /// <summary>
        /// 长按鼠标
        /// 
        /// 子类继承该函数
        /// </summary>
        /// <param name="btn">按下的 Collider</param>
        protected virtual void OnLoopPressCollider(GameObject btn)
        {

        }

    }
}


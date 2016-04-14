using System;
using UnityEngine;

namespace LGame.LBehaviour
{

    /******
     * 
     * 
     *  unity 脚本的生命周期
     * 
     *  行为组件基类，所以行为都需要继承它
     *
     * 
     */

    public abstract class ALBehaviour : CLBehaviour
    {

        /// <summary>
        /// 对象创建后调用
        /// </summary>
        protected virtual void Awake() { }

        /// <summary>
        /// 获得焦点
        /// 
        /// 只在每次激活GameObject的时候执行一次
        /// 例如：gameobject.SetActive(true);
        /// 
        /// </summary>
        protected virtual void OnEnable() { }

        /// <summary>
        /// unity 自动调用
        /// </summary>
        protected virtual void FixedUpdate()
        {
            OnFixedUpdate(Time.deltaTime);
        }

        protected virtual void OnFixedUpdate(float fixedTime) { }

        /// <summary>
        /// 在update第一帧调用前调用
        /// </summary>
        protected virtual void Start() { }

        /// <summary>
        /// 每一帧调用
        /// </summary>
        protected virtual void Update()
        {
            OnUpdate(Time.deltaTime);
        }

        protected virtual void OnUpdate(float deltaTime) { }

        protected virtual void LateUpdate()
        {
            OnLateUpdate(Time.deltaTime);
        }

        protected virtual void OnLateUpdate(float deltaTime) { }

        /// <summary>
        /// unity 界面绘制调用
        /// </summary>
        protected virtual void OnGUI() { }

        /// <summary>
        /// 对象失去焦点时调用
        /// 
        /// 只在每次失去焦点,GameObject的时候执行一次
        /// 例如：gameobject.SetActive(false);
        /// 
        /// </summary>
        protected virtual void OnDisable() { }

        /// <summary>
        /// 被销毁的时候清理数据
        /// </summary>
        protected virtual void OnClear() { }

        /// <summary>
        /// unity 自动调用
        /// 
        /// 对象被销毁时调用
        /// </summary>
        protected virtual void OnDestroy()
        {
            OnClear();
        }

        /// <summary>
        /// 项目暂停时调用
        /// </summary>
        protected virtual void OnApplicationPause() { }

        /// <summary>
        /// 项目退出时调用
        /// </summary>
        protected virtual void OnApplicationQuit() { }

        /// <summary>
        /// 主动调用销毁
        /// 
        /// 框架使用或者继承框架者调用
        /// </summary>
        public virtual void Destroy()
        {
            GameObject.Destroy(gameObject);
        }
    }

}
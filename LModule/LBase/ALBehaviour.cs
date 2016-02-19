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

    public abstract class ALBehaviour : CLBehaviour, ILBehaviour
    {
        public virtual void Awake()
        {
        }

        public virtual void OnEnable()
        {
        }

        public virtual void FixedUpdate()
        {
            OnFixedUpdate(Time.deltaTime);
        }

        public void OnFixedUpdate(float fixedTime)
        {

        }

        public virtual void Start()
        {
        }

        public virtual void Update()
        {
            OnUpdate(Time.deltaTime);
        }

        public virtual void OnUpdate(float deltaTime)
        {

        }

        public virtual void LateUpdate()
        {
            OnLateUpdate(Time.deltaTime);
        }

        public void OnLateUpdate(float deltaTime)
        {

        }

        public virtual void OnGUI()
        {
        }

        public virtual void OnApplicationPause()
        {
        }

        public virtual void OnDisable()
        {
        }

        /// <summary>
        /// 响应自己的销毁
        /// </summary>
        public virtual void OnClear()
        {

        }

        /// <summary>
        /// 手动调用销毁自己
        /// </summary>
        public virtual void Destroy()
        {
            GameObject.Destroy(gameObject);
        }

        /// <summary>
        /// unity 响应销毁
        /// 
        /// 并在自己销毁时调用清理函数
        /// </summary>
        public virtual void OnDestroy()
        {
            OnClear();
        }

        public virtual void OnApplicationQuit()
        {

        }

    }

}
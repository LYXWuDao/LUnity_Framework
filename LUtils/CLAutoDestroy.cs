using LGame.LBehaviour;
using LGame.LCommon;
using UnityEngine;
using System.Collections;

namespace LGame.LUtils
{


    /*****
     * 
     * 
     * 自动销毁自己
     * 
     */


    public class CLAutoDestroy : ALBehaviour
    {

        /// <summary>
        /// 自我销毁时间
        /// </summary>
        public float mDtyTime = 0f;

        /// <summary>
        /// 增加一个销毁脚本
        /// 
        /// 开启一个销毁
        /// </summary>
        /// <returns></returns>
        public static CLAutoDestroy Begin(GameObject go, float dtyTime)
        {
            CLAutoDestroy dest = SLToolsHelper.FindComponet<CLAutoDestroy>(go);
            dest.mDtyTime = dtyTime;
            return dest;
        }

        protected override void OnUpdate(float deltaTime)
        {
            if (mDtyTime > 0)
            {
                mDtyTime -= deltaTime;
                return;
            }
            Destroy();
        }

        protected override void OnClear()
        {
            mDtyTime = 0;
        }

    }
}

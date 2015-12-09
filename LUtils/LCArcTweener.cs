using System.Collections.Generic;
using UnityEngine;
using System.Collections;

/****
 * 
 * 
 * 物体弧形运动
 * 
 * 起始点和目标相同，或者目标点是空 则坐圆形运动
 * 
 */

namespace Game.LUtils
{

    public class LCArcTweener : LCTweener
    {
        /// <summary>
        /// 圆心
        /// </summary>
        private Vector3 mHeart;

        /// <summary>
        /// 移动时间
        /// </summary>
        private float mMoveTime = 0f;

        private LTSpline mMovePath;

        public Transform[] mPoint;

        private Vector3 mCenter;

        public override void Awake()
        {

        }

        public override void Start()
        {

        }

        public override void OnUpdate(float deltaTime)
        {

        }

        private bool isDraw = false;

        /// <summary>
        /// 
        /// // vector3 ( r*cos A, 0, r*sin A  );
        /// 
        /// </summary>

        private void OnDrawGizmos()
        {

            Gizmos.color = Color.red;

            for (int i = 0, len = mPoint.Length; i < len && i + 1 < len; i++)
            {
                Vector3 source = mPoint[i].position;
                Vector3 target = mPoint[i + 1].position;

                Vector3 start = target;
                Vector3 cirCenter = (source + target)/2;
                float radius = Vector3.Distance(source, target)/2;

                UnityEngine.Debug.Log(target.x - cirCenter.x);

                float angle = 180*Mathf.Asin((target.z - cirCenter.z)/radius)/(Mathf.PI*radius);

                UnityEngine.Debug.Log(angle);

                for (; angle < 180; angle++)
                {

                    float degree = ((angle)*Mathf.PI)/180f;
                    Vector3 next = new Vector3(cirCenter.x + radius*Mathf.Cos(degree), cirCenter.y,
                        cirCenter.z + radius*Mathf.Sin(degree));
                    Gizmos.DrawLine(start, next);
                    start = next;

                }

            }

        }

    }
}

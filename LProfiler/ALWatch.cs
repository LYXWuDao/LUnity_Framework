using System.Collections.Generic;
using LGame.LCommon;

namespace LGame.LProfiler
{


    /***
     * 
     * 抽象性能检测类数据处理
     * 
     */

    public abstract class ALWatch : CLTypeDicData<ALWatch, string, RecordWatchEntity>
    {

        /// <summary>
        /// 当前观察数据
        /// </summary>
        private RecordWatchEntity _currentWatch;

        /// <summary>
        /// 增加一个观察
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void AddWatch(RecordWatchEntity entity)
        {
            if (entity == null || string.IsNullOrEmpty(entity.WatchKey)) return;
            Add(entity.WatchKey, entity);
            CurrentWatch = entity;
        }

        /// <summary>
        /// 移出一个观察
        /// </summary>
        /// <param name="watchKey"></param>
        protected virtual void RemoveWatch(string watchKey)
        {
            if (string.IsNullOrEmpty(watchKey) || !Contains(watchKey)) return;
            Remove(watchKey);
            CurrentWatch = null;
        }

        /// <summary>
        /// 移出一个观察
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void RemoveWatch(RecordWatchEntity entity)
        {
            if (entity == null || string.IsNullOrEmpty(entity.WatchKey)) return;
            RemoveWatch(entity.WatchKey);
        }

        /// <summary>
        /// 清除所有的观察
        /// </summary>
        protected virtual void ClearAllWatch()
        {
            RemoveAll();
        }

        /// <summary>
        /// 得到当前的观察数据
        /// </summary>
        public RecordWatchEntity CurrentWatch
        {
            private set
            {
                _currentWatch = value;
            }
            get
            {
                return _currentWatch;
            }
        }

    }

}

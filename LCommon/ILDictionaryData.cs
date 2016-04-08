using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LGame.LCommon
{

    /// <summary>
    /// 数据处理接口模板类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    interface ILDictionaryData<TKey, TValue>
    {

        void Add(TKey key, TValue value);

        /// <summary>
        /// 修改管理数据
        /// 
        /// key 值不允许修改
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">新的值</param>
        void Modify(TKey key, TValue value);

        /// <summary>
        /// 是否包含该值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Contains(TKey key);

        /// <summary>
        /// 根据key查找值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TValue Find(TKey key);

        /// <summary>
        /// 尝试更加key查找值
        /// false， 表示不存在
        /// true，  表示成功
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool TryFind(TKey key, out TValue value);

        /// <summary>
        /// 查找第几个值
        /// </summary>
        /// <param name="index">值下标，从零开始</param>
        /// <returns>找到的值</returns>
        TValue FindValue(int index);

        /// <summary>
        /// 该值的下标
        /// </summary>
        /// <param name="value"></param>
        /// <returns> -1 表示没找到 </returns>
        int ValueIndexOf(TValue value);

        /// <summary>
        /// 查找第几个key
        /// </summary>
        /// <param name="index">值下标，从零开始</param>
        /// <returns>找到的key值</returns>
        TKey FindKey(int index);

        /// <summary>
        /// 该key值的下标
        /// </summary>
        /// <param name="key"></param>
        /// <returns>-1表示没找到</returns>
        int KeyIndexOf(TKey key);

        /// <summary>
        ///  移出某个值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Remove(TKey key);

        /// <summary>
        /// 移出所有的数据
        /// </summary>
        /// <returns></returns>
        bool RemoveAll();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LGame.LCommon
{
    /// <summary>
    /// 
    /// 
    ///  根据类型保存不同的数据
    /// 
    /// 
    /// </summary>
    /// <typeparam name="TTypeKey"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class CLTypeDicData<TTypeKey, TKey, TValue>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        private static Dictionary<Type, CLBaseDicData<TKey, TValue>> _typeDicData = new Dictionary<Type, CLBaseDicData<TKey, TValue>>();

        /// <summary>
        /// 保存的类型类型
        /// </summary>
        public static Type TypeKey
        {
            get
            {
                return typeof(TTypeKey);
            }
        }

        /// <summary>
        /// 数据长度
        /// </summary>
        public static int Length
        {
            get
            {
                CLBaseDicData<TKey, TValue> dicData = _typeDicData[TypeKey];
                return dicData.Length;
            }
        }

        /// <summary>
        ///  返回最后一个数据
        /// </summary>
        public static TValue TopData
        {
            get
            {
                CLBaseDicData<TKey, TValue> dicData = _typeDicData[TypeKey];
                return dicData.TopData;
            }
        }

        public static void Add(TKey key, TValue value)
        {
            CLBaseDicData<TKey, TValue> dicData;
            if (!_typeDicData.TryGetValue(TypeKey, out dicData)) dicData = new CLBaseDicData<TKey, TValue>();
            dicData.Add(key, value);
        }

        /// <summary>
        /// 修改管理数据
        /// 
        /// key 值不允许修改
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">新的值</param>
        public static void Modify(TKey key, TValue value)
        {
            CLBaseDicData<TKey, TValue> dicData;
            if (!_typeDicData.TryGetValue(TypeKey, out dicData)) return;
            dicData.Modify(key, value);
        }

        /// <summary>
        /// 是否包含该值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Contains(TKey key)
        {
            CLBaseDicData<TKey, TValue> dicData;
            if (!_typeDicData.TryGetValue(TypeKey, out dicData)) return false;
            return dicData.Contains(key);
        }

        /// <summary>
        /// 根据key查找值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue Find(TKey key)
        {
            CLBaseDicData<TKey, TValue> dicData;
            if (!_typeDicData.TryGetValue(TypeKey, out dicData)) return default(TValue);
            return dicData.Find(key);
        }

        /// <summary>
        /// 尝试更加key查找值
        /// false， 表示不存在
        /// true，  表示成功
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryFind(TKey key, out TValue value)
        {
            value = default(TValue);
            CLBaseDicData<TKey, TValue> dicData;
            if (!_typeDicData.TryGetValue(TypeKey, out dicData)) return false;
            return dicData.TryFind(key, out value);
        }

        public static List<TValue> FindAllValues()
        {
            CLBaseDicData<TKey, TValue> dicData;
            if (!_typeDicData.TryGetValue(TypeKey, out dicData)) return null;
            return dicData.FindAllValues();
        }

        public static List<TKey> FindAllKeys()
        {
            CLBaseDicData<TKey, TValue> dicData;
            if (!_typeDicData.TryGetValue(TypeKey, out dicData)) return null;
            return dicData.FindAllKeys();
        }

        /// <summary>
        /// 查找第几个值
        /// </summary>
        /// <param name="index">值下标，从零开始</param>
        /// <returns>找到的值</returns>
        public static TValue FindValue(int index)
        {
            CLBaseDicData<TKey, TValue> dicData;
            if (!_typeDicData.TryGetValue(TypeKey, out dicData)) return default(TValue);
            return dicData.FindValue(index);
        }

        /// <summary>
        /// 该值的下标
        /// </summary>
        /// <param name="value"></param>
        /// <returns> -1 表示没找到 </returns>
        public static int ValueIndexOf(TValue value)
        {
            CLBaseDicData<TKey, TValue> dicData;
            if (!_typeDicData.TryGetValue(TypeKey, out dicData)) return -1;
            return dicData.ValueIndexOf(value);
        }

        /// <summary>
        /// 查找第几个key
        /// </summary>
        /// <param name="index">值下标，从零开始</param>
        /// <returns>找到的key值</returns>
        public static TKey FindKey(int index)
        {
            CLBaseDicData<TKey, TValue> dicData;
            if (!_typeDicData.TryGetValue(TypeKey, out dicData)) return default(TKey);
            return dicData.FindKey(index);
        }

        /// <summary>
        /// 该key值的下标
        /// </summary>
        /// <param name="key"></param>
        /// <returns>-1表示没找到</returns>
        public static int KeyIndexOf(TKey key)
        {
            CLBaseDicData<TKey, TValue> dicData;
            if (!_typeDicData.TryGetValue(TypeKey, out dicData)) return -1;
            return dicData.KeyIndexOf(key);
        }

        /// <summary>
        ///  移出某个值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Remove(TKey key)
        {
            CLBaseDicData<TKey, TValue> dicData;
            if (!_typeDicData.TryGetValue(TypeKey, out dicData)) return false;
            return dicData.Remove(key);
        }

        /// <summary>
        ///  移出所有的数据
        /// </summary>
        /// <returns></returns>
        public static bool RemoveAll()
        {
            if (!_typeDicData.ContainsKey(TypeKey)) return false;
            return _typeDicData.Remove(TypeKey);
        }

    }
}

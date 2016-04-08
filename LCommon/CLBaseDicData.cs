using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LGame.LCommon
{

    /// <summary>
    /// 
    /// 
    /// 保存数据的基础类
    /// 
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class CLBaseDicData<TKey, TValue> : ILDictionaryData<TKey, TValue>
    {

        /// <summary>
        /// 保存数据
        /// </summary>
        private Dictionary<TKey, TValue> _dicData = new Dictionary<TKey, TValue>();

        /// <summary>
        /// 保存的 key 值
        /// </summary>
        private List<TKey> _dicKey = new List<TKey>();

        /// <summary>
        /// 保存的 value 值
        /// </summary>
        private List<TValue> _dicValue = new List<TValue>();

        /// <summary>
        /// 数据长度
        /// </summary>
        public int Length
        {
            get
            {
                return _dicKey.Count;
            }
        }

        /// <summary>
        ///  返回最后一个数据
        /// </summary>
        public TValue TopData
        {
            get
            {
                if (Length <= 0) return default(TValue);
                return _dicValue[Length - 1];
            }
        }

        /// <summary>
        /// 增加数据
        /// </summary>
        /// <param name="key">key值</param>
        /// <param name="value">值</param>
        public void Add(TKey key, TValue value)
        {
            if (key == null || _dicData.ContainsKey(key)) return;

            if (_dicData.ContainsKey(key))
            {
                Modify(key, value);
                return;
            }

            _dicData.Add(key, value);
            _dicKey.Add(key);
            _dicValue.Add(value);
        }

        /// <summary>
        /// 修改管理数据
        /// 
        /// key 值不允许修改
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">新的值</param>
        public void Modify(TKey key, TValue value)
        {
            TValue old;
            if (!_dicData.ContainsKey(key) || !_dicData.TryGetValue(key, out old)) return;
            _dicData[key] = value;
            var index = _dicValue.IndexOf(old);
            _dicValue[index] = value;
        }

        /// <summary>
        /// 是否包含该值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(TKey key)
        {
            if (key == null) return false;
            return _dicData.ContainsKey(key);
        }

        /// <summary>
        /// 根据key查找值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue Find(TKey key)
        {
            if (key == null || !_dicData.ContainsKey(key)) return default(TValue);
            return _dicData[key];
        }

        /// <summary>
        /// 尝试更加key查找值
        /// false， 表示不存在
        /// true，  表示成功
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryFind(TKey key, out TValue value)
        {
            value = default(TValue);
            if (key == null || !_dicData.ContainsKey(key)) return false;
            return _dicData.TryGetValue(key, out value);
        }

        /// <summary>
        /// 查找所有的值
        /// </summary>
        /// <returns></returns>
        public List<TValue> FindAllValues()
        {
            return _dicValue;
        }

        /// <summary>
        /// 查找所有的key值
        /// </summary>
        /// <returns></returns>
        public List<TKey> FindAllKeys()
        {
            return _dicKey;
        }

        /// <summary>
        /// 查找第几个值
        /// </summary>
        /// <param name="index">值下标，从零开始</param>
        /// <returns>找到的值</returns>
        public TValue FindValue(int index)
        {
            if (index < 0 || index >= Length) return default(TValue);
            return _dicValue[index];
        }

        /// <summary>
        /// 该值的下标
        /// </summary>
        /// <param name="value"></param>
        /// <returns> -1 表示没找到 </returns>
        public int ValueIndexOf(TValue value)
        {
            if (value == null) return -1;
            return _dicValue.IndexOf(value);
        }

        /// <summary>
        /// 查找第几个key
        /// </summary>
        /// <param name="index">值下标，从零开始</param>
        /// <returns>找到的key值</returns>
        public TKey FindKey(int index)
        {
            if (index < 0 || index >= Length) return default(TKey);
            return _dicKey[index];
        }

        /// <summary>
        /// 该key值的下标
        /// </summary>
        /// <param name="key"></param>
        /// <returns>-1表示没找到</returns>
        public int KeyIndexOf(TKey key)
        {
            if (key == null) return -1;
            return _dicKey.IndexOf(key);
        }

        /// <summary>
        ///  移出某个值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            if (key == null || !_dicData.ContainsKey(key)) return false;
            _dicData.Remove(key);
            int index = KeyIndexOf(key);
            _dicKey.RemoveAt(index);
            _dicValue.RemoveAt(index);
            return true;
        }

        /// <summary>
        ///  移出所有的数据
        /// </summary>
        /// <returns></returns>
        public bool RemoveAll()
        {
            _dicData.Clear();
            return true;
        }
    }
}

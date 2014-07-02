using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Odis.Core.Games
{
    public interface IScriptManager
    {
        /// <summary>
        /// Get all game script
        /// </summary>
        ScriptCollection ScriptCollection { get; }
    }

    public class ScriptCollection : IDictionary<String, String>, INotifyCollectionChanged
    {
        /// <summary>
        /// All scripts
        /// </summary>
        private readonly Dictionary<String, String> scripts;

        /// <summary>
        /// constructor of the script collection
        /// </summary>
        public ScriptCollection()
        {
            scripts = new Dictionary<string, string>();
        }

        #region IDictionary<string,string> Members

        public void Add(string key, string value)
        {
            scripts.Add(key, value);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new List<KeyValuePair<String, String>>() { new KeyValuePair<string, string>(key, value) }));
        }

        public bool ContainsKey(string key)
        {
            return scripts.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return scripts.Keys; }
        }

        public bool Remove(string key)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new List<KeyValuePair<String, String>>() { new KeyValuePair<string, string>(key, scripts[key]) }));
            return scripts.Remove(key);
        }

        public bool TryGetValue(string key, out string value)
        {
            return scripts.TryGetValue(key, out value);
        }

        public ICollection<string> Values
        {
            get { return scripts.Values; }
        }

        public string this[string key]
        {
            get
            {
                return scripts[key];
            }
            set
            {
                scripts[key] = value;
            }
        }

        #endregion

        #region ICollection<KeyValuePair<string,string>> Members

        public void Add(KeyValuePair<string, string> item)
        {
            scripts.Add(item.Key, item.Value);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new List<KeyValuePair<String, String>>() { item }));
        }

        public void Clear()
        {
            scripts.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            return scripts.ToArray().Contains(item);
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            scripts.ToArray().CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return scripts.ToArray().Count(); }
        }

        public bool IsReadOnly
        {
            get { return scripts.ToArray().IsReadOnly; }
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new List<KeyValuePair<String, String>>() { new KeyValuePair<string, string>(item.Key, scripts[item.Key]) }));
            return scripts.ToList().Remove(item);
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,string>> Members

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return scripts.ToList().GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region INotifyCollectionChanged Members

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            NotifyCollectionChangedEventHandler handler = CollectionChanged;
            if (handler != null) handler(this, e);
        }

        #endregion
    }
}
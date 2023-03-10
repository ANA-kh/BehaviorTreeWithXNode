using System;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    [System.Serializable]
    public class Blackboard
    {
        [SerializeField]
        private int CurStage;
        
        //TODO 拆装箱优化      暂时的思路：考虑一个包含各种类型的类，并在其中实现各种类型的get和set方法
        private Dictionary<string,object> _parameters = new Dictionary<string, object>();
        private Dictionary<string,Action> _observers = new Dictionary<string, Action>();
        public void Init()
        {
            _parameters.Add("CurStage",CurStage);
        }

        public T Get<T>(string key)
        {
            if (_parameters.ContainsKey(key))
            {
                return (T)_parameters[key];
            }
            return default;
        }
        
        public bool TryGet<T>(string key,out T value)
        {
            if (_parameters.ContainsKey(key))
            {
                value = (T)_parameters[key];
                return true;
            }
            value = default;
            return false;
        }
        
        public void Set(string key,object value)
        {
            if (_parameters.ContainsKey(key))
            {
                _parameters[key] = value;
                NotifyObservers(key);
            }
            else
            {
                _parameters.Add(key,value);
                NotifyObservers(key);
            }
        }

        private void NotifyObservers(string key)
        {
            if (_observers.ContainsKey(key))
            { 
                _observers[key]?.Invoke();
            }
        }

        public void AddObserver(string s, Action onBlackboardChanged)
        {
            if (_observers.ContainsKey(s))
            {
                _observers[s] += onBlackboardChanged;
            }
            else
            {
                _observers.Add(s,onBlackboardChanged);
            }
        }

        public void RemoveObserver(string s, Action onBlackboardChanged)
        {
            if (_observers.ContainsKey(s))
            {
                _observers[s] -= onBlackboardChanged;
            }
        }
    }
}
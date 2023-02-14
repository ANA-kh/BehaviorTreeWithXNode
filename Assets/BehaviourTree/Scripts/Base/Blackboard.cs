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
        private Dictionary<string,List<Action>> _observers = new Dictionary<string, List<Action>>();
        public void Init()
        {
            _parameters.Add("CurStage",CurStage);
        }
        
        public object Get(string key)
        {
            if (_parameters.ContainsKey(key))
            {
                return _parameters[key];
            }
            return null;
        }
        
        public T Get<T>(string key)
        {
            if (_parameters.ContainsKey(key))
            {
                return (T)_parameters[key];
            }
            return default;
        }
        
        public void Set(string key,object value)
        {
            if (_parameters.ContainsKey(key))
            {
                _parameters[key] = value;
                NotifyObservers(key);
            }
            // else
            // {
            //     _parameters.Add(key,value);
            //     NotifyObservers(key);
            // }
        }

        private void NotifyObservers(string key)
        {
            if (_observers.ContainsKey(key))
            {
                foreach (var action in _observers[key])
                {
                    action();
                }
            }
        }

        public void AddObserver(string s, Action onBlackboardChanged)
        {
            if (_observers.ContainsKey(s))
            {
                _observers[s].Add(onBlackboardChanged);
            }
            else
            {
                _observers.Add(s,new List<Action>());
                _observers[s].Add(onBlackboardChanged);
            }
        }

        public void RemoveObserver(string s, Action onBlackboardChanged)
        {
            if (_observers.ContainsKey(s))
            {
                _observers[s].Remove(onBlackboardChanged);
            }
        }
    }
}
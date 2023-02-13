using System;
using UnityEngine;

namespace BT
{
    public class TestBuildTree :MonoBehaviour
    {
        public BehaviourTreeGraph graph;
        private void OnEnable()
        {
            var tree = graph.Build();
        }
    }
}
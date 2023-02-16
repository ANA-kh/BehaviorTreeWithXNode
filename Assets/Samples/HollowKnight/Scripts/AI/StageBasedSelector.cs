using System.Collections.Generic;
using System.Linq;
using BT;
using BT.Composites;
using UnityEngine;

namespace Core.BT
{
    public class StageBasedSelector : Selector
    {
        public List<string> IncludedTasksPerStage;
        
        [UnityEngine.Tooltip("Seed the random number generator to make things easier to debug")]
        public int seed = 0;
        [UnityEngine.Tooltip("Do we want to use the seed?")]
        public bool useSeed = false;

        // A list of indexes of every child task. This list is used by the Fischer-Yates shuffle algorithm.
        private List<int> childIndexList = new List<int>();
        
        
        private Stack<int> childrenExecutionOrder = new Stack<int>();
        
        private int _currentStage;
        private BehaviourNode _curChild;

        public override void Init(BehaviourTree tree)
        {
            base.Init(tree);
            
            if (useSeed) {
                Random.InitState(seed);
            }
        }

        protected override void OnStart()
        {
            base.OnStart();
            childIndexList.Clear();
            childIndexList = IncludedTasksPerStage[_currentStage].Split(',').Select(int.Parse).ToList();
            _currentStage = Blackboard.Get<int>("CurStage");
            
            ShuffleChildren();
            _curChild = children[childrenExecutionOrder.Pop()];
        }

        protected override State OnUpdate()
        {
            while (childrenExecutionOrder.Count > 0)
            {
                switch (_curChild.Update())
                {
                    case State.Running:
                        return State.Running;
                    case State.Success:
                        return State.Success;
                    case State.Failure:
                        _curChild = children[childrenExecutionOrder.Pop()];
                        continue;
                }
            }

            return State.Failure;
        }

        private void ShuffleChildren()
        {
            // Use Fischer-Yates shuffle to randomize the child index order.
            for (int i = childIndexList.Count; i > 0; --i) {
                int j = Random.Range(0, i);
                int index = childIndexList[j];
                if (index < children.Count)
                {
                    childrenExecutionOrder.Push(index);
                }
                else
                {
                    Debug.LogError("The index " + index + " is out of range of the children list. The children list only contains " + children.Count + " elements.");
                }
                childIndexList[j] = childIndexList[i - 1];
                childIndexList[i - 1] = index;
            }
        }
    }
}
using System;

namespace BT
{
    [System.Serializable]
    public class IntComparison : ConditionNode
    {
        public enum ComparisonType
        {
            Equal,
            NotEqual,
            Greater,
            Less,
            GreaterOrEqual,
            LessOrEqual
        }

        public string Key;
        public int Value;
        public ComparisonType Type;

        protected override bool Condition()
        {
            var value = Blackboard.Get<int>(Key);
            switch (Type)
            {
                case ComparisonType.Equal:
                    return value == Value;
                case ComparisonType.NotEqual:
                    return value != Value;
                case ComparisonType.Greater:
                    return value > Value;
                case ComparisonType.Less:
                    return value < Value;
                case ComparisonType.GreaterOrEqual:
                    return value >= Value;
                case ComparisonType.LessOrEqual:
                    return value <= Value;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal override void OnObserverBegin()
        {
            if (!IsObserving)
            {
                IsObserving = true;
                Blackboard.AddObserver(Key, Evaluate);
            }
        }

        internal override void OnObserverEnd()
        {
            if (IsObserving)
            {
                IsObserving = false;
                Blackboard.RemoveObserver(Key, Evaluate);   
            }
        }
    }
}
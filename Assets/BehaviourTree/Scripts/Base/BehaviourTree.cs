using System.Collections.Generic;

namespace BT
{
    public class BehaviourTree
    {
        private Node rootNode;
        public List<Node> nodes = new List<Node>();
        private int size;
        public Node.State treeState = Node.State.Running;
        
        
        public BehaviourTree(Node root)
        {
            rootNode = root;
            nodes.Add(rootNode);
        }

        public Node.State Update() {
            return rootNode.Update();
        }




        public BehaviourTree Clone() {
            //TODO
            return null;
        }

    }
}
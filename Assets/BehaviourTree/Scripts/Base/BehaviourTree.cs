using System.Collections.Generic;

namespace BehaviourTree
{
    public class BehaviourTree
    {
        private Node rootNode;
        public List<Node> nodes = new List<Node>();
        private int size;
        public Node.State treeState = Node.State.Running;
        
        
        public BehaviourTree() {
            rootNode = new RootNode();
            nodes.Add(rootNode);
        }

        public Node.State Update() {
            if (rootNode.state == Node.State.Running) {
                treeState = rootNode.Update();
            }
            return treeState;
        }




        public BehaviourTree Clone() {
            //TODO
            return null;
        }

    }
}
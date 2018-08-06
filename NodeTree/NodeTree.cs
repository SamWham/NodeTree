using System;
using System.Collections.Generic;
using System.Linq;


namespace NodeTree
{
    public class NodeTree<T>
    {
        // Key pair, Node ID and generic object;
        private KeyValuePair<Guid, T> keypair;

        // List of child nodes
        private List<NodeTree<T>> children;

        // RNG
        Random rand = new Random();

        // Constructor
        public NodeTree(Guid key, T nodeObject)
        {
            keypair = new KeyValuePair<Guid, T>(key, nodeObject);
            children = new List<NodeTree<T>>();
        }

        // Add child node. Return new child.
        public NodeTree<T> SpawnChild(Guid key, T nodeData)
        {
            var child = new NodeTree<T>(key, nodeData);
            this.children.Add(child);
            return child;
        }

        // Returns a random child
        public NodeTree<T> TraversToRandomChild()
        {
            int childCount = this.children.Count();
            int childPosition = rand.Next(childCount);
            return childCount == 0 ? null : this.children[childPosition];
        }

        // Returns ID of node
        public Guid GetNodeID()
        {
            return this.keypair.Key;
        }

        // Find gretest depth. Travers each path and return the max
        public int GreatestDepth(NodeTree<T> node)
        {
            if (node.children == null || !node.children.Any())
                return 1;

            return 1 + node.children.Max(n => GreatestDepth(n));
        }

        // Loop through each child and traverse, reapeat untill found. Null returned if not found
        public NodeTree<T> FindNode(NodeTree<T> node, Guid targetKey)
        {
            if (node.keypair.Key == targetKey)
                return node;

            foreach (NodeTree<T> child in node.children)
            {
                var result = FindNode(child, targetKey);
                if (result != null)
                    return result;
            }
            return null;
        }

        // Generate Random Tree of depth N
        public void GenrateTree(int depth, NodeTree<T> node, T nodeData)
        {
            if (depth <= 1)
                return;

            int randomChildCount = rand.Next(1, 5);

            for (int i = 0; i < randomChildCount; i++)
                node.SpawnChild(Guid.NewGuid(), nodeData);

            // ToList used to resolve "Collection was modified; enumeration operation may not execute."
            node.children.ToList().ForEach(n => GenrateTree(depth - 1, n, nodeData));
        }

        // Selects random node (at bottom of tree)
        public NodeTree<T> SelectRandomNode(NodeTree<T> node)
        {
            if (node.children.Count() == 0)
                return node;

            return node.SelectRandomNode(node.TraversToRandomChild());
        }

    }
}

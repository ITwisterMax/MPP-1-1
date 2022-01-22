using System.Collections.Generic;

namespace TracerLibrary.Helper
{
    public class TreeNode<T>
    {
        // Pointer to parent and children nodes
        public TreeNode<T> parent { get; set; }

        public ICollection<TreeNode<T>> children { get; set; }

        // Information about node
        public T data { get; set; }

        public TreeNode(T data)
        {
            this.data = data;
            this.children = new LinkedList<TreeNode<T>>();
        }

        // Create new child with current parent
        public TreeNode<T> AddChild(T child)
        {
            TreeNode<T> childNode = new TreeNode<T>(child) { parent = this };
            children.Add(childNode);

            return childNode;
        }
    }
}

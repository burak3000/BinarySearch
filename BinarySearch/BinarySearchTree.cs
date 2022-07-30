using System;
using System.Linq;

namespace BinarySearch
{
    public class BinarySearchTree
    {
        public CommandNode Root { get; set; }
        public int Count { get; set; }

        public bool Add(CommandNode node)
        {
            CommandNode currentNode = this.Root;
            CommandNode lastVisitedNode = null;
            try
            {
                while (currentNode != null)
                {
                    lastVisitedNode = currentNode;
                    if (node.Priority < currentNode.Priority) //Is new node in left tree? 
                        currentNode = currentNode.LeftNode;
                    else if (node.Priority > currentNode.Priority) //Is new node in right tree?
                        currentNode = currentNode.RightNode;
                    else
                    {
                        //Same priority exists, add new commands to this priorities list.
                        if (node.Commands != null && node.Commands.Count > 0)
                        {
                            currentNode.Commands.AddRange(node.Commands);
                            return true;
                        }
                        return false;
                    }
                }
                CommandNode newNode = new CommandNode();
                newNode.Priority = node.Priority;
                newNode.Commands = node.Commands;

                if (this.Root == null)//Tree ise empty
                    this.Root = newNode;
                else
                {
                    if (node.Priority < lastVisitedNode.Priority)
                        lastVisitedNode.LeftNode = newNode;
                    else
                        lastVisitedNode.RightNode = newNode;
                }
                Count++;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public CommandNode Find(int priority)
        {
            return this.Find(priority, this.Root);
        }

        public void Remove(CommandNode node)
        {
            this.Root = Remove(this.Root, node);
            Count--;
        }
        private CommandNode Remove(CommandNode parent, CommandNode node)
        {
            if (parent == null)
            {
                return parent;
            }

            if (node.Priority < parent.Priority)
            {
                parent.LeftNode = Remove(parent.LeftNode, node);
            }
            else if (node.Priority > parent.Priority)
            {
                parent.RightNode = Remove(parent.RightNode, node);
            }

            // if value is same as parent's value, then this is the node to be deleted  
            else
            {
                // node with only one child or no child  
                if (parent.LeftNode == null)
                {
                    return parent.RightNode;
                }
                else if (parent.RightNode == null)
                {
                    return parent.LeftNode;
                }

                // node with two children: Get the inorder successor (smallest in the right subtree)  
                parent.Priority = MinValue(parent.RightNode);

                // Delete the inorder successor  
                parent.RightNode = Remove(parent.RightNode, parent);
            }

            return parent;
        }
        private int MinValue(CommandNode node)
        {
            int minv = node.Priority;

            while (node.LeftNode != null)
            {
                minv = node.LeftNode.Priority;
                node = node.LeftNode;
            }

            return minv;
        }
        private CommandNode Find(int value, CommandNode parent)
        {
            if (parent != null)
            {
                if (value == parent.Priority) return parent;
                if (value < parent.Priority)
                    return Find(value, parent.LeftNode);
                else
                    return Find(value, parent.RightNode);
            }

            return null;
        }

        public CommandNode FindNextPrioritizedCommand(CommandNode parent)
        {
            if (parent != null)
            {
                if (parent.RightNode == null)
                {
                    return parent;
                }
                parent = FindNextPrioritizedCommand(parent.RightNode);
            }
            return parent;
        }
    }
}

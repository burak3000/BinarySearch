using System;

namespace BinarySearch
{
    public class BinarySearchTree
    {
        private static object s_Lock = new object();
        public CommandNode Root { get; set; }
        private int m_Count = 0;
        public int Count => m_Count;
        public CommandNode CommandWithMaxPrio { get; }
        /// <summary>
        /// Adds new entry to the tree. If a node with same priority exists in the tree, its commands will be appended to the existing ones command list.
        /// Because of this, when we use find method on this entry, already added object will be returned.
        /// For example we add CommandNode with prio 1. Then we add new CommandNode with prio 1. Commands of latter one will be appended to the first one.
        /// When we call find CommandNode with prio 1, only the address of the first one is valid.
        /// </summary>
        /// <param name="node"></param>
        /// <returns>true if added, otherwise false</returns>
        public bool Add(CommandNode node)
        {
            lock (s_Lock)
            {
                if (node == null)
                {
                    return false;
                }

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

                    if (Root == null)//Tree ise empty
                    {
                        Root = node;
                    }
                    else
                    {
                        if (node.Priority < lastVisitedNode.Priority)
                        {
                            lastVisitedNode.LeftNode = node;
                        }
                        else
                        {
                            lastVisitedNode.RightNode = node;
                        }
                    }
                    m_Count++;
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public CommandNode Find(int priority)
        {
            lock (s_Lock)
            {
                return Find(priority, this.Root);
            }
        }

        public CommandNode Find(CommandNode nodeToFind)
        {
            return Find(nodeToFind, this.Root);
        }

        public bool Remove(CommandNode node)
        {
            try
            {
                lock (s_Lock)
                {
                    Root = Remove(this.Root, node);
                    if (m_Count > 0)
                    {
                        m_Count--;
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        private CommandNode Remove(CommandNode currentNode, CommandNode node)
        {
            if (currentNode == null || node == null)
            {
                return currentNode;
            }

            if (node.Priority < currentNode.Priority)
            {
                //There is no less entry. We must stop searching
                if (currentNode.LeftNode == null)
                {
                    throw new ArgumentException("Node to be removed could not be found.");
                }
                currentNode.LeftNode = Remove(currentNode.LeftNode, node);
            }
            else if (node.Priority > currentNode.Priority)
            {
                //There is no bigger entry. We must stop searching
                if (currentNode.RightNode == null)
                {
                    throw new ArgumentException("Node to be removed could not be found.");
                }
                currentNode.RightNode = Remove(currentNode.RightNode, node);
            }

            // if value is same as currentNode's value, then this is the node to be deleted  
            else
            {
                // node with only one child or no child  
                if (currentNode.LeftNode == null)
                {
                    return currentNode.RightNode;
                }
                else if (currentNode.RightNode == null)
                {
                    return currentNode.LeftNode;
                }

                // node with two children: Get the inorder successor (smallest in the right subtree)  
                currentNode.Priority = MinValue(currentNode.RightNode);

                // Delete the inorder successor  
                currentNode.RightNode = Remove(currentNode.RightNode, currentNode);
            }

            return currentNode;
        }
        private int MinValue(CommandNode node)
        {
            lock (s_Lock)
            {
                int minv = node.Priority;

                while (node.LeftNode != null)
                {
                    minv = node.LeftNode.Priority;
                    node = node.LeftNode;
                }

                return minv;
            }

        }
        private CommandNode Find(int priority, CommandNode currentNode)
        {
            lock (s_Lock)
            {
                if (currentNode != null)
                {
                    if (priority == currentNode.Priority)
                    {
                        return currentNode;
                    }

                    if (priority < currentNode.Priority)
                    {
                        if (currentNode.LeftNode == null)
                        {
                            return null;
                        }
                        return Find(priority, currentNode.LeftNode);
                    }
                    else
                    {
                        if (currentNode.RightNode == null)
                        {
                            return null;
                        }
                        return Find(priority, currentNode.RightNode);
                    }
                }

                return null;
            }
        }

        private CommandNode Find(CommandNode nodeToFind, CommandNode currentNode)
        {
            lock (s_Lock)
            {
                if (currentNode != null)
                {
                    if (nodeToFind == currentNode)
                    {
                        return currentNode;
                    }

                    if (nodeToFind.Priority < currentNode.Priority)
                    {
                        if (currentNode.LeftNode == null)
                        {
                            return null;
                        }
                        return Find(nodeToFind, currentNode.LeftNode);
                    }
                    else if (nodeToFind.Priority > currentNode.Priority)
                    {
                        if (currentNode.RightNode == null)
                        {
                            return null;
                        }
                        return Find(nodeToFind, currentNode.RightNode);
                    }
                }

                return null;
            }
        }

        public CommandNode FindNextPrioritizedCommand()
        {
            lock (s_Lock)
            {
                return FindNextPrioritizedCommand(Root);
            }
        }

        private CommandNode FindNextPrioritizedCommand(CommandNode currentNode)
        {
            if (currentNode != null)
            {
                if (currentNode.RightNode == null)
                {
                    return currentNode;
                }
                currentNode = FindNextPrioritizedCommand(currentNode.RightNode);
            }

            return currentNode;
        }
    }
}

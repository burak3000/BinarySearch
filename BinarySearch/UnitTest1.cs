using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BinarySearch
{
    public class UnitTest1
    {
        BinarySearchTree m_Tree;
        #region SetUp

        [SetUp]
        public void InitializeTree()
        {
            m_Tree = new BinarySearchTree();
        }
        #endregion

        #region Add Tests

        [Test]
        public void Add_NullEntry_ReturnsFalse()
        {
            Assert.False(m_Tree.Add(null));
        }

        [Test]
        public void Add_NewEntry_ElementCountIs1()
        {
            Assert.True(m_Tree.Add(new CommandNode { Priority = 3 }));
            Assert.AreEqual(1, m_Tree.Count);
        }

        [Test]
        public void Add_2EntriesWithSamePriority_ElementCountIs1()
        {
            m_Tree.Add(new CommandNode { Priority = 3 });
            m_Tree.Add(new CommandNode { Priority = 3 });
            Assert.AreEqual(1, m_Tree.Count);
        }

        [Test]
        public void Add_3EntriesWithDifferentPriorities_ElementCountIs3()
        {
            m_Tree.Add(new CommandNode { Priority = 1 });
            m_Tree.Add(new CommandNode { Priority = 2 });
            m_Tree.Add(new CommandNode { Priority = 3 });
            Assert.AreEqual(3, m_Tree.Count);
        }

        [Test]
        public void Add_Add_2EntriesWithSamePriority_ElementCountIs1AndCommandListWillBeAppended()
        {
            int node1CommandCount = 3;
            var node1 = new CommandNode { Priority = 1 };
            for (int i = 0; i < node1CommandCount; i++)
            {
                node1.Commands.Add(new CommandImpl());
            }

            int node2CommandCount = 5;
            var node2 = new CommandNode { Priority = 1 };
            for (int i = 0; i < node2CommandCount; i++)
            {
                node2.Commands.Add(new CommandImpl());
            }

            m_Tree.Add(node1);
            m_Tree.Add(node2);
            Assert.AreEqual(8, m_Tree.Find(1).Commands.Count);
        }

        #endregion

        [Test]
        public void Find_WithPriority_ExistingPriority_ReturnsCommandNode()
        {
            int priority = 1;
            var node = new CommandNode { Priority = priority };
            m_Tree.Add(node);
            var foundItem = m_Tree.Find(priority);
            Assert.AreEqual(node, foundItem);
        }

        [Test]
        public void Find_WithCommandNodeObject_ExistingPriority_ReturnsCommandNode()
        {
            int priority = 1;
            var node = new CommandNode { Priority = priority };
            m_Tree.Add(node);
            var foundItem = m_Tree.Find(node);
            Assert.AreEqual(node, foundItem);
        }

        [Test]
        public void Find_WithCommandNodeObject_NonExistingSearhItem_ReturnsNull()
        {
            int priority = 1;
            var node = new CommandNode { Priority = priority };

            var foundItem = m_Tree.Find(node);
            Assert.Null(foundItem);
        }

        [Test]
        public void Find_WithPriority_NonExistingSearhItem_ReturnsNull()
        {
            int priority = 1;
            var node = new CommandNode { Priority = priority };

            var foundItem = m_Tree.Find(++priority);
            Assert.Null(foundItem);
        }

        [Test]
        public void Find_NonExistingSearhItem_ReturnsNull()
        {
            int priority = 1;
            var node = new CommandNode { Priority = priority };

            var foundItem = m_Tree.Find(node);
            Assert.Null(foundItem);
        }

        [Test]
        public void Find_TwoNewlyAddedSamePrioCommandNodes_ReturnsFirstAddedCommandNode()
        {
            int priority = 1;
            var node = new CommandNode { Priority = priority };
            var node2 = new CommandNode { Priority = priority };
            m_Tree.Add(node);
            m_Tree.Add(node2);
            var foundItem = m_Tree.Find(priority);
            Assert.AreEqual(node, foundItem);
            /*If CommandNodes with same priority is added,
            command list of latter node will be appended to
            the command list of previously added one.*/
            Assert.AreNotEqual(node2, foundItem);
        }

        [Test]
        public void Remove_WithCommandNode_NonExistingCommandNode_ReturnsFalse()
        {
            int elementCount = 10;
            CommandNode node = new CommandNode { Priority = elementCount / 2 };
            //Add one element to the root
            m_Tree.Add(node);
            for (int i = 0; i < elementCount / 2; i++)
            {
                //Add two elements for each iteration to the right and left side of the root.
                CommandNode node2 = new CommandNode { Priority = i };
                CommandNode node3 = new CommandNode { Priority = elementCount - i };
                m_Tree.Add(node2);
                m_Tree.Add(node3);
            }
            m_Tree.Remove(node);
            m_Tree.Remove(m_Tree.Find(3));

            //Root element is added and then for loop two elements in each iteration. We removed 2 of them. 
            Assert.AreEqual(9, m_Tree.Count);
        }


        [Test]
        public void Remove_NonExistingElement_CannotRemoveAnything()
        {
            int elementCount = 10;
            CommandNode node = new CommandNode { Priority = elementCount / 2 };
            //Add one element to the root
            m_Tree.Add(node);
            for (int i = 0; i < elementCount / 2; i++)
            {
                //Add two elements for each iteration to the right and left side of the root.
                CommandNode node2 = new CommandNode { Priority = i };
                CommandNode node3 = new CommandNode { Priority = elementCount - i };
                m_Tree.Add(node2);
                m_Tree.Add(node3);
            }
            m_Tree.Remove(node);
            m_Tree.Remove(m_Tree.Find(3));

            //Root element is added and then for loop two elements in each iteration. We removed 2 of them. 
            Assert.AreEqual(9, m_Tree.Count);

            CommandNode nonExistingNode = new CommandNode { Priority = ++elementCount };
            m_Tree.Remove(nonExistingNode);
            Assert.AreEqual(9, m_Tree.Count);
        }

        [Test]
        public void FindNextPrioritizedCommand()
        {
            for (int i = 0; i < 10; i++)
            {
                m_Tree.Add(new CommandNode { Priority = i });
                Assert.AreEqual(i, m_Tree.FindNextTopPriorityNode().Priority);
            }
            for (int i = 9; i >= 1; i--)
            {
                m_Tree.Remove(m_Tree.Find(i));
                Assert.AreEqual(i - 1, m_Tree.FindNextTopPriorityNode().Priority);
            }
        }
    }
}

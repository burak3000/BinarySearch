using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BinarySearch
{
    public class UnitTest1
    {
        BinarySearchTree m_Tree;
        [SetUp]
        public void InitializeTree()
        {
            m_Tree = new BinarySearchTree();
        }

        [Test]
        public void Add_NullEntry_ReturnsFalse()
        {
            Assert.False(m_Tree.Add(null));
        }

        [Test]
        public void Add_NewEntry_ElementCountIs1()
        {
            m_Tree = new BinarySearchTree();
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
        public void Find_ExistingPriority_ReturnsCommandNode()
        {
            int priority = 1;
            var node = new CommandNode { Priority = priority };
            m_Tree.Add(node);
            Assert.AreEqual(node, m_Tree.Find(priority));

        }


    }
}

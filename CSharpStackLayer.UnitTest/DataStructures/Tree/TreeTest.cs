using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpStackLayer.UnitTest.DataStructures
{
    [TestClass]
    public class TreeTest
    {
        [TestMethod]
        public void Root()
        {
            var tree = new Tree<int>();
            tree.Root.IsNotNull();
        }

        [TestMethod]
        public void CountNodes()
        {
            var tree = new Tree<int>();

            tree.CountNodes.Is( 1 );

            var nodeA = new TreeNode<int>();
            tree.Root.AddChild( nodeA );

            tree.CountNodes.Is( 2 );

            var nodeB = new TreeNode<int>();
            tree.Root.AddChild( nodeB );

            var nodeAa = new TreeNode<int>();
            nodeA.AddChild( nodeAa );

            tree.CountNodes.Is( 4 );
        }

        [TestMethod]
        public void Enumerate()
        {
            var tree = new Tree<int>();

            var n1 = new TreeNode<int>();
            var n2 = new TreeNode<int>();
            var n3 = new TreeNode<int>();
            var n4 = new TreeNode<int>();
            var n5 = new TreeNode<int>();
            var n6 = new TreeNode<int>();
            var n7 = new TreeNode<int>();

            tree.Root.AddChild( n1 );
            tree.Root.AddChild( n2 );
            tree.Root.AddChild( n3 );
            n1.AddChild( n4 );
            n2.AddChild( n5 );
            n4.AddChild( n6 );
            n6.AddChild( n7 );

            var items = new List<TreeNode<int>>();
            foreach (var item in tree.Enumerate())
                items.Add( item );

            tree.CountNodes.Is( items.Count );
        }
    }
}

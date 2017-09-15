using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpStackLayer.UnitTest.DataStructures
{
    [TestClass]
    public class TreeIteratorTest
    {
        [TestMethod]
        public void Iterator()
        {
            var tree = MakeSampleTree();

            var items = new List<int>();

            for (var itr = tree.Iterator(); itr != null; ++itr)
                items.Add( itr.Get() );

            items.Count.Is( 20 );

            items.Clear();

            var llf = tree.Root.FindLastLeaf();
            llf.Item.Is( 80 );

            for (var itr = tree.Iterator( llf ); itr != null; --itr)
                items.Add( itr.Get() );

            items.Count.Is( 20 );
        }

        Tree<int> MakeSampleTree()
        {
            var tree = new Tree<int>();

            TreeNode<int> n1, n2, n3, n4,
                          n1a, n1b, n1c, n2a, n2b, n3a,
                          n1bA, n1bB, n1bC, n1cA,
                          n1bA1, n1bA2, n1bC1,
                          n1bA2a, n1bA2b;

            // (root)[0]
            //   |- (n1)[5]
            //   |   |- (n1a)[10]
            //   |   |- (n1b)[15]
            //   |   |    |- (n1bA)[20]
            //   |   |    |    |- (n1bA1)[25]
            //   |   |    |    |- (n1bA2)[27]
            //   |   |    |          |- (n1bA2a)[28]
            //   |   |    |          |- (n1bA2b)[29]
            //   |   |    |- (n1bB)[30]
            //   |   |    |- (n1bC)[35]
            //   |   |         |- (n1bC1)[40]
            //   |   |- (n1c)[42]
            //   |        |- (n1cA)[45]
            //   |- (n2)[47]
            //   |   |- (n2a)[50]
            //   |   |- (n2b)[60]
            //   |- (n3)[65]
            //   |   |- (n3a)[70]
            //   |- (n4)[80]

            tree.Root.AddChildren( new[] { n1 = V(5), n2 = V(47), n3 = V(65), n4 = V(80) } );
            n1.AddChildren( new[] { n1a = V(10), n1b = V(15), n1c = V(42) } );
            n1b.AddChildren( new[] { n1bA = V(20), n1bB = V(30), n1bC = V(35) } );
            n1bA.AddChildren( new[] { n1bA1 = V(25), n1bA2 = V(27) } );
            n1bA2.AddChildren( new[] { n1bA2a = V(28), n1bA2b = V(29) } );
            n1bC.AddChild( n1bC1 = V(40) );
            n1c.AddChild( n1cA = V(45) );
            n2.AddChildren( new[] { n2a = V(50), n2b = V(60) } );
            n3.AddChild( n3a = V(70) );

            return tree;
        }

        Func<TreeNode<int>> N = () => new TreeNode<int>();
        Func<int, TreeNode<int>> V = val => new TreeNode<int>( val );
    }
}

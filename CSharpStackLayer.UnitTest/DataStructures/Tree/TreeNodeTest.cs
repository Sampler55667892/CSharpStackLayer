using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpStackLayer.UnitTest.DataStructures
{
    [TestClass]
    public class TreeNodeTest
    {
        [TestMethod]
        public void Depth()
        {
            var tree = new Tree<int>();
            tree.Root.Depth.Is( 0 );

            var n1 = new TreeNode<int>();
            tree.Root.AddChild( n1 );
            n1.Depth.Is( 1 );

            var n2 = new TreeNode<int>();
            n1.AddChild( n2 );
            n2.Depth.Is( 2 );
        }

        [TestMethod]
        public void FirstChild()
        {
            var tree = new Tree<int>();

            var n1 = new TreeNode<int>();
            var n2 = new TreeNode<int>();

            tree.Root.AddChild( n1 );
            tree.Root.FirstChild.Is( n1 );

            tree.Root.AddChild( n2 );
            tree.Root.FirstChild.Is( n1 );
        }

        [TestMethod]
        public void HasChild()
        {
            var tree = new Tree<int>();

            tree.Root.HasChild.IsFalse();
            tree.Root.AddChild( 0 );
            tree.Root.HasChild.IsTrue();
        }

        [TestMethod]
        public void HasSibling()
        {
            var tree = new Tree<int>();

            tree.Root.AddChild( 0 );

            var child = tree.Root.FirstChild;
            child.HasSibling.IsFalse();

            tree.Root.AddChild( 1 );
            child.HasSibling.IsTrue();

            tree.Root.AddChild( 2 );
            child.HasSibling.IsTrue();
        }

        [TestMethod]
        public void IsLeaf()
        {
            var tree = new Tree<int>();

            var n1 = new TreeNode<int>();
            var n2 = new TreeNode<int>();

            tree.Root.IsLeaf.IsTrue();

            tree.Root.AddChild( n1 );
            tree.Root.IsLeaf.IsFalse();
            n1.IsLeaf.IsTrue();

            n1.AddChild( n2 );
            n1.IsLeaf.IsFalse();
            n2.IsLeaf.IsTrue();
        }

        [TestMethod]
        public void Item()
        {
            var n = new TreeNode<int>();
            var val = 10;

            n.Item = val;
            n.Item.Is( val );
        }

        [TestMethod]
        public void LastChild()
        {
            var tree = new Tree<int>();

            var n1 = new TreeNode<int>();
            var n2 = new TreeNode<int>();
            var n3 = new TreeNode<int>();

            tree.Root.LastChild.IsNull();

            tree.Root.AddChild( n1 );
            tree.Root.LastChild.Is( n1 );

            tree.Root.AddChild( n3 );
            tree.Root.LastChild.Is( n3 );

            tree.Root.AddChild( n2 );
            tree.Root.LastChild.Is( n2 );
        }

        [TestMethod]
        public void NextSibling()
        {
            var tree = new Tree<int>();

            var n1 = new TreeNode<int>();
            var n2 = new TreeNode<int>();
            var n3 = new TreeNode<int>();

            tree.Root.NextSibling.IsNull();

            tree.Root.AddChild( n1 );
            n1.NextSibling.IsNull();

            tree.Root.AddChild( n2 );
            n1.NextSibling.Is( n2 );
            n2.NextSibling.IsNull();

            tree.Root.AddChild( n3 );
            n1.NextSibling.Is( n2 );
            n2.NextSibling.Is( n3 );
            n3.NextSibling.IsNull();
        }

        [TestMethod]
        public void Parent()
        {
            var tree = new Tree<int>();

            var n1 = new TreeNode<int>();
            var n2 = new TreeNode<int>();
            var n3 = new TreeNode<int>();

            tree.Root.Parent.IsNull();

            tree.Root.AddChild( n1 );
            n1.Parent.Is( tree.Root );

            tree.Root.AddChild( n2 );
            n2.Parent.Is( tree.Root );

            n1.AddChild( n3 );
            n3.Parent.Is( n1 );
            n3.Parent.Parent.Is( tree.Root );
        }

        [TestMethod]
        public void PrevSibling()
        {
            var tree = new Tree<int>();

            var n1 = new TreeNode<int>();
            var n2 = new TreeNode<int>();
            var n3 = new TreeNode<int>();

            tree.Root.PrevSibling.IsNull();

            tree.Root.AddChild( n1 );
            n1.PrevSibling.IsNull();

            tree.Root.AddChild( n2 );
            n2.PrevSibling.Is( n1 );

            tree.Root.AddChild( n3 );
            n3.PrevSibling.Is( n2 );
            n2.PrevSibling.Is( n1 );
            n1.PrevSibling.IsNull();
        }

        [TestMethod]
        public void Root()
        {
            var tree = new Tree<int>();

            var n1 = new TreeNode<int>();
            var n2 = new TreeNode<int>();

            tree.Root.IsNotNull();

            tree.Root.AddChild( n1 );
            n1.Root.Is( tree.Root );

            tree.Root.AddChild( n2 );
            n1.Root.Is( tree.Root );
            n2.Root.Is( tree.Root );
        }

        [TestMethod]
        public void AddChild_Item()
        {
            var tree = new Tree<int>();
            var item = 10;

            tree.Root.HasChild.IsFalse();
            tree.Root.AddChild( item );
            tree.Root.HasChild.IsTrue();

            tree.Root.FirstChild.Parent.Is( tree.Root );
            tree.Root.FirstChild.Item.Is( item );
        }

        [TestMethod]
        public void AddChild_Node()
        {
            var tree = new Tree<int>();
            var n1 = new TreeNode<int>( 10 );

            tree.Root.HasChild.IsFalse();
            tree.Root.AddChild( n1 );
            tree.Root.HasChild.IsTrue();

            tree.Root.FirstChild.Is( n1 );
            n1.Parent.Is( tree.Root );
        }

        [TestMethod]
        public void AddChildren_Items()
        {
            var tree = new Tree<int>();
            var item1 = 10;
            var item2 = 20;

            tree.Root.HasChild.IsFalse();
            tree.Root.AddChildren( new[] { item1, item2 } );
            tree.Root.HasChild.IsTrue();
            tree.Root.CountNodes.Is( 3 );

            tree.Root.FirstChild.Parent.Is( tree.Root );
            tree.Root.FirstChild.IsNot( tree.Root.LastChild );
            tree.Root.LastChild.Parent.Is( tree.Root );
            tree.Root.FirstChild.Item.Is( item1 );
            tree.Root.LastChild.Item.Is( item2 );
        }

        [TestMethod]
        public void AddChildren_Nodes()
        {
            var tree = new Tree<int>();
            var n1 = new TreeNode<int>( 10 );
            var n2 = new TreeNode<int>( 20 );

            tree.Root.HasChild.IsFalse();
            tree.Root.AddChildren( new[] { n1, n2 } );
            tree.Root.HasChild.IsTrue();

            tree.Root.FirstChild.Is( n1 );
            tree.Root.LastChild.Is( n2 );
            n1.Parent.Is( tree.Root );
            n2.Parent.Is( tree.Root );
        }

        [TestMethod]
        public void AddSubtree()
        {
            var tree = new Tree<int>();
            var subTree = new Tree<int>();
            var item1 = 10;
            var item2 = 20;

            // r1 - 10,
            // r2 - 20,
            // ↓
            // r1 - 10
            //    - r2 - 20

            tree.Root.AddChild( item1 );
            subTree.Root.AddChild( item2 );

            tree.Root.AddSubtree( subTree );

            var n1 = tree.Root.FirstChild;
            n1.Item.Is( item1 );
            n1.HasChild.IsFalse();

            var n2 = tree.Root.LastChild;
            n2.HasChild.IsTrue();
            var n3 = n2.FirstChild;
            n3.Item.Is( item2 );

            n3.Parent.Is( n2 );
            n2.Parent.Is( tree.Root );
        }

        [TestMethod]
        public void AddSubtree_Exception()
        {
            var tree = new Tree<int>();
            var item1 = 10;
            tree.Root.AddChild( item1 );

            AssertEx.Throws<Exception>( () => {
                tree.Root.AddSubtree( tree );
            });
        }

        [TestMethod]
        public void CutToSubtree()
        {
            // r - 10 - 20
            //  > r - 10
            //  > 20

            var tree = new Tree<int>();
            var item1 = 10;
            var item2 = 20;
            tree.Root.AddChild( item1 );
            var n1 = tree.Root.FirstChild;
            n1.AddChild( item2 );
            var n2 = n1.FirstChild;

            var subtree = n2.CutToSubtree();

            tree.Root.FirstChild.Item.Is( item1 );
            tree.Root.FirstChild.HasChild.IsFalse();

            subtree.Root.Item.Is( item2 );
            subtree.Root.HasChild.IsFalse();
        }

        [TestMethod]
        public void CutToSubtree_Exception()
        {
            // r - 10 - 20

            var tree = new Tree<int>();
            tree.Root.AddChild( 10 );
            tree.Root.FirstChild.AddChild( 20 );

            AssertEx.Throws<Exception>( () => {
                tree.Root.CutToSubtree();
            });
        }

        [TestMethod]
        public void Enumerate()
        {
            // r - 10 - 30
            //   - 20

            var tree = new Tree<int>();
            var items = new[] { 0, 10, 20, 30 };

            tree.Root.Item = items[ 0 ];
            tree.Root.AddChildren( new[] { items[ 1 ], items[ 2 ] } );
            tree.Root.FirstChild.AddChild( items[ 3 ] );

            var nodes = tree.Root.Enumerate();
            nodes.IsNotNull();
            nodes.Count().Is( 4 );
            var nodesList = nodes.ToList();
            // depth first
            nodesList[ 0 ].Item.Is( items[ 0 ] );
            nodesList[ 1 ].Item.Is( items[ 1 ] );
            nodesList[ 2 ].Item.Is( items[ 3 ] );
            nodesList[ 3 ].Item.Is( items[ 2 ] );
        }

        [TestMethod]
        public void FindAll()
        {
            // r - 10 - 15
            //   - 20 - 25

            var tree = new Tree<int>();
            var items = new[] { 0, 10, 15, 20, 25 };

            tree.Root.Item = items[ 0 ];
            tree.Root.AddChildren( new[] { items[ 1 ], items[ 3 ] } );
            tree.Root.FirstChild.AddChild( items[ 2 ] );
            tree.Root.LastChild.AddChild( items[ 4 ] );

            var nodes = tree.Root.FindAll( x => x.Item <= 15 );
            nodes.Count().Is( 3 );
            nodes.Any( x => x.Item == items[ 0 ] ).IsTrue();
            nodes.Any( x => x.Item == items[ 1 ] ).IsTrue();
            nodes.Any( x => x.Item == items[ 2 ] ).IsTrue();
        }

        [TestMethod]
        public void FindFirst()
        {
            // r - 10 - 15
            //   - 20 - 25

            var tree = new Tree<int>();
            var items = new[] { 0, 10, 15, 20, 25 };

            tree.Root.Item = items[ 0 ];
            tree.Root.AddChildren( new[] { items[ 1 ], items[ 3 ] } );
            tree.Root.FirstChild.AddChild( items[ 2 ] );
            tree.Root.LastChild.AddChild( items[ 4 ] );

            var foundNode = tree.Root.FindFirst( x => x.Item > 15 );
            foundNode.Item.Is( items[ 3 ] );
        }

        [TestMethod]
        public void FindFirstLeaf()
        {
            // r - 10 - 15
            //   - 20 - 25

            var tree = new Tree<int>();
            var items = new[] { 0, 10, 15, 20, 25 };

            tree.Root.Item = items[ 0 ];
            tree.Root.AddChildren( new[] { items[ 1 ], items[ 3 ] } );
            tree.Root.FirstChild.AddChild( items[ 2 ] );
            tree.Root.LastChild.AddChild( items[ 4 ] );

            var foundLeaf = tree.Root.FindFirstLeaf();
            foundLeaf.Item.Is( items[ 2 ] );
        }

        [TestMethod]
        public void FindLast()
        {
            // r - 10 - 15
            //   - 20 - 25

            var tree = new Tree<int>();
            var items = new[] { 0, 10, 15, 20, 25 };

            tree.Root.Item = items[ 0 ];
            tree.Root.AddChildren( new[] { items[ 1 ], items[ 3 ] } );
            tree.Root.FirstChild.AddChild( items[ 2 ] );
            tree.Root.LastChild.AddChild( items[ 4 ] );

            var foundNode = tree.Root.FindLast( x => x.Item >= 15 );
            foundNode.Item.Is( items[ 4 ] );
        }

        [TestMethod]
        public void FindLastLeaf()
        {
            // r - 10 - 15
            //   - 20 - 25
            //   - 30

            var tree = new Tree<int>();
            var items = new[] { 0, 10, 15, 20, 25, 30 };

            tree.Root.Item = items[ 0 ];
            tree.Root.AddChildren( new[] { items[ 1 ], items[ 3 ], items[ 5 ] } );
            tree.Root.FirstChild.AddChild( items[ 2 ] );
            tree.Root.FindFirst( x => x.Item == items[ 3 ] ).AddChild( items[ 4 ] );

            var foundLeaf = tree.Root.FindLastLeaf();
            foundLeaf.Item.Is( items[ 5 ] );
        }

        [TestMethod]
        public void InsertChild_Item()
        {
            // r - 10
            //   - 20
            //   - 30
            // ↓
            // r - 10
            //   - 15
            //   - 20
            //   - 30

            var tree = new Tree<int>();
            var items = new[] { 10, 20, 30 };
            var item4 = 15;

            tree.Root.AddChildren( items );
            tree.Root.InsertChild( 1, item4 );

            var n1 = tree.Root.FirstChild;
            n1.Item.Is( items[ 0 ] );
            var n2 = n1.NextSibling;
            n2.Item.Is( item4 );
            var n3 = n2.NextSibling;
            n3.Item.Is( items[ 1 ] );
            var n4 = n3.NextSibling;
            n4.Item.Is( items[ 2 ] );
        }

        [TestMethod]
        public void InsertChild_Node()
        {
            // r - 10
            //   - 20
            //   - 30
            // ↓
            // r - 10
            //   - 15
            //   - 20
            //   - 30

            var tree = new Tree<int>();
            var items = new[] { 10, 20, 30 };
            var item4 = 40;
            var node4 = new TreeNode<int>( item4 );

            tree.Root.AddChildren( items );
            tree.Root.InsertChild( 1, node4 );

            var n1 = tree.Root.FirstChild;
            n1.Item.Is( items[ 0 ] );
            var n2 = n1.NextSibling;
            n2.Item.Is( item4 );
            var n3 = n2.NextSibling;
            n3.Item.Is( items[ 1 ] );
            var n4 = n3.NextSibling;
            n4.Item.Is( items[ 2 ] );
            var n5 = n4.NextSibling;
            n5.IsNull();
        }

        [TestMethod]
        public void InsertChildren()
        {
            // r - 10
            //   - 20
            //   - 30
            // ↓
            // r - 5
            //   - 8
            //   - 10
            //   - 20
            //   - 30

            var tree = new Tree<int>();
            var items = new[] { 10, 20, 30 };
            var items2 = new[] { 5, 8 };

            tree.Root.AddChildren( items );
            tree.Root.InsertChildren( 0, items2 );

            var n1 = tree.Root.FirstChild;
            n1.Item.Is( items2[ 0 ] );
            var n2 = n1.NextSibling;
            n2.Item.Is( items2[ 1 ] );
            var n3 = n2.NextSibling;
            n3.Item.Is( items[ 0 ] );
            var n4 = n3.NextSibling;
            n4.Item.Is( items[ 1 ] );
            var n5 = n4.NextSibling;
            n5.Item.Is( items[ 2 ] );
            var n6 = n5.NextSibling;
            n6.IsNull();
        }

        [TestMethod]
        public void InsertSubtree()
        {
            // r1 - 10,
            //    - 20
            // r2 - 30,
            // ↓
            // r1 - 10
            //    - r2 - 30
            //    - 20

            var tree1 = new Tree<int>();
            var tree2 = new Tree<int>();
            var items0 = new[] { 1, 2 };
            var item1 = 10;
            var item2 = 20;
            var item3 = 30;

            tree1.Root.Item = items0[ 0 ];
            tree2.Root.Item = items0[ 1 ];
            tree1.Root.AddChildren( new[] { item1, item2 } );
            tree2.Root.AddChild( item3 );

            tree1.Root.InsertSubtree( 1, tree2 );
            tree1.Root.Item.Is( items0[ 0 ] );
            var n1 = tree1.Root.FirstChild;
            n1.Item.Is( item1 );
            n1.HasChild.IsFalse();
            var n2 = n1.NextSibling;
            n2.Item.Is( items0[ 1 ] );
            n2.FirstChild.Item.Is( item3 );
            n2.CountNodes.Is( 2 );  // 自分を含む
            n2.FirstChild.HasChild.IsFalse();
            var n3 = n2.NextSibling;
            n3.Item.Is( item2 );
            n3.HasChild.IsFalse();
            var n4 = n3.NextSibling;
            n4.IsNull();
        }

        [TestMethod]
        public void InsertSubtree_Exception()
        {
            // r1 - 10

            var tree = new Tree<int>();
            tree.Root.AddChild( 10 );

            AssertEx.Throws<Exception>( () => {
                tree.Root.InsertSubtree( 0, tree );
            });
        }

        [TestMethod]
        public void RemoveSubtree_NotRoot()
        {
            // r1 - 10
            //    - 20 - 30
            // ↓
            // r1 - 10

            var tree = new Tree<int>();
            var item0 = 1;
            var items = new[] { 10, 20, 30 };

            tree.Root.Item = item0;
            tree.Root.AddChildren( new[] { items[ 0 ], items[ 1 ] } );
            var n1 = tree.Root.LastChild;
            n1.AddChild( items[ 2 ] );

            n1.RemoveSubtree();
            tree.Root.Item.Is( item0 );
            tree.Root.FirstChild.Item.Is( items[ 0 ] );
            tree.Root.FirstChild.HasChild.IsFalse();
            tree.Root.CountNodes.Is( 2 );  // 自分を含む
        }

        [TestMethod]
        public void RemoveSubtree_Root()
        {
            // r1 - 10

            var tree = new Tree<int>();
            var item0 = 1;
            var item1 = 10;

            tree.Root.Item = item0;
            tree.Root.AddChild( item1 );
            tree.Root.Item.Is( item0 );
            tree.Root.CountNodes.Is( 2 );  // 自分を含む
            tree.Root.HasChild.IsTrue();

            tree.Root.RemoveSubtree();
            tree.Root.CountNodes.Is( 1 );  // 自分を含む
            tree.Root.HasChild.IsFalse();
            tree.Root.Item.Is( default(int) );
        }

        [TestMethod]
        public void _ToString()
        {
            // r1(null) - r2(10)

            var tree = new Tree<int?>();
            var item1 = 10;

            tree.Root.AddChild( item1 );

            tree.Root.ToString().Is( "null" );
            tree.Root.FirstChild.ToString().Is( item1.ToString() );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpStackLayer
{
    /// <summary>
    /// Genericなツリーノード
    /// </summary>
    /// <typeparam name="T">ノードに保持するオブジェクトの型</typeparam>
    public class TreeNode<T>
    {
        #region Properties

        /// <summary>
        /// 子ノード
        /// </summary>
        internal List<TreeNode<T>> Children { get; } = new List<TreeNode<T>>();

        /// <summary>
        /// 自分をルートとする部分木のノード数
        /// </summary>
        /// <remarks>自分もカウントに含めます.</remarks>
        public int CountNodes => CountNodesRecursively( this );

        /// <summary>
        /// 最初の子ノード
        /// </summary>
        public TreeNode<T> FirstChild => HasChild ? Children[ 0 ] : null;

        /// <summary>
        /// 自分の番号
        /// </summary>
        /// <remarks>NextSibling or PrevSibling が空の場合は null になります.</remarks>
        internal int Index => Parent == null ? -1 : Parent.Children.FindIndex( x => x == this );

        /// <summary>
        /// 葉かどうか (子がないかどうか) を判定します.
        /// </summary>
        /// <remarks>true → 葉，false → 葉でない</remarks>
        public bool IsLeaf => !HasChild;

        /// <summary>
        /// ノードに保持する項目
        /// </summary>
        public T Item { get; set; }

        /// <summary>
        /// 子があるかどうかを判定します.
        /// </summary>
        /// <remarks>true → 子がある，false → 子がない</remarks>
        public bool HasChild => Children.Any();

        /// <summary>
        /// 同じ親で自分と異なる子がいるかどうかを判定します.
        /// </summary>
        /// <remarks>true → 自分と異なる子がいる，false → 自分と異なる子がいない</remarks>
        public bool HasSibling => Parent != null && Parent.Children.Count > 1;

        /// <summary>
        /// 最後の子ノード
        /// </summary>
        public TreeNode<T> LastChild => HasChild ? Children[ Children.Count - 1 ] : null;

        /// <summary>
        /// (自分の親から見て) 自分の次の子
        /// </summary>
        public TreeNode<T> NextSibling
        {
            get {
                var index = this.Index;
                return index == -1 ?
                    null :
                    index < Parent.Children.Count - 1 ?
                        Parent.Children[ index + 1 ] :
                        null;
            }
        }

        /// <summary>
        /// 親ノード
        /// </summary>
        public TreeNode<T> Parent { get; set; }

        /// <summary>
        /// (自分の親から見て) 自分の前の子
        /// </summary>
        public TreeNode<T> PrevSibling
        {
            get {
                var index = this.Index;
                return index > 0 ? Parent.Children[ index - 1 ] : null;
            }
        }

        #endregion  // Properties

        #region Constructors

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="item">ノードに保持する項目</param>
        public TreeNode( T item )
        {
            this.Item = item;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TreeNode() : this( default(T) )
        {
        }

        #endregion  // Constructors

        #region Methods

        /// <summary>
        /// ノードを子に追加します.
        /// </summary>
        /// <param name="node">ノード</param>
        public void AddChild( TreeNode<T> node )
        {
            Children.Add( node );
            node.Parent = this;
        }

        /// <summary>
        /// ノード列を子に追加します.
        /// </summary>
        /// <param name="nodes">ノード列</param>
        public void AddChildren( IEnumerable<TreeNode<T>> nodes )
        {
            Children.AddRange( nodes );
            foreach (var node in nodes)
                node.Parent = this;
        }

        /// <summary>
        /// 自分をルートとする部分木のノードを列挙します.
        /// </summary>
        /// <returns>ノード列</returns>
        public IEnumerable<TreeNode<T>> Enumerate()
        {
            var nodes = new List<TreeNode<T>>();
            EnumerateRecursively( this, nodes );
            return nodes;
        }

        /// <summary>
        /// 指定条件を充たすノードを全て見つけます.
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>見つかったノード列</returns>
        public IEnumerable<TreeNode<T>> FindAll( Predicate<TreeNode<T>> predicate ) =>
            Enumerate().Where( x => predicate( x ) );

        /// <summary>
        /// 指定条件を充たす最初のノードを見つけます.
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>見つかったノード</returns>
        public TreeNode<T> FindFirst( Predicate<TreeNode<T>> predicate ) =>
            Enumerate().FirstOrDefault( x => predicate( x ) );

        /// <summary>
        /// 最初の葉を見つけます.
        /// </summary>
        /// <returns>見つかった葉ノード</returns>
        public TreeNode<T> FindFirstLeaf() => FindFirst( x => x.IsLeaf );

        /// <summary>
        /// 指定条件を充たす最後のノードを見つけます.
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>見つかったノード</returns>
        public TreeNode<T> FindLast( Predicate<TreeNode<T>> predicate ) =>
            Enumerate().LastOrDefault( x => predicate( x ) );

        /// <summary>
        /// 最後の葉を見つけます.
        /// </summary>
        /// <returns>見つかった葉ノード</returns>
        public TreeNode<T> FindLastLeaf() => FindLast( x => x.IsLeaf );

        /// <summary>
        /// ノードを子として指定インデックスに追加します.
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <param name="node">ノード</param>
        public void InsertChild( int index, TreeNode<T> node )
        {
            Children.Insert( index, node );
            node.Parent = this;
        }

        /// <summary>
        /// ノード列を子として指定インデックスに追加します.
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <param name="nodes">ノード列</param>
        public void InsertChildren( int index, IEnumerable<TreeNode<T>> nodes )
        {
            Children.InsertRange( index, nodes );
            foreach (var node in nodes)
                node.Parent = this;
        }

        // TODO: Remove

        // TODO: SubTree

        /// <summary>
        /// 文字列に変換します.
        /// </summary>
        /// <returns>項目のToString()</returns>
        public override string ToString()
        {
            return Item == null ? "null" : Item.ToString();
        }

        int CountNodesRecursively( TreeNode<T> currentNode )
        {
            // 自分をカウント
            int countNodes = 1;

            if (currentNode.HasChild) 
                currentNode.Children.ForEach( x => countNodes += CountNodesRecursively( x ) );

            return countNodes;
        }

        void EnumerateRecursively( TreeNode<T> currentNode, List<TreeNode<T>> nodes )
        {
            // 自分を追加
            nodes.Add( currentNode );

            if (currentNode.HasChild)
                currentNode.Children.ForEach( x => EnumerateRecursively( x, nodes ) );
        }

        #endregion  // Methods
    }
}

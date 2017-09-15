using System.Collections.Generic;

namespace CSharpStackLayer
{
    /// <summary>
    /// Genericなツリー
    /// </summary>
    /// <typeparam name="T">ノードに保持するオブジェクトの型</typeparam>
    public partial class Tree<T>
    {
        TreeNode<T> root = new TreeNode<T>();

        /// <summary>
        /// ルートノード
        /// </summary>
        public TreeNode<T> Root => root;

        /// <summary>
        /// ノード数
        /// </summary>
        /// <remarks>ルートノードを含みます.</remarks>
        public int CountNodes => Root.CountNodes;

        /// <summary>
        /// ノードを列挙します.
        /// </summary>
        /// <returns>ノード列</returns>
        public IEnumerable<TreeNode<T>> Enumerate() => Root.Enumerate();

        /// <summary>
        /// ルートノードを設定します.
        /// </summary>
        /// <param name="root">ノード</param>
        internal void SetRoot( TreeNode<T> root )
        {
            if (this.root != null) {
                this.root.Item = default(T);
                this.root = null;
            }

            this.root = root;
        }

        // TODO:
        //public Tree<T> ShallowCopy()
        //{
        //}
    }
}

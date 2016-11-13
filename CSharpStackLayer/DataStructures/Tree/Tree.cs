using System.Collections.Generic;

namespace CSharpStackLayer
{
    /// <summary>
    /// Genericなツリー
    /// </summary>
    /// <typeparam name="T">ノードに保持するオブジェクトの型</typeparam>
    public partial class Tree<T>
    {
        /// <summary>
        /// ルートノード
        /// </summary>
        public TreeNode<T> Root { get; } = new TreeNode<T>();

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
    }
}

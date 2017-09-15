namespace CSharpStackLayer
{
    public partial class Tree<T>
    {
        /// <summary>
        /// Genericツリー用のイテレータ
        /// </summary>
        public class TreeIterator
        {
            /// <summary>
            /// 現在のノード
            /// </summary>
            internal TreeNode<T> CurrentNode { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="currentNode">現在のノード</param>
            public TreeIterator( TreeNode<T> currentNode )
            {
                this.CurrentNode = currentNode;
            }

            /// <summary>
            /// 1つ次のIteratorを返します.
            /// </summary>
            /// <remarks>1つ次が見つからない場合は null を返します.</remarks>
            /// <param name="itr">起点とするTreeIterator</param>
            /// <returns>TreeIterator</returns>
            static TreeIterator Next( TreeIterator itr )
            {
                #region 例
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
                #endregion  // 例

                if (itr.CurrentNode.HasChild)
                    return new TreeIterator( itr.CurrentNode.FirstChild );
                if (itr.CurrentNode.NextSibling != null)
                    return new TreeIterator( itr.CurrentNode.NextSibling );

                var prevNode = itr.CurrentNode;
                itr.CurrentNode = itr.CurrentNode.Parent;
                while (itr.CurrentNode != null) {
                    if (itr.CurrentNode.HasChild && itr.CurrentNode.LastChild != prevNode)
                        return new TreeIterator( prevNode.NextSibling );
                    prevNode = itr.CurrentNode;
                    itr.CurrentNode = itr.CurrentNode.Parent;
                }
                return null;
            }

            /// <summary>
            /// 1つ前のIteratorを返します.
            /// </summary>
            /// <remarks>1つ前が見つからない場合は null を返します.</remarks>
            /// <param name="itr">起点とするTreeIterator</param>
            /// <returns>TreeIterator</returns>
            static TreeIterator Prev( TreeIterator itr )
            {
                #region 例
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
                #endregion  // 例

                var prevSibling = itr.CurrentNode.PrevSibling;
                if (prevSibling != null) {
                    if (prevSibling.IsLeaf)
                        return new TreeIterator( prevSibling );
                    itr.CurrentNode = prevSibling.LastChild;
                    while (!itr.CurrentNode.IsLeaf)
                        itr.CurrentNode = itr.CurrentNode.LastChild;
                    return new TreeIterator( itr.CurrentNode );
                }
                var parent = itr.CurrentNode.Parent;
                return parent == null ? null : new TreeIterator( itr.CurrentNode.Parent );
            }

            /// <summary>
            /// ++ 演算子のオーバーロード
            /// </summary>
            /// <param name="itr">起点とするTreeIterator</param>
            /// <returns>1つ次のTreeIterator</returns>
            public static TreeIterator operator ++( TreeIterator itr ) => Next( itr );

            /// <summary>
            /// -- 演算子のオーバーロード
            /// </summary>
            /// <param name="itr">起点とするTreeIterator</param>
            /// <returns>1つ前のTreeIterator</returns>
            public static TreeIterator operator --( TreeIterator itr ) => Prev( itr );

            /// <summary>
            /// 現在のノードの値を取得します.
            /// </summary>
            /// <returns>現在のノードの値</returns>
            public T Get() => CurrentNode.Item;
        }

        /// <summary>
        /// Root起点のTreeIteratorを返します.
        /// </summary>
        /// <returns>TreeIterator</returns>
        public TreeIterator Iterator() => new TreeIterator( Root );

        /// <summary>
        /// 指定ノード起点のTreeIteratorを返します.
        /// </summary>
        /// <param name="node">ノード</param>
        /// <returns>TreeIterator</returns>
        public TreeIterator Iterator( TreeNode<T> node ) => new TreeIterator( node );
    }
}

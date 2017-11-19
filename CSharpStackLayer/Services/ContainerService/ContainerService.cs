using System;
using System.Collections.Generic;

namespace CSharpStackLayer
{
    /// <summary>
    /// コンテナサービス
    /// </summary>
    public class ContainerService
    {
        static Dictionary<Type, IContainer> containersCache = new Dictionary<Type, IContainer>();

        /// <summary>
        /// Container から指定した型 TItem のインスタンスを取得します.
        /// </summary>
        /// <typeparam name="TContainer">コンテナの型</typeparam>
        /// <typeparam name="TItem">コンテナが持つ public プロパティ (or public フィールド) の型</typeparam>
        /// <returns>検索結果</returns>
        public TItem Open<TContainer, TItem>()
            where TContainer: IContainer, new()
        {
            return OpenContainer<TContainer>().Find<TItem>();
        }

        IContainer OpenContainer<TContainer>()
            where TContainer: IContainer, new()
        {
            var containerType = typeof( TContainer );

            var container = default( IContainer );
            if (containersCache.ContainsKey( containerType ))
                container = containersCache[ containerType ];
            else {
                container = Activator.CreateInstance( containerType ) as IContainer;
                container.Setup();
                containersCache.Add( containerType, container );
            }

            return container;
        }
    }
}

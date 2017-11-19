using System.Linq;
using System.Reflection;

namespace CSharpStackLayer
{
    /// <summary>
    /// コンテナの拡張メソッド
    /// </summary>
    public static class ContainerExtension
    {
        /// <summary>
        /// コンテナ内の型 TItem の public プロパティ (or public フィールド) の値を返します.
        /// 複数ある場合は最初に見つかったものを返します (プロパティ → フィールド の順で検索します).
        /// </summary>
        /// <typeparam name="TItem">コンテナ内の検索項目の型</typeparam>
        /// <param name="container">コンテナ</param>
        /// <returns>コンテナ内の検索項目の値</returns>
        public static TItem Find<TItem>( this IContainer container )
        {
            if (container == null)
                return default( TItem );

            var containerType = container.GetType();
            var itemType = typeof( TItem );

            var propInfos = containerType.GetProperties( BindingFlags.Instance | BindingFlags.Public );
            var pInfo = propInfos.FirstOrDefault( x => x.PropertyType == itemType );
            if (pInfo != null)
                return (TItem)pInfo.GetValue( container );

            var fieldInfos = containerType.GetFields( BindingFlags.Instance | BindingFlags.Public );
            var fInfo = fieldInfos.FirstOrDefault( x => x.FieldType == itemType );
            if (fInfo != null)
                return (TItem)fInfo.GetValue( container );

            return default( TItem );
        }
    }
}

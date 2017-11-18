using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CSharpStackLayer
{
    // AutoMapper 風
    // 　デフォルトマップとカスタムマップを分ける
    // 　カスタムマップ優先でそれ以外はデフォルトマップを使う構成にする
    public class Mapper<Domain, Codomain>
    {
        Dictionary<Type, Action<Domain, Codomain>> additionalMapRecords = new Dictionary<Type, Action<Domain, Codomain>>();

        public void RecordAdditional<In, Out>( Action<In, Out> additionalMapAction )
            where In: Domain
            where Out: Codomain
        {
            // キーの重複があったらエラーにする
            additionalMapRecords.Add( typeof( In ), (@in, @out) => additionalMapAction( (In)@in, (Out)@out ) );
        }

        public Out Map<In, Out>( In input )
            where In: Domain
            where Out: Codomain
        {
            // デフォルトマップ
            var output = applyDefaultMap<In, Out>( input );

            // (追加の) カスタムマップ
            var additionalMapAction = GetMap<In>();
            additionalMapAction( input, output );

            return output;
        }

        // Reflection を使ってマップ可能な箇所はマップする
        // 次の2つが一致する場合に値をコピーする
        // 　(1) 名称 (小文字・大文字の区別なし)
        // 　(2) 型 (完全一致)
        Out applyDefaultMap<In, Out>( In input )
            where In: Domain
            where Out: Codomain
        {
            var output = Activator.CreateInstance<Out>();

            // 見つからない場合は空配列が返る
            var inProps = typeof( In ).GetProperties( BindingFlags.Instance | BindingFlags.Public );
            var inFields = typeof( In ).GetFields( BindingFlags.Instance | BindingFlags.Public );

            applyDefaultMapAtOutProperties<In, Out>( input, output, inProps, inFields );
            applyDefaultMapAtOutFields<In, Out>( input, output, inProps, inFields );

            return output;
        }

        // {In.Properties, In.Fields} -> Out.Properties
        void applyDefaultMapAtOutProperties<In, Out>( In input, Out output, PropertyInfo[] inProps, FieldInfo[] inFields )
            where In: Domain
            where Out: Codomain
        {
            var outProps = typeof( Out ).GetProperties( BindingFlags.Instance | BindingFlags.Public );

            foreach (var op in outProps) {
                var opLowerName = op.Name.ToLower();
                var foundIp =
                    inProps.FirstOrDefault(
                        ip => string.Compare( ip.Name.ToLower(), opLowerName ) == 0 &&
                        ip.PropertyType == op.PropertyType );
                if (foundIp != null) {
                    op.SetValue( output, foundIp.GetValue( input ) );
                    // (小文字・大文字を除いて) 同じ名称で同じ型のプロパティとフィールドはクラススコープでは1つ
                    continue;
                }

                var foundIf =
                    inFields.FirstOrDefault(
                        @if => string.Compare( @if.Name.ToLower(), opLowerName ) == 0 &&
                        @if.FieldType == op.PropertyType );
                if (foundIf != null)
                    op.SetValue( output, foundIf.GetValue( input ) );
            }
        }

        // {In.Properties, In.Fields} -> Out.Fields
        void applyDefaultMapAtOutFields<In, Out>( In input, Out output, PropertyInfo[] inProps, FieldInfo[] inFields )
            where In: Domain
            where Out: Codomain
        {
            var outFields = typeof( Out ).GetFields( BindingFlags.Instance | BindingFlags.Public );

            foreach (var of in outFields) {
                var ofLowerName = of.Name.ToLower();
                var foundIp =
                    inProps.FirstOrDefault(
                        ip => string.Compare( ip.Name.ToLower(), ofLowerName ) == 0 &&
                        ip.PropertyType == of.FieldType );
                if (foundIp != null) {
                    of.SetValue( output, foundIp.GetValue( input ) );
                    continue;
                }

                var foundIf =
                    inFields.FirstOrDefault(
                        @if => string.Compare( @if.Name.ToLower(), ofLowerName ) == 0 &&
                        @if.FieldType == of.FieldType );
                if (foundIf != null)
                    of.SetValue( output, foundIf.GetValue( input ) );
            }
        }

        Action<Domain, Codomain> GetMap<In>()
            where In: Domain
        {
            return additionalMapRecords[ typeof( In ) ];
        }
    }
}

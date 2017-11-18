using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpStackLayer.UnitTest.DataStructures
{
    [TestClass]
    public class MapperTest
    {
        abstract class TestDomain
        {
            public string UserId;
            public DateTime? ServiceStartDateTime { get; set; }
            public DateTime? ServiceEndDateTime { get; set; }
        }

        abstract class TestResponse
        {
            public string userId { get; set; }
            public string serviceStartDateTime { get; set; }
            public string serviceEndDateTime { get; set; }
        }

        class AServiceDomain : TestDomain
        {
            public bool IsPremiumUser;
            // 月額料金 (期間などによって変動)
            public int CurrentMonthlyPrice { get; set; }
        }

        class AServiceResponse : TestResponse
        {
            public string isPremiumUser { get; set; }
            public string currentMonthlyPrice { get; set; }
        }

        class BServiceDomain : TestDomain
        {
            // ユーザのサービス利用履歴に応じた商品メッセージ
            public string CommercialMessage;
        }

        class BServiceResponse : TestResponse
        {
            public string commercialMessage { get; set; }
        }

        [TestMethod]
        public void Map()
        {
            // マップの構成
            var mapper = new Mapper<TestDomain, TestResponse>();
            mapper.RecordAdditional<AServiceDomain, AServiceResponse>( (i, o) => {
                o.serviceStartDateTime = ToDateString( i.ServiceStartDateTime );
                o.serviceEndDateTime = ToDateString( i.ServiceEndDateTime );
                o.isPremiumUser = i.IsPremiumUser ? "1" : "0";
                o.currentMonthlyPrice = i.CurrentMonthlyPrice.ToString();
            });
            mapper.RecordAdditional<BServiceDomain, BServiceResponse>( (i, o) => {
                o.serviceStartDateTime = ToDateString( i.ServiceStartDateTime );
                o.serviceEndDateTime = ToDateString( i.ServiceEndDateTime );
            });

            // 入力を用意
            var today = DateTime.Today;
            var aDomain = new AServiceDomain {
                UserId = "001",
                ServiceStartDateTime = today,
                ServiceEndDateTime = null,
                IsPremiumUser = true,
                CurrentMonthlyPrice = 1000
            };
            var bDomain = new BServiceDomain {
                UserId = "001",
                ServiceStartDateTime = today,
                ServiceEndDateTime = today.AddDays( 10 ),
                CommercialMessage = "ファミリーマートでプチりんごデニッシュ (4個入り) が発売"
            };

            // マップを適用して検証
            var aResponse = mapper.Map<AServiceDomain, AServiceResponse>( aDomain );
            aResponse.userId.Is( aDomain.UserId );
            aResponse.serviceStartDateTime.Is( aDomain.ServiceStartDateTime?.ToString( "yyyy/MM/dd HH:mm:ss" ) );
            aResponse.serviceEndDateTime.Is( aDomain.ServiceEndDateTime?.ToString( "yyyy/MM/dd HH:mm:ss" ) );
            aResponse.isPremiumUser.Is( "1" );
            aResponse.currentMonthlyPrice.Is( "1000" );

            var bResponse = mapper.Map<BServiceDomain, BServiceResponse>( bDomain );
            bResponse.userId.Is( bDomain.UserId );
            bResponse.serviceStartDateTime.Is( bDomain.ServiceStartDateTime?.ToString( "yyyy/MM/dd HH:mm:ss" ) );
            bResponse.serviceEndDateTime.Is( bDomain.ServiceEndDateTime?.ToString( "yyyy/MM/dd HH:mm:ss" ) );
            bResponse.commercialMessage.Is( bDomain.CommercialMessage );
        }

        string ToDateString( DateTime? dt ) => dt?.ToString( "yyyy/MM/dd HH:mm:ss" );
    }
}

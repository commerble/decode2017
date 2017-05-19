using decodedon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace decodedon.Web.Api
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Federate
    {
        // HTTP GET を使用するために [WebGet] 属性を追加します (既定の ResponseFormat は WebMessageFormat.Json)。
        // XML を返す操作を作成するには、
        //     [WebGet(ResponseFormat=WebMessageFormat.Xml)] を追加し、
        //     操作本文に次の行を含めます。
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        [WebGet]
        public IEnumerable<Toot> Toots(int take, int skipToken)
        {
            if (take == 0)
                take = int.MaxValue;

            // 操作の実装をここに追加してください
            var accessor = new TootLocal();
            return accessor.Load(take, skipToken).Select(t => t.ToRemote());
        }

        // 追加の操作をここに追加して、[OperationContract] とマークしてください
    }
}

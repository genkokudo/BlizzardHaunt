using System.Net;
using Api.Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Api
{
    public class Function1
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _db;

        //private readonly string _parameterName = "Escapers";

        public Function1(ILoggerFactory loggerFactory, ApplicationDbContext db)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
            _db = db;
        }

        // http://localhost:7073/api/Function1
        // http://localhost:7073/api/Function1?name=aaaa

        /// <summary>
        /// 今まで脱出した人数を返す
        /// データが無ければ作成する
        /// パラメータなし:現在の値を取得
        /// パラメータあり:現在の値に1を加えて取得、パラメータは使用しないが"name"を付けること。
        /// </summary>
        /// <returns></returns>
        [Function("Function1")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("EscapeCountUpの処理を開始");

            //// まずDBからデータを準備
            //var dbData = _db.Generals.FirstOrDefault(x => x.Name == _parameterName);
            //if (dbData is null)
            //{
            //    // データが無ければ作成する
            //    dbData = new General { Name = _parameterName, Value = "0" };
            //    _db.Generals.Add(dbData);
            //    await _db.SaveChangesAsync();
            //}

            //// パラメータを取得する
            //var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            //var isUpdate = !string.IsNullOrEmpty(query["name"]);

            //if (isUpdate)
            //{
            //    // カウントアップしてDBを更新する
            //    var dbDataInt = int.Parse(dbData.Value!);
            //    dbDataInt++;
            //    dbData.Value = dbDataInt.ToString();
            //    await _db.SaveChangesAsync();
            //}


            //string responseMessage = $"{dbData.Value}";

            // レスポンスを作成する
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            //response.WriteString(responseMessage);
            response.WriteString("なにも実装していません");
            return response;

            // 前のバージョンはreqのBodyからrequestBodyをJSONとして読み取ってデータを受け取ったりしてたけど、今回は知らない。
        }

    }


}

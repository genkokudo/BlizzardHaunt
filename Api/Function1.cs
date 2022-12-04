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
        /// ���܂ŒE�o�����l����Ԃ�
        /// �f�[�^��������΍쐬����
        /// �p�����[�^�Ȃ�:���݂̒l���擾
        /// �p�����[�^����:���݂̒l��1�������Ď擾�A�p�����[�^�͎g�p���Ȃ���"name"��t���邱�ƁB
        /// </summary>
        /// <returns></returns>
        [Function("Function1")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("EscapeCountUp�̏������J�n");

            //// �܂�DB����f�[�^������
            //var dbData = _db.Generals.FirstOrDefault(x => x.Name == _parameterName);
            //if (dbData is null)
            //{
            //    // �f�[�^��������΍쐬����
            //    dbData = new General { Name = _parameterName, Value = "0" };
            //    _db.Generals.Add(dbData);
            //    await _db.SaveChangesAsync();
            //}

            //// �p�����[�^���擾����
            //var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            //var isUpdate = !string.IsNullOrEmpty(query["name"]);

            //if (isUpdate)
            //{
            //    // �J�E���g�A�b�v����DB���X�V����
            //    var dbDataInt = int.Parse(dbData.Value!);
            //    dbDataInt++;
            //    dbData.Value = dbDataInt.ToString();
            //    await _db.SaveChangesAsync();
            //}


            //string responseMessage = $"{dbData.Value}";

            // ���X�|���X���쐬����
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            //response.WriteString(responseMessage);
            response.WriteString("�Ȃɂ��������Ă��܂���");
            return response;

            // �O�̃o�[�W������req��Body����requestBody��JSON�Ƃ��ēǂݎ���ăf�[�^���󂯎�����肵�Ă����ǁA����͒m��Ȃ��B
        }

    }


}

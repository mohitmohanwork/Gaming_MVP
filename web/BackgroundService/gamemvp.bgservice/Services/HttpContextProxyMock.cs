using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using ZNxt.Net.Core.Interfaces;
using ZNxt.Net.Core.Model;

namespace gamemvp.bgservice.Services
{
    public class HttpContextProxyMock : IHttpContextProxy
    {
        public Dictionary<string, string> ResponseHeaders { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int ResponseStatusCode => throw new NotImplementedException();

        public string ResponseStatusMessage => throw new NotImplementedException();

        public byte[] Response => throw new NotImplementedException();

        public string SessionID => throw new NotImplementedException();

        public UserModel User { get {

                return new UserModel()
                {
                     user_id = "001",
                     user_name = "bg_user",
                     first_name = "bb",
                     last_name = "user"
                };
            } }

        public DateTime InitDateTime => throw new NotImplementedException();

        public string TransactionId => throw new NotImplementedException();

        public Task<string> GetAccessTokenAync()
        {
            return Task.FromResult(string.Empty);
        }

        public string GetFormData(string key)
        {
            throw new NotImplementedException();
        }

        public string GetHeader(string key)
        {
            return null;
        }

        public Dictionary<string, string> GetHeaders()
        {
            return new Dictionary<string, string>();
        }

        public string GetHttpMethod()
        {
            throw new NotImplementedException();
        }

        public string GetQueryString(string key)
        {
            return string.Empty;
        }

        public string GetQueryString()
        {
            return string.Empty;
        }

        public string GetRequestBody()
        {
            return string.Empty;
        }

        public T GetRequestBody<T>()
        {
            throw new NotImplementedException();
        }

        public string GetURIAbsolutePath()
        {
            throw new NotImplementedException();
        }

        public void SetResponse(int statusCode, JObject data = null)
        {
            throw new NotImplementedException();
        }

        public void SetResponse(int statusCode, string data)
        {
            throw new NotImplementedException();
        }

        public void SetResponse(int statusCode, byte[] data)
        {
            throw new NotImplementedException();
        }

        public void SetResponse(string data)
        {
            throw new NotImplementedException();
        }

        public void SetResponse(byte[] data)
        {
            throw new NotImplementedException();
        }

        public void UnloadAppDomain()
        {
            throw new NotImplementedException();
        }
    }
}

using CrmStepMobileApp.Helper;
using CrmStepMobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrmStepMobileApp.Services
{
  public  class DocumentsService:ApiBase
    {
        public async Task<DocumentApiModel> GetListByParent(string parentId, int parentType,int page,int count)
        {
            try
            {

                var endPoint = String.Format(GetListByParentKey, parentId,parentType,page,count);
                var result = await HttpClientBase.Get<DocumentApiModel>(endPoint, StaticData.AccessToken);
                return result;
            }
            catch (Exception ex)
            {
                return new DocumentApiModel() { message = ex.Message };
            }

        }


        public async Task<DocumentResponseModel> PostDocument(string json)
        {
            try
            {
                var endPoint = String.Format(PostDocumnetKey);
                var result = await HttpClientBase.Post<DocumentResponseModel>(endPoint, json, StaticData.AccessToken);
                return result;
            }
            catch (Exception ex)
            {
                return new DocumentResponseModel() { msg = ex.Message };
            }

        }

        public async Task<byte[]> DownloadDocument(int id)
        {
            try
            {
                var endPoint = String.Format(DownloadDocumnetKey,id);
                var result = await HttpClientBase.GetBytes(endPoint, StaticData.AccessToken);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<DocumentResponseModel> DeleteDocumnet(int id)
        {
            try
            {
                var endPoint = String.Format(DeleteDocumnetKey, id);
                var result = await HttpClientBase.Delete<DocumentResponseModel>(endPoint, StaticData.AccessToken);
                return result;
            }
            catch (Exception ex)
            {
                return new DocumentResponseModel() { msg = ex.Message };
            }

        }
        

    }
}

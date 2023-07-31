using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelAnd
{
    public enum ErrorCodeEnum
    {
        NoError   //0
       ,Ok//1
       ,NotAuthorize //2
       ,ExistedInDatabase //3
       ,DosentExiteInDatabase //4
       ,CanNotBeDeletedBCRelation //5
       ,DosentHasTheRole //6
       ,Authorize//7
       ,Status500InternalServerError//8
       ,BadRequest //9
       ,InvalidClientRequest//10
       ,InvalidAccessTokenOrRefreshToken//11
            , NoContent//12
            ,Save//13
    }
}

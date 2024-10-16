using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ValidationError), (int)HttpStatusCode.BadRequest)]

    public class APIController : ControllerBase
    {




        /*
         
            اربع حاجات (parent class) بيروث من ابوه child class اي
        
        1. parent بتاع ال Defualt Contractor بينادي علي child class ال
        2. parent موجودة عند ال public نوعها property اي parent class بيورث من ال child class ال
        3. parent موجودة عند ال public نوعها Methods اي parent class بيورث من ال child class ال
        4. parent موجودة عند ال public نوعها Attribute اي parent class بيورث من ال child class ال

                                                 مثل Attribute ال   
                      [ApiController] , [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
                     , [Authorize] , [Required]



         */





    }
}

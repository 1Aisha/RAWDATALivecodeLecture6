using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService.Models;

namespace WebService.Controllers
{
    /// <summary>
    /// Super class for the controllers
    /// </summary>
    public abstract class BaseApiController : ApiController
    {
        ModelFactory _modelFactory;

        /// <summary>
        /// Handle the problem with the UrlHelper. We need a request
        /// before we can create the modelfactory
        /// </summary>
        public ModelFactory ModelFactory
        {
            get
            {
                // defer the creation until it is needed, thus
                // the request is valid
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(Request);
                }
                return _modelFactory;
            }
        }
    }
}

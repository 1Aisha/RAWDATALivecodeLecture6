using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using DataAccessLayer;
using WebService.Models;

namespace WebService.Controllers
{
    public class PersonController : ApiController
    {
        ModelFactory _modelFactory = new ModelFactory();
        PersonRepository _personRepository = new PersonRepository();
        public IEnumerable<PersonModel> Get()
        {
            var helper = new UrlHelper(Request);
            return _personRepository.GetAll()
                .Select(person => _modelFactory.Create(person, helper));
        }

        public HttpResponseMessage Get(int id)
        {
            var helper = new UrlHelper(Request);
            var person = _personRepository.GetById(id);
            if (person == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(
                HttpStatusCode.OK
                , _modelFactory.Create(person, helper));
        }
    }
}

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
    /// <summary>
    /// The person controller handles request about persons
    /// </summary>
    public class PersonsController : BaseApiController
    {
        
        PersonRepository _personRepository = new PersonRepository();

        // get
        public IEnumerable<PersonModel> Get()
        {
            var helper = new UrlHelper(Request);
            return _personRepository.GetAll()
                .Select(person => ModelFactory.Create(person));
        }

        

        // get by id
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
                , ModelFactory.Create(person));
        }

        // add
        public HttpResponseMessage Post([FromBody] PersonModel model)
        {
            // url helper to construct the url
            var helper = new UrlHelper(Request);
            // convert from PersonModel to Person
            var person = ModelFactory.Parse(model);
            // add the person
            _personRepository.Add(person);
            // convert the Person to PersonModel
            // and return
            return Request.CreateResponse(
                HttpStatusCode.Created
                , ModelFactory.Create(person));
        }

        // update
        public HttpResponseMessage Put(int id, [FromBody] PersonModel model)
        {
            // url helper to construct the url
            var helper = new UrlHelper(Request);
            // convert from PersonModel to Person
            var person = ModelFactory.Parse(model);
            // set the id
            person.Id = id;
            // update the person
            _personRepository.Update(person);
            // return ok
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

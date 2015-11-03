using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using DataAccessLayer;

namespace WebService.Models
{
    public class ModelFactory
    {
      
        private UrlHelper _urlHelper;

        // create the url helper with the injected request
        public ModelFactory(HttpRequestMessage request)
        {
            _urlHelper = new UrlHelper(request);
        } 

        public MovieModel Create(Movie movie)
        {
            return new MovieModel
            {
                Url = _urlHelper.Link("MovieApi", new { id = movie.Id }),
                Title = movie.Title,
                Year = movie.Year
            };
        }

        public PersonModel Create(Person person)
        {
            return new PersonModel
            {
                Url = _urlHelper.Link("PersonApi", new { id = person.Id }),
                Name = person.Name,
                Gender = person.Gender
            };
        }

        public Person Parse(PersonModel model)
        {
            return new Person
            {
                Name = model.Name,
                Gender = model.Gender
            };
        }
    }
}
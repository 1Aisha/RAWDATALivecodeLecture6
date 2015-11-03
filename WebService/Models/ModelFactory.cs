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
 public MovieModel Create(Movie movie, UrlHelper urlHelper)
        {
            return new MovieModel
            {
                Url = urlHelper.Link("MovieApi", new {id = movie.Id}),
                Title = movie.Title,
                Year = movie.Year
            };
        }

        public PersonModel Create(Person person, UrlHelper urlHelper)
        {
            return new PersonModel
            {
                Url = urlHelper.Link("PersonApi", new { id = person.Id }),
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
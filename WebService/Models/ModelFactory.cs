using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
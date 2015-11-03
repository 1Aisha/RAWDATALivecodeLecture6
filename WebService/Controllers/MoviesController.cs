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
    public class MoviesController : BaseApiController
    {
        MovieRepository _movieRepository = new MovieRepository();

        public IEnumerable<MovieModel> Get()
        {
            var helper = new UrlHelper(Request);
            return _movieRepository.GetAll()
                                   .Select(movie => ModelFactory.Create(movie));
        }

        public HttpResponseMessage Get(int id)
        {
            var helper =  new UrlHelper(Request);
            var movie = _movieRepository.GetById(id);
            if (movie == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, ModelFactory.Create(movie));
        }
    }
}

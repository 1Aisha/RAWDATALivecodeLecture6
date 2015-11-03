using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService.DAL;
using WebService.Models;

namespace WebService.Controllers
{
    public class MoviesController : ApiController
    {
        MovieRepository _movieRepository = new MovieRepository();
        public IEnumerable<Movie> Get()
        {
            return _movieRepository.GetAll();
        }

        public Movie Get(int id)
        {
            return _movieRepository.GetById(id);
        }
    }
}

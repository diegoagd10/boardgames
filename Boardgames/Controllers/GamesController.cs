using Boardgames.DAL;
using Boardgames.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Boardgames.Controllers
{
    public class GamesController : ApiController
    {
         private GameDataAccess _gameDataAccess;

         public GamesController()
         {
             #region GamesController

             this._gameDataAccess = new GameDataAccess();

             #endregion
         }

        // GET: api/Games
        public IEnumerable<Game> Get()
        {
            #region Get

            return this._gameDataAccess.GetAll();

            #endregion
        }

        // GET: api/Games/5
        public Game Get(long ID)
        {
            #region Get

            return _gameDataAccess.GetByID(ID);

            #endregion
        }

        // POST: api/Games
        public IHttpActionResult Post([FromBody]Game Game)
        {
            #region Post

            if(ModelState.IsValid)
            {
                Game.CreatedDate = DateTime.Now;
                Game.GameID = _gameDataAccess.GetNextGameID();
                _gameDataAccess.Add(Game);
            }
            return StatusCode(HttpStatusCode.Created);
            
            #endregion
        }

        // PUT: api/Games/
        public IHttpActionResult Put([FromBody]Game Game)
        {
            #region Put

            if(ModelState.IsValid)
            {
                _gameDataAccess.Update(Game);
            }
            return StatusCode(HttpStatusCode.OK);

            #endregion
        }

        // DELETE: api/Games/5
        public IHttpActionResult Delete(long ID)
        {
            #region Delete

            _gameDataAccess.Delete(ID);
            return StatusCode(HttpStatusCode.OK);

            #endregion
        }
    }
}

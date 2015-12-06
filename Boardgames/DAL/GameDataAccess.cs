using Boardgames.Models;
using Boardgames.Repository;
using Boardgames.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boardgames.DAL
{
    public class GameDataAccess
    {
        private IXmlUnitOfWork _unitOfWork;
        private IGameRepository _repository;

        public GameDataAccess()
        {
            #region MovieDataAccess

            this._unitOfWork = UnitOfWorkFactory.CreateUnitOfWork<Game>();
            this._repository = RepositoryFactory.CreateRepository<IGameRepository, GameRepository>(this._unitOfWork);

            #endregion
        }

        public IEnumerable<Game> GetAll()
        {
            #region GetAll

            return _repository.GetAll();

            #endregion
        }

        public Game GetByID(long ID)
        {
            #region GetByID

            return _repository.GetByKey(ID);

            #endregion
        }

        public long GetNextGameID()
        {
            #region GetNextGameID

            IEnumerable<Game> games = this.GetAll();
            long nextID = games.Count() == 0 ? 1 : games.Max(m => m.GameID) + 1;

            return nextID;

            #endregion
        }

        public void Add(Game movie)
        {
            #region Add

            try
            {
                _repository.Create(movie);
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            #endregion
        }

        public void Update(Game movie)
        {
            #region Update

            try
            {
                _repository.Update(movie);
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            #endregion
        }

        public void Delete(long ID)
        {
            #region Delete

            try
            {
                Game gameToDelete = _repository.GetByKey(ID);
                if (gameToDelete == null)
                    throw new ArgumentNullException("Not found");

                _repository.Delete(gameToDelete);
                _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            #endregion
        }
    }
}
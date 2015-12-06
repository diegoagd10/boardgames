using Boardgames.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Boardgames.Repository
{
    public class GameRepository : XmlRepository<Game, long>, IGameRepository
    {
        public GameRepository(XDocument document)
            : base(document)
        {

        }
    }
}
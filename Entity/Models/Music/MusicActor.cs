using Entity.Models.MusicEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models.Music
{
    public class MusicActor
    {
        public int MusicId { get; set; }
        public Entity.Models.MusicEntity.Music Music { get; set; }

        public int ActorId { get; set; }
        public Actor Actor { get; set; }
    }
}

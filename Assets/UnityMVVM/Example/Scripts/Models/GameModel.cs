using System.Collections.Generic;

using UnityMvvm;

namespace MVVMExample.Models
{
    public class GameModel : Model
    {
        public List<Draught> Blacks = new List<Draught>();
        public List<Draught> Whites = new List<Draught>();
    }
}

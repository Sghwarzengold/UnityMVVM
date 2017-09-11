using System.Collections.Generic;

using UnityMvvm;

namespace MVVMExample.Models
{
    public class GameModel : Model
    {
        public List<Man> Blacks = new List<Man>();
        public List<Man> Whites = new List<Man>();
    }
}

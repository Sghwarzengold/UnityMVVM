using MVVMExample.Models;
using UnityMvvm;

namespace MVVMExample.ViewModels
{
    public class DraughtViewModel : ViewModel
    {
        private Draught model;

        public DraughtViewModel(Draught model)
        {
            this.model = model;
        }

        public int X
        {
            get { return model.X; }
            set
            {
                model.X = value;
                NotifySubscribers();
            }
        }

        public int Y
        {
            get { return model.Y; }
            set
            {
                model.Y = value;
                NotifySubscribers();
            }
        }
    }
}

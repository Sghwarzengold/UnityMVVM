using MVVMExample.Models;
using UnityMvvm;

namespace MVVMExample.ViewModels
{
    public class ManViewModel : ViewModel
    {
        private Man model;

        public ManViewModel(Man model)
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

using Dashboard_AB_System.Model;
using Dashboard_AB_System.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dashboard_AB_System.ViewModel
{
     class About_UsVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        public bool About_Us
        {
            get { return _pageModel.LocationStatus; }
            set { _pageModel.LocationStatus = value; OnPropertyChanged(); }
        }

        public About_UsVM()
        {
            _pageModel = new PageModel();
            About_Us = true;
        }

        private RelayCommand personal_DetailsCommand;

        public ICommand Personal_DetailsCommand
        {
            get
            {
                if (personal_DetailsCommand == null)
                {
                    personal_DetailsCommand = new RelayCommand(Personal_Details);
                }

                return personal_DetailsCommand;
            }
        }

        private void Personal_Details(object commandParameter)
        {
        }
    }
}

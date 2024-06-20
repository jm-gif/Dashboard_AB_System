using Dashboard_AB_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_AB_System.ViewModel
{
    class PostVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        public bool Post
        {
            get { return _pageModel.LocationStatus; }
            set { _pageModel.LocationStatus = value; OnPropertyChanged(); }
        }

        public PostVM()
        {
            _pageModel = new PageModel();
            Post = true;
        }
    }
}

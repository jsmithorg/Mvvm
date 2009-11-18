using JSmith.Mvvm.ViewModel;

namespace JSmith.Mvvm.View
{
    public interface IView : ILocatable
    {
        IViewModel ViewModel { get; set; }

    }//end interface

}//end namespace

public interface IUIController<TViewModel> : IUIController
{
    void InjectViewModel(TViewModel viewModel);
}

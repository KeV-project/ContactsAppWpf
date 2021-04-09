using ContactsAppViewModel.WindowServices;

namespace ContactsAppView.WindowServices
{
    /// <summary>
	/// Класс <see cref="AboutWindowService"/> 
	/// предоставляет свойства и методы для 
	/// работы с окном <see cref="AboutWindow"/>
	/// </summary>
	public class AboutWindowService: IWindowService
	{
        /// <summary>
        /// Запускаемое окно
        /// </summary>
        private AboutWindow _aboutWindow;
        
        /// <summary>
        /// Выполняет запуск окна
        /// </summary>
        /// <param name="dataContext">ViewModel окна</param>
        public void ShowDialog(object dataContext)
        {
            _aboutWindow = new AboutWindow
            {
                DataContext = dataContext
            };
            _aboutWindow.ShowDialog();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsAppViewModel.WindowServices;
using ContactsAppViewModel.Commands;

namespace ContactsAppView.WindowServices
{
    /// <summary>
	/// Класс <see cref="AboutWindowService"/> 
	/// предоставляет viewModel свойства и методы для 
	/// работы с окном <see cref="AboutWindow"/>
	/// </summary>
	public class AboutWindowService: IWindowService
	{
        /// <summary>
        /// Запускаемое окно
        /// </summary>
        private AboutWindow _aboutWindow;

        //TODO: Команды не проинициализированы и результат диалога не используется. Так должно быть? +

        /// <summary>
        /// Выполняет запуск окна
        /// </summary>
        /// <param name="dataContext">ViewModel окна</param>
        public void ShowDialog(object dataContext)
        {
            _aboutWindow = new AboutWindow();
            _aboutWindow.DataContext = dataContext;
            _aboutWindow.ShowDialog();
        }
    }
}

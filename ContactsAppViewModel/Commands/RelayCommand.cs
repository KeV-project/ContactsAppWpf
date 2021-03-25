using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ContactsAppViewModel.Commands
{
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Хранит логику команды
        /// </summary>
        private Action<object> _execute;

        /// <summary>
        /// Хранит логику метода, реализующего проверку
        /// команды на возвожное выполнение
        /// </summary>
        private Func<object, bool> _canExecute;

        /// <summary>
        /// Событие, возникающее при изменении команды
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        /// <summary>
        /// Инициализирует логику команду и 
        /// метод для проверки ее включения
        /// </summary>
        /// <param name="execute">Метод, содержащий логику команды</param>
        /// <param name="canExecute">Метод, содержащий логику проверки 
        /// команды на достувность выполнения</param>
        public RelayCommand(Action<object> execute,
            Func<object, bool> canExecute = null)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>Возвращает true, если команда включена 
        /// и доступна для использования, и false, 
        /// если команда отключена</returns>
        public bool CanExecute(object parameter)
        {
            return this._canExecute == null ||
                this._canExecute(parameter);
        }

        /// <summary>
        /// Запускает команду на выполнение
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        public void Execute(object parameter)
        {
            this._execute(parameter);
        }
    }
}

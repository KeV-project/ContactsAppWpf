﻿using System;
using System.Windows.Input;

namespace ContactsAppViewModel.Commands
{
    /// <summary>
    /// Класс <see cref="RelayCommand"/> предназначен для
    /// создания команд
    /// </summary>
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
        /// команды на готовность к выполнения</param>
        public RelayCommand(Action<object> execute,
            Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Выполняет проверку команды на возможное выполнение
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        /// <returns>Возвращает true, если команда включена 
        /// и доступна для использования, и false, 
        /// если команда отключена</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        /// Запускает команду на выполнение
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}

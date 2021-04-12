using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using GalaSoft.MvvmLight;

namespace ContactsAppViewModel.ModelViewModels
{
    /// <summary>
    /// Класс <see cref="ModelViewModelBase"/> предназначен для 
    /// проведения валидации данных 
    /// </summary>
	public abstract class ModelViewModelBase : ViewModelBase, 
        INotifyDataErrorInfo
    {
        /// <summary>
        /// Хранит свойства объекта и соответствующие 
        /// им сообщения об ошибках
        /// </summary>
        private Dictionary<string, List<string>> _errors =
            new Dictionary<string, List<string>>();

        /// <summary>
        /// Событие, возникающее при изменении 
        /// списка ошибок валидации объекта
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Возвращает перечень сообщений об ошибках валидации свойства
        /// </summary>
        /// <param name="propertyName">Валидируемое свойство</param>
        /// <returns></returns>
        public IEnumerable GetErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                return _errors[propertyName];
            }
            return null;
        }

        /// <summary>
        /// Возвращает true, если объект имеет ошибки валидации,
        /// в противном случае возвращает false
        /// </summary>
        public bool HasErrors
        {
            get
            {
                return _errors.Count > 0;
            }
        }

        /// <summary>
        /// Возвращает true, если объект успешно прошел валидацию,
        /// в противном случае возвращает false
        /// </summary>
        public bool IsValid => !HasErrors;

        /// <summary>
        /// Добавляет новые сведения об ошибке валидации в список объекта
        /// </summary>
        /// <param name="propertyName">Свойство, 
        /// не прошедшее валидацию</param>
        /// <param name="error">Сообщение об ошибке</param>
        public void AddError(string propertyName, string error)
        {
            _errors[propertyName] = new List<string>() { error };
            NotifyErrorsChanged(propertyName);
            RaisePropertyChanged(nameof(IsValid));
        }

        /// <summary>
        /// Удаляет ошибку валидации из списка
        /// </summary>
        /// <param name="propertyName">Свойство, прошедшее валидацию</param>
        public void RemoveError(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
            }
            NotifyErrorsChanged(propertyName);
            RaisePropertyChanged(nameof(IsValid));
        }

        /// <summary>
        /// Вызывает событие, оповещающее View об 
        /// изменении списка ошибок валидации
        /// </summary>
        /// <param name="propertyName"></param>
        public void NotifyErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(
                propertyName));
        }
    }
}

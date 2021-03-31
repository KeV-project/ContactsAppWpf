using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ContactsAppModel
{
    //TODO: XML комментарии?
    //TODO: название не отражает назначение класса
    //TODO: почему не абстрактный?
	public class ModelBase: INotifyPropertyChanged, INotifyDataErrorInfo
    {
        /// <summary>
        /// Реализует привязку пользовательских данных к элементам управления
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Сообщает View об изменении пользовательских данных
        /// </summary>
        /// <param name="prop">Измененное свойство</param>
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            //TODO: лучше через .?
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

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
        /// Возвращает перечень сообщений об ошибках валидации объекта
        /// </summary>
        /// <param name="propertyName"></param>
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
        public bool IsValid
        {
            get
            {
                return !HasErrors;
            }
        }

        /// <summary>
        /// Добавляет новую сведения об ошибке валидации в список объекта
        /// </summary>
        /// <param name="propertyName">Свойство, 
        /// не прошедшее валидацию</param>
        /// <param name="error">Ошибка валидации</param>
        public void AddError(string propertyName, string error)
        {
            _errors[propertyName] = new List<string>() { error };
            NotifyErrorsChanged(propertyName);
            //TODO: nameof
            OnPropertyChanged("IsValid");
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
            //TODO: nameof
            OnPropertyChanged("IsValid");
        }

        /// <summary>
        /// Вызывает событие, оповещающее View об 
        /// изменении списка ошибок валидации
        /// </summary>
        /// <param name="propertyName"></param>
        public void NotifyErrorsChanged(string propertyName)
        {
            //TODO: лучше через .?
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this,
                    new DataErrorsChangedEventArgs(propertyName));
            }
        }
    }
}

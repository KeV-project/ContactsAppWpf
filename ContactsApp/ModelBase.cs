using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ContactsAppModel
{
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
        /// 
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Возвращает перечень сообщений об
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

        public bool HasErrors
        {
            get
            {
                return _errors.Count > 0;
            }
        }

        public bool IsValid
        {
            get
            {
                return !HasErrors;
            }

        }

        public void AddError(string propertyName, string error)
        {
            _errors[propertyName] = new List<string>() { error };
            NotifyErrorsChanged(propertyName);
        }

        public void RemoveError(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
            }
            NotifyErrorsChanged(propertyName);
        }

        public void NotifyErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this,
                    new DataErrorsChangedEventArgs(propertyName));
            }
        }
    }
}

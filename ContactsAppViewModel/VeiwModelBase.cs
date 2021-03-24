using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;


namespace ContactsAppViewModel
{
	public class VeiwModelBase: INotifyPropertyChanged, INotifyDataErrorInfo
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
                return !this.HasErrors; 
            }

        }

        public void AddError(string propertyName, string error)
        {
            this._errors[propertyName] = new List<string>() { error };
            this.NotifyErrorsChanged(propertyName);
        }

        public void RemoveError(string propertyName)
        {
            if (this._errors.ContainsKey(propertyName))
			{
                this._errors.Remove(propertyName);
            }
            this.NotifyErrorsChanged(propertyName);
        }

        public void NotifyErrorsChanged(string propertyName)
        {
            if (this.ErrorsChanged != null)
			{
                this.ErrorsChanged(this, 
                    new DataErrorsChangedEventArgs(propertyName));
            }
        }
    }
}

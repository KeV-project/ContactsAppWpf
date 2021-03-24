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
    public class VeiwModelBase : INotifyPropertyChanged
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
    }
}

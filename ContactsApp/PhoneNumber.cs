using System;

namespace ContactsAppModel
{
    /// <summary>
    /// Класс <see cref="PhoneNumber"/> хранит информацию 
    /// о номере телефона контакта
    /// </summary>
    public class PhoneNumber: ModelBase, IComparable<PhoneNumber>
    {
        /// <summary>
        /// Поле предназначено для хранения номера телефона контакта
        /// </summary>
        private long _number;

        /// <summary>
        /// Возвращает и создает номер телефона контакта
        /// Номер телефона должен начинаться с цифры "7" и состоять из 11 цифр
        /// </summary>
        public long Number
        {
            get
            {
                return _number;
            }
            set
            {
				try
				{
                    ValueValidator.AssertRussianPhoneNumber(value,
                        "номер телефона");
                    RemoveError("Number");
                }
                catch(ArgumentException e)
				{
                    AddError("Number", e.Message);
				}
                
                _number = value;

                OnPropertyChanged("Number");
            }
        }

        /// <summary>
        /// Инициализирует объект класса <see cref="PhoneNumber">
        /// </summary>
        public PhoneNumber()
        {
            Number = 70000000000;
        }

        /// <summary>
        /// Инициализирует объект класса <see cref="PhoneNumber">
        /// </summary>
        /// <param name="number">Номер телефона контакта</param>
        public PhoneNumber(long number)
        {
            Number = number;
        }

        /// <summary>
        /// Сравнивает два объекта класса
        /// </summary>
        /// <param name="number">Объект класса, который будет сравниваться
        /// с текущим объектом</param>
        /// <returns>Возвращаемое значение показывает
        /// рывны ли сравниваемые объекты класса.
        /// Если объекты равны, возвращает 1.
        /// Если объекты не равны, позвращает 0</returns>
        public int CompareTo(PhoneNumber number)
        {
            if (this.Number == number.Number)
            {
                return 1;
            }
            return 0;
        }
    }
}

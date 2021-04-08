using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ContactsAppModel
{
    /// <summary>
    /// Класс <see cref="Project"/> предназначен для хранения
    /// пользовательской информации приложения
    /// </summary>
    [DataContract]
    public class Project : IEquatable<Project>
    {
        /// <summary>
        /// Хранит все контакты пользователя
        /// </summary>
        [DataMember]
        private List<Contact> _contacts;

        /// <summary>
        /// Позволяет получить или добавить контакт в список 
        /// по указанному индуксу
        /// </summary>
        /// <param name="index">Индекс возвращаемого контакта
        /// или позиция для добавления контакта в список</param>
        /// <returns>Возвращает контакт по указанному индексу</returns>
        public Contact this[int index] => _contacts[index];

        /// <summary>
        /// Инициализирует объект класса <see cref="Project"/>
        /// </summary>
        public Project()
        {
            _contacts = new List<Contact>();
        }

        /// <summary>
        /// Возвращает количество контактов в списке
        /// </summary>
        /// <returns>Значение показывает, скоько контактов в списке</returns>
        public int ContactsCount => _contacts.Count;

        /// <summary>
        /// Метод добавляет новый контакт в список проекта
        /// и сортирует список по фамилиям контактов
        /// </summary>
        /// <param name="newContact">Добавляемый контакт</param>
        public void AddContact(Contact newContact)
        {
            if (newContact != null)
            {
                _contacts.Add(newContact);
                SortContacts();
            }
            else
            {
                throw new ArgumentException("ИСКЛЮЧЕНИЕ: попытка " +
                    "добавить в список значение null");
            }
        }

        /// <summary>
        /// Метод удаляет контакт из списка
        /// </summary>
        /// <param name="removableContact">Удаляемый контакт</param>
        public void RemoveContact(Contact removableContact)
        {
            if (!_contacts.Remove(removableContact))
            {
                throw new ArgumentException("ИСКЛЮЧЕНИЕ: контакт " +
                    "не существует");
            }
        }
        
        /// <summary>
        /// Сортирует список контактов по фамилии в 
        /// алфавитном порядке
        /// </summary>
        public void SortContacts()
		{
            _contacts.Sort();
        }

        public bool Equals(Project other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(_contacts, other._contacts);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Project) obj);
        }

        public override int GetHashCode()
        {
            return (_contacts != null ? _contacts.GetHashCode() : 0);
        }
    }
}

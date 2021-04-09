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
        public List<Contact> Contacts { get; set; }

        /// <summary>
        /// Позволяет получить или добавить контакт в список 
        /// по указанному индуксу
        /// </summary>
        /// <param name="index">Индекс возвращаемого контакта
        /// или позиция для добавления контакта в список</param>
        /// <returns>Возвращает контакт по указанному индексу</returns>
        public Contact this[int index] => Contacts[index];

        /// <summary>
        /// Инициализирует объект класса <see cref="Project"/>
        /// </summary>
        public Project()
        {
            Contacts = new List<Contact>();
        }

        /// <summary>
        /// Возвращает количество контактов в списке
        /// </summary>
        /// <returns>Значение показывает, 
        /// скоько контактов в списке</returns>
        public int ContactsCount => Contacts.Count;

        /// <summary>
        /// Метод добавляет новый контакт в список проекта
        /// </summary>
        /// <param name="newContact">Добавляемый контакт</param>
        public void AddContact(Contact newContact)
        {
            if (newContact != null)
            {
                Contacts.Add(newContact);
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
            if (!Contacts.Remove(removableContact))
            {
                throw new ArgumentException("ИСКЛЮЧЕНИЕ: контакт " +
                    "не существует");
            }
        }

        /// <summary>
        /// Определяет равенство двух объектов.
        /// класса <see cref="Project"/>
        /// </summary>
        /// <param name="other">Сравниваемый объект</param>
        /// <returns>Возвращает true, если объекты равны.
        /// Иначе возвращает false</returns>
        public bool Equals(Project other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(Contacts, other.Contacts);
        }

        /// <summary>
        /// Определяет равенство двух объектов.
        /// </summary>
        /// <param name="obj">Сравниваемый объект</param>
        /// <returns>Возвращает true, если объекты равны.
        /// Иначе возвращает false</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Project) obj);
        }

        /// <summary>
        /// Метод предназначен для генерации хеша
        /// </summary>
        /// <returns>Возвращает хеш объекта
        /// класса <see cref="Project"/></returns>
        public override int GetHashCode()
        {
            return (Contacts != null ? Contacts.GetHashCode() : 0);
        }
    }
}

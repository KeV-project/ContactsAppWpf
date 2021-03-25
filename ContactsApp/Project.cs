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
    public class Project : IComparable<Project>
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
        /// Инициализирует объект класса <see cref="Project">
        /// </summary>
        public Project()
        {
            _contacts = new List<Contact>();
        }

        /// <summary>
        /// Возвращает количество контактов в списке
        /// </summary>
        /// <returns>Значение показывает, скоько контактов в списке</returns>
        public int GetContactsCount()
        {
            return _contacts.Count;
        }

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
                _contacts.Sort();
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
        /// Метод отбирает все контакты,
        /// имя и фамилия которых содержит подстроку
        /// </summary>
        /// <param name="text">Подстрока, наличие которой определяется
        /// в имени и фамилии контакта</param>
        /// <returns>Список контактов, имя и фамилия которых содержит подстроку</returns>
        public List<Contact> GetContactsWithText(string text)
        {
            List<Contact> contactsWithText = new List<Contact>();

            foreach (Contact currentContact in _contacts)
            {
                if ((currentContact.LastName + " "
                    + currentContact.FirstName).Contains(text))
                {
                    contactsWithText.Add(currentContact);
                }
            }

            return contactsWithText;
        }

        /// <summary>
        /// Метод отбирает все контакты, 
        /// у которых сегодня день рождения
        /// </summary>
        /// <returns>Список контактов, 
        /// у которых сегодня день рождения</returns>
        public List<Contact> GetAllBirthContacts()
        {
            List<Contact> birthCotacts = new List<Contact>();

            foreach (Contact currentContact in _contacts)
            {
                if (currentContact.BirthDate.Day == DateTime.Today.Day
                    && currentContact.BirthDate.Month
                    == DateTime.Today.Month)
                {
                    birthCotacts.Add(currentContact);
                }
            }
            return birthCotacts;
        }

        /// <summary>
        /// Метод предназначен для сравнивания объектов
        /// </summary>
        /// <param name="project">Сравниваемый объект</param>
        /// <returns>Возвращает 1, если объекты равны.
        /// Возвращает 0, если объекты не равны<returns>
        public int CompareTo(Project project)
        {
            if (this.GetContactsCount() == project.GetContactsCount())
            {
                for (int i = 0; i < this.GetContactsCount(); i++)
                {
                    if (this[i].FirstName != project[i].FirstName ||
                        this[i].LastName != project[i].LastName ||
                        this[i].Number.Number != project[i].Number.Number ||
                        this[i].Email != project[i].Email ||
                        this[i].BirthDate != project[i].BirthDate)
                    {
                        return 0;
                    }
                }
                return 1;
            }
            return 0;
        }
    }
}

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using laba9modules;
using System.Runtime.CompilerServices; // Это обязательно для [CallerMemberName]
using laba9modules.Services;


namespace laba9modules.ViewModels
{
    public class ContactsListViewModel : INotifyPropertyChanged
    {
        // Список контактов с авто-обновлением UI
        private readonly IDialogService _dialogService;
        public ObservableCollection<Contact> Contacts { get; set; } = new ObservableCollection<Contact>();

        // Свойства для привязки к полям ввода
        private string _newName;
        public string NewName
        {
            get => _newName;
            set
            {
                _newName = value;
                OnPropertyChanged(); // Просто вызываем метод, без параметров
            }
        }

        private string _newPhone;
        public string NewPhone
        {
            get => _newPhone;
            set
            {
                _newPhone = value;
                OnPropertyChanged(); // Просто вызываем метод, без параметров
            }
        }

        public RelayCommand AddCommand { get; }
        public RelayCommand DeleteCommand { get; }

        //public MainViewModel()
        //{
        //    // AddCommand без параметра (использует поля NewName и NewPhone)
        //    AddCommand = new RelayCommand(obj => AddContact(), obj => Validate());

        //    // DeleteCommand с параметром (выбранный контакт из списка)
        //    DeleteCommand = new RelayCommand(obj => DeleteContact(obj as Contact), obj => obj is Contact);
        //}
        public ContactsListViewModel() : this(new DialogService())
        {
        }
        public ContactsListViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            AddCommand = new RelayCommand(obj => AddContact(), obj => Validate());
            DeleteCommand = new RelayCommand(obj => DeleteContact(obj as Contact), obj => obj is Contact);
        }

        //private void AddContact()
        //{
        //    Contacts.Add(new Contact { Name = NewName, Phone = NewPhone });
        //    NewName = string.Empty; // Очистка полей после добавления
        //    NewPhone = string.Empty;
        //}

        private void AddContact()
        {
            // Проверка на дубликаты
            if (Contacts.Any(c => c.Phone == NewPhone))
            {
                _dialogService.ShowWarning("Контакт с таким номером уже существует!");
                return;
            }

            Contacts.Add(new Contact { Name = NewName, Phone = NewPhone });
            _dialogService.ShowInfo("Контакт успешно добавлен.");

            NewName = string.Empty;
            NewPhone = string.Empty;
        }

        //private void DeleteContact(Contact contact)
        //{
        //    if (contact != null) Contacts.Remove(contact);
        //}

        private void DeleteContact(Contact contact)
        {
            if (contact != null)
            {
                // Запрос подтверждения
                if (_dialogService.ShowConfirmation($"Удалить контакт {contact.Name}?"))
                {
                    Contacts.Remove(contact);
                }
            }
        }

        // Валидация данных
        private bool Validate()
        {
            if (string.IsNullOrWhiteSpace(NewName)) return false;
            if (string.IsNullOrWhiteSpace(NewPhone)) return false;
            // Регулярка: +7 и 10 цифр ИЛИ просто 11 цифр (упрощенно)
            return Regex.IsMatch(NewPhone, @"^(\+7|8)?\d{10}$");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        // Убедитесь, что в скобках написано именно (string prop)
        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}

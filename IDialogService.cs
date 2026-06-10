using System.Windows;

namespace laba9modules.Services
{
    public interface IDialogService
    {
        void ShowInfo(string message);
        void ShowWarning(string message);
        bool ShowConfirmation(string message);
    }

    public class DialogService : IDialogService
    {
        public void ShowInfo(string message) =>
            MessageBox.Show(message, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

        public void ShowWarning(string message) =>
            MessageBox.Show(message, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);

        public bool ShowConfirmation(string message) =>
            MessageBox.Show(message, "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
    }
}

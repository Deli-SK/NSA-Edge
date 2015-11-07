namespace NSA.WPF.Services
{
    public interface IMessageService
    {
        void ShowError(string error);
        void ShowInfo(string message);
        void ShowWarning(string warning);
    }
}

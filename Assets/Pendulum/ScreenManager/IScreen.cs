namespace Pendulum.Screens
{
    public interface IScreen
    {
        bool IsActive();
        void SetActive(bool isActive);
        string GetName();
    }
}
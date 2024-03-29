namespace Clones.Services
{
    public interface ILocalization : IService
    {
        string GetIsoLanguage();
        string GetLeanLanguage();
    }
}
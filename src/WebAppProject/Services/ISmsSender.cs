using System.Threading.Tasks;

namespace WebAppProject.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}

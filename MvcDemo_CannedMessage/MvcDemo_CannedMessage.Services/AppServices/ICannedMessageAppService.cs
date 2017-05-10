using System.Linq;
using MvcDemo_CannedMessage.Entity;

namespace MvcDemo_CannedMessage.Services.AppServices
{
    public interface ICannedMessageAppService
    {
        void Delete(CannedMessage message);
        CannedMessage Find(int id);
        IQueryable<CannedMessage> FindAll();
        void Update(CannedMessage message);
        void Insert(CannedMessage message);
    }
}
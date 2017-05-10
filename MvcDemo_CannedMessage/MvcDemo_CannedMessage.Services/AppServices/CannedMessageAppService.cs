using MvcDemo_CannedMessage.Entity;
using MvcDemo_CannedMessage.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo_CannedMessage.Service.AppServices
{
    public class CannedMessageAppService : ICannedMessageAppService
    {
        private ICannedMessageRepository _repository;
        public CannedMessageAppService(ICannedMessageRepository repository)
        {
            _repository = repository;
        }

        public CannedMessage Find(int id)
        {
            return _repository.Find(id);
        }

        public IQueryable<CannedMessage> FindAll()
        {
            return _repository.FindAll().OrderByDescending(t=>t.Id);
        }

        public void Delete(CannedMessage message)
        {
            _repository.Delete(message);
        }

        public void Update(CannedMessage message)
        {
            _repository.Update(message);
        }

        public void Insert(CannedMessage message)
        {
            _repository.Insert(message);
        }
    }
}

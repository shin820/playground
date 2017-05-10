using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcDemo_CannedMessage.EF;
using X.PagedList;
using MvcDemo_CannedMessage.Models;
using MvcDemo_CannedMessage.Entity;
using MvcDemo_CannedMessage.Repository.Repositories;

namespace MvcDemo_CannedMessage.Controllers
{
    public class CannedMessagesController : Controller
    {
        private ICannedMessageRepository _repository;

        public CannedMessagesController(ICannedMessageRepository repository)
        {
            _repository = repository;
        }

        // GET: CannedMessages
        public ActionResult Index(CannedMessageListInput input)
        {
            var cannedMessages = _repository.FindAll().OrderByDescending(t => t.Id);
            var pageList = cannedMessages.ToPagedList(input.Page.Value, input.PageSize.Value);
            ViewBag.CurrentPageItems = pageList.Count();
            return View(pageList);
        }

        // GET: CannedMessages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CannedMessage cannedMessage = _repository.Find(id.Value);
            if (cannedMessage == null)
            {
                return HttpNotFound();
            }
            return View(cannedMessage);
        }

        // GET: CannedMessages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CannedMessages/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Message,Shortcuts")] CannedMessage cannedMessage)
        {
            if (ModelState.IsValid)
            {
                _repository.Insert(cannedMessage);
                return RedirectToAction("Index");
            }

            return View(cannedMessage);
        }

        // GET: CannedMessages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CannedMessage cannedMessage = _repository.Find(id.Value);
            if (cannedMessage == null)
            {
                return HttpNotFound();
            }
            return View(cannedMessage);
        }

        // POST: CannedMessages/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Message,Shortcuts")] CannedMessage cannedMessage)
        {
            CannedMessage message = _repository.Find(cannedMessage.Id);
            if (message == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                message.Name = cannedMessage.Name;
                message.Message = cannedMessage.Message;
                message.Shortcuts = cannedMessage.Shortcuts;
                _repository.Update(message);
                return RedirectToAction("Index");
            }
            return View(cannedMessage);
        }

        // GET: CannedMessages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CannedMessage cannedMessage = _repository.Find(id.Value);
            if (cannedMessage == null)
            {
                return HttpNotFound();
            }
            return View(cannedMessage);
        }

        // POST: CannedMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CannedMessage cannedMessage = _repository.Find(id);
            _repository.Delete(cannedMessage);
            return RedirectToAction("Index");
        }
    }
}

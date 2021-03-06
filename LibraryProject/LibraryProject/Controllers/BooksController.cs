﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryProject.DAL;
using LibraryProject.Models;

namespace LibraryProject.Controllers
{
    public class BooksController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Books
        [AllowAnonymous]
        public ActionResult Index()
        {
            var books = unitOfWork.BookRepository.Get();
            return View(books);
        }

        [Auth(Code ="libraryadmin")]
        public ActionResult BooksManage()
        {
            var books = unitOfWork.BookRepository.Get();
            return View(books);
        }

        // GET: Books/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Book book = db.Books.Find(id);
            Book book = unitOfWork.BookRepository.GetByID(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/
        [Auth(Code ="libraryadmin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Auth(Code ="libraryadmin")]
        public ActionResult Create([Bind(Include = "ID,Name,IsAvailable,Isbn,Summary,Author,Type,Price,PublishTime,Location")] Book book)
        {
            if (ModelState.IsValid)
            {
                // db.Books.Add(book);
                // db.SaveChanges();
                unitOfWork.BookRepository.Insert(book);
                unitOfWork.Save();
                return RedirectToAction("BooksManage");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        [Auth(Code ="libraryadmin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Book book = db.Books.Find(id);
            Book book = unitOfWork.BookRepository.GetByID(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Auth(Code ="libraryadmin")]
        public ActionResult Edit([Bind(Include = "ID,Name,IsAvailable,Isbn,Summary,Author,Type,Price,PublishTime,Location")] Book book)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.context.Entry(book).State = EntityState.Modified;
                // db.Entry(book).State = EntityState.Modified;
                // db.SaveChanges();
                unitOfWork.Save();
                return RedirectToAction("BooksManage");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        [Auth(Code ="libraryadmin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Book book = db.Books.Find(id);
            Book book = unitOfWork.BookRepository.GetByID(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Auth(Code ="libraryadmin")]
        public ActionResult DeleteConfirmed(int id)
        {
            //Book book = db.Books.Find(id);
            //db.Books.Remove(book);
            //db.SaveChanges();
            var book = unitOfWork.BookRepository.GetByID(id);
            unitOfWork.BookRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

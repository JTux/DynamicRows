using DynamicRows.Web.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicRows.Web.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            var context = new ApplicationDbContext();

            var model = context.Defaults.Select(d => new DefaultListItem
            {
                Id = d.Id,
                Name = d.Name,
                ItemCount = d.Items.Count
            });

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DefaultCreate model)
        {
            if (!ModelState.IsValid)
                return View(ModelState);

            var defaultEntity = new DefaultEntity
            {
                Name = model.Name,
                Items = model.Items?.Select(i => new ItemEntity { Name = i.Name, Number = i.Number }).ToList()
            };

            var context = new ApplicationDbContext();
            context.Defaults.Add(defaultEntity);

            var itemCount = model.Items is null ? 0 : model.Items.Count;
            var expectedChangeCount = 1 + itemCount;
            if (context.SaveChanges() != expectedChangeCount)
                return View(model);

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Detail(int? id)
        {
            if (id is null)
                return RedirectToAction(nameof(Index));

            var context = new ApplicationDbContext();
            var detail = context.Defaults.Find(id);
            if (detail is null)
                return RedirectToAction(nameof(Index));

            var model = new DefaultDetail
            {
                Id = detail.Id,
                Name = detail.Name,
                Items = detail.Items.Select(i =>
                    new ItemDetail
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Number = i.Number
                    }).ToList()
            };

            return View(model);
        }

        public ActionResult DeleteDefault(int? id)
        {
            if (id is null)
                return RedirectToAction(nameof(Index));

            var context = new ApplicationDbContext();
            var entity = context.Defaults.Find(id);
            if (entity is null)
                return RedirectToAction(nameof(Index));

            context.Defaults.Remove(entity);
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public ActionResult AddItems(int? id)
        {
            if (id is null)
                return RedirectToAction(nameof(Index));

            var context = new ApplicationDbContext();
            var entity = context.Defaults.Find(id);
            if (entity is null)
                return RedirectToAction(nameof(Index));

            var model = new DefaultItemCreate
            {
                Id = entity.Id,
                Name = entity.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddItems(DefaultItemCreate model)
        {
            if (!ModelState.IsValid)
                return View(ModelState);

            var context = new ApplicationDbContext();
            var entity = context.Defaults.Find(model.Id);
            if (entity is null)
                return RedirectToAction(nameof(Index));

            entity.Items.AddRange(model.Items.Select(i => new ItemEntity { Name = i.Name, Number = i.Number }));
            if (context.SaveChanges() != model.Items?.Count)
                return View(model);

            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteItem(int id)
        {
            var context = new ApplicationDbContext();
            var entity = context.Items.Find(id);
            if (entity != null)
            {
                context.Items.Remove(entity);
                context.SaveChanges();
            }

            return RedirectToAction(nameof(Detail), new { id = entity.DefaultId });
        }
    }
}
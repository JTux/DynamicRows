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

            var defaultEntity = new Default
            {
                Name = model.Name,
                Items = model.Items?.Select(i => new Item { Name = i.Name, Number = i.Number }).ToList()
            };

            var context = new ApplicationDbContext();
            context.Defaults.Add(defaultEntity);

            var itemCount = (model.Items?.Count).HasValue ? model.Items.Count : 0;
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
    }

    public class DefaultCreate
    {
        public string Name { get; set; }
        public List<ItemCreate> Items { get; set; }
    }

    public class ItemCreate
    {
        public string Name { get; set; }
        public int Number { get; set; }
    }

    public class DefaultListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ItemCount { get; set; }
    }

    public class DefaultDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ItemDetail> Items { get; set; }
    }

    public class ItemDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
    }
}
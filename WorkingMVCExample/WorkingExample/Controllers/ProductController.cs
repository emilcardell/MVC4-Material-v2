using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Raven.Client;
using WorkingExample.Models;

namespace WorkingExample.Controllers
{
    public class ProductController : Controller
    {
        private readonly IDocumentSession _documentSession;

        public ProductController(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public ActionResult Edit(ProductInputModel productInputModel, bool? isNotOk)
        {
            if(isNotOk != null && isNotOk.Value)
                return View(productInputModel);

            if(string.IsNullOrEmpty(productInputModel.ProductId))
                return View();
                

            var productToEdit = _documentSession.Load<Product>(productInputModel.ProductId);
            var productModel = new ProductInputModel();
            productModel.ProductId = productToEdit.Id;
            productModel.Name = productToEdit.Name;
            productModel.Price = productToEdit.Price;
            productModel.ImageId = productToEdit.ImageId.ToString();
            
            if(productToEdit.Tags.Count() > 1)
                productModel.Tag1 = productToEdit.Tags[0];

            if (productToEdit.Tags.Count() > 2)
                productModel.Tag2 = productToEdit.Tags[1];

            if (productToEdit.Tags.Count() > 3)
                productModel.Tag3 = productToEdit.Tags[2];

            return View(productModel);
        }

        [HttpPost]
        public ActionResult Save(ProductInputModel productInputModel)
        {
            if (!ModelState.IsValid)
                Edit(productInputModel, true);

            var productToEdit = new Product();
            productToEdit.Id = productInputModel.ProductId;
            productToEdit.Name = productInputModel.Name;
            productToEdit.Price = productInputModel.Price;
            productToEdit.ImageId = int.Parse(productInputModel.ImageId);

            if (!string.IsNullOrEmpty(productInputModel.Tag1))
                productToEdit.Tags.Add(productInputModel.Tag1);

            if (!string.IsNullOrEmpty(productInputModel.Tag2))
                productToEdit.Tags.Add(productInputModel.Tag2);

            if (!string.IsNullOrEmpty(productInputModel.Tag3))
                productToEdit.Tags.Add(productInputModel.Tag3);

            productToEdit.UrlToSmallImage = "http://lorempixel.com/300/200/nature/" + productInputModel.ImageId + "/";
            productToEdit.UrlToLargeImage = "http://lorempixel.com/900/600/nature/" + productInputModel.ImageId + "/";

            _documentSession.Store(productToEdit);
            _documentSession.SaveChanges();


            return Display(productToEdit.Id);

        }

        public ActionResult Display(string productId)
        {
            var productToEdit = _documentSession.Load<Product>(productId);
            return View("Display", productToEdit);
        }

    }

    public class ProductInputModel
    {
        public string ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public string Tag1 { get; set; }
        public string Tag2 { get; set; }
        public string Tag3 { get; set; }
        public string ImageId { get; set; }
    }
}

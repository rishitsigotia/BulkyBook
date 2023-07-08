using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
             _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;

            return View(objCategoryList);
        }

		//EDIT GET
		public IActionResult Edit(int? id)
		{
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var CategoryFromdb = _db.Categories.Find(id); // as we have primary key this will be most suited
            //var CategoryFromFirst = _db.Categories.FirstOrDefault(c => c.Id == id);  These are also method to retrive id // it will return first element of the list
            //var CategoryFromSingle = _db.Categories.SingleOrDefault()  // it will not throw exception it will be just empty if element not found , it will throw if there are more than one element
            
            if(CategoryFromdb == null)
            {
                return NotFound();
            }

			return View(CategoryFromdb); //if id is found we will send it to view
		}
		//EDIT POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.TryAddModelError("Name", "The Name and display order cannot be same");
			}
			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj);
				_db.SaveChanges();
                TempData["success"] = "Category Edited Successfully";
				return RedirectToAction("Index");
			}
			return View(obj);
		}





		//CREATE GET
		public IActionResult Create()
        {

            return View();
        }
        //CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.TryAddModelError("Name","The Name and display order cannot be same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //DELETE GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var CategoryFromdb = _db.Categories.Find(id); 

            if (CategoryFromdb == null)
            {
                return NotFound();
            }

            return View(CategoryFromdb); 
        }
        //DELETE POST
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);

            if (obj == null)
                return NotFound();
            
               _db.Categories.Remove(obj);
               _db.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
          
        }

    }
    
}

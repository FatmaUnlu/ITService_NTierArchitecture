using DevExtreme.AspNet.Data;
using ItServiceApp.Business.Repository;
using ItServiceApp.Core.Entities;
using ItServiceApp.Core.ViewModels;
using ItServiceApp.Data;
using ItServiceApp.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace ItServiceApp.Areas.Admin.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SubscriptionTypeApiController : Controller
    {
        //private readonly MyContext _dbContext;

        //public SubscriptionTypeApiController(MyContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}
        private readonly SubscriptionTypeRepo _repo;

        public SubscriptionTypeApiController(SubscriptionTypeRepo repo)
        {
            _repo = repo;
        }

        #region Crud

        [HttpGet]
        public IActionResult Get(DataSourceLoadOptions options)
        {
            //var data = _dbContext.SubscriptionTypes;
            var data = _repo.Get();

            return Ok(DataSourceLoader.Load(data, options));
        }

        [HttpGet]

        //neye göre gruplanacağı vs. loadOptions içerisinde
        public IActionResult Detail(Guid id, DataSourceLoadOptions loadOptions)
        {
            //var data = _dbContext.SubscriptionTypes.Where(x => x.Id == id);
            var data = _repo.Get(x => x.Id == id);
            return Ok(DataSourceLoader.Load(data, loadOptions));
        }

        [HttpPost]
        public IActionResult Insert(string values)
        {
            var data = new SubscriptionType();
            JsonConvert.PopulateObject(values, data);

            if (!TryValidateModel(data))
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = ModelState.ToFullErrorString()

                });
            try
            {
                var result = _repo.Insert(data);
            }
            catch
            {
                return BadRequest(new JsonResponseViewModel
                {
                    IsSuccess = false,
                    ErrorMessage = "Yeni üyelik tipi kaydedilemedi."
                });
            }

            //_dbContext.SubscriptionTypes.Add(data);

            //var result = _dbContext.SaveChanges();
            //if (result == 0)
            //    return BadRequest(new JsonResponseViewModel()
            //    {
            //        IsSuccess = false,
            //        ErrorMessage = "Yeni üyelik tipi kaydedilmedi"
            //    });

            return Ok(new JsonResponseViewModel());
        }

        [HttpPut]
        public IActionResult Update(Guid key, string values)
        {
            //var data = _dbContext.SubscriptionTypes.Find(key);
            var data = _repo.GetById(key);

            if (data == null)
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = ModelState.ToFullErrorString()

                });

            JsonConvert.PopulateObject(values, data);

            if (!TryValidateModel(data))
                return BadRequest(ModelState.ToFullErrorString());

            //var result = _dbContext.SaveChanges();
            var result = _repo.Update(data);
            if (result == 0)
                return BadRequest(new JsonResponseViewModel()
                {
                    IsSuccess = false,
                    ErrorMessage = "Üyelik tipi güncellenemedi"
                });
            return Ok(new JsonResponseViewModel());
        }

        [HttpDelete]
        public IActionResult Delete(Guid key)
        {
            //var data = _dbContext.SubscriptionTypes.Find(key);
             var data = _repo.GetById(key);
            if (data == null)
                return StatusCode(StatusCodes.Status409Conflict, "Üyelik Tipi bulunamadı");

            //_dbContext.SubscriptionTypes.Remove(data);

            //var result = _dbContext.SaveChanges();
            var result = _repo.Delete(data.Id);
            if (result == 0)
                return BadRequest("Silme işlemi başarısız");
            return Ok(new JsonResponseViewModel());
        }
        #endregion

    }
}

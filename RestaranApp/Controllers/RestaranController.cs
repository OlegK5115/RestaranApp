using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaranApp.Dto;
using RestaranApp.Entities;
using RestaranApp.Services;

namespace RestaranApp.Controllers
{
    [Route("[controller]")]
    public class RestaransController : Controller
    {
        private IRestaranService _restaranService;

        private Restaran ParseRestaran(Restaran r)
        {
            r.Id = 0;

            return r;
        }

        private List<Restaran> ParseRestarans(List<Restaran> rlist)
        {
            rlist.ForEach(r => {
                ParseRestaran(r);
            });

            return rlist;
        }

        public RestaransController(IRestaranService rs)
        {
            _restaranService = rs;
        }

        [HttpPost("create")]
        public async Task Create(CreateRestaranDto r)
        {
            await _restaranService.Create(r);
        }

        [HttpPost("update")]
        public async Task Update(UpdateRestaranDto r)
        {
            await _restaranService.Update(r);
        }

        [HttpPost("delete")]
        public async Task Delete(DeleteRestaranDto r)
        {
            await _restaranService.Delete(r.Uuid);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(ParseRestarans(await _restaranService.GetAll()));
        }

        [HttpGet("{uuid}")] // url -- /user/{uuid}
        public async Task<IActionResult> GetByUuid(string uuid)
        {
            var restaran = await _restaranService.GetByUuid(uuid);
            return Json(ParseRestaran(restaran));
        }
    }
}
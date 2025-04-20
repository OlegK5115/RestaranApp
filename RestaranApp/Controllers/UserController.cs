using Microsoft.AspNetCore.Mvc;
using RestaranApp.Dto;
using RestaranApp.Entities;
using RestaranApp.Services;

namespace RestaranApp.Controllers
{
    [Route("[controller]")] // извлекает из названия класса User (необходим постфикс Controller)
    public class UsersController : Controller
    {
        private IUserService _userService;

        private User ParseUser(User u)
        {
            u.Id = 0;
            u.Email = "";

            return u;
        }

        private List<User> ParseUsers(List<User> ulist)
        {
            ulist.ForEach(u => {
                ParseUser(u);
            });

            return ulist;
        }

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create")] // url -- /user/create
        public async Task Create(CreateUserDto u)
        {
            await _userService.Create(u);
        }

        [HttpPost("update")] // url -- /user/create
        public async Task Update(UpdateUserDto u)
        {
            await _userService.Update(u);
        }

        [HttpPost("delete")] // url -- /user/create
        public async Task Delete(DeleteUserDto u)
        {
            await _userService.Delete(u.Uuid);
        }

        [HttpGet] // url -- /user/
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll(); // возвращает List<User>
            return Json(ParseUsers(users)); // метод Json преобразует список в json и возвращает IActionResult
        }

        [HttpGet("{uuid}")] // url -- /user/{uuid}
        public async Task<IActionResult> GetByUuid(string uuid)
        {
            var user = await _userService.GetByUuid(uuid);
            return Json(ParseUser(user));
        }
    }
}

/* Task<IActionResult> говорит о том, что это асинхронная операция, которая возвращает
 * IActionResult (IActionResult может быть view, json, text и другие)
 */
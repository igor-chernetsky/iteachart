using System.Linq;
using System.Web.Mvc;
using Infrastructure.Models;
using Infrastructure.Services;

namespace iteachart.Controllers
{
    [Authorize]
    public class GameController : BaseSecureController
    {
        private IGameService gameService;

        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public ActionResult Index()
        {
            var userToGuess = gameService.GetRandomUser(CurrentUser.Id);
            var usersToChooseFrom = gameService.GetRandomUsers(userToGuess.Id).ToList();
            usersToChooseFrom.Add(userToGuess);
            var gameModel = new GuessUserModel
            {
                UserToGuess = userToGuess,
                Choices = usersToChooseFrom
            };
            return View(gameModel);
        }
    }
}
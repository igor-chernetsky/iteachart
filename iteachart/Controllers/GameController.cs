using System;
using System.Linq;
using System.Web.Mvc;
using Infrastructure.Code;
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
            var gameModel = GuessUserModel();
            return View(gameModel);
        }

        private GuessUserModel GuessUserModel()
        {
            var userToGuess = gameService.GetRandomUser(CurrentUser.Id);
            var usersToChooseFrom = gameService.GetRandomUsers(userToGuess.Id).ToList();
            var randomPosition = new Random().Next(Constants.RandomUserNum - 1);
            usersToChooseFrom.Insert(randomPosition, userToGuess);
            var gameModel = new GuessUserModel
            {
                UserToGuess = userToGuess,
                Choices = usersToChooseFrom
            };
            return gameModel;
        }

        public ActionResult Random()
        {
            var gameModel = GuessUserModel();
            return Json(gameModel, JsonRequestBehavior.AllowGet);
        }

        public void WriteStatistic(int guessPerson, bool isGuessed)
        {
            gameService.WriteStatistics(CurrentUser.Id, guessPerson, isGuessed);
        }
    }
}
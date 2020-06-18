using System;
using kol02.DAL;
using kol02.DTO.Requests;
using kol02.DTO.Responses;
using kol02.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace kol02.Controllers
{
    [ApiController]
    [Route("api")]
    public class ChampionshipController : ControllerBase
    {
        private readonly IDbService _service;

        public ChampionshipController(IDbService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [Route("championship/{championshipId}/teams")]
        public IActionResult GetTeams(int championshipId)
        {
            IActionResult response;
            try
            {
                GetTeamsResponse teams = _service.GetTeams(championshipId);
                response = Ok(teams);
            }
            catch (ChampionshipDoesntExistException e)
            {
                response = NotFound($"No such championship:\n{e.StackTrace}\n{e.Message}");
            }
            catch (Exception e)
            {
                response = BadRequest($"Some other error occurred:\n{e.StackTrace}\n{e.Message}");
            }
            return response;
        }

        [HttpPost]
        [Route("teams/{teamId}/players")]
        public IActionResult AddPlayerToTeam(int teamId, AddPlayerRequest request)
        {
            IActionResult response;
            try
            {
                _service.AddPlayerToTeam(teamId, request);
                response = Ok();
            }
            catch (PlayerIsToOldException e)
            {
                response = BadRequest($"This player is too old:\n{e.StackTrace}\n{e.Message}");
            }
            catch (PlayerDoesntExistException e)
            {
                response = BadRequest($"No such player:\n{e.StackTrace}\n{e.Message}");
            }
            catch (PlayerAlreadyInTheTeamException e)
            {
                response = BadRequest($"Player already in the team:\n{e.StackTrace}\n{e.Message}");
            }
            catch (Exception e)
            {
                response = BadRequest($"Some other error occurred:\n{e.StackTrace}\n{e.Message}");
            }
            return response;
        }

    }
}
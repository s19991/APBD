using System;
using System.Collections.Generic;
using System.Linq;
using kol02.DTO.Requests;
using kol02.DTO.Responses;
using kol02.Exceptions;
using kol02.Models;

namespace kol02.DAL
{
    public class SqlServerDbService : IDbService
    {
        private readonly ChampionshipDbContext _context;

        public SqlServerDbService(ChampionshipDbContext context)
        {
            _context = context;
        }
        
        public GetTeamsResponse GetTeams(int championshipId)
        {
            var championshipExists = _context.Championships
                .Any(x => x.IdChampionship.Equals(championshipId));
            if (!championshipExists)
            {
                throw new ChampionshipDoesntExistException($"No such championship {championshipId} found");
            }
            
            var teamsInGivenChampionship = _context.ChampionshipTeams
                .Where(x => x.IdChampionship.Equals(championshipId))
                .OrderByDescending(x => x.Score)
                .ToList();

            Dictionary<string, float> teamResults = new Dictionary<string, float>();
            foreach (var team in teamsInGivenChampionship)
            {
                teamResults.Add(
                    _context.Teams.Single(x => x.IdTeam.Equals(team.IdTeam)).TeamName,
                    team.Score
                );
            }
            
            return new GetTeamsResponse
            {
                IdChampionship = championshipId,
                TeamScore = teamResults
            };
        }

        public void AddPlayerToTeam(int teamId, AddPlayerRequest request)
        {
            var maxAge = _context.Teams
                .Single(x => x.IdTeam.Equals(teamId))
                .MaxAge;
            var playerAge = DateTime.Now.Year - request.BirthDate.Year;
            if (playerAge > maxAge)
            {
                throw new PlayerIsToOldException($"The player ({playerAge}) is too old ({maxAge})!");
            }
            
            
            var playerExists = _context.Players
                .Any(x => x.FirstName.Equals(request.FirstName) && x.LastName.Equals(request.LastName));
            if (!playerExists)
            {
                throw new PlayerDoesntExistException($"No such player {request.FirstName} {request.LastName}found");
            }
            
            var playerId = _context.Players
                .Single(x => x.FirstName.Equals(request.FirstName) && x.LastName.Equals(request.LastName))
                .IdPlayer;
            
            var playerAlreadyInTheTeam = _context.PlayerTeams
                .Any(x => x.IdPlayer.Equals(playerId) && x.IdTeam.Equals(teamId));
            if (playerAlreadyInTheTeam)
            {
                throw new PlayerAlreadyInTheTeamException($"The player {playerId} is already in the team {teamId}");
            }

            _context.PlayerTeams.Add(new PlayerTeam
                {
                    IdTeam = teamId,
                    IdPlayer = playerId,
                    NumOnShirt = request.NumOnShirt,
                    Comment = request.Comment
                }
            );
            _context.SaveChanges();
        }
    }
}
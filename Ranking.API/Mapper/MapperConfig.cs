using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ranking.API.DTO;
using Ranking.Domain;
using Ranking.Domain.Enum;

namespace Ranking.API.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<MatchTypeDTO, MatchType>();
            CreateMap<MatchType, Data.Entities.MatchTypes>()
                .ForMember(e => e.MatchTypeID, opt => opt.MapFrom(e => e.Id)).ReverseMap();

            CreateMap<ConfederationDTO, Confederation>();
            CreateMap<Confederation, Data.Entities.Confederations>()
                .ForMember(e => e.ConfederationID, opt => opt.MapFrom(e => e.Id)).ReverseMap();

            CreateMap<TeamDTO, Team>();
            CreateMap<Team, Data.Entities.Teams>()
                .ForMember(e => e.TeamID, opt => opt.MapFrom(e => e.Id)).ReverseMap();

            CreateMap<PlayerDTO, Player>()
                .ForMember(e => e.Position, opt => opt.MapFrom(e => (PlayerPosition)e.Position));
            CreateMap<Player, Data.Entities.Players>()
                .ForMember(e => e.PlayerID, opt => opt.MapFrom(e => e.Id))
                .ForMember(e => e.PositionID, opt => opt.MapFrom(e => (int)e.Position))
                    .ReverseMap().ForMember(e => e.Position, opt => opt.MapFrom(e => (PlayerPosition)e.PositionID));

            CreateMap<RankingDTO, Domain.Ranking>();
            CreateMap<Domain.Ranking, Data.Entities.Rankings>()
                .ForMember(e => e.RankingID, opt => opt.MapFrom(e => e.Id)).ReverseMap();

            CreateMap<MatchDTO, Match>()
                .ForMember(e => e.MatchRound, opt => opt.MapFrom(e => (MatchRound)e.MatchRoundId));
            CreateMap<Match, Data.Entities.Matches>()
                .ForMember(e => e.MatchID, opt => opt.MapFrom(e => e.Id))
                .ForMember(e => e.MatchResultID, opt => opt.MapFrom(e => (int)e.MatchResult))
                .ForMember(e => e.MatchRoundID, opt => opt.MapFrom(e => (int)e.MatchRound))
                    .ReverseMap()
                    .ForMember(e => e.MatchResult, opt => opt.MapFrom(e => (MatchResult)e.MatchResultID))
                    .ForMember(e => e.MatchRound, opt => opt.MapFrom(e => (MatchRound)e.MatchRoundID));

            CreateMap<TournamentTypeDTO, TournamentType>()
                .ForMember(e => e.Format, opt => opt.MapFrom(e => (TournamentFormat)e.Format));
            CreateMap<TournamentType, Data.Entities.TournamentTypes>()
                .ForMember(e => e.TournamentTypeID, opt => opt.MapFrom(e => e.Id))
                .ForMember(e => e.FormatID, opt => opt.MapFrom(e => (int)e.Format))
                    .ReverseMap().ForMember(e => e.Format, opt => opt.MapFrom(e => (TournamentFormat)e.FormatID));

            CreateMap<TournamentDTO, Tournament>();
            CreateMap<Tournament, Data.Entities.Tournaments>()
                .ForMember(e => e.TournamentID, opt => opt.MapFrom(e => e.Id)).ReverseMap();

            CreateMap<PositionDTO, Position>();
            CreateMap<Position, Data.Entities.Positions>()
                .ForMember(e => e.PositionID, opt => opt.MapFrom(e => e.Id)).ReverseMap();

            CreateMap<GoalscorerDTO, Goalscorer>();
            CreateMap<Goalscorer, Data.Entities.Goalscorers>()
                .ForMember(e => e.GoalscorerID, opt => opt.MapFrom(e => e.Id)).ReverseMap();

            CreateMap<TeamStat, Data.Entities.TeamStats>()
                .ForMember(e => e.TeamStatsID, opt => opt.MapFrom(e => e.Id)).ReverseMap();

            CreateMap<TeamStatWorldCup, Data.Entities.TeamStatsWorldCup>()
                .ForMember(e => e.TeamStatsWorldCupID, opt => opt.MapFrom(e => e.Id)).ReverseMap();

            CreateMap<Head2Head, Data.Entities.H2H>()
                .ForMember(e => e.H2HID, opt => opt.MapFrom(e => e.Id)).ReverseMap();

            CreateMap<Head2HeadWorldCup, Data.Entities.H2HWorldCup>()
                .ForMember(e => e.H2HWorldCupID, opt => opt.MapFrom(e => e.Id)).ReverseMap();
        }
    }
}

using AutoMapper;
using Ranking.Domain;
using Ranking.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ranking.Sync
{
    public class MapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(x => x.AddProfile<MyProfile>());
        }
    }

    public class MyProfile : Profile
    {
        public override string ProfileName
        {
            get { return "MyProfile"; }
        }

        public MyProfile()
        {
            CreateMap<MatchType, Data.Entities.MatchTypes>()
                .ForMember(e => e.MatchTypeID, opt => opt.MapFrom(e => e.Id)).ReverseMap();

            CreateMap<Confederation, Data.Entities.Confederations>()
                .ForMember(e => e.ConfederationID, opt => opt.MapFrom(e => e.Id)).ReverseMap();

            CreateMap<Team, Data.Entities.Teams>()
                .ForMember(e => e.TeamID, opt => opt.MapFrom(e => e.Id)).ReverseMap();

            CreateMap<Player, Data.Entities.Players>()
                .ForMember(e => e.PlayerID, opt => opt.MapFrom(e => e.Id))
                .ForMember(e => e.PositionID, opt => opt.MapFrom(e => (int)e.Position))
                    .ReverseMap().ForMember(e => e.Position, opt => opt.MapFrom(e => (PlayerPosition)e.PositionID));

            CreateMap<Domain.Ranking, Data.Entities.Rankings>()
                .ForMember(e => e.RankingID, opt => opt.MapFrom(e => e.Id)).ReverseMap();

            CreateMap<TournamentType, Data.Entities.TournamentTypes>()
                .ForMember(e => e.TournamentTypeID, opt => opt.MapFrom(e => e.Id))
                .ForMember(e => e.FormatID, opt => opt.MapFrom(e => (int)e.Format))
                    .ReverseMap().ForMember(e => e.Format, opt => opt.MapFrom(e => (TournamentFormat)e.FormatID));

            CreateMap<Tournament, Data.Entities.Tournaments>()
                .ForMember(e => e.TournamentID, opt => opt.MapFrom(e => e.Id)).ReverseMap();

            CreateMap<Position, Data.Entities.Positions>()
                .ForMember(e => e.PositionID, opt => opt.MapFrom(e => e.Id)).ReverseMap();

            CreateMap<Goalscorer, Data.Entities.Goalscorers>()
                .ForMember(e => e.GoalscorerID, opt => opt.MapFrom(e => e.Id)).ReverseMap();
        }
    }
}

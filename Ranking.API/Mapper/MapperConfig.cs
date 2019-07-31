using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ranking.API.DTO;
using Ranking.Domain;

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

            CreateMap<RankingDTO, Domain.Ranking>();
            CreateMap<Domain.Ranking, Data.Entities.Rankings>()
                .ForMember(e => e.RankingID, opt => opt.MapFrom(e => e.Id)).ReverseMap();

            CreateMap<MatchDTO, Match>();
        }
    }
}

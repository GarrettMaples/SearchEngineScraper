using AutoMapper;
using SearchEngineScraper.Service.Dtos;
using System.Linq;

namespace SearchEngineScraper.Api.Models.Results.AutoMapperProfiles
{
    public class UrlScrapeResultsModelProfile : Profile
    {
        public UrlScrapeResultsModelProfile()
        {
            CreateMap<UrlScrapeResultsDto, UrlScrapeResultsModel>()
                .ForMember
                (
                    model => model.Results,
                    options => options.MapFrom(dto => dto.Results.Select(x => new UrlScrapeResultModel(x.Key, x.Value)))
                );
        }
    }
}
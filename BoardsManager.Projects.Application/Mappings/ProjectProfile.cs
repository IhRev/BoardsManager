using AutoMapper;
using BoardsManager.Projects.Core.Dto;
using BoardsManager.Projects.Domain.Entities;

namespace BoardsManager.Projects.Application.Mappings
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDto>().ReverseMap();
        }
    }
}
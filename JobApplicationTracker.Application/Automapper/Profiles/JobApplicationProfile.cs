using AutoMapper;
using JobApplicationTracker.Application.Models;
using JobApplicationTracker.Application.ViewModels;
using JobApplicationTracker.Core.Entities;

namespace JobApplicationTracker.Application.Automapper
{
    public class JobApplicationProfile : Profile
    {
        public JobApplicationProfile()
        {
            CreateMap<JobApplication, JobApplicationListViewModel>();
            CreateMap<JobApplication, JobApplicationDetailViewModel>();
            CreateMap<JobApplicationCreateModel, JobApplication>();
            CreateMap<JobApplicationUpdateModel, JobApplication>();
        }
    }
}

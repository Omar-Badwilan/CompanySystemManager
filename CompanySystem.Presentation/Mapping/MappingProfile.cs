using AutoMapper;
using CompanySystem.BusinessLogic.DTOS.Departments;
using CompanySystem.Presentation.ViewModels.Departments;

namespace CompanySystem.Presentation.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Employee

            #endregion

            #region Department

            CreateMap<DepartmentDetailsDto , DepartmentViewModel>()
                /*.ForMember(dest => dest.Name, config => config.MapFrom(src => src.Name))*/
                
                //incase if i want to map from Viewmodel to dto
                .ReverseMap();


            CreateMap<DepartmentViewModel, UpdateDepartmentDto>();
            CreateMap<DepartmentViewModel, CreatedDepartmentDto>();
            #endregion
        }
    }
}

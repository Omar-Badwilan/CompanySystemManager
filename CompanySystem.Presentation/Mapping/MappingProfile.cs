using AutoMapper;
using CompanySystem.BusinessLogic.DTOS.Departments;
using CompanySystem.BusinessLogic.DTOS.Employees;
using CompanySystem.Presentation.ViewModels.Departments;
using CompanySystem.Presentation.ViewModels.Employees;

namespace CompanySystem.Presentation.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Employee
            
            CreateMap<EmployeeDetailsDto,EmployeeViewModel>();

            CreateMap<EmployeeViewModel,UpdateEmployeeDto>();

            CreateMap<EmployeeViewModel, CreatedEmployeeDto>();



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

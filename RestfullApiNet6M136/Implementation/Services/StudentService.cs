using AutoMapper;
using RestfullApiNet6M136.Abstraction.IRepositories;
using RestfullApiNet6M136.Abstraction.IRepositories.IStudentRepos;
using RestfullApiNet6M136.Abstraction.IUnitOfWorks;
using RestfullApiNet6M136.Abstraction.Services;
using RestfullApiNet6M136.Context;
using RestfullApiNet6M136.DTOs.StudentDTOs;
using RestfullApiNet6M136.Entities.AppdbContextEntity;
using RestfullApiNet6M136.Implementation.UnitOfWorks;
using RestfullApiNet6M136.Models;

namespace RestfullApiNet6M136.Implementation.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentRepository student;


        public StudentService(IMapper _mapper, IUnitOfWork unitOfWork, IStudentRepository student)
        {
            mapper = _mapper;
            _unitOfWork = unitOfWork;
            this.student = student;
        }

        public async Task<GenericResponseModel<StudentCreateDTO>> AddStudent(StudentCreateDTO model)
        {
            //var studentEntity = _studentRepository.Insert(new Student
            //{
            //    // Model verilerini entity verilerine dönüştürme
            //    Name = model.Name,
            //    Age = model.Age
            //    // Diğer özellikler
            //});

            //_unitOfWork.SaveChangesAsync();

           //await _unitOfWork.GetRepository<Student>().GetByIdAsync(2);
           // await _unitOfWork.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<bool>> ChangeSchool(int studentId, int schoolid)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<bool>> ChangeSchool(ChangeSchoolDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<bool>> DeleteStudent(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<List<StudentGetDTO>>> GetAllStudent()
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponseModel<List<StudentGetDTO>>> GetAllStudentBySchoolId(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericResponseModel<StudentGetDTO>> GetStudentById(int Id)
        {
            //var data = await _unitOfWork.Repository.GetByIdAsync(Id);
            await _unitOfWork.SaveChangesAsync();
            return null;
        }

        public Task<GenericResponseModel<bool>> UpdateStudent(StudentUpdateDTO model)
        {
            throw new NotImplementedException();
        }

        public async Task<Student> GetStudentId(int Id)
        {
            var data1= await student.GetByIdAsync(Id);
            //var data = await _unitOfWork.StudentRepo.GetByIdAsync(Id);

            var data = await _unitOfWork.GetRepository<Student>().GetByIdAsync(Id);
            await _unitOfWork.SaveChangesAsync();
            return data;
        }
    }
}

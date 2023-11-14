using AutoMapper;
using RestfullApiNet6M136.Abstraction.IRepositories;
using RestfullApiNet6M136.Abstraction.IRepositories.IStudentRepos;
using RestfullApiNet6M136.Abstraction.IUnitOfWorks;
using RestfullApiNet6M136.Abstraction.Services;
using RestfullApiNet6M136.Context;
using RestfullApiNet6M136.DTOs.SchoolDTOs;
using RestfullApiNet6M136.DTOs.StudentDTOs;
using RestfullApiNet6M136.Entities.AppdbContextEntity;
using RestfullApiNet6M136.Implementation.UnitOfWorks;
using RestfullApiNet6M136.Models;
using System;
using System.Net.Cache;

namespace RestfullApiNet6M136.Implementation.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Student> student;
        private readonly IRepository<School> _school;


        public StudentService(IMapper _mapper, IUnitOfWork unitOfWork)
        {
            this.student = _unitOfWork.GetRepository<Student>();
            this._school = _unitOfWork.GetRepository<School>();

            mapper = _mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericResponseModel<StudentCreateDTO>> AddStudent(StudentCreateDTO model)
        {
            #region MyRegion
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
            #endregion


            if (model != null)
            {
                /*bool addResult =*/
                await student.AddAsync(new()
                {
                    Name = model.Name,
                });

                int result = await _unitOfWork.SaveChangesAsync();

                if (result > 0)
                {
                    return new GenericResponseModel<StudentCreateDTO>
                    {
                        Data = model,
                        StatusCode = 201,
                    };
                }
                else
                {
                    return new GenericResponseModel<StudentCreateDTO>
                    {
                        Data = model,
                        StatusCode = 500,
                    };

                    //Sadece xeta cixar-refactoring
                }

            }
            else
            {
                return new GenericResponseModel<StudentCreateDTO>
                {
                    Data = model,
                    StatusCode = 400,
                };
            }
        }

        public async Task<GenericResponseModel<bool>> ChangeSchool(int studentId, int newSchoolId)
        {
            var myStudent = await student.GetByIdAsync(studentId);

            if (myStudent == null)
            {
                return new GenericResponseModel<bool>
                {
                    Data = false,
                    StatusCode = 400,
                };
            }

            var mySchool = await _school.GetByIdAsync(newSchoolId);//Any ile de yazmaq olardi amma repoda yoxdu

            if (mySchool == null)
            {
                return new GenericResponseModel<bool>
                {
                    Data = false,
                    StatusCode = 400,
                };
            }

            myStudent.SchoolId = newSchoolId;

            student.Update(myStudent);


            var rowAffect = await _unitOfWork.SaveChangesAsync();

            if (rowAffect > 0)
            {
                return new GenericResponseModel<bool>
                {
                    Data = true,
                    StatusCode = 200,
                };
            }
            else
            {
                return new GenericResponseModel<bool>
                {
                    Data = false,
                    StatusCode = 500,
                };
            }
        }

        public async Task<GenericResponseModel<bool>> ChangeSchool(ChangeSchoolDTO model)
        {
            var myStudent = await student.GetByIdAsync(model.StudentID);

            if (myStudent == null)
            {
                return new GenericResponseModel<bool>
                {
                    Data = false,
                    StatusCode = 400,
                };
            }

            var mySchool = await _school.GetByIdAsync(model.NewSchoolID);//Any ile de yazmaq olardi amma repoda yoxdu

            if (mySchool == null)
            {
                return new GenericResponseModel<bool>
                {
                    Data = false,
                    StatusCode = 400,
                };
            }

            myStudent.SchoolId = model.NewSchoolID;

            student.Update(myStudent);


            var rowAffect = await _unitOfWork.SaveChangesAsync();

            if (rowAffect > 0)
            {
                return new GenericResponseModel<bool>
                {
                    Data = true,
                    StatusCode = 200,
                };
            }
            else
            {
                return new GenericResponseModel<bool>
                {
                    Data = false,
                    StatusCode = 500,
                };
            }
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

            var data1 = await student.GetByIdAsync(Id);
            //var data = await _unitOfWork.StudentRepo.GetByIdAsync(Id);

            var data = await _unitOfWork.GetRepository<Student>().GetByIdAsync(Id);
            await _unitOfWork.SaveChangesAsync();
            return data;
        }
    }
}

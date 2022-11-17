using School.Domain.Entities.StudentAggregate;
using School.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain.Services
{
    public class StudentServices: IStudentService
    {
        private readonly IGenericRepository<Student> _studentRepository;
        public StudentServices(IGenericRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task EnrollCourse(int studentId, int courseId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            student.EnrollCourse(student.Id, courseId);
            await _studentRepository.UpdateAsync(student);
        }
    }
}

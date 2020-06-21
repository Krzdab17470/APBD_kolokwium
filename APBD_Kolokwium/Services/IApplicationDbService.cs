using APBD_Kolokwium.Models;
using APBD_Kolokwium.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_Kolokwium.Services
{
    //INTERFACE
    public interface IApplicationDbService
    {
        Enrollment2 EnrollStudent(EnrollStudentRequest2 request);

        Enrollment2 PromoteStudents(int semester, string studies);

        string GetStudents(int IdEnrollment);
    }
}

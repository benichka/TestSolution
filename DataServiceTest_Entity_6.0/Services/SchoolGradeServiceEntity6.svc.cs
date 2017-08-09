//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Data.Services.Providers;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using DataServiceTestEntity60.Model;

namespace DataServiceTest_Entity_6._0.Services
{
    public class SchoolGradeServiceEntity6 : EntityFrameworkDataService<SchoolGradesDBEntities>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            // TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
            // Examples:
            // config.SetEntitySetAccessRule("MyEntityset", EntitySetRights.AllRead);
            // config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;

            // Configure all entity sets to permit read and write access
            config.SetEntitySetAccessRule("Grades", EntitySetRights.All);
            config.SetEntitySetAccessRule("Teachers", EntitySetRights.All);
            config.SetEntitySetAccessRule("Students", EntitySetRights.All);
            config.SetEntitySetAccessRule("Subjects", EntitySetRights.All);
            config.SetEntitySetAccessRule("Users", EntitySetRights.All);
        }

        [WebGet]
        public IEnumerable<Student> StudentsInClass(string className)
        {
            var students = from Student s in this.CurrentDataSource.Students
                           where String.Equals(s.Teacher.Class, className)
                           select s;
            return students;
        }
    }
}

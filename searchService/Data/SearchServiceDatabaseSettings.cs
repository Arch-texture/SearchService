    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    namespace searchService.Data
    {
        public class SearchServiceDatabaseSettings
        {
            public string ConnectionString { get; set; } = null!;
            public string DatabaseName { get; set; } = null!;
            public string StudentsCollectionName { get; set; } = null!;
            public string TeachersCollectionName { get; set; } = null!;
            public string GradesCollectionName { get; set; } = null!;
            public string RestrictionsCollectionName { get; set; } = null!;
            public string SubjectsCollectionName { get; set; } = null!;



        }
    }
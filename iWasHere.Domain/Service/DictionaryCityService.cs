using iWasHere.Domain.DTOs;
using iWasHere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWasHere.Domain.Service
{
    public class DictionaryCityService
    {
        private readonly ScarletWitchContext _dbContext;
        public DictionaryCityService(ScarletWitchContext databaseContext)
        {
            _dbContext = databaseContext;
        }



    }
}

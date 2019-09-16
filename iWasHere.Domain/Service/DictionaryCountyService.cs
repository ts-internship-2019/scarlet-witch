using iWasHere.Domain.DTOs;
using iWasHere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;

namespace iWasHere.Domain.Service
{
    public class DictionaryCountyService
    {
        private ScarletWitchContext _dbContext;

        public DictionaryCountyService(ScarletWitchContext context)
        {
            this._dbContext = context;
        }

        public IEnumerable<DictionaryCountyModel> Read()
        {
            return GetDictionaryCounties();
        }

        public List<DictionaryCountyModel> GetDictionaryCounties()
        {
            List<DictionaryCountyModel> counties = _dbContext.DictionaryCounty.Select(county => new DictionaryCountyModel()
            {
                CountyId = county.CountyId,
                CountyName = county.CountyName
            }).ToList();

            return counties;
        }
    }
}
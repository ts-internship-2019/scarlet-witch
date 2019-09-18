using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using iWasHere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWasHere.Domain.Service
{
    public class DictionaryService
    {
        private readonly ScarletWitchContext _dbContext;

        public DictionaryService(ScarletWitchContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public List<DictionaryCountryModel> GetDictionaryCountryModels()
        {
            List<DictionaryCountryModel> dictionaryCountryModels = _dbContext.DictionaryCountry.Select(b => new DictionaryCountryModel()
            {
                CountryId = b.CountryId,
                LanguageId = b.LanguageId,
                CountryName = b.CountryName
            }).ToList();

            return dictionaryCountryModels;
        }

        public IQueryable<DictionaryCityModel> GetDictionaryCities()
        {
            IQueryable<DictionaryCityModel> dictionaryCities = _dbContext.DictionaryCity.Select(a => new DictionaryCityModel()
            {
                Id = a.CityId,
                Name = a.CityName
            }
            );
            return dictionaryCities;
        }

        public IQueryable<DictionaryCountyModel> GetDictionaryCounties()
        {
            IQueryable<DictionaryCountyModel> dictionaryCounties = _dbContext.DictionaryCounty.Select(a => new DictionaryCountyModel()
            {
                CountyId = a.CountyId,
                CountyName = a.CountyName,
                CountryName = _dbContext.DictionaryCountry.Where(c => c.CountryId == a.CountryId).Select(c => c.CountryName).FirstOrDefault().ToString()
            } );
            return dictionaryCounties;
        }

        public IQueryable<DictionaryCountyModel> GetDictionaryCountiesByName(string nameFilter)
        {
            if (nameFilter == null)
            {
                IQueryable<DictionaryCountyModel> dictionaryCounties = _dbContext.DictionaryCounty.Select(a => new DictionaryCountyModel()
                {
                    CountyId = a.CountyId,
                    CountyName = a.CountyName,
                    CountryName = _dbContext.DictionaryCountry.Where(c => c.CountryId == a.CountryId).Select(c => c.CountryName).FirstOrDefault().ToString()
                });
                return dictionaryCounties;
            }
            else
            {
                IQueryable<DictionaryCountyModel> dictionaryCounties = _dbContext.DictionaryCounty.Select(a => new DictionaryCountyModel()
                {
                    CountyId = a.CountyId,
                    CountyName = a.CountyName,
                    CountryName = _dbContext.DictionaryCountry.Where(c => c.CountryId == a.CountryId).Select(c => c.CountryName).FirstOrDefault().ToString()
                }
                ).Where(c => c.CountyName.Contains(nameFilter));
                return dictionaryCounties;
            }
        }

        public List<DictionaryCountyModel> GetDictionaryCountiesByCountry(string countryName)
        {
            List<DictionaryCountyModel> dictionaryCounties = _dbContext.DictionaryCounty.Select(a => new DictionaryCountyModel()
            {
                CountyId = a.CountyId,
                CountyName = a.CountyName,
                CountryName = _dbContext.DictionaryCountry.Where(c => c.CountryId == a.CountryId).Select(c => c.CountryName).FirstOrDefault().ToString()
            }
            ).Where(d => d.CountryName == countryName).ToList();

            return dictionaryCounties;
        }
        public List<DictionaryCurrencyModel> GetDictionaryCurrencyModels()
        {

            List<DictionaryCurrencyModel> dictionaryCurrencyModels = _dbContext.DictionaryCurrency.Select(b => new DictionaryCurrencyModel()
            {
                CurrencyId = b.CurrencyId,
                CurrencyName = b.CurrencyName,
                CurrencyCode = b.CurrencyCode,
                CurrencyExchange = Convert.ToDecimal(b.CurrencyExchange)
                
            }).ToList();

            return dictionaryCurrencyModels;
        }

        public List<DictionaryLandmarkTypeModel> GetDictionaryLandmarkTypeModels()
        {
            List<DictionaryLandmarkTypeModel> dictionaryLandmarkTypeModels = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkTypeModel()
            {
                LandmarkTypeCode = a.LandmarkTypeCode,
                Description = a.Description
            }).ToList();

            return dictionaryLandmarkTypeModels;
        }

        public IQueryable<DictionaryCityModel> GetDictionaryCitiesFiltered(String cityName)
        {
            if (cityName==null)
            {
                IQueryable<DictionaryCityModel> dictionaryCities = _dbContext.DictionaryCity.Select(a => new DictionaryCityModel()
                {
                    Id = a.CityId,
                    Name = a.CityName
                });
                return dictionaryCities;
            }
            else
            {
                IQueryable<DictionaryCityModel> dictionaryCities = _dbContext.DictionaryCity.Select(a => new DictionaryCityModel()
                {
                    Id = a.CityId,
                    Name = a.CityName
                }
                ).Where(c => c.Name == cityName);
                return dictionaryCities;
            }
         
          
        }


       
    }
}

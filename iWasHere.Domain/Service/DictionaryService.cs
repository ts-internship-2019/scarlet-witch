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
            });
            return dictionaryCounties;
        }

        public IQueryable<DictionaryCountyModel> GetDictionaryCountiesByName(string countyName, int countryId)
        {
            if (countyName == null && countryId == 0)
            {
                IQueryable<DictionaryCountyModel> dictionaryCounties = _dbContext.DictionaryCounty.Select(a => new DictionaryCountyModel()
                {
                    CountyId = a.CountyId,
                    CountyName = a.CountyName,
                    CountryId = a.CountryId,
                    CountryName = _dbContext.DictionaryCountry.Where(c => c.CountryId == a.CountryId).Select(c => c.CountryName).FirstOrDefault().ToString()
                });
                return dictionaryCounties;
            }
            else if(countyName!= null && countryId == 0)
            {
                IQueryable<DictionaryCountyModel> dictionaryCounties = _dbContext.DictionaryCounty.Select(a => new DictionaryCountyModel()
                {
                    CountyId = a.CountyId,
                    CountyName = a.CountyName,
                    CountryId = a.CountryId,
                    CountryName = _dbContext.DictionaryCountry.Where(c => c.CountryId == a.CountryId).Select(c => c.CountryName).FirstOrDefault().ToString()
                }
                ).Where(c => c.CountyName.Contains(countyName));
                return dictionaryCounties;
            }
            else if (countyName ==null && countryId != 0)
            {
                IQueryable<DictionaryCountyModel> dictionaryCounties = _dbContext.DictionaryCounty.Select(a => new DictionaryCountyModel()
                {
                    CountyId = a.CountyId,
                    CountyName = a.CountyName,
                    CountryId = a.CountryId,
                    CountryName = _dbContext.DictionaryCountry.Where(c => c.CountryId == a.CountryId).Select(c => c.CountryName).FirstOrDefault().ToString()
                }).Where(c => c.CountryId == countryId);
                return dictionaryCounties;
            }
            else
            {
                IQueryable<DictionaryCountyModel> dictionaryCounties = _dbContext.DictionaryCounty.Select(a => new DictionaryCountyModel()
                {
                    CountyId = a.CountyId,
                    CountyName = a.CountyName,
                    CountryId = a.CountryId,
                    CountryName = _dbContext.DictionaryCountry.Where(c => c.CountryId == a.CountryId).Select(c => c.CountryName).FirstOrDefault().ToString()
                }).Where(c => c.CountryId == countryId && c.CountyName == countyName);
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

        public List<DictionaryCountryModel> GetDictionaryCountriesByLanguage(string languageName)
        {
            List<DictionaryCountryModel> dictionaryCountries = _dbContext.DictionaryCountry.Select(a => new DictionaryCountryModel()
            {
                CountryId = a.CountryId,
                CountryName = a.CountryName,
                LanguageName = _dbContext.DictionaryLanguage.Where(c => c.LanguageId == a.LanguageId).Select(c => c.LanguageName).FirstOrDefault().ToString()
            }
            ).Where(d => d.LanguageName == languageName).ToList();

            return dictionaryCountries;
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

        public IQueryable<DictionaryLandmarkTypeModel> GetDictionaryLandmarkTypesFiltered(String landmarkTypeName)
        {
            if (landmarkTypeName == null)
            {
                IQueryable<DictionaryLandmarkTypeModel> dictionaryLandmarkTypes = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkTypeModel()
                {
                    LandmarkTypeId = a.LandmarkTypeId,
                    LandmarkTypeCode = a.LandmarkTypeCode,
                    Description = a.Description
                });
                return dictionaryLandmarkTypes;
            }
            else
            {
                IQueryable<DictionaryLandmarkTypeModel> dictionaryLandmarkTypes = _dbContext.DictionaryLandmarkType.Select(a => new DictionaryLandmarkTypeModel()
                {
                    LandmarkTypeId = a.LandmarkTypeId,
                    LandmarkTypeCode = a.LandmarkTypeCode,
                    Description = a.Description
                }
                ).Where(c => c.LandmarkTypeCode.Contains(landmarkTypeName));
                return dictionaryLandmarkTypes;
            }


        }

        public IQueryable<DictionaryCityModel> GetDictionaryCitiesFiltered(String cityName, int countyId)
        {
            if (cityName == null && countyId == 0)
            {
                IQueryable<DictionaryCityModel> dictionaryCities = _dbContext.DictionaryCity.Select(a => new DictionaryCityModel()
                {
                    Id = a.CityId,
                    CountyId = a.CountyId,
                    Name = a.CityName,
                    County = _dbContext.DictionaryCounty.Where(c => c.CountyId == a.CountyId).Select(c => c.CountyName).FirstOrDefault().ToString()
                });
                return dictionaryCities;
            }
            else if (cityName != null && countyId == 0)
            {
                IQueryable<DictionaryCityModel> dictionaryCities = _dbContext.DictionaryCity.Select(a => new DictionaryCityModel()
                {
                    Id = a.CityId,
                    CountyId = a.CountyId,
                    Name = a.CityName,
                    County = _dbContext.DictionaryCounty.Where(c => c.CountyId == a.CountyId).Select(c => c.CountyName).FirstOrDefault().ToString()
                }
               ).Where(c => c.Name == cityName);
                return dictionaryCities;
            }
            else if (cityName == null && countyId != 0)
            {
                IQueryable<DictionaryCityModel> dictionaryCities = _dbContext.DictionaryCity.Select(a => new DictionaryCityModel()
                {
                    Id = a.CityId,
                    CountyId = a.CountyId,
                    Name = a.CityName,
                    County = _dbContext.DictionaryCounty.Where(c => c.CountyId == a.CountyId).Select(c => c.CountyName).FirstOrDefault().ToString()
                }
               ).Where(c =>  c.CountyId == countyId);
                return dictionaryCities;
            }
            else 
            {
                IQueryable<DictionaryCityModel> dictionaryCities = _dbContext.DictionaryCity.Select(a => new DictionaryCityModel()
                {
                    Id = a.CityId,
                    CountyId = a.CountyId,
                    Name = a.CityName,
                    County = _dbContext.DictionaryCounty.Where(c => c.CountyId == a.CountyId).Select(c => c.CountyName).FirstOrDefault().ToString()
                }
                ).Where(c => c.Name == cityName && c.CountyId == countyId);
                return dictionaryCities;
            }
        }

        public void DeleteUsuarios(int id)
        {
            DictionaryCity city = new DictionaryCity() { CityId = id };

            _dbContext.DictionaryCity.Remove(city);
            _dbContext.SaveChanges();
        }

        public int DeleteCounty(int id)
        {
            int sters = 0;
            try
            {
                DictionaryCounty c = new DictionaryCounty() { CountyId = id };
                _dbContext.DictionaryCounty.Remove(c);
                _dbContext.SaveChanges();
                sters = 1;
                return sters;
            }
            catch (Exception ex)
            {
                sters = 0;
                return sters;
            }
        }

        public void DeleteCurrency(int id)
        {
            DictionaryCurrency currency = new DictionaryCurrency() { CurrencyId = id };

            _dbContext.DictionaryCurrency.Remove(currency);
            _dbContext.SaveChanges();

        }

        public IQueryable<DictionaryLanguageModel> GetDictionaryLanguagesFiltered(String languageName)
        {
            if (languageName == null)
            {
                IQueryable<DictionaryLanguageModel> dictionaryLanguage = _dbContext.DictionaryLanguage.Select(a => new DictionaryLanguageModel()
                {
                    LanguageId = a.LanguageId,
                    LanguageCode = a.LanguageCode,
                    LanguageName = a.LanguageName
                });
                return dictionaryLanguage;
            }
            else
            {
                IQueryable<DictionaryLanguageModel> dictionaryLanguage = _dbContext.DictionaryLanguage.Select(a => new DictionaryLanguageModel()
                {
                    LanguageId = a.LanguageId,
                    LanguageCode = a.LanguageCode,
                    LanguageName = a.LanguageName
                }
                ).Where(c => c.LanguageName.Contains(languageName));
                return dictionaryLanguage;
            }
        }

        public IQueryable<DictionaryCountryModel> GetDictionaryCountriesFiltered(String countryName)
        {
            if (countryName == null)
            {
                IQueryable<DictionaryCountryModel> dictionaryCountries = _dbContext.DictionaryCountry.Select(a => new DictionaryCountryModel()
                {
                    LanguageId = a.LanguageId,
                    CountryId = a.CountryId,
                    CountryName = a.CountryName,
                });
                return dictionaryCountries;
            }
            else
            {
                IQueryable<DictionaryCountryModel> dictionaryCountry = _dbContext.DictionaryCountry.Select(a => new DictionaryCountryModel()
                {
                    LanguageId = a.LanguageId,
                    CountryId = a.CountryId,
                    CountryName = a.CountryName,
                }).Where(c => c.CountryName.Contains(countryName));
                return dictionaryCountry;
            }
        }

        public List<DictionaryCountyModel> PopulateCountyCombo()
        {
            List<DictionaryCountyModel> dictionaryCurrencyModels = _dbContext.DictionaryCounty.Select(b => new DictionaryCountyModel()
            {
                CountyId = b.CountyId,
                CountyName = b.CountyName

            }).ToList();
            return dictionaryCurrencyModels;
        }

        public DictionaryCityModel GetDataToEdit(int id)
        {
            DictionaryCityModel city = _dbContext.DictionaryCity.Select(c => new DictionaryCityModel()
            {
                Id = c.CityId,
                Name = c.CityName,
                County = _dbContext.DictionaryCounty.Where(d => d.CountyId == c.CountyId).Select(a => a.CountyName).FirstOrDefault().ToString()

            }).Where(a => a.Id == id).FirstOrDefault();
            return city;
        }

        public DictionaryLandmarkTypeModel GetDataToEditLandmarkType(int id)
        {
            DictionaryLandmarkTypeModel landmark = _dbContext.DictionaryLandmarkType.Select(c => new DictionaryLandmarkTypeModel()
            {
                LandmarkTypeId = c.LandmarkTypeId,
                LandmarkTypeCode = c.LandmarkTypeCode,
                Description = c.Description

            }).Where(a => a.LandmarkTypeId == id).FirstOrDefault();

            return landmark;

        }

        public DictionaryLanguageModel GetDataToEditLanguage(int id)
        {
            DictionaryLanguageModel city = _dbContext.DictionaryLanguage.Select(c => new DictionaryLanguageModel()
            {
                LanguageId = c.LanguageId,
                LanguageName = c.LanguageName,
                LanguageCode=c.LanguageCode
            }).Where(a => a.LanguageId == id).FirstOrDefault();
            return city;
        }

        public CountryXlanguage GetDataToDeleteLang(int id)
        {
            CountryXlanguage cxl = _dbContext.CountryXlanguage.Select(c => new CountryXlanguage()
            {
                CountryId = c.CountryId,
                LanguageId = c.LanguageId
            }).Where(a => a.LanguageId == id).FirstOrDefault();

            return cxl;
        }

        public IQueryable<DictionaryCurrencyModel> GetDictionaryCurrencyFiltered(String currencyName)
        {
            if (currencyName == null)
            {
                IQueryable<DictionaryCurrencyModel> dictionaryCurrency = _dbContext.DictionaryCurrency.Select(c => new DictionaryCurrencyModel()
                {
                    CurrencyId = c.CurrencyId,
                    CurrencyCode = c.CurrencyCode,
                    CurrencyName = c.CurrencyName,
                    CurrencyExchange = Convert.ToDecimal(c.CurrencyExchange)

                    
                });
                return dictionaryCurrency;
            }
            else
            {
                IQueryable<DictionaryCurrencyModel> dictionaryCurrency = _dbContext.DictionaryCurrency.Select(c => new DictionaryCurrencyModel()
                {
                    CurrencyId = c.CurrencyId,
                    CurrencyCode = c.CurrencyCode,
                    CurrencyName = c.CurrencyName,
                    CurrencyExchange = Convert.ToDecimal(c.CurrencyExchange)
                    
                }
                ).Where(c => c.CurrencyName.Contains(currencyName));
                return dictionaryCurrency;
            }
        }

        public bool VerifyLanguages(string languageName, string languageCode)
        {
            int stateName=0, stateCode = 0;
            if (_dbContext.DictionaryLanguage.Any(c => c.LanguageName == languageName)) stateName = 1;
            if (_dbContext.DictionaryLanguage.Any(c => c.LanguageCode == languageCode)) stateCode = 1;

            if(stateCode==1 || stateName==1)
                return false;
            else
                return true;
        }



        public void DeleteLanguages(int id)
        {
            DictionaryLanguage language = new DictionaryLanguage() { LanguageId = id };
            _dbContext.DictionaryLanguage.Remove(language);
            _dbContext.SaveChanges();

        }

        public bool VerifyCityName(String cityName)
        {

            if (_dbContext.DictionaryCity.Any(c => c.CityName == cityName))
                return false;
            else
                return true;
        }
        public void DeleteLandmarkType(int id)
        {
            DictionaryLandmarkType landmark = new DictionaryLandmarkType() { LandmarkTypeId = id };
            _dbContext.DictionaryLandmarkType.Remove(landmark);
            _dbContext.SaveChanges();
        }

        public DictionaryCurrencyModel GetCurrencyToEdit(int id)
        {
            DictionaryCurrencyModel currency = _dbContext.DictionaryCurrency.Select(c => new DictionaryCurrencyModel()
            {
                CurrencyId = c.CurrencyId,
                CurrencyCode = c.CurrencyCode,
                CurrencyName = c.CurrencyName,
                CurrencyExchange = Convert.ToDecimal(c.CurrencyExchange),
               // CountryName = _dbContext.DictionaryCountry.Where(a => a.CountryId == c.CountryId).Select(a => a.CountryName).FirstOrDefault().ToString()
            }).Where(a => a.CurrencyId == id).FirstOrDefault();
            return currency;
        }

        public List<DictionaryCountryModel> PopulateCountryCombo()
        {

            List<DictionaryCountryModel> dictionaryCurrencyModels = _dbContext.DictionaryCountry.Select(b => new DictionaryCountryModel()
            {
                CountryId = b.CountryId,
                CountryName = b.CountryName

            }).ToList();

            return dictionaryCurrencyModels;

        }


        public DictionaryCountyModel GetCountyToEdit(int id)
        {
            DictionaryCountyModel x = _dbContext.DictionaryCounty.Select(c => new DictionaryCountyModel()
            {
                CountyId = c.CountyId,
                CountyName = c.CountyName,
                CountryName = _dbContext.DictionaryCountry.Where(d => d.CountryId == c.CountryId).Select(a => a.CountryName).FirstOrDefault().ToString()

            }).Where(a => a.CountyId == id).FirstOrDefault();
            return x;
        }

        public IQueryable<LandmarkModel> GetLandmarksFiltered()
        {
            IQueryable<LandmarkModel> landmarks = _dbContext.Landmark.Select(a => new LandmarkModel()
            {
                LandmarkId = a.LandmarkId,
                LandmarkDescription = a.LandmarkDescription
            });
            return landmarks;

        }
    }
}
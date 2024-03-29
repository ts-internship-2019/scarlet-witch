﻿using iWasHere.Domain.DTOs;
using iWasHere.Domain.Model;
using iWasHere.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;

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

        public void SaveLandmark(string landmarkName, int landmarkTypeId, bool hasEntryTicket, int visitIntervalId, int ticketId, string streetName, int streetNumber, int cityId, float latitude, float longitude, int landmarkId)
        {

            Landmark landmark = new Landmark()
            {

                LandmarkTypeId = landmarkTypeId,
                HasEntryTicket = hasEntryTicket,
                VisitIntervalId = visitIntervalId,
                LandmarkDescription = landmarkName,
                TicketId = ticketId,
                StreetName = streetName,
                StreetNumber = streetNumber,
                CityId = cityId,
                Latitude = latitude,
                Longitude = longitude
            };

            _dbContext.Landmark.Add(landmark);
            _dbContext.SaveChanges();
        }

        public void SaveLandmark(string v1, string landmarkName, int v2, int landmarkTypeId, bool v3, bool hasEntryTicket, int v4, int visitIntervalId, int v5, int ticketId, string v6, string streetName, int v7, int streetNumber, int v8, int cityId, float v9, float latitude, float v10, float longitude, int v11, object landmarkId)
        {
            throw new NotImplementedException();
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
            else if (countyName != null && countryId == 0)
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
            else if (countyName == null && countryId != 0)
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
        public List<DictionaryIntervalModel> GetDictionaryIntervalModels()
        {

            List<DictionaryIntervalModel> dictionaryIntervalModels = _dbContext.DictionaryInterval.Select(b => new DictionaryIntervalModel()
            {
                VisitIntervalId = b.VisitIntervalId,
                VisitIntervalName = b.VisitIntervalName,
            }).ToList();

            return dictionaryIntervalModels;

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
               ).Where(c => c.CountyId == countyId);
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

        public void DeleteInterval(int id)
        {
            DictionaryInterval interval = new DictionaryInterval() { VisitIntervalId = id };

            _dbContext.DictionaryInterval.Remove(interval);
            _dbContext.SaveChanges();

        }
        public void DeleteTicket(int id)
        {
            DictionaryTicketType ticket = new DictionaryTicketType() { TicketTypeId = id };

            _dbContext.DictionaryTicketType.Remove(ticket);
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
                CountyId = c.CountyId,
                County = _dbContext.DictionaryCounty.Where(d => d.CountyId == c.CountyId).Select(a => a.CountyName).FirstOrDefault().ToString(),

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
                LanguageCode = c.LanguageCode
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
        public IQueryable<DictionaryIntervalModel> GetDictionaryIntervalFiltered(String intervalName)
        {
            if (intervalName == null)
            {
                IQueryable<DictionaryIntervalModel> dictionaryInterval = _dbContext.DictionaryInterval.Select(c => new DictionaryIntervalModel()
                {
                    VisitIntervalId = c.VisitIntervalId,
                    VisitIntervalName = c.VisitIntervalName
                });
                return dictionaryInterval;
            }
            else
            {
                IQueryable<DictionaryIntervalModel> dictionaryInterval = _dbContext.DictionaryInterval.Select(c => new DictionaryIntervalModel()
                {
                    VisitIntervalId = c.VisitIntervalId,
                    VisitIntervalName = c.VisitIntervalName
                }
                ).Where(c => c.VisitIntervalName.Contains(intervalName));
                return dictionaryInterval;
            }
        }
        public IQueryable<DictionaryTicketModel> GetDictionaryTicketFiltered(String ticketName)
        {
            if (ticketName == null)
            {
                IQueryable<DictionaryTicketModel> dictionaryTicket = _dbContext.DictionaryTicketType.Select(c => new DictionaryTicketModel()
                {
                    TicketTypeId = c.TicketTypeId,
                    TicketTypeName = c.TicketTypeName
                });
                return dictionaryTicket;
            }
            else
            {
                IQueryable<DictionaryTicketModel> dictionaryTicket = _dbContext.DictionaryTicketType.Select(c => new DictionaryTicketModel()
                {
                    TicketTypeId = c.TicketTypeId,
                    TicketTypeName = c.TicketTypeName
                }
                ).Where(c => c.TicketTypeName.Contains(ticketName));
                return dictionaryTicket;
            }
        }

        public bool VerifyLanguages(string languageName, string languageCode)
        {
            int stateName = 0, stateCode = 0;
            if (_dbContext.DictionaryLanguage.Any(c => c.LanguageName == languageName)) stateName = 1;
            if (_dbContext.DictionaryLanguage.Any(c => c.LanguageCode == languageCode)) stateCode = 1;

            if (stateCode == 1 || stateName == 1)
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

        public bool VerifyCountyName(String name)
        {
            if (_dbContext.DictionaryCounty.Any(c => c.CountyName == name))
                return true;
            else
                return false;
        }

        public bool VerifyCityName(String cityName)
        {

            if (_dbContext.DictionaryCity.Any(c => c.CityName == cityName))
                return false;
            else
                return true;
        }
        public bool VerifyLandmark(String name, double lat, double longitud)
        {
            if (_dbContext.Landmark.Any(c => c.LandmarkDescription == name && c.Latitude == lat && c.Longitude == longitud))
                return true;
            else
                return false;
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
            }).Where(a => a.CurrencyId == id).FirstOrDefault();
            return currency;
        }
        public DictionaryIntervalModel GetIntervalToEdit(int id)
        {
            DictionaryIntervalModel interval = _dbContext.DictionaryInterval.Select(c => new DictionaryIntervalModel()
            {
                VisitIntervalId = c.VisitIntervalId,
                VisitIntervalName = c.VisitIntervalName
            }).Where(a => a.VisitIntervalId == id).FirstOrDefault();
            return interval;
        }
        public DictionaryTicketModel GetTicketToEdit(int id)
        {
            DictionaryTicketModel ticket = _dbContext.DictionaryTicketType.Select(c => new DictionaryTicketModel()
            {
                TicketTypeName = c.TicketTypeName,
                TicketTypeId = c.TicketTypeId
            }).Where(a => a.TicketTypeId == id).FirstOrDefault();
            return ticket;
        }

        public List<DictionaryCountryModel> PopulateCountryCombo()
        {
            List<DictionaryCountryModel> dictionaryCountries = _dbContext.DictionaryCountry.Select(b => new DictionaryCountryModel()
            {
                CountryId = b.CountryId,
                CountryName = b.CountryName

            }).ToList();

            return dictionaryCountries;
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

        public IQueryable<LandmarkModel> GetLandmarksFiltered(int countryId)
        {
            if(countryId == 0)
            {
                IQueryable<LandmarkModel> landmarks = _dbContext.Landmark.Select(a => new LandmarkModel()
                {
                    LandmarkId = a.LandmarkId,
                    LandmarkDescription = a.LandmarkDescription,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude,
                    StreetName = a.StreetName,
                    StreetNumber = a.StreetNumber,
                    CityName = _dbContext.DictionaryCity.Where(d => d.CityId == a.CityId).Select(x => x.CityName).FirstOrDefault().ToString(),
                    CountryName = _dbContext.DictionaryCountry.Where(d => d.CountryId == a.City.County.Country.CountryId).Select(x => x.CountryName).FirstOrDefault().ToString(),
                    CountyName = _dbContext.DictionaryCounty.Where(d => d.CountyId == a.City.County.CountyId).Select(x => x.CountyName).FirstOrDefault().ToString()
                });
                return landmarks;
            }
            else
            {
                IQueryable<LandmarkModel> landmarks = _dbContext.Landmark.Select(a => new LandmarkModel()
                {
                    LandmarkId = a.LandmarkId,
                    LandmarkDescription = a.LandmarkDescription,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude,
                    Country = a.City.County.Country.CountryId,
                    StreetName = a.StreetName,
                    StreetNumber = a.StreetNumber,
                    CityName = _dbContext.DictionaryCity.Where(d => d.CityId == a.CityId).Select(x => x.CityName).FirstOrDefault().ToString(),
                    CountryName = _dbContext.DictionaryCountry.Where(d => d.CountryId == a.City.County.Country.CountryId).Select(x => x.CountryName).FirstOrDefault().ToString(),
                    CountyName = _dbContext.DictionaryCounty.Where(d => d.CountyId == a.City.County.CountyId).Select(x => x.CountyName).FirstOrDefault().ToString()
                }).Where(a => a.Country == countryId);
                return landmarks;
            }
        
           

        }


        public LandmarkModel GetLandmarkSingle(int landmarkId)
        {
            DictionaryService service = new DictionaryService(_dbContext);
            try
            {
                LandmarkModel city = _dbContext.Landmark.Select(c => new LandmarkModel()
                {
                    LandmarkId = c.LandmarkId,
                    LandmarkDescription = c.LandmarkDescription,
                    HasEntryTicket = c.HasEntryTicket,
                    VisitIntervalId = c.VisitIntervalId,
                    VisitInterval = c.VisitInterval,
                    TicketId = c.TicketId,
                    Ticket = _dbContext.DictionaryTicketType.Where(d => d.TicketTypeId == c.Ticket.TicketTypeId).Select(a => a.TicketTypeName).FirstOrDefault(),
                    TicketCost =Convert.ToDecimal(_dbContext.Ticket.Where(d => d.TicketId == c.TicketId).Select(a => a.TicketCost).FirstOrDefault()),
                    CurrencyRate = Convert.ToDecimal(_dbContext.DictionaryCurrency.Where(d => d.CurrencyId == c.Ticket.CurrencyId).Select(a => a.CurrencyExchange).FirstOrDefault()),
                    LandmarkType = c.LandmarkType,
                    LandmarkTypeId = c.LandmarkTypeId,
                    StreetName = c.StreetName,
                    StreetNumber = c.StreetNumber,
                    CityId = c.CityId,
                    City = c.City,
                    Latitude = c.Latitude,
                    Longitude = c.Longitude,
                    CountyId = _dbContext.DictionaryCounty.Where(d => d.CountyId == c.City.CountyId).Select(a => a.CountyId).FirstOrDefault(),
                    County = _dbContext.DictionaryCounty.Where(d => d.CountyId == c.City.CountyId).Select(a => a.CountyName).FirstOrDefault().ToString(),
                    CountryName = _dbContext.DictionaryCountry.Where(d => d.CountryId == c.City.County.CountryId).Select(a => a.CountryName).FirstOrDefault().ToString(),
                    Country = Convert.ToInt32(_dbContext.DictionaryCountry.Where(d => d.CountryId == c.City.County.CountryId).Select(a => a.CountryId).FirstOrDefault().ToString()),
                    Path = _dbContext.Images.Where(d => d.LandmarkId == landmarkId).Select(b => new Images() { Path = b.Path }).ToList(),
                    Reviews = _dbContext.Review.Where(d => d.LandmarkId == landmarkId).Select(b => new Review()
                    {
                        Review1 = b.Review1,
                        Grade = b.Grade,
                        UserName = b.UserName,
                        Title = b.Title
                    }).ToList()
                }).Where(a => a.LandmarkId == landmarkId).FirstOrDefault();
                return city;
            }
            catch
            {
                LandmarkModel city = _dbContext.Landmark.Select(c => new LandmarkModel()
                {
                    LandmarkId = c.LandmarkId,
                    LandmarkDescription = c.LandmarkDescription,
                    HasEntryTicket = c.HasEntryTicket,
                    VisitIntervalId = c.VisitIntervalId,
                    VisitInterval = c.VisitInterval,
                    TicketId = c.TicketId,
                    Ticket = _dbContext.DictionaryTicketType.Where(d => d.TicketTypeId == c.Ticket.TicketTypeId).Select(a => a.TicketTypeName).FirstOrDefault(),
                    LandmarkType = c.LandmarkType,
                    LandmarkTypeId = c.LandmarkTypeId,
                    StreetName = c.StreetName,
                    StreetNumber = c.StreetNumber,
                    CityId = c.CityId,
                    City = c.City,
                    Latitude = c.Latitude,
                    Longitude = c.Longitude,
                    CountyId = _dbContext.DictionaryCounty.Where(d => d.CountyId == c.City.CountyId).Select(a => a.CountyId).FirstOrDefault(),
                    County = _dbContext.DictionaryCounty.Where(d => d.CountyId == c.City.CountyId).Select(a => a.CountyName).FirstOrDefault().ToString(),
                    CountryName = _dbContext.DictionaryCountry.Where(d => d.CountryId == c.City.County.CountryId).Select(a => a.CountryName).FirstOrDefault().ToString(),
                    Country = Convert.ToInt32(_dbContext.DictionaryCountry.Where(d => d.CountryId == c.City.County.CountryId).Select(a => a.CountryId).FirstOrDefault().ToString()),
                    Reviews = _dbContext.Review.Where(d => d.LandmarkId == landmarkId).Select(b => new Review()
                    {
                        Review1 = b.Review1,
                        Grade = b.Grade,
                        UserName = b.UserName,
                        Title = b.Title
                    }).ToList()
                }).Where(a => a.LandmarkId == landmarkId).FirstOrDefault();
                return city;
            }
        }

        public List<Review> GetReviews(int landmarkId)
        {
            List<Review> reviews = _dbContext.Review.Select(b => new Review()
            {
                Review1 = b.Review1,
                Grade = b.Grade,
                UserName = b.UserName,
                Title = b.Title

            }).Where(a => a.LandmarkId == landmarkId).ToList();

            return reviews;
        }


        public void SaveImagesDB(string path)
        {
            var landmarkId = _dbContext.Landmark.Max(u => u.LandmarkId);

            Images img = new Images()
            {
                Path = path,
                LandmarkId = landmarkId

            };
            _dbContext.Images.Add(img);
            _dbContext.SaveChanges();
        }

        public string GetImageToShow(int id)
        {
            ImageModel x = _dbContext.Images.Select(c => new ImageModel()
            {
                Path = c.Path

            }).Where(a => a.LandmarkId == id).FirstOrDefault();
            return x.Path;
        }

        public int DeleteCountry(int id)
        {
            //CountryXlanguage cxl = new CountryXlanguage() { CountryId = id };
            //_dbContext.CountryXlanguage.Remove(cxl);
            //_dbContext.SaveChanges();

            int sters = 0;
            try
            {
                DictionaryCountry c = new DictionaryCountry() { CountryId = id };
                _dbContext.DictionaryCountry.Remove(c);
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

        public int DeleteLandmark(int id)
        {
            int sters = 0;
            try
            {
                Landmark c = new Landmark() { LandmarkId = id };
                _dbContext.Landmark.Remove(c);
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

        public Stream ExportFile(int id)
        {
            bool noComments = true;
            decimal? min = 0;
            decimal? max = 0;
            Review minItem = null;
            Review maxItem = null;
            LandmarkModel landmark = _dbContext.Landmark.Select(a => new LandmarkModel()
            {
                LandmarkId = a.LandmarkId,
                LandmarkDescription = a.LandmarkDescription,
                Latitude = a.Latitude,
                Longitude = a.Longitude,
                StreetName = a.StreetName,
                StreetNumber = a.StreetNumber,
                CityName = _dbContext.DictionaryCity.Where(d => d.CityId == a.CityId).Select(x => x.CityName).FirstOrDefault().ToString(),
                CountryName = _dbContext.DictionaryCountry.Where(d => d.CountryId == a.City.County.Country.CountryId).Select(x => x.CountryName).FirstOrDefault().ToString(),
                CountyName = _dbContext.DictionaryCounty.Where(d => d.CountyId == a.City.County.CountyId).Select(x => x.CountyName).FirstOrDefault().ToString(),
                Path = _dbContext.Images.Where(d => d.LandmarkId == id).Select(b => new Images() { Path = b.Path }).ToList(),
                Reviews = _dbContext.Review.Where(d => d.LandmarkId == id).Select(b => new Review()
                {
                    Review1 = b.Review1,
                    Grade = b.Grade,
                    UserName = b.UserName,
                    Title = b.Title
                }).ToList()
            }).Where(a => a.LandmarkId == id).FirstOrDefault();
            if (landmark.Reviews.Count > 0)
            {
                min = landmark.Reviews.Min(p => p.Grade);
                max = landmark.Reviews.Max(p => p.Grade);
                minItem = landmark.Reviews.First(a => a.Grade == min);
                maxItem = landmark.Reviews.First(a => a.Grade == max);
                noComments = false;
            }
            var stream = new MemoryStream();

            using (WordprocessingDocument doc = WordprocessingDocument.Create(stream, DocumentFormat.OpenXml.WordprocessingDocumentType.Document, true))
            {

                MainDocumentPart mainPart = doc.AddMainDocumentPart();
                new Document(new Body()).Save(mainPart);
                Body body = mainPart.Document.Body;
                if (noComments == false)
                {
                    body.Append(
                          new Body(
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                            new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Numele obiectivului este: " + landmark.LandmarkDescription))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Adresa obiectivului: "))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Tara - " + landmark.CountryName))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Judet -  " + landmark.CountyName))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Oras - " + landmark.CityName))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Strada - " + landmark.StreetName))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Numar - " + landmark.StreetNumber))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Nota medie: " + landmark.Reviews.Average(a => a.Grade)))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Comentarii: "))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Autor: " + maxItem.UserName))),
                           new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Titlul: " + maxItem.Title + " Mesaj: " + maxItem.Review1 + " Nota: " + maxItem.Grade + "/10"))),
                             new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Autor: " + minItem.UserName))),
                           new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Titlul: " + minItem.Title + " Mesaj: " + minItem.Review1 + " Nota: " + minItem.Grade + "/10")))
                                  ));
                }
                else
                {
                    body.Append(
                          new Body(
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                            new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Numele obiectivului este: " + landmark.LandmarkDescription))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Adresa obiectivului: "))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Tara - " + landmark.CountryName))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Judet -  " + landmark.CountyName))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Oras - " + landmark.CityName))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Strada - " + landmark.StreetName))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Numar - " + landmark.StreetNumber))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Nota medie: " + landmark.Reviews.Average(a => a.Grade)))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Comentarii: "))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Fara comentarii ")))
                          ));
                }
                mainPart.Document.Save();
            }

            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public void SaveImagesInEditDB(string path, int landmarkId)
        {



            Images img = new Images()
            {
                Path = path,
                LandmarkId = landmarkId

            };
            _dbContext.Images.Add(img);
            _dbContext.SaveChanges();
        }

        public IQueryable<TopLandmark> GetTopLandmarks()
        {
           

            IQueryable<TopLandmark> landmark = _dbContext.Landmark.Select(a => new TopLandmark()
            {
                LandmarkId = a.LandmarkId,
                LandmarkDescription = a.LandmarkDescription,              
                Path = _dbContext.Images.Where(d => d.LandmarkId == a.LandmarkId).Select(b => new Images() { Path = b.Path }).ToList(),
                Reviews = _dbContext.Review.Where(d => d.LandmarkId == a.LandmarkId).Select(b => new Review()
                {
                    Review1 = b.Review1,
                    Grade = b.Grade,
                    UserName = b.UserName,
                    Title = b.Title
                }).ToList()

            });

            IQueryable<TopLandmark> topLandmarks = landmark.OrderByDescending(a => a.Reviews.Average(b => b.Grade)).Take(3);
            return topLandmarks;
        }

        public MemoryStream ExportFileAlice(int id)
        {
            bool noComments = true;
            decimal? min = 0;
            decimal? max = 0;
            Review minItem = null;
            Review maxItem = null;
            LandmarkModel landmark = _dbContext.Landmark.Select(a => new LandmarkModel()
            {
                LandmarkId = a.LandmarkId,
                LandmarkDescription = a.LandmarkDescription,
                Latitude = a.Latitude,
                Longitude = a.Longitude,
                StreetName = a.StreetName,
                StreetNumber = a.StreetNumber,
                CityName = _dbContext.DictionaryCity.Where(d => d.CityId == a.CityId).Select(x => x.CityName).FirstOrDefault().ToString(),
                CountryName = _dbContext.DictionaryCountry.Where(d => d.CountryId == a.City.County.Country.CountryId).Select(x => x.CountryName).FirstOrDefault().ToString(),
                CountyName = _dbContext.DictionaryCounty.Where(d => d.CountyId == a.City.County.CountyId).Select(x => x.CountyName).FirstOrDefault().ToString(),
                Path = _dbContext.Images.Where(d => d.LandmarkId == id).Select(b => new Images() { Path = b.Path }).ToList(),
                Reviews = _dbContext.Review.Where(d => d.LandmarkId == id).Select(b => new Review()
                {
                    Review1 = b.Review1,
                    Grade = b.Grade,
                    UserName = b.UserName,
                    Title = b.Title
                }).ToList()
            }).Where(a => a.LandmarkId == id).FirstOrDefault();
            if (landmark.Reviews.Count > 0)
            {
                min = landmark.Reviews.Min(p => p.Grade);
                max = landmark.Reviews.Max(p => p.Grade);
                minItem = landmark.Reviews.First(a => a.Grade == min);
                maxItem = landmark.Reviews.First(a => a.Grade == max);
                noComments = false;
            }

            var stream = new MemoryStream();

            using (WordprocessingDocument doc = WordprocessingDocument.Create(stream, DocumentFormat.OpenXml.WordprocessingDocumentType.Document, true))
            {

                MainDocumentPart mainPart = doc.AddMainDocumentPart();

                new Document(new Body()).Save(mainPart);

                Body body = mainPart.Document.Body;
                if (noComments == false)
                {
                    body.Append(
                          new Body(
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                            new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Numele obiectivului este: " + landmark.LandmarkDescription))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Adresa obiectivului: "))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Tara - " + landmark.CountryName))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Judet -  " + landmark.CountyName))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Oras - " + landmark.CityName))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Strada - " + landmark.StreetName))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Numar - " + landmark.StreetNumber))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Nota medie: " + landmark.Reviews.Average(a => a.Grade)))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Comentarii: "))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Autor: " + maxItem.UserName))),
                           new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Titlul: " + maxItem.Title + " Mesaj: " + maxItem.Review1 + " Nota: " + maxItem.Grade + "/10"))),
                             new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Autor: " + minItem.UserName))),
                           new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Titlul: " + minItem.Title + " Mesaj: " + minItem.Review1 + " Nota: " + minItem.Grade + "/10")))
                                  ));
                }
                else
                {
                    body.Append(
                          new Body(
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                            new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Numele obiectivului este: " + landmark.LandmarkDescription))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Adresa obiectivului: "))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Tara - " + landmark.CountryName))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Judet -  " + landmark.CountyName))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Oras - " + landmark.CityName))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Strada - " + landmark.StreetName))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Numar - " + landmark.StreetNumber))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Nota medie: " + landmark.Reviews.Average(a => a.Grade)))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Comentarii: "))),
                          new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
                              new DocumentFormat.OpenXml.Wordprocessing.Run(
                              new DocumentFormat.OpenXml.Wordprocessing.Text("Fara comentarii ")))
                          ));
                }
                mainPart.Document.Save();
            }

            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }
}
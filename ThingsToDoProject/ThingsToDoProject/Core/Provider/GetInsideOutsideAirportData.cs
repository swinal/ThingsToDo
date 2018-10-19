﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using ThingsToDoProject.Core.Interface;
using ThingsToDoProject.Model;
using ThingsToDoProject.Core.Translater;

namespace ThingsToDoProject.Core.Provider
{
    public class GetInsideOutsideAirportData:IGetInsideOutsideData
    {
        private readonly IHttpClientFactory _httpClientFactory;
        IConfiguration _iconfiguration;

        private readonly IGetOutsideData _getOutsideData;
        private readonly IGetData _getInsideData;

        public GetInsideOutsideAirportData(IHttpClientFactory httpClientFactory, IConfiguration configuration, IGetOutsideData getOutsideData, IGetData getInsideData)
        {
            _httpClientFactory = httpClientFactory;
            _iconfiguration = configuration;
            _getOutsideData = getOutsideData;
            _getInsideData = getInsideData;

        }
        public async Task<List<DataAttributes>> GetInsideOutsideData(Location Position, String DeparturePlace, String ArrivalDateTime, String DepartureDateTime, String PointOfInterest)
        {
            
            List<DataAttributes> OutsideData = await _getOutsideData.GetAllData(DeparturePlace, ArrivalDateTime, DepartureDateTime, PointOfInterest);
            List<DataAttributes> InsideData = await _getInsideData.GetData(Position,DeparturePlace, ArrivalDateTime, DepartureDateTime, PointOfInterest);
            
            for(int index=0;index<InsideData.Count;index++)
            {
                OutsideData.Add(InsideData[index]);
            }
            
            return OutsideData;
            
        }
    }
}

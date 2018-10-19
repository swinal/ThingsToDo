﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThingsToDoProject.Core.Interface;
using ThingsToDoProject.Core.Provider;
using ThingsToDoProject.Model;

namespace ThingsToDoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IGetOutsideData _getAllData;
        private readonly IGetData _getData;
        private readonly IGetLatitudeLongitude _getLatitudeLongitude;
        private readonly IGetInsideOutsideData _getInsideOutsideData;
        public DataController(IGetOutsideData getAllData, IGetData getData, IGetLatitudeLongitude getLatitudeLongitude,IGetInsideOutsideData getInsideOutsideData)
        {
            _getAllData = getAllData;
            _getData = getData;
            _getLatitudeLongitude = getLatitudeLongitude;
            _getInsideOutsideData = getInsideOutsideData;
        }
        //GET: api/Data/outsideAirport
        [HttpGet("outsideAirport/{DeparturePlace}/{ArrivalDateTime}/{DepartureDateTime}/{PointOfInterest}")]
        public async Task<IActionResult> GetOutsideData(String DeparturePlace, String ArrivalDateTime, String DepartureDateTime, String PointOfInterest)
        {
            var Data = await _getAllData.GetAllData(DeparturePlace, ArrivalDateTime, DepartureDateTime, PointOfInterest);
            if (Data != null)
                return Ok(Data);
            else
                return BadRequest("Data Not Found");
        }

        //GET: api/Data/insideAirport
        [HttpGet("insideAirport/{DeparturePlace}/{ArrivalDateTime}/{DepartureDateTime}/{PointOfInterest}")]

        //PointOfInterest is any Stores/Restorents...etc
        public async Task<IActionResult> GetInsideData(String DeparturePlace, String ArrivalDateTime, String DepartureDateTime, String PointOfInterest)
        {
            
            Location Position = _getLatitudeLongitude.Get(DeparturePlace);
            var Data = await _getData.GetData(Position, DeparturePlace, ArrivalDateTime, DepartureDateTime, PointOfInterest);
            if (Data != null)
                return Ok(Data);
            else
                return BadRequest("Not Found");
        }

        //GET: api/Data/InsideOutsideAirport
        [HttpGet("InsideOutsideAirport/{DeparturePlace}/{ArrivalDateTime}/{DepartureDateTime}/{PointOfInterest}")]
        public async Task<IActionResult> GetInsideOutsideData(String DeparturePlace, String ArrivalDateTime, String DepartureDateTime, String PointOfInterest)
        {
            Location Position = _getLatitudeLongitude.Get(DeparturePlace);
            var Data = await _getInsideOutsideData.GetInsideOutsideData(Position,DeparturePlace, ArrivalDateTime, DepartureDateTime, PointOfInterest);
            if (Data != null)
                return Ok(Data);
            else
                return BadRequest("Not Found");
        }


        //GET: api/Data/position
        [HttpGet("position/{Location}")]
        public Location GetPosition(string Location)
        {
            Location Position = _getLatitudeLongitude.Get(Location);
            return Position;
        }
        
    }
}

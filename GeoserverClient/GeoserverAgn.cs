﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using GeoserverClient.Model;
using Newtonsoft.Json;
using RestSharp;

namespace GeoserverClient
{
    public class GeoserverAgn
    {
        public static string BaseUrl = "https://geoserver.nl/rest/agn";
        public static string UserId = string.Empty;

        static readonly RestClient Client = new RestClient(BaseUrl);
        
        public static void GetGebouwen(string wkt, Action<List<Gebouw>> callback )
        {
            var request = new RestRequest("gebouwen");
            request.AddParameter("wkt", wkt);
            Client.ExecuteAsync<List<Gebouw>>(request, response => callback(response.Data));
        }

        public static void GetGebouw(string id, Action<Gebouw> callback )
        {
            var request = new RestRequest("gebouwen");
            request.AddParameter(id, ParameterType.UrlSegment);
            if (!string.IsNullOrEmpty(UserId)) request.AddParameter("uid", UserId); 
            Client.ExecuteAsync<Gebouw>(request, response => callback(response.Data));
        }

        public static void GetGebouwen(string pc6, Action<List<Gebouw>> callback, string huisnummer = "", string huisletter = "", string huisnummerToevoeging = "")
        {
            var request = new RestRequest("gebouwen");
            request.AddParameter("postcode", pc6);
            if (huisnummer != string.Empty) request.AddParameter("huisnummer", huisnummer);
            if (huisletter != string.Empty) request.AddParameter("huisletter", huisletter);
            if(huisnummerToevoeging!=string.Empty) request.AddParameter("huisnrtoev", huisnummerToevoeging);
            if (!string.IsNullOrEmpty(UserId)) request.AddParameter("uid", UserId); 
            Client.ExecuteAsync<List<Gebouw>>(request, response => callback(response.Data));
        }

        public static void GetAdres(string id, Action<Adres> callback )
        {
            var request = new RestRequest("adressen");
            request.AddParameter(id, ParameterType.UrlSegment);
            if (!string.IsNullOrEmpty(UserId)) request.AddParameter("uid", UserId);
            Client.ExecuteAsync<Adres>(request, response => callback(response.Data));
        }

        public static void GetAdressen(string pc6, Action<List<Adres>> callback, string huisnummer="", string huisletter = "", string huisnummerToevoeging = "")
        {
            var request = new RestRequest("adressen");
            request.AddParameter("postcode", pc6);
            if (huisnummer != string.Empty) request.AddParameter("huisnummer", huisnummer);
            if (huisletter != string.Empty) request.AddParameter("huisletter", huisletter);
            if(huisnummerToevoeging!=string.Empty) request.AddParameter("huisnrtoev", huisnummerToevoeging);
            if (!string.IsNullOrEmpty(UserId)) request.AddParameter("uid", UserId);
            Client.ExecuteAsync<List<Adres>>(request, response => callback(response.Data));
        }


        /**
         * Sample urrels:
        http://geoserver.nl/rest/agn/gebouwen?wkt=POLYGON((234403 579310,234403 579313,234397 579312,234403 579310))
        http://geoserver.nl/rest/agn/gebouwen?wkt=POINT(234403 579310)
        http://geoserver.nl/rest/agn/gebouwen/3100000117486
        http://geoserver.nl/rest/agn/gebouwen?postcode=1094jb&huisnummer=21
        http://geoserver.nl/rest/agn/adressen/363010000705816
        http://geoserver.nl/rest/agn/adressen?postcode=1053el&huisnummer=188
        http://geoserver.nl/rest/agn/gebouwen?postcode=1053el&huisnummer=188&huisnrtoev=H
        */

    }
}

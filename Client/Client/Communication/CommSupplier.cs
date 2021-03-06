﻿using Client.Interfaces;
using System;
using System.Collections.Generic;
using Client.Model;
using RestSharp;
using Newtonsoft.Json;
using static System.Diagnostics.Debug;

namespace Client.Communication
{
    class CommSupplier: ICommSupplier
    {
        private static CommSupplier _instance;
        private string urlAddress {  get; } = ("http://c414305-001-site1.btempurl.com");

        protected CommSupplier() { }

        public static CommSupplier GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CommSupplier();
            }
            return _instance;
        }   

        public IEnumerable<Dostawca> GetSuppliers()
        {
            try
            {
                string baseUrl = $"{urlAddress}/api/Supply";
                var client = new RestClient(baseUrl);
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                IRestResponse response = client.Execute(request);
                return JsonConvert.DeserializeObject<List<Dostawca>>(response.Content);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("500"))
                {
                    throw new Exception("Server Error");
                }
                else
                    WriteLine($"{Environment.NewLine}Error in {nameof(_instance)}  {nameof(GetSuppliers)}: {ex}{Environment.NewLine}");
            }
            throw new Exception("Exception in GetSuppliers");
        }
    }
}

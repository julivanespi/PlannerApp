using AKSoftware.WebApi.Client;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp.Shared.Services
{
    public class PlannerService
    {

        private readonly string _baseUrl;

        ServiceClient client = new ServiceClient();

        public PlannerService(string url)
        {
            _baseUrl = url;
        }

        public string AccessToken
        {
            get => client.AccessToken;
            set
            {
                client.AccessToken = value;
            }
        }

        /// <summary>
        /// Retrieve all the plans from the API with pagination
        /// </summary>
        /// <param name="page">Number of the page</param>
        /// <returns></returns>
        public async Task<PlansCollectionResponse> GetAllPlansByPageAsync(int page = 1)
        {
            var response = await client.GetProtectedAsync<PlansCollectionResponse>($"{_baseUrl}/api/plans?page={page}");
            return response.Result;
        }

        /// <summary>
        /// Return a plan by ID
        /// </summary>
        /// <param name="id">Id of plan to get</param>
        /// <returns></returns>
        public async Task<PlanSingleResponse> GetAllPlansByIdAsync(string id)
        {
            var response = await client.GetProtectedAsync<PlanSingleResponse>($"{_baseUrl}/api/plans/{id}");
            return response.Result;
        }

        /// <summary>
        /// Retrieve all the plans with query from the API with pagination
        /// </summary>
        /// <param name="page">Number of the page</param>
        /// <returns></returns>
        public async Task<PlansCollectionResponse> SearchPlansByPageAsync(string query, int page = 1)
        {
            var response = await client.GetProtectedAsync<PlansCollectionResponse>($"{_baseUrl}/api/plans/search?query={query}&page={page}");
            return response.Result;
        }

        /// <summary>
        /// Post plan to the api
        /// </summary>
        /// <param name="model">object represents the plan to be added</param>
        /// <returns></returns>
        public async Task<PlanSingleResponse> PostPlanAsync(PlanRequest model)
        {
            var formKeyValues = new List<FormKeyValue>()
            {
                new StringFormKeyValue("Title", model.Title),
                new StringFormKeyValue("Description", model.Description)
            };

            if (model.CoverFile != null)
                formKeyValues.Add(new FileFormKeyValue("CoverFile", model.CoverFile, model.FileName));

            var response = await client.SendFormProtectedAsync<PlanSingleResponse>($"{_baseUrl}/api/plans",
                ActionType.POST, formKeyValues.ToArray());

            return response.Result;
        }

        /// <summary>
        /// Edit plan to the api
        /// </summary>
        /// <param name="model">object represents the plan to be added</param>
        /// <returns></returns>
        public async Task<PlanSingleResponse> EditPlanAsync(PlanRequest model)
        {
            var formKeyValues = new List<FormKeyValue>()
            {
                new StringFormKeyValue("ID", model.Id),
                new StringFormKeyValue("Title", model.Title),
                new StringFormKeyValue("Description", model.Description)
            };

            if (model.CoverFile != null)
                formKeyValues.Add(new FileFormKeyValue("CoverFile", model.CoverFile, model.FileName));


            var response = await client.SendFormProtectedAsync<PlanSingleResponse>($"{_baseUrl}/api/plans",
                ActionType.PUT, formKeyValues.ToArray());

            return response.Result;
        }

        /// <summary>
        /// Delete plan by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PlanSingleResponse> DeletePlanAsync(string id)
        {
            var response = await client.DeleteProtectedAsync<PlanSingleResponse>($"{_baseUrl}/api/plans/{id}");
            return response.Result;
        }
    }
}

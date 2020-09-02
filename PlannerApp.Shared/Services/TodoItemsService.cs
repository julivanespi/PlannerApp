using AKSoftware.WebApi.Client;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp.Shared.Services
{
    public class TodoItemsService
    {
        private readonly string _baseUrl;

        ServiceClient client = new ServiceClient();

        public TodoItemsService(string url)
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
        /// Insert new todo item inside specific plan
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ToDoItemsSingleResponse> CreateItemAsync(ToDoItemRequest model)
        {
            var response = await client.PostProtectedAsync<ToDoItemsSingleResponse>($"{_baseUrl}/api/todoitems", model);
            return response.Result;
        }

        /// <summary>
        /// edit the description of a specific item
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ToDoItemsSingleResponse> EditItemAsync(ToDoItemRequest model)
        {
            var response = await client.PutProtectedAsync<ToDoItemsSingleResponse>($"{_baseUrl}/api/todoitems", model);
            return response.Result;
        }

        /// <summary>
        /// mark the item as done if it's undone.
        /// </summary>
        /// <param name="id">ID of the item to be updated</param>
        /// <returns></returns>
        public async Task<ToDoItemsSingleResponse> ChangeItemStateAsync(string id)
        {
            var response = await client.PutProtectedAsync<ToDoItemsSingleResponse>($"{_baseUrl}/api/todoitems/{id}", null);
            return response.Result;
        }

        public async Task<ToDoItemsSingleResponse> DeleteItemAsync(string id)
        {
            var response = await client.DeleteProtectedAsync<ToDoItemsSingleResponse>($"{_baseUrl}/api/todoitems/{id}");
            return response.Result;
        }
    }
}

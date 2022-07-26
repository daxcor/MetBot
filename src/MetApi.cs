﻿using System.Text.Json;
using MetBot.Models;

namespace MetBot
{
    public class MetApi : IMetApi
    {
        private static HttpClient _httpClient = new();
        private static readonly string _baseUrl = "https://collectionapi.metmuseum.org/public/collection/v1";

        private async Task<string> GetResponseAsync(string endpoint)
        {
            var returnMessage = await _httpClient.GetAsync(_baseUrl + (endpoint ?? "")).ConfigureAwait(false);

            return await returnMessage.Content.ReadAsStringAsync();
        }

        public async Task<CollectionItem> GetCollectionItemAsync(string objectNum)
        {
            var jsonResponse = await GetResponseAsync("/objects/" + objectNum);

            var collectionItem = JsonSerializer.Deserialize<CollectionItem>(jsonResponse);

            return collectionItem ?? throw new ArgumentException("Error returning collection item");
        }
    }
}
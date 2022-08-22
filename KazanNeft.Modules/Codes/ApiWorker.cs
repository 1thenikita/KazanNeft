using KazanNeft.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net;

namespace KazanNeft.Modules.Codes
{
    /// <summary>
    /// Класс по работе с WebApi.
    /// </summary>
    public class ApiWorker
    {
        private WebClient webClient;
        private string WEB_API_URL;

        /// <summary>
        /// Конструктор класса по работе с WebApi.
        /// </summary>
        /// <param name="wEB_API_URL">Ссылка до WebApi. Указывается по желанию.</param>
        public ApiWorker(string wEB_API_URL)
        {
            this.webClient = new WebClient();
            WEB_API_URL = String.IsNullOrWhiteSpace(wEB_API_URL)? "http://localhost:58835/api/": wEB_API_URL;
            SetHeaders();
        }

        /// <summary>
        /// Обработчик установки заголовков.
        /// </summary>
        private void SetHeaders()
        {
            webClient.Headers.Set("Context-type", "application/json");
        }

        /// <summary>
        /// Обработчик получения Asset'ов.
        /// </summary>
        /// <returns>Asset'ы</returns>
        public List<Asset> GetAssets()
        {
            var result = new List<Asset>();

            try
            {
                var response = webClient.DownloadString(WEB_API_URL + "assets");
                if (response == null) return null;

                result = JsonConvert.DeserializeObject<List<Asset>>(response);
                return result;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Обработчик получения Asset по ID.
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Asset</returns>
        public Asset GetAsset(int id)
        {
            var result = new Asset();

            try
            {
                var response = GetAssets();
                if (response == null) return null;

                result = response.FirstOrDefault(a => a.ID == id);
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}

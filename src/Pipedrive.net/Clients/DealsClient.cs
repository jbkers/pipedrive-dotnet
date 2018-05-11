﻿using Pipedrive.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pipedrive
{
    /// <summary>
    /// A client for Pipedrive's Deal API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developers.pipedrive.com/docs/api/v1/#!/Deals">Deal API documentation</a> for more information.
    public class DealsClient : ApiClient, IDealsClient
    {
        /// <summary>
        /// Initializes a new Deal API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public DealsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Task<IReadOnlyList<Deal>> GetAll(DealFilters filters)
        {
            Ensure.ArgumentNotNull(filters, nameof(filters));

            var parameters = filters.Parameters;
            parameters.Add("owned_by_you", "0");
            var options = new ApiOptions
            {
                StartPage = filters.StartPage,
                PageCount = filters.PageCount,
                PageSize = filters.PageSize
            };

            return ApiConnection.GetAll<Deal>(ApiUrls.Deals(), parameters, options);
        }

        public Task<IReadOnlyList<Deal>> GetAllForCurrent(DealFilters filters)
        {
            Ensure.ArgumentNotNull(filters, nameof(filters));

            var parameters = filters.Parameters;
            parameters.Add("owned_by_you", "1");
            var options = new ApiOptions
            {
                StartPage = filters.StartPage,
                PageCount = filters.PageCount,
                PageSize = filters.PageSize
            };

            return ApiConnection.GetAll<Deal>(ApiUrls.Deals(), parameters, options);
        }

        public Task<IReadOnlyList<Deal>> GetAllForUserId(int userId, DealFilters filters)
        {
            Ensure.ArgumentNotNull(filters, nameof(filters));

            var parameters = filters.Parameters;
            parameters.Add("user_id", userId.ToString());
            var options = new ApiOptions
            {
                StartPage = filters.StartPage,
                PageCount = filters.PageCount,
                PageSize = filters.PageSize
            };

            return ApiConnection.GetAll<Deal>(ApiUrls.Deals(), parameters, options);
        }

        public Task<Deal> Get(long id)
        {
            return ApiConnection.Get<Deal>(ApiUrls.Deal(id));
        }

        public Task<Deal> Create(NewDeal data)
        {
            Ensure.ArgumentNotNull(data, nameof(data));

            return ApiConnection.Post<Deal>(ApiUrls.Deals(), data);
        }

        public Task<Deal> Edit(long id, DealUpdate data)
        {
            Ensure.ArgumentNotNull(data, nameof(data));

            return ApiConnection.Put<Deal>(ApiUrls.Deal(id), data);
        }

        public Task Delete(long id)
        {
            return ApiConnection.Delete(ApiUrls.Deal(id));
        }

        public Task<IReadOnlyList<DealUpdateFlow>> GetUpdates(long dealId, DealUpdateFilters filters)
        {
            Ensure.ArgumentNotNull(filters, nameof(filters));

            var parameters = filters.Parameters;
            parameters.Add("id", dealId.ToString());
            var options = new ApiOptions
            {
                StartPage = filters.StartPage,
                PageCount = filters.PageCount,
                PageSize = filters.PageSize
            };

            return ApiConnection.GetAll<DealUpdateFlow>(ApiUrls.DealUpdates(dealId), parameters, options);
        }

        public Task<IReadOnlyList<Follower>> GetFollowers(long dealId)
        {
            var parameters = new Dictionary<string, string>()
            {
                { "id", dealId.ToString() }
            };

            return ApiConnection.GetAll<Follower>(ApiUrls.DealFollowers(dealId), parameters);
        }

        public Task<IReadOnlyList<Activity>> GetActivities(long dealId, DealActivityFilters filters)
        {
            Ensure.ArgumentNotNull(filters, nameof(filters));

            var parameters = filters.Parameters;
            parameters.Add("id", dealId.ToString());
            var options = new ApiOptions
            {
                StartPage = filters.StartPage,
                PageCount = filters.PageCount,
                PageSize = filters.PageSize
            };

            return ApiConnection.GetAll<Activity>(ApiUrls.DealActivities(dealId), parameters, options);
        }
    }
}
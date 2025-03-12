//using Blazorise.DataGrid;
using BlazorBootstrap;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components;
using Platforms.gRPC;
using System.Collections.Generic;

namespace PS.Website.Components.Pages
{
    public partial class FetchPlatforms
    {
        [Inject]
        public required GrpcChannel Channel { get; set; }
        private List<Platform>? _platforms;
        private int _platformsCount = 0;
        //private DataGrid<Platform>? MyDataGrid;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            (_platforms, _platformsCount) = await GetPlatformsAsync(count: 10, offset: 0);
        }


        private async Task<GridDataProviderResult<Platform>> PlatformsDataProvider(GridDataProviderRequest<Platform> request)
        {
            if (_platforms == null)
            {
                (_platforms, _platformsCount) = await GetPlatformsAsync(count: request.PageSize, offset: request.PageNumber);
            }
            return await Task.FromResult(request.ApplyTo(_platforms));
        }

        //private async Task OnReadData(DataGridReadDataEventArgs<Platform> args)
        //{
        //    int limit = args.PageSize;
        //    int offset = (args.Page - 1) * limit;

        //    if (limit <= 0)
        //    {
        //        return;
        //    }
        //    Console.WriteLine($"OnReadLine invoked with params count: {args.Page - 1}, offset: {args.PageSize}");
        //   (_platforms,_platformsCount )= await GetPlatformsAsync(count: limit, offset: offset);
        //}

        private async Task<(List<Platform>, int totalCount)> GetPlatformsAsync(int count, int offset)
        {
            var client = new PlatformsService.PlatformsServiceClient(Channel);
            GetPlatformResponse response = await client.GetPlatformsAsync(new PlatformRequest()
            {                
                Count = count,
                Offset = offset
            });
            List<Platform> platforms = response.Platforms.ToList();
            int totalCount = response.Total;
            return (platforms, totalCount);

        }
    }
}

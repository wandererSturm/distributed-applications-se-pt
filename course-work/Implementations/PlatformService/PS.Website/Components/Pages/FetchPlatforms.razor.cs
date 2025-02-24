using Blazorise.DataGrid;
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
        private DataGrid<Platform>? MyDataGrid;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            (_platforms, _platformsCount) = await GetPlatformsAsync(count: 20, offset: 0);
        }


        private async Task OnReadData(DataGridReadDataEventArgs<Platform> args)
        {
            int limit = args.PageSize;
            int offset = (args.Page - 1) * limit;

            if (limit <= 0)
            {
                return;
            }
            Console.WriteLine($"OnReadLine invoked with params count: {args.Page - 1}, offset: {args.PageSize}");
           (_platforms,_platformsCount )= await GetPlatformsAsync(count: limit, offset: offset);
        }

        private async Task<(List<Platform>, int totalCount)> GetPlatformsAsync(int count, int offset)
        {
            var client = new PlatformsService.PlatformsServiceClient(Channel);
            List<Platform> platforms = (await client.GetPlatformsAsync(new PlatformRequest() { Id = 0 })).Platforms.ToList();
            int totalCount = platforms.Count;
            return (platforms, totalCount);
            
        }
    }
}

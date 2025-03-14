//using Blazorise.DataGrid;
using BlazorBootstrap;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components;
using Platforms.gRPC;
using System.Collections.Generic;

namespace PS.Website.Components.Pages
{
    public partial class FetchOperatingSystems
    {
        [Inject]
        public required GrpcChannel Channel { get; set; }
        private List<Platforms.gRPC.OperatingSystem>? _operatingSystems;
        private int _operatingSystemsCount = 0;
        //private DataGrid<Platform>? MyDataGrid;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            (_operatingSystems, _operatingSystemsCount) = await GetOperatingSystemsAsync(count: 10, offset: 0);
        }


        private async Task<GridDataProviderResult<Platforms.gRPC.OperatingSystem>> OperatingSystemDataProvider(GridDataProviderRequest<Platforms.gRPC.OperatingSystem> request)
        {
            if (_operatingSystems == null)
            {
                (_operatingSystems, _operatingSystemsCount) = await GetOperatingSystemsAsync(count: request.PageSize, offset: request.PageNumber);
            }
            return await Task.FromResult(request.ApplyTo(_operatingSystems));
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

        private async Task<(List<Platforms.gRPC.OperatingSystem>, int totalCount)> GetOperatingSystemsAsync(int count, int offset)
        {
            var client = new OperatingSystemService.OperatingSystemServiceClient(Channel);
            Platforms.gRPC.GetOperatingSystemResponse response = await client.GetOperatingSystemsAsync(new Platforms.gRPC.OperatingSystemRequest()
            {                
                Count = count,
                Offset = offset
            });
            List<Platforms.gRPC.OperatingSystem> operatingSystems = response.OperatingSystem.ToList();
            int totalCount = response.Total;
            return (operatingSystems, totalCount);

        }
    }
}
